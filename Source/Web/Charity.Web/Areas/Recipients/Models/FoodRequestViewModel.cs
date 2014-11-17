namespace Charity.Web.Areas.Recipients.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using AutoMapper;
    using Charity.Data.Models;
    using Charity.Web.Infrastructure.Mapping;
        
    public class FoodRequestViewModel : IMapFrom<FoodRequest>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public FoodDonation FoodDonation { get; set; }

        public Donor Donor { get; set; }

        public string Quantity { get; set; }

        [Display(Name = "Need From")]
        public DateTime NeedFrom { get; set; }

        [Display(Name = "Need To")]
        public DateTime NeedTo { get; set; }

        public string Description { get; set; }

        [Display(Name = "Completed")]
        public bool IsCompleted { get; set; }

        public IEnumerable<FoodRequestCommentViewModel> Comments { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<FoodRequest, FoodRequestViewModel>()
                .ForMember(destination => destination.Donor, opt => opt.MapFrom(src => src.FoodDonation.Donor));
        }
    }
}