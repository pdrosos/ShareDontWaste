namespace Charity.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class FoodDonation : SoftDeletable
    {
        public int Id { get; set; }

        [Required]
        public string DonorId { get; set; }

        public virtual Donor Donor { get; set; }

        [Required]
        public int FoodCategoryId { get; set; }

        public virtual FoodCategory FoodCategory { get; set; }

        [Required]
        public string Quantity { get; set; }

        [Required]
        public DateTime ExpirationDate { get; set; }

        [Required]
        public DateTime AvailableFrom { get; set; }

        [Required]
        public DateTime AvailableTo { get; set; }

        public string Description { get; set; }

        public string AdminNotes { get; set; }

        public bool IsCompleted { get; set; }
    }
}
