namespace Charity.Web.Infrastructure.Identity
{
    using System.Security.Principal;
    using Microsoft.AspNet.Identity;
    
    using Charity.Data.Models;
    using Charity.Data.Common;
    using Charity.Data;
    
    public class CurrentUser : ICurrentUser
    {
        private readonly IIdentity currentIdentity;
        private readonly ApplicationDbContext currentDbContext;

        private ApplicationUser user;

        public CurrentUser(IIdentity identity, ApplicationDbContext context)
        {
            this.currentIdentity = identity;
            this.currentDbContext = context;
        }

        public ApplicationUser Get()
        {
            return this.user ?? (this.user = this.currentDbContext.Users.Find(this.currentIdentity.GetUserId()));
        }
    }
}