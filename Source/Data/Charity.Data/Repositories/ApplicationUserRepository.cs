namespace Charity.Data.Repositories
{
    using System;
    using System.Linq;
    using Charity.Data.Models;

    public class ApplicationUserRepository : DeletableEntityRepository<ApplicationUser>
    {
        public ApplicationUserRepository(IApplicationDbContext context)
            : base(context)
        {
        }
    }
}