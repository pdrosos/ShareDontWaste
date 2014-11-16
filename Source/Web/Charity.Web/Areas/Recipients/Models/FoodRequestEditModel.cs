namespace Charity.Web.Areas.Recipients.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using Charity.Data.Models;
    using Charity.Web.Infrastructure.Mapping;

    public class FoodRequestEditModel : IMapFrom<FoodRequest>
    {
        public int Id { get; set; }

        [Editable(false)]
        [Display(Name = "Donation")]
        public FoodDonation FoodDonation { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must not be more than {1} characters.")]
        public string Quantity { get; set; }

        [Required]
        [UIHint("DatePicker")]
        [Display(Name = "Need From")]
        public DateTime NeedFrom { get; set; }

        [Required]
        [UIHint("DatePicker")]
        [Display(Name = "Need To")]
        public DateTime NeedTo { get; set; }

        [StringLength(600, ErrorMessage = "The {0} must not be more than {1} characters.")]
        public string Description { get; set; }
    }
}