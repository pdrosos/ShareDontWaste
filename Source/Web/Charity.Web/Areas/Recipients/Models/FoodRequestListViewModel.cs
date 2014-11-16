namespace Charity.Web.Areas.Recipients.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using Charity.Data.Models;
    using Charity.Web.Infrastructure.Mapping;

    public class FoodRequestListViewModel : IMapFrom<FoodRequest>
    {
        public int Id { get; set; }

        [Display(Name = "Donation")]
        public FoodDonation FoodDonation { get; set; }

        public string Quantity { get; set; }

        [Display(Name = "Need From")]
        public DateTime NeedFrom { get; set; }

        [Display(Name = "Need To")]
        public DateTime NeedTo { get; set; }

        [Display(Name = "Completed")]
        public bool IsCompleted { get; set; }
    }
}