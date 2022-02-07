using System;
using System.Threading.Tasks;
using Acme.WebApiStarter.Dto;

namespace Acme.WebApiStarter.DomainService {
    public interface IOrderService {
        Task<OrderDto> CreateOrderAsync(OrderDto dto);
        //Task DeleteOrderAsync(int widgetId);
        Task<OrderDto> GetOrderAsync(Guid id);
        //Task<List<OrderDto>> GetOrdersAsync();
        //Task PublishOrderStateChangedEventAsync(int id);
        //Task<OrderDto> UpdateOrderAsync(OrderDto dto);
    }
}
