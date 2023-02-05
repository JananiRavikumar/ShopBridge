using Microsoft.EntityFrameworkCore;
using ShopBridgeAPI.Models;
using ShopBridgeAPI.Services.DataServices.Interfaces;

namespace ShopBridgeAPI.Services.DataServices
{
    public class CategoryService: ICategoryService
    {
        private readonly ILogger<CategoryService> _logger;
        private readonly ShopBridgeContext _dbContext;

        public CategoryService(ShopBridgeContext dbContext, ILogger<CategoryService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<Category?> GetByIdAsync(int categoryId) 
        {
            return await _dbContext.Categories.FindAsync(categoryId);
        }

        public async Task<Category?> GetByNameAsync(string categoryName)
        {
            return await _dbContext.Categories.SingleOrDefaultAsync(x => x.Name == categoryName);
        }
    }
}
