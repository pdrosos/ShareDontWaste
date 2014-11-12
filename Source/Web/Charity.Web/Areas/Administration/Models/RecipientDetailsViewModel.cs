namespace Charity.Web.Areas.Administration.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using Charity.Data.Models;
    using Charity.Web.Infrastructure.Mapping;

    public class RecipientDetailsViewModel : IMapFrom<Recipient>
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The organization name must not be more than {1} characters.")]
        [Display(Name = "Organization Name")]
        public string OrganizationName { get; set; }

        [StringLength(250, ErrorMessage = "The {0} must not be more than {1} characters.")]
        public string Address { get; set; }
                
        public City City { get; set; }

        public RecipientType RecipientType { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The contact name must not be more than {1} characters.")]
        [Display(Name = "Contact Name")]
        public string ContactName { get; set; }

        public AccountDetailsViewModel AccountDetailsViewModel { get; set; }
    }
}