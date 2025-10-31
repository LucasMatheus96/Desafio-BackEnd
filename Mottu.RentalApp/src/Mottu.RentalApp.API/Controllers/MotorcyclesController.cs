using Microsoft.AspNetCore.Mvc;
using Mottu.RentalApp.Application.Interfaces.Services;
using Mottu.RentalApp.Application.DTOs.Requests;
using Mottu.RentalApp.Application.DTOs.Responses;
using static MassTransit.ValidationResultExtensions;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Mottu.RentalApp.API.Controllers
{
    [ApiController]
    [Route("api/motorcycles")]
    [ApiExplorerSettings(GroupName = "motos")]
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
            try
            {
                var result = await _motorcycleService.CreateAsync(request);
                return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
            }
            catch (Exception ex)
            {
                return StatusCode(400, new ErrorResponse
                {
                    ErrorMessage = "Dados inválidos",                   
                });
            }
           
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MotorcycleResponse>>> GetAll([FromQuery] string? plate)
        {
            try
            {
                var list = await _motorcycleService.GetAllAsync(plate);
                return Ok(list);
            }
            catch
            {
                return NoContent();
            }
            
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var all = await _motorcycleService.GetAllAsync(null);
                var found = System.Linq.Enumerable.FirstOrDefault(all, m => m.Id == id);
                if (found == null) return NotFound();
                return Ok(found);
            }
            catch
            {
                return NoContent();
            }
           
        }

        [HttpPut("{id:guid}/plate")]
        public async Task<IActionResult> UpdatePlate(Guid id, [FromBody] UpdateMotorcyclePlateRequest request)
        {
            try
            {
                 var result =  await _motorcycleService.UpdatePlateAsync( id, request);
                return Ok(result); 
            }
            catch
            {
                return StatusCode(400, new ErrorResponse
                {
                    ErrorMessage = "Dados inválidos",
                });
            }
            
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _motorcycleService.DeleteAsync(id);
                return Ok();
            }
            catch
            {
                return StatusCode(400, new ErrorResponse
                {
                    ErrorMessage = "Dados inválidos",
                });
            }
           
        }
    }
   
}
