using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ShopBridgeAPI.Exceptions;
using ShopBridgeAPI.Models;
using ShopBridgeAPI.Models.Dtos;
using ShopBridgeAPI.Services.DataServices.Interfaces;

namespace ShopBridgeAPI.Controllers
{
    [ApiController]
    //[Authorize]
    [Route("api/Product")]
    public class ProductController : ControllerBase
    {
        private readonly IGetProductService _getProductService;
        private readonly IProductService _productService;
        private readonly ILogger<ProductController> _logger;

        public ProductController(IGetProductService getProductService, IProductService productService, ILogger<ProductController> logger)
        {
            _productService = productService;
            _getProductService = getProductService;
            _logger = logger;
        }

        // Add product
        //[Authorize(Policy = "CreateAccess")]
        [HttpPost]
        public async Task<ActionResult> PostAsync(ProductRequestDto product)
        {
            try
            {
                await _productService.AddAsync(product);
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (ValidationException ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status422UnprocessableEntity, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        // Fetch product by Id
        //[Authorize(Roles = "ProductUser")]
        [HttpGet("{productId:int}")]
        [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetAsync(int productId)
        {
            try
            {
                if (productId == 0)
                {
                    return BadRequest(ErrorMessages.InvalidProductId);
                }

                var result = await _getProductService.GetByIdAsync(productId);
                return Ok(result);
            }
            catch (EntityNotFoundException ex)
            {
                _logger.LogError(ex.Message);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        //// Add product
        //[Authorize(Policy = "GetAccess")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<FetchProductResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetAsync([FromQuery]FetchProductRequestDto requestDto)
        {
            try
            {
                _logger.LogInformation($"Fetch Products request: {requestDto}");
                var result = await _getProductService.GetAsync(requestDto);

                _logger.LogInformation($"Fetched Products: {JsonConvert.SerializeObject(requestDto)}");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        //[Authorize(Policy = "UpdateAccess")]
        [HttpPut("{productId:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> PutAsync(int productId, ProductRequestDto requestDto)
        {
            try
            {
                _logger.LogInformation($"Update Product request for product {productId}: {requestDto}");
                await _productService.UpdateAsync(productId, requestDto);

                _logger.LogInformation($"Update Product completed");
                return NoContent();
            }
            catch(ValidationException ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status422UnprocessableEntity, ex.Message);
            }
            catch (EntityNotFoundException ex)
            {
                _logger.LogError(ex.Message);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        //[Authorize(Policy = "DeleteAccess")]
        [HttpDelete("{productId:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteAsync(int productId)
        {
            try
            {
                if (productId == 0)
                {
                    return BadRequest(ErrorMessages.InvalidProductId);
                }

                await _productService.DeleteAsync(productId);
                return NoContent();
            }
            catch (EntityNotFoundException ex)
            {
                _logger.LogError(ex.Message);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
