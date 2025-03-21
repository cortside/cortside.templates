using System;
using System.Threading.Tasks;
using Acme.DomainEvent.Events;
using Acme.ShoppingCart.Data.Repositories;
using Acme.ShoppingCart.Data.Searches;
using Acme.ShoppingCart.Domain.Entities;
using Acme.ShoppingCart.Dto.Input;
using Cortside.AspNetCore.Common.Paging;
using Cortside.Common.Logging;
using Cortside.DomainEvent.EntityFramework;
using Microsoft.Extensions.Logging;

namespace Acme.ShoppingCart.DomainService {
    public class CustomerService : ICustomerService {
        private readonly IDomainEventOutboxPublisher publisher;
        private readonly ILogger<CustomerService> logger;
        private readonly ICustomerRepository customerRepository;

        public CustomerService(ICustomerRepository customerRepository, IDomainEventOutboxPublisher publisher, ILogger<CustomerService> logger) {
            this.publisher = publisher;
            this.logger = logger;
            this.customerRepository = customerRepository;
        }

        public async Task<Customer> CreateCustomerAsync(UpdateCustomerDto dto) {
            var entity = new Customer(dto.FirstName, dto.LastName, dto.Email);
            using (logger.PushProperty("CustomerResourceId", entity.CustomerResourceId)) {
                await customerRepository.AddAsync(entity);
                logger.LogInformation("Created new customer");

                var @event = new CustomerStateChangedEvent() { CustomerResourceId = entity.CustomerResourceId, Timestamp = entity.LastModifiedDate };
                await publisher.PublishAsync(@event).ConfigureAwait(false);

                return entity;
            }
        }

        public async Task<Customer> GetCustomerAsync(Guid customerResourceId) {
            var entity = await customerRepository.GetAsync(customerResourceId).ConfigureAwait(false);
            return entity;
        }

        public Task<PagedList<Customer>> SearchCustomersAsync(CustomerSearch search) {
            return customerRepository.SearchAsync(search);
        }

        public async Task<Customer> UpdateCustomerAsync(Guid resourceId, UpdateCustomerDto dto) {
            var entity = await customerRepository.GetAsync(resourceId).ConfigureAwait(false);
            using (logger.PushProperty("CustomerResourceId", entity.CustomerResourceId)) {
                entity.Update(dto.FirstName, dto.LastName, dto.Email);
                logger.LogInformation("Updated existing customer");

                var @event = new CustomerStateChangedEvent() { CustomerResourceId = entity.CustomerResourceId, Timestamp = entity.LastModifiedDate };
                await publisher.PublishAsync(@event).ConfigureAwait(false);

                return entity;
            }
        }

        public async Task PublishCustomerStateChangedEventAsync(Guid resourceId) {
            var entity = await customerRepository.GetAsync(resourceId).ConfigureAwait(false);

            var @event = new CustomerStateChangedEvent() { CustomerResourceId = entity.CustomerResourceId, Timestamp = entity.LastModifiedDate };
            await publisher.PublishAsync(@event).ConfigureAwait(false);
        }
    }
}
