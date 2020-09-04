using LindaEShop.Core.Services.Interfaces;
using LindaEShop.DataLayer.Context;
using LindaEShop.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LindaEShop.Core.Services
{
	public class OrderService : IOrder
	{
		private LindaContext _context;
		private IUser _userService;
		public OrderService(LindaContext lindaContext,IUser user)
		{
			_context = lindaContext;
			_userService = user;
		}
		public int AddOrder(int ProductId,string userNumber)
		{
			User user = _userService.GetUserByNumber(userNumber);
			Order order = _context.Orders.FirstOrDefault(p => !p.IsFinaly);
			Product product = _context.Products.Find(ProductId);
			if (order == null)
			{
				order = new Order()
				{
					IsFinaly = false,
					CreateDate = DateTime.Now,
					OrderSum = product.Price,
					UserId=user.UserId,
					OrderDetails = new List<OrderDetail>()
					 {
						 new OrderDetail()
						 {
							 ProductId = product.Id,
							 Count = 1,
							 Price = product.Price,
						 }
					 }
				};

				_context.Orders.Add(order);
				_context.SaveChanges();
			}
			else
			{
				OrderDetail detail = _context.OrderDetails
					.FirstOrDefault(o => o.OrderId == order.OrderId && o.ProductId == product.Id);
				if (detail != null)
				{
					detail.Count += 1;
					_context.OrderDetails.Update(detail);
				}
				else
				{
					detail = new OrderDetail()
					{
						OrderId = order.OrderId,
						Count = 1,
						ProductId = product.Id,
						Price = product.Price
					};
					_context.OrderDetails.Add(detail);
				}
				_context.SaveChanges();
				UpdatePriceOrder(order.OrderId);
			}

			return order.OrderId;
		}

		public void UpdatePriceOrder(int orderId)
		{
			Order order = _context.Orders.Find(orderId);
			order.OrderSum = _context.OrderDetails.Where(d => d.OrderId == orderId)
				.Sum(s => s.Price);
			_context.Orders.Update(order);
			_context.SaveChanges();
		}
	}
}
