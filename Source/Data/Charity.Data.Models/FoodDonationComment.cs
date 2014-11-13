namespace Charity.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class FoodDonationComment : SoftDeletable
    {
        public int Id { get; set; }

        [Required]
        public string ApplicationUserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        [Required]
        public int FoodDonationId { get; set; }

        public virtual FoodDonation FoodDonation { get; set; }

        [Required]
        public string Text { get; set; }
    }
}
