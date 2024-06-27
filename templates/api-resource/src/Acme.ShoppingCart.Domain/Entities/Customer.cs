using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;
using Cortside.AspNetCore.Auditable.Entities;
using Cortside.Common.Messages;
using Cortside.Common.Messages.MessageExceptions;
using Microsoft.EntityFrameworkCore;

namespace Acme.ShoppingCart.Domain.Entities {
    [Index(nameof(CustomerResourceId), IsUnique = true)]
    [Table("Customer")]
    public class Customer : AuditableEntity {
        public Customer() {
            CustomerResourceId = Guid.NewGuid();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerId { get; private set; }

        public Guid CustomerResourceId { get; private set; } = Guid.NewGuid();

    }
}
