namespace Charity.Data.Models.Common
{
    using System;

    public interface ISoftDeletable
    {
        bool IsDeleted { get; set; }

        DateTime? DeletedOn { get; set; }
    }
}