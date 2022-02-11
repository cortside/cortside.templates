using Acme.WebApiStarter.Data;
using Cortside.DomainEvent;
using Moq;
using Xunit.Abstractions;

namespace Acme.WebApiStarter.DomainService.Tests {
    public class WebApiStartererviceTest : DomainServiceTest<ICustomerService> {
        private readonly DatabaseContext databaseContext;
        private readonly Mock<IDomainEventPublisher> domainEventPublisherMock;
        private readonly ITestOutputHelper testOutputHelper;

        public WebApiStartererviceTest(ITestOutputHelper testOutputHelper) : base() {
            databaseContext = GetDatabaseContext();
            domainEventPublisherMock = testFixture.Mock<IDomainEventPublisher>();
            this.testOutputHelper = testOutputHelper;
        }
    }
}