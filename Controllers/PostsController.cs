using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ChallengePostsAPI.Models;

[Route("api/[controller]")]
[ApiController]
public class PostsController : ControllerBase
{
    private readonly PostContext _context;

    public PostsController(PostContext context)
    {
        _context = context;
    }

    // GET: api/posts
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Post>>> GetPosts()
    {
        return await _context.Posts.ToListAsync();
    }

    // POST: api/posts
    [HttpPost]
    public async Task<ActionResult<Post>> PostPost(Post post)
    {
        _context.Posts.Add(post);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetPosts", new { id = post.Id }, post);
    }

    // DELETE: api/posts/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePost(int id)
    {
        var post = await _context.Posts.FindAsync(id);
        if (post == null)
        {
            return NotFound();
        }

        _context.Posts.Remove(post);
        await _context.SaveChangesAsync();

        return Ok(post); 
    }
}
