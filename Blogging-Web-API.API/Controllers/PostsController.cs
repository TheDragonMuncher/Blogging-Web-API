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
    private readonly IPostRepository _postRepository;
    private readonly ICommentRepository _commentRepository;
    public PostsController(IPostRepository postRepo, ICommentRepository commentRepo)
    {
        _postRepository = postRepo;
        _commentRepository = commentRepo;
    }

    // GET: api/posts
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Post>>> GetAllPosts()
    {
        var posts = await _postRepository.GetAllAsync();
        return Ok(posts);
    }

    // GET: api/posts/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Post>> GetPostById(int id)
    {
        var post = await _postRepository.GetByIdAsync(id);
        if (post == null)
        {
            return NotFound(new { message = $"Post with Id: {id} not found" });
        }
        return Ok(post);
    }

    // POST: api/posts
    [HttpPost]
    public async Task<ActionResult<Post>> CreatePost([FromBody] CreatePostDto createPostDto)
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

        await _postRepository.CreateAsync(post);
        return CreatedAtAction(nameof(GetPostById), new { id = post.Id }, post);
    }

    // PUT: api/posts/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePost(int id, [FromBody] UpdatePostDto updatePostDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var post = await _postRepository.GetByIdAsync(id);
        if (post == null)
        {
            return NotFound(new { message = $"Post by Id: {id} not found" });
        }
        post.Title = updatePostDto.Title;
        post.Content = updatePostDto.Content;
        var updatedPost = await _postRepository.UpdateAsync(post);

        return Ok(updatedPost);
    }

    // PATCH: api/posts/{id}
    [HttpPatch("{id}")]
    public async Task<IActionResult> PatchPost(int id, [FromBody] JsonPatchDocument<Post> patchDoc)
    {
        if (patchDoc == null)
        {
            return BadRequest(new { message = "Patch document is null" });
        }
        var post = await _postRepository.GetByIdAsync(id);

        if (post == null)
        {
            return NotFound(new { message = $"Post by Id: {id} not found" });
        }
        patchDoc.ApplyTo(post, ModelState);

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        post.UpdatedAt = DateTime.UtcNow;
        var updatedPost = await _postRepository.UpdateAsync(post);
        return Ok(updatedPost);
    }

    // DELETE: api/posts/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePost(int id)
    {
        var deleted = await _postRepository.DeleteAsync(id);
        if (!deleted)
        {
            return NotFound(new { message = $"Post by Id: {id} not found" });
        }
        return NoContent();
    }


    // GET: /api/posts/{postId}/comments
    [HttpGet("{postId}/comments")]
    public async Task<ActionResult<IEnumerable<Comment>>> GetComments(int postId)
    {
        var comments = await _commentRepository.GetByPostIdAsync(postId);
        return Ok(comments);
    }

    // POST: /api/posts/{postId}/comments
    [HttpPost("{postId}/comments")]
    public async Task<ActionResult<Comment>> CreateComment(int postId, [FromBody] CreateCommentDto createCommentDto)
    {
        var comment = new Comment
        {
            PostId = postId,
            Name = createCommentDto.Name,
            Email = createCommentDto.Email,
            Content = createCommentDto.Content,
            CreatedAt = DateTime.UtcNow
        };

        var createdComment = await _commentRepository.CreateAsync(comment);
        return Ok(createdComment);
    }
}