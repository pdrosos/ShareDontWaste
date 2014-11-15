namespace Charity.Web.Areas.Donors.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using Charity.Data.Models;
    using Charity.Web.Infrastructure.Mapping;

    public class FoodDonationRegisterModel : IMapFrom<FoodDonation>
    {
        [Required]
        [Display(Name = "Category")]
        public int FoodCategoryId { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must not be more than {1} characters.")]
        public string Quantity { get; set; }

        [Required]
        //[DataType(DataType.Date)]
        [UIHint("DatePicker")]
        [Display(Name = "Expiration Date")]
        public DateTime? ExpirationDate { get; set; }

        [Required]
        //[DataType(DataType.Date)]
        [UIHint("DatePicker")]
        [Display(Name = "Available From")]
        public DateTime AvailableFrom { get; set; }

        [Required]
        //[DataType(DataType.Date)]
        [UIHint("DatePicker")]
        [Display(Name = "Available To")]
        public DateTime AvailableTo { get; set; }

        [StringLength(600, ErrorMessage = "The {0} must not be more than {1} characters.")]
        public string Description { get; set; }
    }
}