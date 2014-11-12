namespace Charity.Web.Areas.Administration.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using AutoMapper;
    using Charity.Data.Models;
    using Charity.Web.Infrastructure.Mapping;

    public class RecipientListViewModel : IMapFrom<Recipient>, IHaveCustomMappings
    {
        public Guid Id { get; set; }

        [Display(Name = "Organization Name")]
        public string OrganizationName { get; set; }

        [Display(Name = "Recipient Type")]
        public string RecipientType { get; set; }

        [Display(Name = "Contact Name")]
        public string ContactName { get; set; }

        [Display(Name = "City")]
        public string CityName { get; set; }

        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Recipient, RecipientListViewModel>()
                .ForMember(destination => destination.UserName, opt => opt.MapFrom(src => src.ApplicationUser.UserName))
                .ForMember(destination => destination.Email, opt => opt.MapFrom(src => src.ApplicationUser.Email))
                .ForMember(destination => destination.PhoneNumber, opt => opt.MapFrom(src => src.ApplicationUser.PhoneNumber))
                .ForMember(destination => destination.CityName, opt => opt.MapFrom(src => src.City.Name))
                .ForMember(destination => destination.RecipientType, opt => opt.MapFrom(src => src.RecipientType.Name));
        }
    }
}