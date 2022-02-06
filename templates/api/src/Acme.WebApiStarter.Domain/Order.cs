using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Acme.WebApiStarter.Domain {
    [Table("Order")]
    public class Order : AuditableEntity {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        public OrderStatus Status { get; set; }

        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }
        [ForeignKey("AddressId")]
        public Address Address { get; set; }

        public List<OrderItem> Items { get; set; }
    }
}
