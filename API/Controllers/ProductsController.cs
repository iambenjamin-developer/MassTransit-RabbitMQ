using API.Producers;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly QueueProducerService _queueProducerService;
        private readonly ILogger<ProductsController> _logger;
        private readonly IPublishEndpoint _publisher;

        public ProductsController(QueueProducerService queueProducerService, ILogger<ProductsController> logger, IPublishEndpoint publisher)
        {
            _queueProducerService = queueProducerService;
            _logger = logger;
            _publisher = publisher;
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Product product)
        {
            await _queueProducerService.SendSubscribeProductEvent(product);

            return Ok(new { Message = "Se ha publicado correctamente.", Product = product });

        }

    }
}
