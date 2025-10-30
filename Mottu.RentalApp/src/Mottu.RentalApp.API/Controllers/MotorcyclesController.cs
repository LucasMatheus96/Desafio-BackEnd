using Microsoft.AspNetCore.Mvc;
using Mottu.RentalApp.Application.Interfaces.Services;
using Mottu.RentalApp.Application.DTOs.Requests;
using Mottu.RentalApp.Application.DTOs.Responses;

namespace Mottu.RentalApp.API.Controllers
{
    [ApiController]
    [Route("api/motorcycles")]
    public class MotorcyclesController : ControllerBase
    {
        private readonly IMotorcycleService _motorcycleService;

        public MotorcyclesController(IMotorcycleService motorcycleService)
        {
            _motorcycleService = motorcycleService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateMotorcycleRequest request)
        {
            var result = await _motorcycleService.CreateAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MotorcycleResponse>>> GetAll([FromQuery] string? plate)
        {
            var list = await _motorcycleService.GetAllAsync(plate);
            return Ok(list);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            // optional: implement GetById in service
            var all = await _motorcycleService.GetAllAsync(null);
            var found = System.Linq.Enumerable.FirstOrDefault(all, m => m.Id == id);
            if (found == null) return NotFound();
            return Ok(found);
        }

        [HttpPut("{id:guid}/plate")]
        public async Task<IActionResult> UpdatePlate(Guid id, [FromBody] UpdateMotorcyclePlateRequest request)
        {
            await _motorcycleService.UpdatePlateAsync(request);
            return Ok();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _motorcycleService.DeleteAsync(id);
            return NoContent();
        }
    }
   
}
