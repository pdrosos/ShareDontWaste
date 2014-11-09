namespace Charity.Data.Repositories
{
    using System;
    using System.Linq;
    using Charity.Data.Models;

    public class RecipientRepository : DeletableEntityRepository<Recipient>
    {
        public RecipientRepository(IApplicationDbContext context)
            : base(context)
        {
        }
    }
}