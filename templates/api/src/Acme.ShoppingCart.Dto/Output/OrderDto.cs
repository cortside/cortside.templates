using System;
using System.Collections.Generic;
using Acme.ShoppingCart.Dto.Enumerations;
using Cortside.AspNetCore.Common.Dtos;

namespace Acme.ShoppingCart.Dto.Output {
    public class OrderDto : AuditableEntityDto {
        public int OrderId { get; set; }
        public Guid OrderResourceId { get; set; }
        public OrderStatus Status { get; set; }
        public CustomerDto Customer { get; set; }
        public AddressDto Address { get; set; }
        public List<OrderItemDto> Items { get; set; }
    }
}
