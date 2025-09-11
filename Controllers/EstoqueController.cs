using EstoqueApi.DTOs;
using EstoqueApi.Interface;
using EstoqueApi.Messaging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EstoqueApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public sealed class EstoqueController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMessageProducer _messageProducer;
        private readonly ILogger<EstoqueController> _logger;

        public EstoqueController(IProductService productService, 
                                IMessageProducer messageProducer,
                                ILogger<EstoqueController> logger)
        {
            _productService = productService;
            _messageProducer = messageProducer;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var products = await _productService.GetAllProducts();

                return Ok(products);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("{uuid:guid}")]
        public async Task<IActionResult> GetById(Guid uuid)
        {
            try
            {
                var product = await _productService.GetProductById(uuid);
                if (product == null)
                {
                    return NotFound();
                }

                return Ok(product);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] ProductDto productDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var addedProduct = await _productService.AddProduct(productDto);

                _messageProducer.SendMessage(addedProduct);

                _logger.LogInformation("Novo produto adicionado: {ProductName}", addedProduct.Name);

                return CreatedAtAction(nameof(GetById), new { uuid = addedProduct.Uuid }, addedProduct);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPut("{uuid:guid}")]
        public async Task<IActionResult> Update(Guid uuid, [FromBody] ProductDto productDto)
        {
            try
            {
                var updatedProduct = await _productService.UpdateProduct(uuid, productDto);
                if (updatedProduct == null)
                {
                    return NotFound();
                }

                _messageProducer.SendMessage(updatedProduct);

                _logger.LogInformation("Novo produto atualizado: {ProductName}", updatedProduct.Name);

                return Ok(updatedProduct);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpDelete("{uuid:guid}")]
        public async Task<IActionResult> Delete(Guid uuid)
        {
            try
            {
                var result = await _productService.DeleteProduct(uuid);
                if (!result)
                {
                    return NotFound();
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}