namespace Charity.Data.Common.Repositories
{
    using System;
    using Charity.Data.Models;

    public interface IRecipientRepository : IDeletableEntityRepository<Recipient>
    {
        Recipient GetById(Guid id);
    }
}