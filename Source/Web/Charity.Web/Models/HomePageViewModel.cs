namespace Charity.Web.Models
{
    using System.Collections.Generic;
    using Charity.Web.Models.Donors;
    using Charity.Web.Models.FoodDonations;

    public class HomePageViewModel
    {
        public IEnumerable<DonorViewModel> MostActiveDonors { get; set; }

        public IEnumerable<FoodDonationViewModel> LatestDonations { get; set; }
    }
}
