using System;
using System.Collections.Generic;
using Acme.WebApiStarter.Domain;

namespace Acme.WebApiStarter.Dto {
    public class OrderDto : AuditableEntityDto {
        public int OrderId { get; set; }
        public Guid OrderResourceId { get; set; }

        public OrderStatus Status { get; set; }
        public CustomerDto Customer { get; set; }
        public AddressDto Address { get; set; }
        public List<OrderItemDto> Items { get; set; }
    }
}
