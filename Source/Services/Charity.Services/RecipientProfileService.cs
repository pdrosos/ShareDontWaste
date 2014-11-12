namespace Charity.Services
{
    using Charity.Data.Common.Repositories;
    using Charity.Data.Models;
    using System;
    using System.Linq;

    public class RecipientProfileService
    {
        private readonly IRecipientRepository recipientRepository;

        public RecipientProfileService(IRecipientRepository recipientRepository)
        {
            this.recipientRepository = recipientRepository;
        }
        
        public Recipient GetById(Guid id)
        {
            return this.recipientRepository.GetById(id);
        }

        public Recipient GetByApplicationUserId(string applicationUserId)
        {
            return this.recipientRepository.All().FirstOrDefault(d => d.ApplicationUserId == applicationUserId);
        }

        public Recipient GetByUserName(string userName)
        {
            return this.recipientRepository.All().FirstOrDefault(d => d.ApplicationUser.UserName == userName);
        }

        public void Update(Recipient recipient)
        {
            this.recipientRepository.Update(recipient);
            this.recipientRepository.SaveChanges();
        }

        public void Add(Recipient recipient)
        {
            this.recipientRepository.Add(recipient);
            this.recipientRepository.SaveChanges();
        }

        public IQueryable<Recipient> All()
        {
            return this.recipientRepository.All();
        }

        //TODO: Move method's content in RecipientRepository. 
        public IQueryable<Recipient> SearchByOrganizationName(string searchString)
        {
            var query = this.recipientRepository.All().Where(recipient => recipient.OrganizationName.Contains(searchString));
            return query;
        }
    }
}
