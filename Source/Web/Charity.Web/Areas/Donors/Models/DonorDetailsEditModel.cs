namespace Charity.Web.Areas.Donors.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web.Mvc;
    using Charity.Data.Models;
    using Charity.Web.Infrastructure.Mapping;

    public class DonorDetailsEditModel : IMapFrom<Donor>
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The organization name must not be more than {1} characters.")]
        [Display(Name = "Organization Name")]
        public string OrganizationName { get; set; }

        [StringLength(250, ErrorMessage = "The {0} must not be more than {1} characters.")]
        public string Address { get; set; }

        public City City { get; set; }

        [Required]
        [Display(Name = "City")]
        public int CityId { get; set; }

        public IEnumerable<SelectListItem> Cities { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The contact name must not be more than {1} characters.")]
        [Display(Name = "Contact Name")]
        public string ContactName { get; set; }

        public AccountDetailsEditModel AccountDetailsEditModel { get; set; }
    }
}