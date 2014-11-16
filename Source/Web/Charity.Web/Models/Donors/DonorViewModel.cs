namespace Charity.Web.Models.Donors
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using AutoMapper;
    using Charity.Data.Models;
    using Charity.Web.Infrastructure.Mapping;

    public class DonorViewModel : IMapFrom<Donor>, IHaveCustomMappings
    {
        public string UserName { get; set; }

        [Display(Name = "Orgaization name")]
        public string OrganizationName { get; set; }

        [Display(Name = "City")]
        public string CityName { get; set; }

        [Display(Name = "Donations")]
        public int DonationsCount { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Donor, DonorViewModel>()
                .ForMember(destination => destination.CityName, opt => opt.MapFrom(src => src.City.Name))
                .ForMember(destination => destination.DonationsCount, opt => opt.MapFrom(src => src.FoodDonations.Count))
                .ForMember(destination => destination.UserName, opt => opt.MapFrom(src => src.ApplicationUser.UserName));
        }
    }
}