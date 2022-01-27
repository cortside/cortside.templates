using Cortside.Common.Messages.MessageExceptions;

namespace Acme.WebApiStarter.Exceptions {
    public class BadRequestMessage : BadRequestResponseException {
        public BadRequestMessage() : base("Invalid arguments") {
        }

        public BadRequestMessage(string message) : base(message) {
        }
    }
}
