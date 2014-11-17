namespace Charity.Web.Areas.Recipients.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using Charity.Data.Models;
    using Charity.Web.Infrastructure.Mapping;

    public class FoodRequestCommentCreateModel : IMapFrom<FoodRequestComment>
    {
        public FoodRequestCommentCreateModel()
        {

        }

        public FoodRequestCommentCreateModel(int foodRequestId)
        {
            this.FoodRequestId = foodRequestId;
        }

        [Required]
        [Display(Name = "Food Request")]
        public int FoodRequestId { get; set; }

        [Required]
        [StringLength(600, ErrorMessage = "The {0} must not be more than {1} characters.")]
        [UIHint("MultilineText")]
        public string Text { get; set; }
    }
}