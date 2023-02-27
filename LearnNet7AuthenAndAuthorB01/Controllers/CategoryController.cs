using LearnNet7AuthenAndAuthorB01.UnitOfWorks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LearnNet7AuthenAndAuthorB01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork= unitOfWork;
        }

        [HttpGet("getAllCates")]
        public async Task<IActionResult> GetAllCates() {
            var res = _unitOfWork.CategoryRepository.GetAll();

            return await Task.FromResult(StatusCode(StatusCodes.Status200OK, res));
        }

        [HttpGet("getAllCatesWithItems")]
        public async Task<IActionResult> GetAllCatesWithItems() {
            var res = _unitOfWork.CategoryRepository.GetAllCatesWithProduct();
            return await Task.FromResult(StatusCode(StatusCodes.Status200OK, res));
        }
    }
}
