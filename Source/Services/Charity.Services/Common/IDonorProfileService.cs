namespace Charity.Services.Common
{
    using System;
    using System.Linq;
    using Charity.Data.Models;

    public interface IDonorProfileService
    {
        Donor GetById(Guid id);

        Donor GetByApplicationUserId(string applicationUserId);

        Donor GetByUserName(string userName);

        IQueryable<Donor> GetMostActiveDonors(int donorsCount);

        void Update(Donor donor);

        void Add(Donor donor);

        IQueryable<Donor> All();

        IQueryable<Donor> List();
         
        IQueryable<Donor> SearchByOrganizationName(string searchString);
    }
}
