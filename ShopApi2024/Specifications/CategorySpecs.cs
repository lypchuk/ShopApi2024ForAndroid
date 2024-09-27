using Ardalis.Specification;
using ShopApi2024.Entities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ShopApi2024.Specifications
{
    public class CategorySpecs
    {
        internal class ById : Specification<Category>
        {
            public ById(int id)
            {
                Query
                    .Where(x => x.Id == id)
                    .Include(x => x.Products);
            }
        }
        internal class All : Specification<Category>
        {
            public All()
            {
                Query.Include(x => x.Products);
            }
        }
        internal class ByIds : Specification<Category>
        {
            public ByIds(IEnumerable<int> ids)
            {
                Query
                    .Where(x => ids.Contains(x.Id))
                    .Include(x => x.Products);
            }
        }
    }
}
