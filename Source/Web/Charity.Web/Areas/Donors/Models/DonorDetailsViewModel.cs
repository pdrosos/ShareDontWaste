namespace Charity.Web.Areas.Donors.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using Charity.Data.Models;
    using Charity.Web.Infrastructure.Mapping;

    public class DonorDetailsViewModel : IMapFrom<Donor>
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The organization name must not be more than {1} characters.")]
        public string OrganizationName { get; set; }

        [StringLength(250, ErrorMessage = "The {0} must not be more than {1} characters.")]
        public string Address { get; set; }

        [StringLength(50, ErrorMessage = "The {0} must not be more than {1} characters.")]
        public string City { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The contact name must not be more than {1} characters.")]
        public string ContactName { get; set; }

        public AccountDetailsViewModel AccountDetailsViewModel { get; set; }
    }
}