using System.ComponentModel.DataAnnotations;

namespace Blogging_Web_API.Core.DTOs;

public class UpdateCommentDto
{
    [Required]
    [MaxLength(1000)]
    public string Content { get; set; } = string.Empty;
}