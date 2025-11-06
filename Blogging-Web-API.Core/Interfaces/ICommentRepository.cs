using Blogging_Web_API.Core.Models;

namespace Blogging_Web_API.Core.Interfaces;

public interface ICommentRepository
{
    Task<ICollection<Comment>?> GetAllAsync();
    Task<ICollection<Comment>?> GetByPostAsync(Post post);
    Task<Comment?> GetByIdAsync(int id);
    Task<Comment> CreateAsync(Comment comment);
    Task<Comment?> UpdateAsync(Comment comment);
    Task<bool> DeleteAsync(int id);
}