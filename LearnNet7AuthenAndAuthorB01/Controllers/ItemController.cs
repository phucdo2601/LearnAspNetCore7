using LearnNet7AuthenAndAuthorB01.UnitOfWorks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LearnNet7AuthenAndAuthorB01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ItemController(IUnitOfWork unitOfWork)
        {
            _unitOfWork= unitOfWork;
        }

        [HttpGet("getAllItems")]
        [Authorize]
        public async Task<IActionResult> GetAllItems() {
            var res = _unitOfWork.ItemRepository.GetAll();

            return await Task.FromResult(StatusCode(StatusCodes.Status200OK, res));
        }

        [HttpGet("filterItemByName/{searchString}/{sortBy}")]
        
        public async Task<IActionResult> FilterItemByName([FromRoute(Name = "searchString")] string search, 
            [FromQuery(Name = "PriceBegin")] double? PriceBegin, 
            [FromQuery(Name = "PriceEnd")] double? PriceEnd,
            [FromRoute(Name = "sortBy")] string sortBy,
            [FromQuery(Name = "Page")] int page,
            [FromQuery(Name = "PageSize")] int pageSize
            )
        {
            Console.WriteLine(sortBy);
            var result = _unitOfWork.ItemRepository.filteredItem(search, PriceBegin, PriceEnd, sortBy, page, pageSize);

            return await Task.FromResult(StatusCode(StatusCodes.Status200OK, result));
        }
    }
}
