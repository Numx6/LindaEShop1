using LindaEShop.Core.DTOs;
using LindaEShop.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LindaEShop.Core.Services.Interfaces
{
	public interface IOrder
	{
		int AddOrder(int ProductId, string userNumber, int sizeId, int colorId, int quantityNumber);
		void UpdatePriceOrder(int orderId);
		Order GetOrderForuserPanel(string userNumber);
		int GetOrderIdByUserName(string userName);
		bool FinalyOrder(string userName, int orderId,int addressId);
		void DeleteDetileInvoice(int DitileId);
		void EditOrderdescription(int orderId,string description);

		#region order for adminPanel

		List<Order> TakingOrdersToAdminPanel();
		List<Order> PakeagingOrdersToAdminPanel();
		void EditTakingToOrderPackaging(int orderId);
		void EditOrderPackagingToTaking(int orderId);

		public ShowOrderForUserPanelViewModel ShowOrderForUserPanel(int orderID);

		#endregion
	}
}
