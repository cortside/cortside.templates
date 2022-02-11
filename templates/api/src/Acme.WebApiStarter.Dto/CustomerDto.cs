using System;

namespace Acme.WebApiStarter.Dto {
    public class CustomerDto : AuditableEntityDto {
        public int CustomerId { get; set; }
        public Guid CustomerResourceId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}