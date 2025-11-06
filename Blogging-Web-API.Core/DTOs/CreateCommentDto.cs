using System.ComponentModel.DataAnnotations;
using Blogging_Web_API.Core.Models;

namespace Blogging_Web_API.Core.DTOs;

public class CreateCommentDto
{
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
}