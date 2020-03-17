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

        public DateTime DateLastUpdated { get; set; } =  DateTime.Now;

        public int ViewsCount { get; set; } = 0;

        public string BannerImageLink { get; set; }


        public Category Category { get; set; }

        public string author { get; set; }
    }
}
