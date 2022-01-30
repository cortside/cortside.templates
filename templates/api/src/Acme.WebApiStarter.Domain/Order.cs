using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Acme.WebApiStarter.Domain {
    [Table("Order")]
    public class Order : AuditableEntity {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public List<OrderItem> Items { get; set; }
    }
}
