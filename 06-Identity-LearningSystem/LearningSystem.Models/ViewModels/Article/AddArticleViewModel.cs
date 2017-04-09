using System.ComponentModel.DataAnnotations;

namespace LearningSystem.Models.ViewModels.Article
{
    public class AddArticleViewModel
    {
        [Required, MinLength(5)]
        public string Title { get; set; }

        [Required, MinLength(40)]
        public string Content { get; set; }

        [Display(Name = "Image URL")]
        [RegularExpression("(http|https)://.+")]
        public string ImageUrl { get; set; }
    }
}