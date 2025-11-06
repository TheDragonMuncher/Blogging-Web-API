using Blogging_Web_API.Core.Interfaces;
using Blogging_Web_API.Core.Models;
using Blogging_Web_API.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Blogging_Web_API.Infrastructure.Repositories;

public class CommentRepository : ICommentRepository
{
    private readonly AppDbContext _context;
    public CommentRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Comment> CreateAsync(Comment comment)
    {
        _context.Comments.Add(comment);
        await _context.SaveChangesAsync();
        return comment;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var comment = await _context.Comments.FindAsync(id);
        if (comment == null)
        {
            return false;
        }
        _context.Comments.Remove(comment);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<ICollection<Comment>?> GetAllAsync()
    {
        return await _context.Comments.ToListAsync();
    }

    public async Task<Comment?> GetByIdAsync(int id)
    {
        var comment = await _context.Comments.FindAsync(id);
        if (comment == null)
        {
            return null;
        }
        return comment;
    }

    public async Task<ICollection<Comment>?> GetByPostIdAsync(int postId)
    {
        return await _context.Comments.Where(c => c.PostId == postId).ToListAsync();
    }

    public async Task<Comment?> UpdateAsync(Comment comment)
    {
        var currentComment = await _context.Comments.FindAsync(comment.Id);
        if (currentComment == null)
        {
            return null;
        }
        currentComment.Content = comment.Content;
        currentComment.UpdatedAt = DateTime.UtcNow;
        _context.Comments.Update(currentComment);
        await _context.SaveChangesAsync();
        return currentComment;
    }
}