namespace Charity.Web.Areas.Donors.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using Charity.Data.Models;
    using Charity.Web.Areas.Recipients.Models;
    using Charity.Web.Infrastructure.Mapping;

    public class FoodRequestViewModel : IMapFrom<FoodRequest>
    {
        public int Id { get; set; }

        [Display(Name = "Donation")]
        public FoodDonation FoodDonation { get; set; }

        public Recipient Recipient { get; set; }

        public string Quantity { get; set; }

        [Display(Name = "Need From")]
        public DateTime NeedFrom { get; set; }

        [Display(Name = "Need To")]
        public DateTime NeedTo { get; set; }

        public string Description { get; set; }

        [Display(Name = "Completed")]
        public bool IsCompleted { get; set; }

        public IEnumerable<FoodRequestCommentViewModel> Comments { get; set; }
    }
}