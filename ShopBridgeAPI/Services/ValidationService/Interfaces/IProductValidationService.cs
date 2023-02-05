using FluentValidation.Results;
using ShopBridgeAPI.Models.Dtos;

namespace ShopBridgeAPI.Services.ValidationService.Interfaces
{
    public interface IProductValidationService
    {
        Task<ValidationResult> ValidateAsync(ProductRequestDto product);
    }
}
