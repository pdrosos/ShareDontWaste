namespace Charity.Services.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Charity.Data.Models;

    public interface ICityService
    {
        IEnumerable<City> GetAll();
    }
}
