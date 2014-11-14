namespace Charity.Web.Areas.Recipients.Models
{
    using AutoMapper;
    using Charity.Data.Models;
    using Charity.Web.Infrastructure.Mapping;

    public class FoodCategoryEditModel : IMapFrom<FoodCategory>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsChecked { get; set; }
    }
}