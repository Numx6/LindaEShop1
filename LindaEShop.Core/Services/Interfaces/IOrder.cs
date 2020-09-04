using System;
using System.Collections.Generic;
using System.Text;

namespace LindaEShop.Core.Services.Interfaces
{
	public interface IOrder
	{
		int AddOrder(int ProductId, string userNumber);
		void UpdatePriceOrder(int orderId);
	}
}
