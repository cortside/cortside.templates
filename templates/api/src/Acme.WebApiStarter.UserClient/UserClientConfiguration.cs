using Cortside.Common.RestSharpClient.Models;

namespace Acme.WebApiStarter.UserClient {
    public class UserClientConfiguration {
        public string ServiceUrl { get; set; }
        public AuthenticationTokenRequest Authentication { get; set; }
    }
}
