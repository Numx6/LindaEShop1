using System;
using System.Collections.Generic;
using System.Text;

namespace LindaEShop.Core
{
	public static class Sms
	{
		public static bool SendSms(string number, string massage)
		{
			try
			{
				Kavenegar.KavenegarApi api = new Kavenegar.KavenegarApi("7864554F796645716F67752F61753571694E7461437534774A4D6A615653594D545744544D696D674465453D");
				var result = api.Send("10005000020020", number, massage);
				return true;
			}
			catch (Kavenegar.Core.Exceptions.ApiException ex)
			{
				// در صورتی که خروجی وب سرویس 200 نباشد این خطارخ می دهد.
				Console.Write("Message : " + ex.Message);
				return false;
			}
			catch (Kavenegar.Core.Exceptions.HttpException ex)
			{
				// در زمانی که مشکلی در برقرای ارتباط با وب سرویس وجود داشته باشد این خطا رخ می دهد
				Console.Write("Message : " + ex.Message);
				return false;
			}
		}
	}
}
