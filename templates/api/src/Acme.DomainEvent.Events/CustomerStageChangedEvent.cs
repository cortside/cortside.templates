using System;

namespace Acme.DomainEvent.Events {
    public class CustomerStageChangedEvent {
        public int CustomerId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public DateTime Timestamp { get; set; }
    }
}
