namespace Charity.Services
{
    using System;
    using System.Linq;
    using Charity.Data.Common.Repositories;
    using Charity.Data.Models;
    using Charity.Services.Common;

    public class DonorProfileService : IDonorProfileService
    {
        private readonly IDonorRepository donorRepository;

        public DonorProfileService(IDonorRepository donorRepository)
        {
            this.donorRepository = donorRepository;
        }
        
        public Donor GetById(Guid id)
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

        public void Add(Donor donor)
        {
            this.donorRepository.Add(donor);
            this.donorRepository.SaveChanges();
        }

        public IQueryable<Donor> All()
        {
            return this.donorRepository.All();
        }

        //TODO: Move method's content in DonorRepository. 
        public IQueryable<Donor> SearchByOrganizationName(string searchString)
        {
            var query = this.donorRepository.All().Where(donor => donor.OrganizationName.Contains(searchString));
            return query;
        }
    }
}
