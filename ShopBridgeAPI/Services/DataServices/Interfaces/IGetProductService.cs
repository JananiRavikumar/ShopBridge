using ShopBridgeAPI.Models;
using ShopBridgeAPI.Models.Dtos;

namespace ShopBridgeAPI.Services.DataServices.Interfaces
{
    public interface IGetProductService
    {
        Task<FetchProductResponseDto> GetByIdAsync(int productId);

        Task<IEnumerable<FetchProductResponseDto>> GetAsync(FetchProductRequestDto requestDto);
    }
}
