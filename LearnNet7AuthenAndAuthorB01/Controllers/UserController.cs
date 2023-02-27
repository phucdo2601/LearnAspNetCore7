using LearnNet7AuthenAndAuthorB01.Dtos;
using LearnNet7AuthenAndAuthorB01.Models;
using LearnNet7AuthenAndAuthorB01.UnitOfWorks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
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
                //cap toke

                return await Task.FromResult(StatusCode(StatusCodes.Status200OK, new ApiResponseModel
                {
                    Success = true,
                    Message = "Authenticate success",
                    Data = GenerateToken(user)
                }));
            }
        }

        private string GenerateToken(Account account)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var secretKeyBytes = Encoding.UTF8.GetBytes(_appSettings.SecretKey);

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Email, account.Email),
                    new Claim(ClaimTypes.Name, account.FullName),
                    new Claim("Username", account.UserName),
                    new Claim("Id", account.Id.ToString()),
                    //roles
                    new Claim("TokenId", Guid.NewGuid().ToString()),
                }),
                Expires= DateTime.UtcNow.AddMinutes(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes), SecurityAlgorithms.HmacSha512Signature)
                
            };

            var token = jwtTokenHandler.CreateToken(tokenDescription);

            return jwtTokenHandler.WriteToken(token);
        }

    }
}
