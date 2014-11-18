namespace Charity.Web.Areas.Donors.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using Charity.Data.Models;
    using Charity.Web.Infrastructure.Mapping;
    using System.Collections.Generic;
    using System.Web.Mvc;

    public class FoodDonationEditModel : IMapFrom<FoodDonation>
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Title")]
        [StringLength(250, ErrorMessage = "The {0} must not be more than {1} characters.")]
        public string Name { get; set; }
        
        [Required]
        [Display(Name = "Category")]
        public int FoodCategoryId { get; set; }

        public IEnumerable<SelectListItem> FoodCategories { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must not be more than {1} characters.")]
        public string Quantity { get; set; }

        [Required]
        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [UIHint("DatePicker")]
        [Display(Name = "Expiration Date")]
        public DateTime ExpirationDate { get; set; }

        [Required]
        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [UIHint("DatePicker")]
        [Display(Name = "Available From")]
        public DateTime AvailableFrom { get; set; }

        [Required]
        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [UIHint("DatePicker")]
        [Display(Name = "Available To")]
        public DateTime AvailableTo { get; set; }

        [StringLength(600, ErrorMessage = "The {0} must not be more than {1} characters.")]
        public string Description { get; set; }
    }
}