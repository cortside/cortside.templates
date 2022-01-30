#pragma warning disable CS1591

namespace Acme.WebApiStarter.WebApi.Models.Requests {
    /// <summary>
    /// Represents a single loan
    /// </summary>
    public class CustomerRequest {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
