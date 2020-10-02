using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace LindaEShop.Core
{
	public static class Sms
	{
		public async static void SendSms(string number, string massage)
		{
			//Kavenegar.KavenegarApi api = new Kavenegar.KavenegarApi("7864554F796645716F67752F61753571694E7461437534774A4D6A615653594D545744544D696D674465453D");
			//var result = api.Send("10005000020020", number, massage);

			HttpClient httpClient = new HttpClient();
			string url = $"https://api.homais.com/services/messaging/sms/api/sendMessage/direct/?username=fr26040&password=52080&PortalCode=10002545&mobile={number}&message={massage}&ServerType=100";
			await httpClient.GetAsync(url);
		}
	}
}
