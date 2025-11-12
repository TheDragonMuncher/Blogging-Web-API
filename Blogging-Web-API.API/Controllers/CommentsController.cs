using Blogging_Web_API.Core.DTOs;
using Blogging_Web_API.Core.Interfaces;
using Blogging_Web_API.Core.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Blogging_Web_API.API.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class CommentsController : ControllerBase
{
    private readonly ICommentRepository _repository;
    public CommentsController(ICommentRepository repo)
    {
        _repository = repo;
    }

    // GET: /api/comments
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Comment>>> GetAllComments()
    {
        var comments = await _repository.GetAllAsync();
        return Ok(comments);
    }

    // GET: /api/comments/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Comment>> GetCommentById(int id)
    {
        var comment = await _repository.GetByIdAsync(id);
        if (comment == null)
        {
            return NotFound(new { message = $"Comment by Id: {id} not found" });
        }
        return Ok(comment);
    }

    // PUT: /api/comments/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateComment(int id, [FromBody] UpdateCommentDto updateCommentDto)
    {
        var comment = await _repository.GetByIdAsync(id);
        if (comment == null)
        {
            return NotFound(new { message = $"Comment by Id: {id} not found" });
        }
        comment.Content = updateCommentDto.Content;
        comment.UpdatedAt = DateTime.UtcNow;
        var updatedComment = await _repository.UpdateAsync(comment);
        return Ok(updatedComment);
    }

    // PATCH: api/comments/{id}
    [HttpPatch("{id}")]
    public async Task<IActionResult> PatchComment(int id, [FromBody] JsonPatchDocument<Comment> patchDoc)
    {
        if (patchDoc == null)
        {
            return BadRequest(new { message = "Patch document is null" });
        }
        var comment = await _repository.GetByIdAsync(id);

        if (comment == null)
        {
            return NotFound(new { message = $"Comment by Id: {id} not found" });
        }
        patchDoc.ApplyTo(comment, ModelState);

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        comment.UpdatedAt = DateTime.UtcNow;
        var updatedComment = await _repository.UpdateAsync(comment);
        return Ok(updatedComment);
    }

    // DELETE: api/comments/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteComment(int id)
    {
        var deleted = await _repository.DeleteAsync(id);
        if (!deleted)
        {
            return NotFound(new { message = $"Comment by Id: {id} not found" });
        }
        return NoContent();
    }
}