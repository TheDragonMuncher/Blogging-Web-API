using Blogging_Web_API.Core.Models;

namespace Blogging_Web_API.Core.Interfaces;

public interface IPostRepository
{
    Task<ICollection<Post>> GetAllAsync();
    Task<Post> GetByIdAsync();
    Task<Post> CreateAsync(Post post);
    Task<Post> UpdateAsync(Post post);
    Task<bool> DeleteAsync(int id);
}