using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace TechBlog.Pages.Admin
{
    [Authorize]
    public class MetricsModel : PageModel
    {
        private readonly TechBlog.Data.ApplicationDbContext _context;


        public MetricsModel(TechBlog.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public int ToTalPost { get; set; }

        public async Task OnGetAsync()
        {
            ToTalPost = (await _context.BlogPosts.ToListAsync()).Count;
        }
    }
}
