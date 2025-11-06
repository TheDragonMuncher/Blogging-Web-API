using System.ComponentModel.DataAnnotations;

namespace Blogging_Web_API.Core.Models;

public class Comment
{
    public int Id { get; set; }

    [Required]
    public int PostId { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [MaxLength(150)]
    public string Email { get; set; } = string.Empty;

    [Required]
    [MaxLength(1000)]
    public string Content { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }

    public Post? Post { get; set; }
}