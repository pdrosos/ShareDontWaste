namespace Charity.Data.Repositories
{
    using System;
    using System.Linq;
    using Charity.Data.Models;

    public class AdministratorRepository : DeletableEntityRepository<Administrator>
    {
        public AdministratorRepository(IApplicationDbContext context)
            : base(context)
        {
        }
    }
}