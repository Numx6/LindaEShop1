using System;
using System.Collections.Generic;
using System.Text;

namespace LindaEShop.Core.Services.Interfaces
{
	public interface IOrder
	{
		int AddOrder(int ProductId, string userNumber, int sizeId, int colorId, int quantityNumber);
		void UpdatePriceOrder(int orderId);
	}
}
