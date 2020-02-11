using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TechBlog.Models;

namespace TechBlog.Pages
{
    public class IndexModel : PageModel
    {
        private readonly TechBlog.Data.ApplicationDbContext _context;

        public string TitleSort { get; set; }
        public string DateSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        public PaginatedList<Post> Posts { get; set; }

        public List<Post> RecentPost { get; set; }

        public IndexModel(TechBlog.Data.ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task OnGetAsync(string sortOrder, string searchString, string currentFilter, int? pageIndex)
        {

            CurrentSort = sortOrder;

            TitleSort = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            DateSort = sortOrder == "Date" ? "date_desc" : "Date";


            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }


            CurrentFilter = searchString;

            IQueryable<Post> postsIQ = from s in _context.BlogPosts
                                             select s;

            RecentPost = await postsIQ.OrderByDescending(s => s.DatePosted).Take(3).ToListAsync();

            if (!String.IsNullOrEmpty(searchString))
            {
                postsIQ = postsIQ.Where(s => s.Title.ToUpper().Contains(searchString.ToUpper())
                                       || s.Description.ToUpper().Contains(searchString.ToUpper()));
            }


            switch (sortOrder)
            {
                case "title_desc":
                    postsIQ = postsIQ.OrderByDescending(s => s.Title);
                    break;
                case "Date":
                    postsIQ = postsIQ.OrderBy(s => s.DatePosted);
                    break;
                case "date_desc":
                    postsIQ = postsIQ.OrderByDescending(s => s.DateLastUpdated);
                    break;
                default:
                    postsIQ = postsIQ.OrderByDescending(s => s.DatePosted);
                    break;
            }

            int pageSize = 3;
            Posts = await PaginatedList<Post>.CreateAsync(
                postsIQ.AsNoTracking(), pageIndex ?? 1, pageSize);

          


        }
    }
}
