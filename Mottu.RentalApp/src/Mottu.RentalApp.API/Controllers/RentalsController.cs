using Microsoft.AspNetCore.Mvc;
using Mottu.RentalApp.Application.DTOs.Requests;
using Mottu.RentalApp.Application.DTOs.Responses;
using Mottu.RentalApp.Application.Interfaces.Services;
using Mottu.RentalApp.Application.Services;

namespace Mottu.RentalApp.API.Controllers
{
    [ApiController]
    [Route("api/rentals")]
    [ApiExplorerSettings(GroupName = "locação")]
    public class RentalsController : ControllerBase
    {
        private readonly IRentalService _rentalService;

        public RentalsController(IRentalService rentalService)
        {
            _rentalService = rentalService;
        }

      
        [HttpPost]
        public async Task<IActionResult> CreateRental([FromBody] CreateRentalRequest request)
        {

            if (request == null)
                return BadRequest("Invalid request.");

            try
            {
                var rental = await _rentalService.CreateRentalAsync(request);
                return StatusCode(201);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

      
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetRentalById(Guid id)        {

            var rental = await _rentalService.GetByIdAsync(id);
            if (rental is null)
                return NotFound($"Nenhuma locação encontrada com o ID {id}.");

            return Ok(rental);
        }

     
        [HttpPut("{rentalId:guid}/devolucao")]
        public async Task<IActionResult> ReturnRental(Guid rentalId, [FromBody] CreateRentalRequest request)
        {
            if (request == null)
                return BadRequest("Invalid return request.");

            try
            {
                var rental = await _rentalService.ReturnRentalAsync(rentalId, request.StartDate);
                return Ok(new
                {
                    rental.Id,
                    rental.Status,
                    rental.TotalAmount,
                    rental.EndDateUtc
                });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
