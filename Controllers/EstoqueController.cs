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
            var products = await _productService.GetAllProducts();

            return Ok(products);
        }

        [HttpGet("{uuid:guid}")]
        public async Task<IActionResult> GetById(Guid uuid)
        {
            var product = await _productService.GetProductById(uuid);
            if (product == null)
            {
                return NotFound();
            }
            
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] ProductDto productDto)
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

        [HttpPut("{uuid:guid}")]
        public async Task<IActionResult> Update(Guid uuid, [FromBody] ProductDto productDto)
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

        [HttpDelete("{uuid:guid}")]
        public async Task<IActionResult> Delete(Guid uuid)
        {
            var result = await _productService.DeleteProduct(uuid);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}