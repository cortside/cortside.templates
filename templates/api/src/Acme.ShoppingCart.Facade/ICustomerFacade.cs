using System;
using System.Threading.Tasks;
using Acme.ShoppingCart.Dto.Input;
using Acme.ShoppingCart.Dto.Output;
using Acme.ShoppingCart.Dto.Search;
using Cortside.AspNetCore.Common.Paging;

namespace Acme.ShoppingCart.Facade {
    public interface ICustomerFacade {
        Task<CustomerDto> CreateCustomerAsync(UpdateCustomerDto dto);
        Task<CustomerDto> GetCustomerAsync(Guid resourceId);
        Task<PagedList<CustomerDto>> SearchCustomersAsync(CustomerSearchDto search);
        Task<CustomerDto> UpdateCustomerAsync(Guid resourceId, UpdateCustomerDto dto);
        Task PublishCustomerStateChangedEventAsync(Guid resourceId);
    }
}
