namespace Charity.Data.Common.Repositories
{
    using Charity.Data.Models;

    public interface IDonorRepository : IDeletableEntityRepository<Donor>
    {
        Donor GetById(string id);
    }
}