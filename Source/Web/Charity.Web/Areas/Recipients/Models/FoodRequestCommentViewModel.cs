namespace Charity.Web.Areas.Recipients.Models
{
    using System;
    using System.Linq;
    using Charity.Data.Models;
    using Charity.Web.Infrastructure.Mapping;
    using AutoMapper;

    public class FoodRequestCommentViewModel : IMapFrom<FoodRequestComment>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public int FoodRequestId { get; set; }

        public string UserName {get; set;}

        public string Text { get; set; }

        public DateTime CreatedOn { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<FoodRequestComment, FoodRequestCommentViewModel>()
                .ForMember(destination => destination.UserName,
                    opt => opt.MapFrom(src => src.ApplicationUser.UserName));
        }
    }
}