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

        [Display(Name = "Organization Name")]
        public string OrganizationName { get; set; }

        public string Address { get; set; }
                
        public City City { get; set; }

        [Display(Name = "Contact Name")]
        public string ContactName { get; set; }

        public AccountDetailsViewModel AccountDetailsViewModel { get; set; }
    }
}