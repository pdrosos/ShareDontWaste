namespace Charity.Web.Areas.Administration.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using Charity.Data.Models;
    using Charity.Web.Infrastructure.Mapping;
    using AutoMapper;

    public class FoodCategoryViewModel : IHaveCustomMappings
    {
        public int Id { get; set; }

        [Display(Name = "Category")]
        public string Name { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<FoodCategory, FoodCategoryViewModel>();
        }
    }
}