namespace Charity.Data.Common.Repositories
{
    using System;
    using Charity.Data.Models;

    public interface IDonorRepository : IDeletableEntityRepository<Donor>
    {
        Donor GetById(Guid id);
    }
}