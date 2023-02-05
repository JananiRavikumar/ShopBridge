using FluentValidation;
using FluentValidation.Results;
using ShopBridgeAPI.Models.Dtos;
using ShopBridgeAPI.Services.DataServices.Interfaces;
using ShopBridgeAPI.Services.ValidationService.Interfaces;

namespace ShopBridgeAPI.Services.DataServices.ValidationService
{
    public class ProductValidationService: IProductValidationService
    {
        private readonly ICategoryService _categoryService;

        public ProductValidationService(ICategoryService categoryService) 
        {
            _categoryService = categoryService;

        }

        public async Task<ValidationResult> ValidateAsync(ProductRequestDto product) 
        {
            var validator = new ProductValidator(_categoryService);
            return await validator.ValidateAsync(product);
        }

        
    }

    public class ProductValidator: AbstractValidator<ProductRequestDto>
    {
        private readonly ICategoryService _categoryService;
        private readonly IGetProductService _getProductService;

        public ProductValidator(ICategoryService categoryService) 
        {
            _categoryService = categoryService;
            RuleFor(x => x.ProductName).NotEmpty().Length(10,50);
            RuleFor(x => x.Description).Length(0, 255);
            RuleFor(x => x.Price).GreaterThan(0);
            RuleFor(x => x.CategoryName).MustAsync((x, cancellation) => BeAValidCategoryAsync(x))
                .WithMessage("Please specify a valid category");
        }

        private async Task<bool> BeAValidCategoryAsync(string categoryName) =>
           (await _categoryService.GetByNameAsync(categoryName)) != null;

        private async Task<bool> ProductExistsAsync(int productId) =>
           (await _getProductService.GetByIdAsync(productId)) == null;
    }
}
