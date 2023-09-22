using Common.MassTransit.Models.Products;
using MassTransit;

namespace API.Producers
{
    public class QueueProducerService
    {
        readonly IBus _bus;

        public QueueProducerService(IBus bus)
        {
            _bus = bus;
        }

        public async Task<bool> SendSubscribeProductEvent(Product product)
        {
            try
            {
                await _bus.Publish(GenerateSubscribeProductMessage(product));

                return true;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }


        #region private Methods

        private ProductMessage GenerateSubscribeProductMessage(Product product)
        {
            var productMessage = new ProductMessage()
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Stock = product.Stock,
                DueDate = product.DueDate,
                Enabled = product.Enabled,
                Aliases = product.Aliases,
            };

            return productMessage;
        }

        #endregion

    }
}
