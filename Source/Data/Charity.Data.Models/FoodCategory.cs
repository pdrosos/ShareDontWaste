namespace Charity.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class FoodCategory : SoftDeletable
    {
        private ICollection<FoodDonation> foodDonations;

        public FoodCategory()
        {
            this.foodDonations = new HashSet<FoodDonation>();
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        [Index(IsUnique = true)]
        public string Name { get; set; }

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
