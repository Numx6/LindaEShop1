using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace LindaEShop.DataLayer
{
	public enum OrderType
	{
		[Description("دریافت سفارش")]
		TakingOrders = 1,

		[Description("بررسی سفارش")]
		OrderHeck = 2,

		[Description("بسته بندی سفارش")]
		OrderPackaging = 3,

		[Description("ارسال سفارش")]
		SendOrder = 4,

		[Description("برگشت سفارش")]
		ReturnOrder = 5
	}
	public enum GroupType
	{
		[Description("گروه اصلی")]
		Main = 1,

		[Description("گروه فرعی")]
		Sub = 2
	}

}
