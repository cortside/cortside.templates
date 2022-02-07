using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Acme.WebApiStarter.Dto;

namespace Acme.WebApiStarter.DomainService {
    public interface ICustomerService {
        Task<CustomerDto> CreateCustomerAsync(CustomerDto dto);
        Task<CustomerDto> GetCustomerAsync(Guid customerResourceId);
        Task<List<CustomerDto>> GetCustomersAsync();
        Task<CustomerDto> UpdateCustomerAsync(CustomerDto dto);
        Task<CustomerDto> DeleteCustomerAsync(int widgetId);
        Task PublishCustomerStateChangedEventAsync(int id);
    }
}
