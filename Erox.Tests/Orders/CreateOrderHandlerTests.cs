using Xunit;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Erox.Application.Orders.CommandHandler;
using Erox.Application.Orders.Command;
using Erox.DataAccess;
using Erox.Domain.Aggregates.OrderAggregate;

public class CreateOrderHandlerTests
{
    [Fact]
    public async Task Handle_ShouldCreateOrder_WhenValidRequest()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: "OrderTestDb")
            .Options;

        await using var context = new DataContext(options);
        var handler = new CreateOrderHandler(context);

        var command = new CreateOrder
        {
            UserId = Guid.NewGuid(),
            PaymentMethod = "Card",
            ShippingMethod = "Courier",
            Address = "Chisinau, str. Stefan cel Mare 10",
            Status = "Pending",
            Sum = 1999,
            Items = new[]
            {
                 new CreateOrderItem
                 {
                     ProductId = Guid.NewGuid(),
                     SizeId = Guid.NewGuid(),
                     Quantity = 2
                 }
             }

        };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsError.Should().BeFalse();
        var order = await context.Orders.Include(o => o.Items).FirstOrDefaultAsync();
        order.Should().NotBeNull();
        order!.UserId.Should().Be(command.UserId);
        order.Items.Should().HaveCount(1);
        order.Sum.Should().Be(1999);
    }
}
