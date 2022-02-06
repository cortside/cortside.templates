using Acme.WebApiStarter.Domain;

namespace Acme.WebApiStarter.Dto {
    public class OrderDto : AuditableEntityDto {
        public int OrderId { get; set; }

        public OrderStatus Status { get; set; }
        //public Customer Customer { get; set; }
        //public Address Address { get; set; }
        //public List<OrderItem> Items { get; set; }
    }
}
