using System.Threading.Tasks;
using Acme.WebApiStarter.Domain;

namespace Acme.WebApiStarter.Data.Repositories {
    public interface IOrderRepository : IRepository<Order> {
        Order Add(Order order);

        void Update(Order order);

        Task<Order> GetAsync(int orderId);
    }
}
