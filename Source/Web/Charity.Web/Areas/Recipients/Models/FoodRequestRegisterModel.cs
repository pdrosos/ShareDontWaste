namespace Charity.Web.Areas.Recipients.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using Charity.Data.Models;
    using Charity.Web.Infrastructure.Mapping;

    public class FoodRequestRegisterModel : IMapFrom<FoodRequest>
    {
        public FoodRequestRegisterModel()
        {

        }

        public FoodRequestRegisterModel(int foodDonationId)
        {
            this.FoodDonationId = foodDonationId;
        }

        [Required]
        [Display(Name = "Donation")]
        public int FoodDonationId { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must not be more than {1} characters.")]
        [UIHint("SinglelineText")]
        public string Quantity { get; set; }

        [Required]
        [UIHint("DatePickerLine")]
        [Display(Name = "Need From")]
        public DateTime NeedFrom { get; set; }

        [Required]
        [UIHint("DatePickerLine")]
        [Display(Name = "Need To")]
        public DateTime NeedTo { get; set; }

        [StringLength(600, ErrorMessage = "The {0} must not be more than {1} characters.")]
        [UIHint("MultilineText")]
        public string Description { get; set; }
    }
}