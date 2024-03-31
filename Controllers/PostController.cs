using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using uowpublic.Services;
namespace uowpublic.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PostController : ControllerBase
{
    private readonly IPostService _postService;

    public PostController(IPostService postService) =>
        _postService = postService;


    [HttpGet]
    public async Task<List<Models.Post>> Get() =>
        await _postService.GetAsync();

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Models.Post>> Get(int id)
    {
        var post = await _postService.GetAsync(id);

        if (post is null)
        {
            return NotFound();
        }

        return post;
    }

    [HttpPost]
    public async Task<IActionResult> Post(Models.Post newPost)
    {
        await _postService.CreateAsync(newPost);

        return CreatedAtAction(nameof(Get), new { id = newPost.Id }, newPost);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, Models.Post updatedPost)
    {
        var post = await _postService.GetAsync(id);

        if (post is null)
        {
            return NotFound();
        }

        updatedPost.Id = post.Id;

        await _postService.UpdateAsync(id, updatedPost);

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var post = await _postService.GetAsync(id);

        if (post is null)
        {
            return NotFound();
        }

        await _postService.RemoveAsync(id);

        return NoContent();
    }
}