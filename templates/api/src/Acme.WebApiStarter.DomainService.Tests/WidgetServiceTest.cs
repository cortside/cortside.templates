using System;
using System.Linq;
using System.Threading.Tasks;
using Acme.WebApiStarter.Data;
using Acme.WebApiStarter.Dto;
using Cortside.DomainEvent;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Xunit;
using Xunit.Abstractions;

namespace Acme.WebApiStarter.DomainService.Tests {
    public class WidgetServiceTest : DomainServiceTest<ICustomerService> {
        private readonly DatabaseContext databaseContext;
        private readonly Mock<IDomainEventPublisher> domainEventPublisherMock;
        private readonly ITestOutputHelper testOutputHelper;

        public WidgetServiceTest(ITestOutputHelper testOutputHelper) : base() {
            databaseContext = GetDatabaseContext();
            domainEventPublisherMock = testFixture.Mock<IDomainEventPublisher>();
            this.testOutputHelper = testOutputHelper;
        }

        [Fact]
        public async Task ShouldCreateWidget() {
            // Arrange
            var dto = new CustomerDto() {
                FirstName = Guid.NewGuid().ToString()
            };

            var publisher = new Mock<IDomainEventOutboxPublisher>();
            var service = new CustomerService(databaseContext, publisher.Object, NullLogger<CustomerService>.Instance);

            // Act
            await service.CreateCustomerAsync(dto);

            // Assert
            Assert.True(databaseContext.Customers.Any(x => x.FirstName == dto.FirstName));
        }
    }
}
