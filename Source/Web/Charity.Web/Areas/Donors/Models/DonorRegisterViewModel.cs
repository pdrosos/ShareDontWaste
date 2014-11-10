namespace Charity.Web.Areas.Donors.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using Charity.Data.Models;
    using Charity.Web.Infrastructure.Mapping;
    using Charity.Web.Models;

    public class DonorRegisterViewModel : IMapFrom<Donor>
    {
        [StringLength(100, ErrorMessage = "The {0} must not be longer than {1} symbols.")]
        [Display(Name = "Organization Name")]
        public string OrganizationName { get; set; }

        public RegisterViewModel RegisterViewModel { get; set; }
    }
}