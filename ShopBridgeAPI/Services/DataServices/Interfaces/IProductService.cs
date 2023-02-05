using ShopBridgeAPI.Models;
using ShopBridgeAPI.Models.Dtos;

namespace ShopBridgeAPI.Services.DataServices.Interfaces
{
    public interface IProductService
    {
        Task AddAsync(ProductRequestDto requestDto);

        Task UpdateAsync(int productId, ProductRequestDto requestDto);

        Task DeleteAsync(int productId);

    }
}
