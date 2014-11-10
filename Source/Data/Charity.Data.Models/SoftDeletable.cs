namespace Charity.Data.Models
{
    using Charity.Data.Models.Common;
    using System;
    using System.ComponentModel.DataAnnotations;

    public abstract class SoftDeletable : AuditInfo, ISoftDeletable
    {
        [Display(Name = "Deleted?")]
        [Editable(false)]
        public bool IsDeleted { get; set; }

        [Display(Name = "Deletion date")]
        [Editable(false)]
        [DataType(DataType.DateTime)]
        public DateTime? DeletedOn { get; set; }
    }
}
