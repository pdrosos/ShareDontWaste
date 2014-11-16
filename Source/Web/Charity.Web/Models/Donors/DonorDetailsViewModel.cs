namespace Charity.Web.Models.Donors
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using AutoMapper;
    using Charity.Data.Models;
    using Charity.Web.Infrastructure.Mapping;

    public class DonorDetailsViewModel : IMapFrom<Donor>, IHaveCustomMappings
    {
        public int Id { get; set; }

        [Display(Name = "Orgaization name")]
        public string OrganizationName { get; set; }

        [Display(Name = "City")]
        public string CityName { get; set; }

        [Display(Name = "Donations")]
        public IEnumerable<FoodDonation> FoodDonations { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Donor, DonorViewModel>()
                .ForMember(destination => destination.CityName, opt => opt.MapFrom(src => src.City.Name));
        }
    }
}