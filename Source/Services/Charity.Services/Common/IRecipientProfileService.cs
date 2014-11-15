namespace Charity.Services.Common
{
    using System;
    using System.Linq;
    using Charity.Data.Models;

    public interface IRecipientProfileService
    {
        Recipient GetById(Guid id);

        Recipient GetByApplicationUserId(string applicationUserId);

        Recipient GetByUserName(string userName);

        void Update(Recipient recipient);

        void Add(Recipient recipient);

        IQueryable<Recipient> All();

        IQueryable<Recipient> SearchByOrganizationName(string searchString);
    }
}
