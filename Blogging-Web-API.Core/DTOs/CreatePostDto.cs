using System.ComponentModel.DataAnnotations;

namespace Blogging_Web_API.Core.DTOs;

public class CreatePostDto
{
    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    [Required]
    public string Content { get; set; } = string.Empty;

    [Required]
    public string Author { get; set; } = string.Empty;
}