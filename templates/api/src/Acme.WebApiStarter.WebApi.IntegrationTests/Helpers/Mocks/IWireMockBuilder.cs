using WireMock.Server;

namespace Acme.WebApiStarter.WebApi.IntegrationTests.Helpers.Mocks {
    public interface IWireMockBuilder {
        public void Configure(WireMockServer server);
    }
}
