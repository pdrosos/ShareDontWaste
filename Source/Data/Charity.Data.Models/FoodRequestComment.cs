namespace Charity.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class FoodRequestComment : SoftDeletable
    {
        public int Id { get; set; }

        [Required]
        public string ApplicationUserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        [Required]
        public int FoodRequestId { get; set; }

        public virtual FoodRequest FoodRequest { get; set; }

        public bool IsReadFromDonor { get; set; }

        public bool IsReadFromRecipient { get; set; }

        [Required]
        public string Text { get; set; }
    }
}
