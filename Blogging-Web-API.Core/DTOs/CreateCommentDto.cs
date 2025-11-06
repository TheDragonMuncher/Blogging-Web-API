using System.ComponentModel.DataAnnotations;

namespace Blogging_Web_API.Core.DTOs;

public class CreateCommentDto
{
    [Required]
    public int PostId { get; set; }
    
    [Required]
    [MaxLength(1000)]
    public string Content { get; set; } = string.Empty;
}