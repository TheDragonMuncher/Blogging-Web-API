using Blogging_Web_API.Core.Interfaces;
using Blogging_Web_API.Core.Models;
using Blogging_Web_API.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Blogging_Web_API.Infrastructure.Repositories;

public class PostRepository : IPostRepository
{
    private readonly AppDbContext _context;
    public PostRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Post> CreateAsync(Post post)
    {
        post.CreatedAt = DateTime.UtcNow;

        _context.Add(post);
        await _context.SaveChangesAsync();
        return post;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var post = await _context.Posts.FindAsync(id);
        if (post == null)
        {
            return false;
        }
        _context.Posts.Remove(post);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<ICollection<Post>?> GetAllAsync()
    {
        return await _context.Posts.ToListAsync();
    }

    public async Task<Post?> GetByIdAsync(int id)
    {
        var post = await _context.Posts.FindAsync(id);
        if (post == null)
        {
            return null;
        }
        return post;
    }

    public async Task<Post?> UpdateAsync(Post post)
    {
        var currentPost = await _context.Posts.FindAsync(post.Id);
        if (currentPost == null)
        {
            return null;
        }
        currentPost.Title = post.Title;
        currentPost.Content = post.Content;
        currentPost.UpdatedAt = DateTime.UtcNow;
        _context.Posts.Update(currentPost);
        await _context.SaveChangesAsync();
        return currentPost;
    }
}