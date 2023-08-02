using CampX.Common;
using CampX.Context;

namespace CampX.DataAccess
{
    public class BaseRepository<TEntity> : IRepository<TEntity>
      where TEntity : class, IEntity
    {
        private readonly CampXContext Context;


        public BaseRepository(CampXContext context)
        {
            this.Context = context;
        }

        public IQueryable<TEntity> Get()
        {

            return Context.Set<TEntity>().AsQueryable();
        }

        public TEntity Insert(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
            return entity;
        }

        public TEntity Update(TEntity entity)
        {
            Context.Set<TEntity>().Update(entity);

            return entity;
        }

        public void Delete(TEntity entity)
        {

            Context.Set<TEntity>().Remove(entity);
        }
    }
}