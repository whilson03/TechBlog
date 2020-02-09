using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TechBlog.Models
{
    public class Post
    {
        public int Id { get; set; }

        [Display(Name = "Post Title")]
        public string Title { get; set; }
        [Display(Name = "Post Description")]
        public string Description { get; set; }

        [Display(Name = "Post Content")]
        public string Content { get; set; }

        public DateTime DatePosted { get; set; } = DateTime.Now;

        public DateTime DateLastUpdated { get; set; }
        
        public int ViewsCount { get; set; }

        public string BannerImageLink { get; set; }

        public int Likes { get; set; }

        public int Dislikes { get; set; }
    }
}
