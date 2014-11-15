namespace Charity.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class FoodDonation : SoftDeletable
    {
        private ICollection<FoodRequest> foodRequests;

        public FoodDonation()
        {
            this.foodRequests = new HashSet<FoodRequest>();
        }

        public int Id { get; set; }

        [Required]
        public Guid DonorId { get; set; }

        public virtual Donor Donor { get; set; }

        [Required]
        public int FoodCategoryId { get; set; }

        public virtual FoodCategory FoodCategory { get; set; }

        [Required]
        public string Name { get; set; }

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

        public virtual ICollection<FoodRequest> FoodRequests
        {
            get { return this.foodRequests; }

            set { this.foodRequests = value; }
        }
    }
}
