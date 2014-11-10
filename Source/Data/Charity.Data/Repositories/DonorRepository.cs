namespace Charity.Data.Repositories
{
    using System;
    using System.Linq;
    using Charity.Data.Common;
    using Charity.Data.Common.Repositories;
    using Charity.Data.Models;

    public class DonorRepository : DeletableEntityRepository<Donor>, IDonorRepository
    {
        public DonorRepository(IApplicationDbContext context) : base(context)
        {
        }

        public Donor GetById(string id)
        {
            return this.DbSet.Find(id);
        }
    }
}
