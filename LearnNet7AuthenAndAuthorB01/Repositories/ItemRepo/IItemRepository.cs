using LearnNet7AuthenAndAuthorB01.Dtos;
using LearnNet7AuthenAndAuthorB01.Models;
using LearnNet7AuthenAndAuthorB01.Repositories.Generic;

namespace LearnNet7AuthenAndAuthorB01.Repositories.ItemRepo
{
    public interface IItemRepository : IGenericRepository<Item>
    {
        List<ItemFilterDto> filteredItem(string search, double? PriceBegin, double? PriceEnd, string sortBy, int page, int pageSize);
    }
}
