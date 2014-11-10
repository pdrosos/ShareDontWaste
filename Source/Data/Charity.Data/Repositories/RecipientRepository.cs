namespace Charity.Data.Repositories
{
    using System;
    using System.Linq;
    using Charity.Data.Common;
    using Charity.Data.Common.Repositories;
    using Charity.Data.Models;

    public class RecipientRepository : DeletableEntityRepository<Recipient>, IRecipientRepository
    {
        public RecipientRepository(IApplicationDbContext context)
            : base(context)
        {
        }
    }
}