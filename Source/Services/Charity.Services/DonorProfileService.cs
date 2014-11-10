namespace Charity.Services
{
    using System;
    using System.Linq;
    using Charity.Data.Common.Repositories;
    using Charity.Data.Models;

    public class DonorProfileService
    {
        private readonly IDonorRepository donorRepository;

        public DonorProfileService(IDonorRepository donorRepository)
        {
            this.donorRepository = donorRepository;
        }
        
        public Donor GetById(string id)
        {
            return this.donorRepository.GetById(id);
        }

        public Donor GetByApplicationUserId(string applicationUserId)
        {
            return this.donorRepository.All().FirstOrDefault(d => d.ApplicationUserId == applicationUserId);
        }

        public Donor GetByUserName(string userName)
        {
            return this.donorRepository.All().FirstOrDefault(d => d.ApplicationUser.UserName == userName);
        }

        public void Update(Donor donor)
        {
            this.donorRepository.Update(donor);
            this.donorRepository.SaveChanges();
        }
    }
}
