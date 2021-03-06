﻿namespace Charity.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public class Donor : SoftDeletable
    {
        private ICollection<FoodDonation> foodDonations;

        public Donor()
        {
            this.foodDonations = new HashSet<FoodDonation>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string ApplicationUserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
        
        public string OrganizationName { get; set; }

        public string ContactName { get; set; }

        public string Address { get; set; }

        public int? CityId { get; set; }

        public virtual City City { get; set; }        

        public virtual ICollection<FoodDonation> FoodDonations
        {
            get
            {
                return this.foodDonations;
            }

            set
            {
                this.foodDonations = value;
            }
        }
    }
}
