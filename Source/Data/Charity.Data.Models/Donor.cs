namespace Charity.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public class Donor : SoftDeletable
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string ContactName { get; set; }

        public string OrganizationName { get; set; }

        public string Address { get; set; }

        public int? CityId { get; set; }

        public virtual City City { get; set; }

        public string ApplicationUserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        //public virtual ICollection<Food> Food { get; set; }
    }
}
