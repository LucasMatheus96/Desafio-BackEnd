using Mottu.RentalApp.Application.DTOs.Requests;
using Mottu.RentalApp.Application.DTOs.Responses;

namespace Mottu.RentalApp.Application.Interfaces.Services
{
    public interface IRiderService
    {
        /// <summary>
        /// Cadastra um novo entregador (Rider) com os dados informados.
        /// </summary>
        /// <param name="createRiderRequest">Dados para criação do Rider</param>
        /// <returns>O Rider criado</returns>
        Task<RiderResponse> CreateAsync(CreateRiderRequest createRiderRequest);

        /// <summary>
        /// Faz upload da imagem da CNH do Rider (base64) e atualiza o registro.
        /// </summary>
        /// <param name="riderId">Identificador do Rider</param>
        /// <param name="uploadCnhRequest">Imagem CNH em base64</param>
        /// <returns>Informações do upload (URL, data/hora)</returns>
        Task<UploadCnhResponse> UploadCnhImageAsync(Guid riderId, UploadCnhRequest uploadCnhRequest);
    }
}
