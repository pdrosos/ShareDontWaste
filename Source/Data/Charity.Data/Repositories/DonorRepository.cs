namespace Charity.Data.Repositories
{
    using Charity.Data.Models;
    using System;
    using System.Linq;

    public class DonorRepository : DeletableEntityRepository<Donor>
    {
        public DonorRepository(IApplicationDbContext context) : base(context)
        {
        }
    }
}
