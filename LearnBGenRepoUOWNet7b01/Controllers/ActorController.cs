using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieManagement.Domain.UnitOfWorks;

namespace LearnBGenRepoUOWNet7b01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
   
        public ActorController(IUnitOfWork unitOfWork)
        {
            _unitOfWork= unitOfWork;
        }

        [HttpGet("getAllActors")]
        public async Task<IActionResult> GetAllActors() 
        {
            var actorsFromRepo = _unitOfWork.ActorRepositoy.GetAll();
            return await Task.FromResult(StatusCode(StatusCodes.Status200OK, actorsFromRepo));
        }

        [HttpGet("getListActWithMovies")]
        public async Task<IActionResult> GetActorsWithMovie() {
            var actorsWithMovieFromRepo = _unitOfWork.ActorRepositoy.GetActorsWithMovies();
            return await Task.FromResult(StatusCode(StatusCodes.Status200OK, actorsWithMovieFromRepo));
        }
    }
}
