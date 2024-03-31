using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using uowpublic.Services;
namespace uowpublic.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CommentController : ControllerBase
{
    private readonly ICommentService _commentService;

    public CommentController(ICommentService commentService) =>
        _commentService = commentService;


    [HttpGet]
    public async Task<List<Models.Comment>> Get() =>
        await _commentService.GetAsync();

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Models.Comment>> Get(int id)
    {
        var comment = await _commentService.GetAsync(id);

        if (comment is null)
        {
            return NotFound();
        }

        return comment;
    }

    [HttpPost]
    public async Task<IActionResult> Post(Models.Comment newComment)
    {
        await _commentService.CreateAsync(newComment);

        return CreatedAtAction(nameof(Get), new { id = newComment.Id }, newComment);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, Models.Comment updatedComment)
    {
        var comment = await _commentService.GetAsync(id);

        if (comment is null)
        {
            return NotFound();
        }

        updatedComment.Id = comment.Id;

        await _commentService.UpdateAsync(id, updatedComment);

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var comment = await _commentService.GetAsync(id);

        if (comment is null)
        {
            return NotFound();
        }

        await _commentService.RemoveAsync(id);

        return NoContent();
    }
}