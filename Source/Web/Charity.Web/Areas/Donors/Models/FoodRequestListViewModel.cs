namespace Charity.Web.Areas.Donors.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using AutoMapper;
    using Charity.Data.Models;
    using Charity.Web.Infrastructure.Mapping;    

    public class FoodRequestListViewModel : IMapFrom<FoodRequest>, IHaveCustomMappings
    {
        public int Id { get; set; }

        [Display(Name = "Donation")]
        public FoodDonation FoodDonation { get; set; }

        public Recipient Recipient { get; set; }

        public string Quantity { get; set; }

        [Display(Name = "Need From")]
        public DateTime NeedFrom { get; set; }

        [Display(Name = "Need To")]
        public DateTime NeedTo { get; set; }

        public int CommentsCount { get; set; }

        public int UnreadCommentsCount { get; set; }

        [Display(Name = "Completed")]
        public bool IsCompleted { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<FoodRequest, FoodRequestListViewModel>()
                .ForMember(destination => destination.CommentsCount, 
                    opt => opt.MapFrom(src => src.Comments.Count()))
                .ForMember(destination => destination.UnreadCommentsCount,
                    opt => opt.MapFrom(src => src.Comments.Count(c => c.IsReadFromDonor == false)));
        }
    }
}