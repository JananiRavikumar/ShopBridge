using ShopBridgeAPI.Models;

namespace ShopBridgeAPI.Services.DataServices.Interfaces
{
    public interface ICategoryService
    {
        Task<Category?> GetByIdAsync(int categoryId);

        Task<Category?> GetByNameAsync(string categoryName);
    }
}
