using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using uowpublic.Services;
namespace uowpublic.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyController : ControllerBase
{
    private readonly IPropertyService _propertyService;

    public PropertyController(IPropertyService propertyService) =>
        _propertyService = propertyService;


    [HttpGet]
    public async Task<List<Models.Property>> Get() =>
        await _propertyService.GetAsync();

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Models.Property>> Get(int id)
    {
        var property = await _propertyService.GetAsync(id);

        if (property is null)
        {
            return NotFound();
        }

        return property;
    }

    [HttpPost]
    public async Task<IActionResult> Post(Models.Property newProperty)
    {
        await _propertyService.CreateAsync(newProperty);

        return CreatedAtAction(nameof(Get), new { id = newProperty.Id }, newProperty);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, Models.Property updatedProperty)
    {
        var property = await _propertyService.GetAsync(id);

        if (property is null)
        {
            return NotFound();
        }

        updatedProperty.Id = property.Id;

        await _propertyService.UpdateAsync(id, updatedProperty);

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var property = await _propertyService.GetAsync(id);

        if (property is null)
        {
            return NotFound();
        }

        await _propertyService.RemoveAsync(id);

        return NoContent();
    }
}