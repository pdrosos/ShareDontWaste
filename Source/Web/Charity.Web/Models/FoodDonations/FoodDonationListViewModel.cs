namespace Charity.Web.Models.FoodDonations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Charity.Data.Models;
    using PagedList;

    public class FoodDonationListViewModel
    {
        public IEnumerable<FoodCategory> FoodCategories { get; set; }

        public IPagedList<FoodDonationViewModel> FoodDonations { get; set; }
    }
}