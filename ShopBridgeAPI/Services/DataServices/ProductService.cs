using AutoMapper;
using FluentValidation;
using ShopBridgeAPI.Exceptions;
using ShopBridgeAPI.Models;
using ShopBridgeAPI.Models.Dtos;
using ShopBridgeAPI.Services.DataServices.Interfaces;
using ShopBridgeAPI.Services.ValidationService.Interfaces;

namespace ShopBridgeAPI.Services.DataServices
{
    public class ProductService : IProductService
    {
        private readonly ILogger<ProductService> _logger;
        private readonly ShopBridgeContext _dbContext;
        private readonly IGetProductService _getProductService;
        private readonly ICategoryService _categoryService;
        private readonly IProductValidationService _productValidationService;
        private readonly IMapper _mapper;

        public ProductService(ShopBridgeContext dbContext, IGetProductService getProductService, 
            ICategoryService categoryService, IProductValidationService productValidationService, 
            ILogger<ProductService> logger, IMapper mapper)
        {
            _dbContext = dbContext;
            _categoryService = categoryService;
            _getProductService = getProductService;
            _productValidationService = productValidationService;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task AddAsync(ProductRequestDto requestDto)
        {
            var validationResult = await _productValidationService.ValidateAsync(requestDto);
            if (!validationResult.IsValid) 
            {
                throw new ValidationException(validationResult.Errors);
            }

            var product = _mapper.Map<Product>(requestDto);
            product.CategoryId = (await _categoryService.GetByNameAsync(requestDto.CategoryName)).Id;
            await _dbContext.AddAsync(product);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(int productId, ProductRequestDto requestDto)
        {
            var validationResult = await _productValidationService.ValidateAsync(requestDto);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            //var existingProduct = await _dbContext.Products.FindAsync(productId);
            var existingProduct = _mapper.Map<Product>(await _getProductService.GetByIdAsync(productId));
            if (existingProduct == null)
            {
                throw new EntityNotFoundException(string.Format(ErrorMessages.EntityNotFoundException, nameof(Product), nameof(Product.Id)));
            }

            existingProduct.Name = requestDto.ProductName;
            existingProduct.Description = requestDto.Description;
            existingProduct.Price = requestDto.Price;
            existingProduct.CategoryId = (await _categoryService.GetByNameAsync(requestDto.CategoryName)).Id;
            _dbContext.Products.Update(existingProduct);
            await _dbContext.SaveChangesAsync();
        }

        private bool IsValidCategory(Product product)
        {
            if (_categoryService.GetByIdAsync(product.CategoryId) == null)
            {
                throw new EntityNotFoundException(string.Format(ErrorMessages.EntityNotFoundException, nameof(Category), nameof(Product.CategoryId)));
            }
            return true;
        }

        public async Task DeleteAsync(int productId)
        {
            var product = await _dbContext.Products.FindAsync(productId);

            if (product == null)
            {
                throw new EntityNotFoundException(string.Format(ErrorMessages.EntityNotFoundException, nameof(Product), nameof(Product.Id)));
            }

            _dbContext.Products.Remove(product);
            await _dbContext.SaveChangesAsync();
        }


    }
}
