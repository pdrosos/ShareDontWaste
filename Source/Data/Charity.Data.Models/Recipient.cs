namespace Charity.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public class Recipient : SoftDeletable
    {
        private ICollection<FoodCategory> foodCategories;

        private ICollection<FoodRequest> foodRequests;

        public Recipient()
        {
            this.foodCategories = new HashSet<FoodCategory>();
            this.foodRequests = new HashSet<FoodRequest>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string ApplicationUserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        public int? RecipientTypeId { get; set; }

        public virtual RecipientType RecipientType { get; set; }

        public string OrganizationName { get; set; }
        
        public string ContactName { get; set; }

        public string ContactPhone { get; set; }
        
        public string Address { get; set; }

        public int? CityId { get; set; }

        public virtual City City { get; set; }

        public virtual ICollection<FoodCategory> FoodCategories
        {
            get
            {
                return this.foodCategories;
            }

            set
            {
                this.foodCategories = value;
            }
        }

        public virtual ICollection<FoodRequest> FoodRequests
        {
            get
            {
                return this.foodRequests;
            }

            set
            {
                this.foodRequests = value;
            }
        }
    }
}
