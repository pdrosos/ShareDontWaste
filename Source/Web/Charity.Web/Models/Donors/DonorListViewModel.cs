namespace Charity.Web.Models.Donors
{
    using System;
    using System.Linq;
    using PagedList;

    public class DonorListViewModel
    {
        public IPagedList<DonorViewModel> Donors { get; set; }
    }
}