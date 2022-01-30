using Xunit;

namespace Acme.WebApiStarter.Domain.Tests {
    public class WidgetTest {
        [Fact]
        public void Foo() {
            // Arrange
            var widget = new Customer();

            // Act
            widget.CustomerId = 1;

            // Assert
            Assert.Equal(1, widget.CustomerId);
        }
    }
}
