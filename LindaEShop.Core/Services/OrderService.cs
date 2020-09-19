using LindaEShop.Core.DTOs;
using LindaEShop.Core.Services.Interfaces;
using LindaEShop.DataLayer;
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
		private IProduct _productService;

		public OrderService(LindaContext lindaContext, IUser user, IProduct productService)
		{
			_context = lindaContext;
			_userService = user;
			_productService = productService;
		}

		public int AddOrder(int ProductId, string userNumber, int sizeId, int colorId, int quantityNumber)
		{
			User user = _userService.GetUserByNumber(userNumber);
			Order order = _context.Orders.FirstOrDefault(p => !p.IsFinaly);
			Product product = _context.Products.Find(ProductId);
			string colorName = _productService.GetColorNameById(colorId);
			string sizeName = _productService.GetSizeNameById(sizeId);

			if (order == null)
			{
				order = new Order()
				{
					IsFinaly = false,
					CreateDate = DateTime.Now,
					OrderSum = product.Price,
					UserId = user.UserId,
					OrderType = OrderType.TakingOrders,

					OrderDetails = new List<OrderDetail>()
					 {
						 new OrderDetail()
						 {
							 ProductId = product.Id,
							 Count = quantityNumber,
							 Price = product.Price,
							 SizeId=sizeId,
							 ColorId=colorId,
							 ColorName=colorName,
							 SizeName=sizeName
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
						ColorId = colorId,
						ColorName = colorName,
						SizeName = sizeName
					};
					_context.OrderDetails.Add(detail);
				}
				_context.SaveChanges();			
			}

			UpdatePriceOrder(order.OrderId);

			return order.OrderId;
		}

		public void DeleteDetileInvoice(int DitileId)
		{
			var orderDitile = _context.OrderDetails.Find(DitileId);
			_context.OrderDetails.Remove(orderDitile);
			_context.SaveChanges();
		}

		public void EditOrderdescription(int orderId, string description)
		{
			var user = _context.Orders.Find(orderId);
			user.Description = description;
			_context.Orders.Update(user);
			_context.SaveChanges();
		}

		public void EditOrderPackagingToTaking(int orderId)
		{
			var order = _context.Orders.Find(orderId);
			order.OrderType = OrderType.TakingOrders;
			_context.Orders.Update(order);
			_context.SaveChanges();
		}

		public void EditTakingToOrderPackaging(int orderId)
		{
			var order = _context.Orders.Find(orderId);
			order.OrderType = OrderType.OrderPackaging;
			_context.Orders.Update(order);
			_context.SaveChanges();
		}

		public bool AddAddressToOrder( int orderId, int addressId)
		{
			var order = _context.Orders.FirstOrDefault(o => o.OrderId == orderId);

			if (order != null)
			{
				order.AddressId = addressId;

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

		public List<Order> PakeagingOrdersToAdminPanel()
		{
			return _context.Orders.Include(od => od.OrderDetails).Include(ou => ou.User)
				.Where(w => w.IsFinaly == true && w.OrderType == OrderType.OrderPackaging).ToList();
		}

		public ShowOrderForUserPanelViewModel ShowOrderForUserPanel(int orderID)
		{
			var order = _context.Orders.Include(c => c.OrderDetails).ThenInclude(op => op.Product)
				.FirstOrDefault(o => o.OrderId == orderID && o.IsFinaly == true);

			var user = _context.Users.Include(r => r.Role).FirstOrDefault(i => i.UserId == order.UserId);
			var address = _context.UserAddresses.Find(order.AddressId);


			return new ShowOrderForUserPanelViewModel()
			{
				Order = order,

				Provinc = address.Province,
				City = address.City,
				Address = address.Address,
				addressNo = address.AddressNo,

				Name = user.Name,
				userName = user.Number,
				RoleName = user.Role.RoleTitle
			};
		}

		public List<Order> TakingOrdersToAdminPanel()
		{
			return _context.Orders.Include(od => od.OrderDetails).Include(ou => ou.User)
				.Where(w => w.IsFinaly == true && w.OrderType == OrderType.TakingOrders).ToList();
		}

		public void UpdatePriceOrder(int orderId)
		{
			Order order = _context.Orders.Include(od=>od.OrderDetails).FirstOrDefault(o=>o.OrderId==orderId);
			var sum = order.OrderDetails.Sum(o => o.Count * o.Price);
			order.OrderSum = sum;

			UpdateOrder(order);
		}

		public Order GetOrderByOrderId(int orderId)
		{
			return _context.Orders.Find(orderId);
		}

		public void UpdateOrder(Order order)
		{
			_context.Orders.Update(order);
			_context.SaveChanges();
		}
	}
}
