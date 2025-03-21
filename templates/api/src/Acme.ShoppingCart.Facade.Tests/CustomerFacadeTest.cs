using System;
using System.IO;
using System.Threading.Tasks;
using Acme.ShoppingCart.DomainService;
using Acme.ShoppingCart.TestUtilities;
using Cortside.AspNetCore.EntityFramework;
using Medallion.Threading.FileSystem;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Xunit;

namespace Acme.ShoppingCart.Facade.Tests {
    public class CustomerFacadeTest {
        [Fact]
        public async Task ShouldGetCustomerAsync() {
            // arrange
            var uow = new Mock<IUnitOfWork>();
            var customerService = new Mock<ICustomerService>();
            var facade = new CustomerFacade(new NullLogger<CustomerFacade>(), uow.Object, customerService.Object, new Mappers.CustomerMapper(new Mappers.SubjectMapper()), new FileDistributedSynchronizationProvider(new DirectoryInfo(System.IO.Path.GetTempPath())));
            var customerResourceId = Guid.NewGuid();
            customerService.Setup(x => x.GetCustomerAsync(customerResourceId)).ReturnsAsync(EntityBuilder.GetCustomerEntity());

            // act
            var customer = await facade.GetCustomerAsync(customerResourceId);

            // assert
            Assert.NotNull(customer);
        }
    }
}
