namespace Charity.Web.Areas.Donors.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using Charity.Data.Models;
    using Charity.Web.Infrastructure.Mapping;
    using AutoMapper;

    public class FoodDonationListViewModel : IMapFrom<FoodDonation>, IHaveCustomMappings
    {
        public int Id { get; set; }

        [Display(Name = "Title")]
        public string Name { get; set; }

        [UIHint("GridForeignKey")]
        public FoodCategoryViewModel Category { get; set; }

        public string Quantity { get; set; }

        [UIHint("DatePicker")]
        [Display(Name = "Expiration Date")]
        public DateTime ExpirationDate { get; set; }

        [UIHint("DatePicker")]
        [Display(Name = "Available From")]
        public DateTime AvailableFrom { get; set; }

        [UIHint("DatePicker")]
        [Display(Name = "Available To")]
        public DateTime AvailableTo { get; set; }

        [Display(Name = "Completed")]
        public bool IsCompleted { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<FoodDonation, FoodDonationListViewModel>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.FoodCategory));
        }
    }
}