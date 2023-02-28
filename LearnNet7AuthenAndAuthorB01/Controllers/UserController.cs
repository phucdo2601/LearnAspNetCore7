using LearnNet7AuthenAndAuthorB01.Dtos;
using LearnNet7AuthenAndAuthorB01.Models;
using LearnNet7AuthenAndAuthorB01.UnitOfWorks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace LearnNet7AuthenAndAuthorB01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DatabaseContext _context;
        private readonly IUnitOfWork _unitOfWork;
        private readonly AppSettingDto _appSettings;

        public UserController(DatabaseContext context, IUnitOfWork unitOfWork, IOptionsMonitor<AppSettingDto> optionsMonitor)
        {
            _unitOfWork = unitOfWork;
            _context = context;
            _appSettings = optionsMonitor.CurrentValue;
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginValidate([FromBody] LoginDto model)
        {
            var user = _unitOfWork.AccountRepository.Find(p => p.UserName== model.UserName && p.Password == model.Password).FirstOrDefault();
            if (user == null)
            {
                //khong dung nguoi dung
                return await Task.FromResult(StatusCode(StatusCodes.Status400BadRequest, new ApiResponseModel
                {
                    Success = false,
                    Message = "Invalid username/password"
                }));
            } else
            {
                //cap token
                var token = GenerateToken(user);

                return await Task.FromResult(StatusCode(StatusCodes.Status200OK, new ApiResponseModel
                {
                    Success = true,
                    Message = "Authenticate success",
                    Data = token
                }));
            }
        }

        [HttpPost("renewToken")]
        public async Task<IActionResult> RenewToken(TokenDto model)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var secretKeyBytes = Encoding.UTF8.GetBytes(_appSettings.SecretKey);
            var tokenValidateParam = new TokenValidationParameters
            {
                //tu cap token 
                ValidateIssuer = false,
                ValidateAudience = false,

                //Ky vao token
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(secretKeyBytes),

                ClockSkew = TimeSpan.Zero,

                ValidateLifetime = false, //no check expired time
            };

            try
            {
                //check 1: AccessToken valid format
                var tokenInVerify = jwtTokenHandler.ValidateToken(model.AccessToken, tokenValidateParam, out var validatedToken);

                //check 2 : Check alg
                if (validatedToken is JwtSecurityToken jwtSecurityToken)
                {
                    var result = jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha512,
                        StringComparison.InvariantCultureIgnoreCase);
                    if (!result)
                    {
                        return await Task.FromResult(StatusCode(StatusCodes.Status400BadRequest, new ApiResponseModel
                        {
                            Success = false,
                            Message = "Invalid Token"
                        }));
                    }
                }

                //check 3: Check accessToken expire?
                var utcExpireDate = long.Parse(tokenInVerify.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Exp).Value);

                var expireDate = ConvertUnixTimeToDateTime(utcExpireDate);
                if (expireDate > DateTime.UtcNow)
                {
                    return await Task.FromResult(StatusCode(StatusCodes.Status400BadRequest, new ApiResponseModel
                    {
                        Success = false,
                        Message = "Access token has not yet expired"
                    }));
                }

                //check 4: Check refreshtoken exist in DB
                var storedToken = _unitOfWork.RefreshTokenRepository.Find(x => x.Token == model.RefreshToken).FirstOrDefault();
                if (storedToken == null)
                {
                    return await Task.FromResult(StatusCode(StatusCodes.Status400BadRequest, new ApiResponseModel
                    {
                        Success = false,
                        Message = "Refresh token does not exist"
                    }));
                }

                //check 5: check refreshToken is used/revoked?
                if (storedToken.IsUsed)
                {
                    return await Task.FromResult(StatusCode(StatusCodes.Status400BadRequest, new ApiResponseModel
                    {
                        Success = false,
                        Message = "Refresh Token has been used"
                    }));
                }

                //check xem token co bi thu hoi hay chua
                if (storedToken.IsRevoked)
                {
                    return await Task.FromResult(StatusCode(StatusCodes.Status400BadRequest, new ApiResponseModel
                    {
                        Success = false,
                        Message = "Refresh token has been revoked"
                    }));
                }

                //check 6: AccessToken id == JwtId in RefreshToken
                var jti = tokenInVerify.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti).Value;
                if (storedToken.JwtId != jti)
                {
                    return await Task.FromResult(StatusCode(StatusCodes.Status400BadRequest, new ApiResponseModel
                    {
                        Success = false,
                        Message = "Token doesn't match"
                    }));
                }

                //Update token is used
                storedToken.IsRevoked = true;
                storedToken.IsUsed = true;
                _context.Update(storedToken);
                _context.SaveChanges();

                //create new token
                var user = _context.Accounts.SingleOrDefault(nd => nd.Id == storedToken.AccountId);
                var token = GenerateToken(user);

                return await Task.FromResult(StatusCode(StatusCodes.Status200OK, new ApiResponseModel
                {
                    Success = true,
                    Message = "Authenticate success",
                    Data = token
                }));
            }
            catch (Exception ex)
            {

                return await Task.FromResult(StatusCode(StatusCodes.Status400BadRequest, new ApiResponseModel
                {
                    Success = false,
                    Message = "Something went wrong"
                }));
            }
        }

        private TokenDto GenerateToken(Account account)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var secretKeyBytes = Encoding.UTF8.GetBytes(_appSettings.SecretKey);

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Name, account.FullName),
                    new Claim(JwtRegisteredClaimNames.Email, account.Email),
                    new Claim(JwtRegisteredClaimNames.Sub, account.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim("UserName", account.UserName),
                    new Claim("Id", account.Id.ToString()),

                    //roles
                }),
                Expires = DateTime.UtcNow.AddMinutes(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes), SecurityAlgorithms.HmacSha512Signature)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescription);
            var accessToken = jwtTokenHandler.WriteToken(token);
            var refreshToken = GenerateRefreshToken();

            //Lưu database
            var refreshTokenEntity = new RefreshToken
            {
                Id = Guid.NewGuid(),
                JwtId = token.Id,
                AccountId = account.Id,
                Token = refreshToken,
                IsUsed = false,
                IsRevoked = false,
                IssuedAt = DateTime.UtcNow,
                ExpiredAt = DateTime.UtcNow.AddHours(1)
            };

            _context.Add(refreshTokenEntity);
            _context.SaveChanges();

            return new TokenDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };

        }

        private string GenerateRefreshToken()
        {
            var random = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(random);
                return Convert.ToBase64String(random);
            }
         
        }

        private DateTime ConvertUnixTimeToDateTime(long utcExpireDate)
        {
            var  dateTimeInterval = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTimeInterval.AddSeconds(utcExpireDate).ToUniversalTime();

            return dateTimeInterval;
        }
    }
}
