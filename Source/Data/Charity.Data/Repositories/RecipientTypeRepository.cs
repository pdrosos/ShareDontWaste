namespace Charity.Data.Repositories
{
    using System;
    using System.Linq;
    using Charity.Data.Models;

    public class RecipientTypeRepository : DeletableEntityRepository<RecipientType>
    {
        public RecipientTypeRepository(IApplicationDbContext context)
            : base(context)
        {
        }
    }
}