namespace Charity.Data.Repositories
{
    using System;
    using System.Linq;
    using System.Data.Entity;
    using Charity.Data.Common.Models;
    using Charity.Data.Common.Repositories;

    public class DeletableEntityRepository<T> : GenericRepository<T>, IDeletableEntityRepository<T>
        where T : class, IDeletableEntity
    {
        public DeletableEntityRepository(IApplicationDbContext context)
            : base(context)
        {
        }

        public override void Delete(T entity)
        {
            entity.IsDeleted = true;
            entity.DeletedOn = DateTime.Now;

            base.ChangeEntityState(entity, EntityState.Modified);
        }

        public void HardDelete(T entity)
        {
            base.Delete(entity);
        }

        public override IQueryable<T> All()
        {
            return base.All().Where(x => !x.IsDeleted);
        }

        public IQueryable<T> AllWithDeleted()
        {
            return base.All();
        }
    }
}
