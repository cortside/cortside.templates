using System;
using System.Text.RegularExpressions;
using Acme.WebApiStarter.UserClient.Models.Responses;
using Newtonsoft.Json;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;

namespace Acme.WebApiStarter.WebApi.IntegrationTests.Helpers.Mocks {
    public class UserClientWireMock : IWireMockBuilder {
        public void Configure(WireMockServer server) {
            var getUserUrl = new Regex(@"^\/api/v1/users\/([0-9A-Fa-f]{8}[-][0-9A-Fa-f]{4}[-][0-9A-Fa-f]{4}[-][0-9A-Fa-f]{4}[-][0-9A-Fa-f]{12})$");
            server
               .Given(
                   Request.Create().WithPath(p => getUserUrl.IsMatch(p)).UsingGet()
               )
               .RespondWith(
                   Response.Create()
                       .WithStatusCode(200)
                       .WithHeader("Content-Type", "application/json")
                       .WithBody(r => JsonConvert.SerializeObject(
                           new UserInfoResponse() {
                               UserId = Guid.NewGuid().ToString(),
                               FirstName = "first",
                               LastName = "last",
                               EmailAddress = "first@last.com"
                           }
                       ))
               );
        }
    }
}
