using LindaEShop.Core.Services.Interfaces;
using LindaEShop.DataLayer.Context;
using LindaEShop.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
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

		public OrderService(LindaContext lindaContext, IUser user)
		{
			_context = lindaContext;
			_userService = user;
		}

		public int AddOrder(int ProductId, string userNumber, int sizeId, int colorId, int quantityNumber)
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
					UserId = user.UserId,
					OrderDetails = new List<OrderDetail>()
					 {
						 new OrderDetail()
						 {
							 ProductId = product.Id,
							 Count = quantityNumber,
							 Price = product.Price,
							 SizeId=sizeId,
							 ColorId=colorId
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

				if (detail != null && detail.ColorId == colorId && detail.SizeId == sizeId)
				{
					detail.Count += quantityNumber;
					_context.OrderDetails.Update(detail);
				}
				else
				{
					detail = new OrderDetail()
					{
						OrderId = order.OrderId,
						Count = quantityNumber,
						ProductId = product.Id,
						Price = product.Price,
						SizeId = sizeId,
						ColorId = colorId
					};
					_context.OrderDetails.Add(detail);
				}
				_context.SaveChanges();
				UpdatePriceOrder(order.OrderId);
			}

			return order.OrderId;
		}

		public void DeleteDetileInvoice(int DitileId)
		{
			var orderDitile = _context.OrderDetails.Find(DitileId);
			_context.OrderDetails.Remove(orderDitile);
			_context.SaveChanges();
		}

		public bool FinalyOrder(string userName, int orderId)
		{
			int userId = _userService.GetUserIdByUserName(userName);
			var order = _context.Orders.FirstOrDefault(o => o.OrderId == orderId);

			if (order != null)
			{
				order.IsFinaly = true;
				order.FinalyDate = DateTime.Now;

				_context.Orders.Update(order);
				_context.SaveChanges();

				return true;
			}

			return false;
		}

		public Order GetOrderForuserPanel(string userNumber)
		{
			int userId = _userService.GetUserIdByUserName(userNumber);

			return _context.Orders.Include(c => c.OrderDetails).ThenInclude(op => op.Product)
				.ThenInclude(dc => dc.SizeToProducts).ThenInclude(dcn => dcn.Size)
				.Include(d => d.OrderDetails).ThenInclude(d => d.Color)
				.FirstOrDefault(o => o.UserId == userId && o.IsFinaly == false);
		}

		public int GetOrderIdByUserName(string userName)
		{
			int userId = _userService.GetUserByNumber(userName).UserId;

			return _context.Orders.FirstOrDefault(i => i.UserId == userId && i.IsFinaly == false).OrderId;
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
