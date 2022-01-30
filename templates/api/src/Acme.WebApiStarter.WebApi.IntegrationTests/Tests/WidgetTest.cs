using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Acme.WebApiStarter.WebApi.Models.Requests;
using FluentAssertions;
using Newtonsoft.Json;
using Xunit;

namespace Acme.WebApiStarter.WebApi.IntegrationTests.Tests {
    public class WidgetTest : IClassFixture<IntegrationTestFactory<Startup>> {
        private readonly IntegrationTestFactory<Startup> fixture;
        private readonly HttpClient testServerClient;

        public WidgetTest(IntegrationTestFactory<Startup> fixture) {
            this.fixture = fixture;
            testServerClient = fixture.CreateAuthorizedClient("api");
        }

        [Fact]
        public async Task ShouldCreateWidgetAsync() {
            //arrange
            var request = new CustomerRequest() {
                FirstName = Guid.NewGuid().ToString(),
                LastName = "last",
                Email = "email"
            };

            var requestBody = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            //act
            var response = await testServerClient.PostAsync("/api/v1/widgets", requestBody).ConfigureAwait(false);

            //assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Fact]
        public async Task ShouldGetWidgetAsync() {
            //arrange
            var db = fixture.NewScopedDbContext();
            var id = db.Customers.First().CustomerId;

            //act
            var response = await testServerClient.GetAsync($"api/v1/widgets/{id}").ConfigureAwait(false);

            //assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
