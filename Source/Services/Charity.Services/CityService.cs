namespace Charity.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Charity.Data.Common.Repositories;
    using Charity.Data.Models;

    public class CityService
    {
        private readonly ICityRepository cityRepository;

        public CityService(ICityRepository cityRepository)
        {
            this.cityRepository = cityRepository;
        }

        public IEnumerable<City> GetAll()
        {
            return this.cityRepository.All().OrderBy(c => c.Id).AsEnumerable();
        }
    }
}
