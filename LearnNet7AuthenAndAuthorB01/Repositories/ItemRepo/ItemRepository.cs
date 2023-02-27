using LearnNet7AuthenAndAuthorB01.Dtos;
using LearnNet7AuthenAndAuthorB01.Models;
using LearnNet7AuthenAndAuthorB01.Repositories.Generic;

namespace LearnNet7AuthenAndAuthorB01.Repositories.ItemRepo
{
    public class ItemRepository : GenericRepositoy<Item>, IItemRepository
    {
        private readonly DatabaseContext _context;
        public ItemRepository(DatabaseContext context) : base(context)
        {
            _context = context;

        }

        public List<ItemFilterDto> filteredItem(string search, double? PriceBegin, double? PriceEnd, string sortBy, int page, int pageSize)
        {
            var allProducts = _context.Items.AsQueryable();

            #region Filter data
            if (!string.IsNullOrEmpty(search))
            {
                allProducts = allProducts.Where(hh => hh.Name.Contains(search));
            }

            if (PriceBegin.HasValue)
            {
                allProducts = allProducts.Where(hh => hh.Price >= PriceBegin);
            }

            if (PriceEnd.HasValue)
            {
                allProducts = allProducts.Where(hh => hh.Price <= PriceEnd);
            }
            #endregion

            #region Sorting data theo ten hang hoa tang dan
            //Default sort by Name
            allProducts = allProducts.OrderBy(hh => hh.Name);

            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy)
                {
                    case "tenhh_desc": allProducts = allProducts.OrderByDescending(hh => hh.Name); break;
                    case "gia_asc": allProducts = allProducts.OrderBy(hh => hh.Price); break;
                    case "gia_desc": allProducts = allProducts.OrderByDescending(hh => hh.Price); break;
                }
            }

            #endregion

            #region pagination traditional
            /*allProducts = allProducts.Skip((page -1) * pageSize).Take(pageSize);*/

            #endregion



            /*var result = allProducts.Select(hh => new
            ItemFilterDto
            {
                Id = hh.Id,
                Name = hh.Name,
                Price = hh.Price,
                Quantity = hh.Quantity,
                Description = hh.Description,
                CategoryName = hh.Category.Name
            });
            return result.ToList();*/

            #region pagination by libary
            var result = PaginatedList<Item>.Create(allProducts, page, pageSize);
            return result.Select(hh => new
            ItemFilterDto
            {
                Id = hh.Id,
                Name = hh.Name,
                Price = hh.Price,
                Quantity = hh.Quantity,
                Description = hh.Description,
                CategoryName = hh.Category?.Name
            }).ToList();

            #endregion


        }
    }
}
