using System;
using System.ComponentModel.DataAnnotations;

namespace Acme.ShoppingCart.WebApi.Models.Requests {
    /// <summary>
    /// Request information to create a new customer
    /// </summary>
    public class UpdateOrderCustomerModel {
        /// <summary>
        /// Gets or sets the customer resource identifier.
        /// </summary>
        /// <value>
        /// The customer resource identifier.
        /// </value>
        public Guid? CustomerResourceId { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        [Required]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        [Required]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        [Required]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        public DateOnly? BirthDate { get; set; }
    }
}
