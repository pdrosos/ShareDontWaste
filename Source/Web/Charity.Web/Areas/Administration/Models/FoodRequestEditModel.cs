namespace Charity.Web.Areas.Administration.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using Charity.Data.Models;
    using Charity.Web.Infrastructure.Mapping;
    using AutoMapper;

    public class FoodRequestEditModel : IMapFrom<FoodRequest>, IHaveCustomMappings
    {
        public int Id { get; set; }

        [Editable(false)]
        [Display(Name = "Donation")]
        public FoodDonation Donation { get; set; }

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

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<FoodRequest, FoodRequestEditModel>()
                .ForMember(dest => dest.Donation, opt => opt.MapFrom(src => src.FoodDonation));
        }
    }
}