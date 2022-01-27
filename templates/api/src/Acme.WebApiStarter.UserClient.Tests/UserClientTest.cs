using System;
using System.Linq;
using System.Threading.Tasks;
using Acme.WebApiStarter.UserClient;
using Acme.WebApiStarter.UserClient.Models.Responses;
using Acme.WebApiStarter.UserClient.Tests.Mock;
using Cortside.Common.RestSharpClient.Models;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;

namespace Acme.WebApiStarter.UserClient.Tests {
    public class UserClientTest {
        private readonly IUserClient userClient;
        private readonly UserClientConfiguration config;

        public UserClientTest() {
            UserWireMock userMock = new UserWireMock();
            AuthenticationTokenRequest request = new AuthenticationTokenRequest {
                Url = $"{userMock.fluentMockServer.Urls.First()}/connect/token"
            };
            config = new UserClientConfiguration { ServiceUrl = $"{userMock.fluentMockServer.Urls.First()}/api", Authentication = request };
            userClient = new UserClient(config, new Logger<UserClient>(new NullLoggerFactory()));
        }

        [Fact]
        public async Task ShouldGetUserAsync() {
            //arrange
            Guid userId = Guid.NewGuid();

            //act
            UserInfoResponse user = await userClient.GetUserByIdAsync(userId).ConfigureAwait(false);

            //assert
            user.Should().NotBeNull();
        }
    }
}
