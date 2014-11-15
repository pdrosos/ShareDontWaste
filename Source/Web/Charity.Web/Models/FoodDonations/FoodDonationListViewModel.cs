namespace Charity.Web.Models.FoodDonations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Charity.Data.Models;
    using Charity.Web.Infrastructure.Mapping;
    using PagedList;

    public class FoodDonationListViewModel : IMapFrom<FoodDonation>
    {
        public IEnumerable<FoodCategory> FoodCategories { get; set; }

        public IPagedList<FoodDonationViewModel> FoodDonations { get; set; }
    }
}