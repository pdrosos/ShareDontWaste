namespace Charity.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Charity.Data.Common.Models;

    public class RecipientType : DeletableEntity
    {
        private ICollection<Recipient> recipients;

        public RecipientType()
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
