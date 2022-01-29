using Cortside.RestSharpClient.Authenticators.OpenIDConnect;

namespace Acme.WebApiStarter.UserClient {
    public class UserClientConfiguration {
        public string ServiceUrl { get; set; }
        public TokenRequest Authentication { get; set; }
    }
}
