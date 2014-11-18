namespace Charity.Web.Areas.Administration.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using Charity.Data.Models;
    using Charity.Web.Infrastructure.Mapping;

    public class FoodRequestViewModel : IMapFrom<FoodRequest>
    {
        public int Id { get; set; }

        public FoodDonation FoodDonation { get; set; }

        public string Quantity { get; set; }

        [Display(Name = "Need From")]
        public DateTime NeedFrom { get; set; }

        [Display(Name = "Need To")]
        public DateTime NeedTo { get; set; }

        public string Description { get; set; }

        [Display(Name = "Completed")]
        public bool IsCompleted { get; set; }
    }
}