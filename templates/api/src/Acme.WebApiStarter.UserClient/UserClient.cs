using System;
using System.Threading.Tasks;
using Acme.WebApiStarter.Exceptions;
using Acme.WebApiStarter.UserClient.Models.Responses;
using Cortside.Common.RestSharpClient;
using Cortside.Common.RestSharpClient.Services;
using Microsoft.Extensions.Logging;
using RestSharp;
using RestSharp.Serialization.Json;

namespace Acme.WebApiStarter.UserClient {
    public class UserClient : BaseClient, IUserClient {
        private readonly UserClientConfiguration userClientConfiguration;

        public UserClient(UserClientConfiguration userClientConfiguration, ILogger<UserClient> logger)
         : base(new JsonSerializer(), logger, userClientConfiguration.Authentication, userClientConfiguration.ServiceUrl, new InMemoryCache()) {
            this.userClientConfiguration = userClientConfiguration;
        }

        public async Task<UserInfoResponse> GetUserByIdAsync(Guid userId) {
            logger.LogInformation($"Getting User by ID: {userId}.");
            IRestRequest request = new RestRequest($"/v1/users/{userId}", Method.GET);
            try {
                var response = await GetAsync<UserInfoResponse>(request).ConfigureAwait(false);
                return response;
            } catch (Exception ex) {
                logger.LogError($"Error contacting user api to retrieve user info for {userId}.");
                throw new ExternalCommunicationFailureMessage($"Error contacting user api to retrieve user info for {userId}.", ex);
            }
        }
    }
}
