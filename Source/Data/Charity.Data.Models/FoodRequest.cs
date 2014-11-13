namespace Charity.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class FoodRequest : SoftDeletable
    {
        private ICollection<FoodRequestComment> comments;

        public FoodRequest()
        {
            this.comments = new HashSet<FoodRequestComment>();
        }

        public int Id { get; set; }

        [Required]
        public string RecipientId { get; set; }

        public virtual Recipient Recipient { get; set; }

        [Required]
        public int FoodId { get; set; }

        public virtual FoodDonation Food { get; set; }

        [Required]
        public string Quantity { get; set; }

        [Required]
        public DateTime NeedFrom { get; set; }

        [Required]
        public DateTime NeedTo { get; set; }
        
        public string Description { get; set; }

        public string AdminNotes { get; set; }

        public bool IsCompleted { get; set; }

        public virtual ICollection<FoodRequestComment> Comments
        {
            get
            {
                return this.comments;
            }

            set
            {
                this.comments = value;
            }
        }
    }
}
