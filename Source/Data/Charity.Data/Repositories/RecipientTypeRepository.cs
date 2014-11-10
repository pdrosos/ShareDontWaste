namespace Charity.Data.Repositories
{
    using System;
    using System.Linq;
    using Charity.Data.Common;
    using Charity.Data.Common.Repositories;
    using Charity.Data.Models;

    public class RecipientTypeRepository : DeletableEntityRepository<RecipientType>, IRecipientTypeRepository
    {
        public RecipientTypeRepository(IApplicationDbContext context)
            : base(context)
        {
        }
    }
}