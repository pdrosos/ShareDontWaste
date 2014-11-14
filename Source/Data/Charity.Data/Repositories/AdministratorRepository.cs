namespace Charity.Data.Repositories
{
    using System;
    using System.Linq;
    using Charity.Data.Common;
    using Charity.Data.Common.Repositories;
    using Charity.Data.Models;

    public class AdministratorRepository : DeletableEntityRepository<Administrator>, IAdministratorRepository
    {
        public AdministratorRepository(ApplicationDbContext context)
            : base(context)
        {
        }
    }
}