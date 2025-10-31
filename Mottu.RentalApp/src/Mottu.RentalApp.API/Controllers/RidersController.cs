using Microsoft.AspNetCore.Mvc;
using Mottu.RentalApp.Application.DTOs.Requests;
using Mottu.RentalApp.Application.Interfaces.Services;
using Mottu.RentalApp.Application.DTOs.Responses;
using System;
using System.Threading.Tasks;

namespace Mottu.RentalApp.API.Controllers
{
    [ApiController]
    [Route("api/riders")]
    [ApiExplorerSettings(GroupName = "entregadores")]
    public class RidersController : ControllerBase
    {
        private readonly IRiderService _riderService;

        public RidersController(IRiderService riderService)
        {
            _riderService = riderService;
        }

        /// <summary>
        /// Cadastra um novo entregador (Rider)
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(RiderResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateRiderRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await _riderService.CreateAsync(request);

                return StatusCode(201);
            }
            catch
            {
                return StatusCode(400, new ErrorResponse
                {
                    ErrorMessage = "Dados inválidos",
                });
            }
           
        }

        /// <summary>
        /// Faz upload da imagem da CNH (em Base64)
        /// </summary>
        [HttpPost("{riderId:guid}/upload-cnh")]
        [ProducesResponseType(typeof(UploadCnhResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UploadCnh(Guid riderId, [FromBody] UploadCnhRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var response = await _riderService.UploadCnhImageAsync(riderId, request);
                return Ok(response);
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
