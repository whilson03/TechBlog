using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TechBlog.Data;
using TechBlog.Models;

namespace TechBlog
{
    public class IndexModel : PageModel
    {
        private readonly TechBlog.Data.ApplicationDbContext _context;

        public IndexModel(TechBlog.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Post> Post { get;set; }

        public async Task OnGetAsync()
        {
            Post = await _context.BlogPosts.ToListAsync();
        }
    }
}
