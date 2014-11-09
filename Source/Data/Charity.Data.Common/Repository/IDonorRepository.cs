namespace Charity.Data.Common.Repository
{
    using System;
    using System.Linq;
    using Charity.Data.Models;

    public interface IDonorRepository : IDeletableEntityRepository<Donor>
    {
    }
}
