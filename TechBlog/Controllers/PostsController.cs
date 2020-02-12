using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechBlog.Data;
using TechBlog.Models;

namespace TechBlog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {

        public string TitleSort { get; set; }
        public string DateSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        public PaginatedList<Post> Posts { get; set; }

        private readonly ApplicationDbContext _context;

        public PostsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Posts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Post>>> GetBlogPosts([FromQuery] string sortOrder, [FromQuery] string searchString,
           [FromQuery] string currentFilter, [FromQuery] int? pageIndex)
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

            return Posts;

        }

        // GET: api/Posts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Post>> GetPost(int id)
        {
            var post = await _context.BlogPosts.FindAsync(id);

            if (post == null)
            {
                return NotFound();
            }

            return post;
        }

        // PUT: api/Posts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPost(int id, Post post)
        {
            if (id != post.Id)
            {
                return BadRequest();
            }

            _context.Entry(post).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PostExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Posts
        [HttpPost]
        public async Task<ActionResult<Post>> PostPost(Post post)
        {
            _context.BlogPosts.Add(post);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPost", new { id = post.Id }, post);
        }

        // DELETE: api/Posts/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Post>> DeletePost(int id)
        {
            var post = await _context.BlogPosts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            _context.BlogPosts.Remove(post);
            await _context.SaveChangesAsync();

            return post;
        }

        private bool PostExists(int id)
        {
            return _context.BlogPosts.Any(e => e.Id == id);
        }
    }
}
