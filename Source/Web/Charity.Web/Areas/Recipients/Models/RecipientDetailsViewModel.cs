namespace Charity.Web.Areas.Recipients.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using Charity.Data.Models;
    using Charity.Web.Infrastructure.Mapping;

    public class RecipientDetailsViewModel : IMapFrom<Recipient>
    {
        public Guid Id { get; set; }

        [Display(Name = "Organization Name")]
        public string OrganizationName { get; set; }

        public string Address { get; set; }
                
        public City City { get; set; }

        public RecipientType RecipientType { get; set; }

        [Display(Name = "Contact Name")]
        public string ContactName { get; set; }

        public AccountDetailsViewModel AccountDetailsViewModel { get; set; }

        [Display(Name = "Your food needs")]
        public ICollection<FoodCategory> FoodCategories { get; set; }
    }
}