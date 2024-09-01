using Erox.Application.Enums;
using Erox.Application.Models;
using Erox.Application.Orders.Command;
using Erox.DataAccess;
using Erox.Domain.Aggregates.CardAggregate;
using Erox.Domain.Aggregates.OrderAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erox.Application.Orders.CommandHandler
{
    public class CreateOrderHandler : IRequestHandler<CreateOrder, OperationResult<Order>>
    {
        private readonly DataContext _ctx;
        private readonly OperationResult<Order> _result;
        public CreateOrderHandler(DataContext ctx)
        {
                _ctx = ctx;
            _result=new OperationResult<Order>();   
        }
        public async Task<OperationResult<Order>> Handle(CreateOrder request, CancellationToken cancellationToken)
        {
            try
            {
                

                // Если вишлист не найден, создаем новый
               
                    var id = Guid.NewGuid();
                    var order = new Order
                    {
                        OrderId = id,
                        UserId = request.UserId,
                        PaymenentMethod = request.PaymentMethod,
                        ShippingMethod = request.ShippingMethod,
                        CreatedDate = DateTime.Now,
                        Address = request.Address,
                        Status = request.Status,
                        Sum = request.Sum,
                        Items = request.Items.Select(s => new OrderItem 
                        {
                            OrderId = id,
                            
                            SizeId =s.SizeId,
                            Quantity=s.Quantity,
                            ProductId=s.ProductId,
                            Order = null,
                            Product = null,
                            Size = null
                        }).ToArray()  // Инициализируем коллекцию элементов
                    };
                    _ctx.Orders.Add(order);
                    await _ctx.SaveChangesAsync(cancellationToken);  // Сохраняем новый вишлист в базу данных
                

             
            }
            catch (DbUpdateException ex)
            {
                // Логируем внутреннюю ошибку для получения более детальной информации
                var innerExceptionMessage = ex.InnerException?.Message ?? ex.Message;
                _result.AddUnknownError(innerExceptionMessage);
                return _result;
            }
            catch (Exception ex)
            {
                // Общая обработка других ошибок
                _result.AddUnknownError(ex.Message);
                return _result;
            }

            return _result;
        }
    }
}
