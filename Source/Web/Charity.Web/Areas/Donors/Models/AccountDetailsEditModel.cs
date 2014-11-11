namespace Charity.Web.Areas.Donors.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using Charity.Data.Models;
    using Charity.Web.Infrastructure.Mapping;

    public class AccountDetailsEditModel : IMapFrom<ApplicationUser>
    {
        [Phone]
        public string PhoneNumber { get; set; }
    }
}