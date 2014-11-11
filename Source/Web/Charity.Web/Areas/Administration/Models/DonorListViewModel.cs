﻿namespace Charity.Web.Areas.Administration.Models
{
    using System;
    using System.Linq;
    using Charity.Data.Models;
    using Charity.Web.Infrastructure.Mapping;
    using System.ComponentModel.DataAnnotations;
    using AutoMapper;

    public class DonorListViewModel : IMapFrom<Donor>, IHaveCustomMappings
    {
        public Guid Id { get; set; }

        [Display(Name = "Organization Name")]
        public string OrganizationName { get; set; }

        [Display(Name = "Contact Name")]
        public string ContactName { get; set; }

        [Display(Name = "City")]
        public string CityName { get; set; }

        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Donor, DonorListViewModel>()
                .ForMember(destination => destination.UserName, opt => opt.MapFrom(src => src.ApplicationUser.UserName))
                .ForMember(destination => destination.Email, opt => opt.MapFrom(src => src.ApplicationUser.Email))
                .ForMember(destination => destination.PhoneNumber, opt => opt.MapFrom(src => src.ApplicationUser.PhoneNumber))
                .ForMember(destination => destination.CityName, opt => opt.MapFrom(src => src.City.Name));
        }
    }
}