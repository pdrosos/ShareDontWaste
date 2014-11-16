namespace Charity.Web.Models.FoodDonations
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using Charity.Data.Models;
    using Charity.Web.Infrastructure.Mapping;

    public class FoodDonationViewModel : IMapFrom<FoodDonation>
    {
        public int Id { get; set; }

        [Display(Name = "Donor")]
        public Donor Donor { get; set; }

        [Display(Name = "Category")]
        public FoodCategory FoodCategory { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Quantity { get; set; }

        public string ImageUrl { get; set; }

        [Display(Name = "Expiration Date")]
        public DateTime ExpirationDate { get; set; }

        [Display(Name = "Available From")]
        public DateTime AvailableFrom { get; set; }

        [Display(Name = "Available To")]
        public DateTime AvailableTo { get; set; }
    }
}