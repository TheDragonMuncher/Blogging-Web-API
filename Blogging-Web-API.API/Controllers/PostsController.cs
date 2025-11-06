using Blogging_Web_API.Core.DTOs;
using Blogging_Web_API.Core.Interfaces;
using Blogging_Web_API.Core.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Blogging_Web_API.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PostsController : ControllerBase
{
    private readonly IPostRepository _repository;
    public PostsController(IPostRepository repo)
    {
        _repository = repo;
    }

    // GET: api/posts
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Post>>> GetAll()
    {
        var posts = await _repository.GetAllAsync();
        return Ok(posts);
    }

    // GET: api/posts/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Post>> GetById(int id)
    {
        var post = await _repository.GetByIdAsync(id);
        if (post == null)
        {
            return NotFound(new { message = $"Post with Id: {id} not found" });
        }
        return Ok(post);
    }

    // POST: api/posts
    [HttpPost]
    public async Task<ActionResult<Post>> Create([FromBody] CreatePostDto createPostDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var post = new Post
        {
            Title = createPostDto.Title,
            Content = createPostDto.Content,
            Author = "Admin"
        };

        await _repository.CreateAsync(post);
        return CreatedAtAction(nameof(GetById), new { id = post.Id }, post);
    }

    // PUT: api/posts/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdatePostDto updatePostDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var post = await _repository.GetByIdAsync(id);
        if (post == null)
        {
            return NotFound(new { message = $"Post by Id: {id} not found" });
        }
        post.Title = updatePostDto.Title;
        post.Content = updatePostDto.Content;
        var updatedPost = await _repository.UpdateAsync(post);

        return Ok(updatedPost);
    }

    // PATCH: api/posts/{id}
    [HttpPatch("{id}")]
    public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<Post> patchDoc)
    {
        if (patchDoc == null)
        {
            return BadRequest(new { message = "Patch document is null" });
        }
        var post = await _repository.GetByIdAsync(id);

        if (post == null)
        {
            return NotFound(new { message = $"Post by Id: {id} not found" });
        }
        patchDoc.ApplyTo(post, (Microsoft.AspNetCore.JsonPatch.Adapters.IObjectAdapter)ModelState);

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        post.UpdatedAt = DateTime.UtcNow;
        var updatedPost = await _repository.UpdateAsync(post);
        return Ok(updatedPost);
    }

    // DELETE: api/posts/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _repository.DeleteAsync(id);
        if (!deleted)
        {
            return NotFound(new { message = $"Post by Id: {id} not found" });
        }
        return NoContent();
    }
}