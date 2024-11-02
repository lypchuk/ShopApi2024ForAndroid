using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShopApi2024.Interfaces;

namespace ShopApi2024.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class 
    {
        internal ShopApi2024DbContext context;
        internal DbSet<TEntity> dbSet;

        public Repository(ShopApi2024DbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        public IEnumerable<TEntity> GetAll()
        {
            return dbSet.ToList();
        }

        ///////////////////////////////////////////////////////////
        ///
        public virtual TEntity GetById (object id)
        {
            var item = dbSet.Find(id);
            dbSet.Entry(item).State = EntityState.Detached;
            //return dbSet.Find(id);
            return item;
        }

        public virtual void Insert(TEntity entity)
        {
            dbSet.Add(entity);
        }

        public virtual void Delete(object id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }

        public virtual void Update(TEntity entityToUpdate)
        {

            dbSet.Attach(entityToUpdate);//.State = EntityState.Detached;
            //dbSet.AsNoTracking();
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public void Save()
        {
            context.SaveChanges();
        }


        // working with specifications
        public async Task<IEnumerable<TEntity>> GetListBySpec(ISpecification<TEntity> specification)
        {
            return await ApplySpecification(specification).ToListAsync();
        }

        public async Task<TEntity?> GetItemBySpec(ISpecification<TEntity> specification)
        {
            return await ApplySpecification(specification).FirstOrDefaultAsync();
        }

        private IQueryable<TEntity> ApplySpecification(ISpecification<TEntity> specification)
        {
            var evaluator = new SpecificationEvaluator();
            return evaluator.GetQuery(dbSet, specification);
        }
    }
}
