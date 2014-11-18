namespace Charity.Web.Areas.Administration.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using Charity.Data.Models;
    using Charity.Web.Infrastructure.Mapping;
    using AutoMapper;

    public class FoodRequestListViewModel : IMapFrom<FoodRequest>, IHaveCustomMappings
    {
        public int Id { get; set; }

        [Display(Name = "Donation")]
        public string DonationName { get; set; }

        public string Quantity { get; set; }

        [UIHint("DatePicker")]
        [Display(Name = "Need From")]
        public DateTime NeedFrom { get; set; }

        [UIHint("DatePicker")]
        [Display(Name = "Need To")]
        public DateTime NeedTo { get; set; }

        [Display(Name = "Completed")]
        public bool IsCompleted { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<FoodRequest, FoodRequestListViewModel>()
                .ForMember(dest => dest.DonationName, opt => opt.MapFrom(src => src.FoodDonation.Name));
        }
    }
}