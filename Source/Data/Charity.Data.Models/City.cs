namespace Charity.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class City : SoftDeletable
    {
        private ICollection<Recipient> recipients;

        public City()
        {
            this.recipients = new HashSet<Recipient>();
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        [Index(IsUnique = true)]
        public string Name { get; set; }

        public virtual ICollection<Recipient> Recipients
        {
            get
            {
                return this.recipients;
            }

            set
            {
                this.recipients = value;
            }
        }
    }
}
