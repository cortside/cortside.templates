using System;
using System.Threading.Tasks;
using Acme.WebApiStarter.Domain;

namespace Acme.WebApiStarter.Data.Repositories {
    public interface IOrderRepository : IRepository<Order> {
        Task<Order> AddAsync(Order order);

        Task<Order> UpdateAsync(Order order);

        Task<Order> GetAsync(Guid id);
    }
}
