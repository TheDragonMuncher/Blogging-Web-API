using System.ComponentModel.DataAnnotations;

namespace Blogging_Web_API.Core.Models;

public class Post
{
    public int Id { get; set; }

    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    [Required]
    public string Content { get; set; } = string.Empty;

    [Required]
    public string Author { get; set; } = String.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }

    public ICollection<Comment>? Comments { get; set;}
}