using Ardalis.Specification;
using ShopApi2024.Entities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ShopApi2024.Specifications
{
    public class ProductSpecs
    {
        internal class ById : Specification<Product>
        {
            public ById(int id)
            {
                Query.Where(x => x.Id == id)
                    .Include(x => x.Category);
            }
        }
        internal class All : Specification<Product>
        {
            public All()
            {
                Query.Include(x => x.Category);
            }
        }
        internal class ByIds : Specification<Product>
        {
            public ByIds(IEnumerable<int> ids)
            {
                Query
                    .Where(x => ids.Contains(x.Id))
                    .Include(x => x.Category);
            }
        }
    }
}
