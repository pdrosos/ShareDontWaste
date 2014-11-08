namespace Charity.Web.Infrastructure.Identity
{
    using Charity.Data.Models;

    public interface ICurrentUser
    {
        ApplicationUser Get();
    }
}