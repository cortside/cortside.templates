using System.Collections.Generic;
using System.Threading.Tasks;
using Acme.WebApiStarter.Dto;

namespace Acme.WebApiStarter.DomainService {
    public interface ICustomerService {
        Task<CustomerDto> CreateWidgetAsync(CustomerDto dto);
        Task<CustomerDto> GetWidgetAsync(int widgetId);
        Task<List<CustomerDto>> GetWidgetsAsync();
        Task<CustomerDto> UpdateWidgetAsync(CustomerDto dto);
        Task<CustomerDto> DeleteWidgetAsync(int widgetId);
        Task PublishWidgetStateChangedEventAsync(int id);
    }
}
