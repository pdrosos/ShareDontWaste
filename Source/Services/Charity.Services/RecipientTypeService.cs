namespace Charity.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Charity.Data.Common.Repositories;
    using Charity.Data.Models;

    public class RecipientTypeService
    {
        private readonly IRecipientTypeRepository recipientTypeRepository;

        public RecipientTypeService(IRecipientTypeRepository recipientTypeRepository)
        {
            this.recipientTypeRepository = recipientTypeRepository;
        }

        public IEnumerable<RecipientType> GetAll()
        {
            return this.recipientTypeRepository.All().OrderBy(r => r.Name).AsEnumerable();
        }
    }
}
