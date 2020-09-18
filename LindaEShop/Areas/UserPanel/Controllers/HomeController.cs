using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using LindaEShop.Core;
using LindaEShop.Core.DTOs;
using LindaEShop.Core.Security;
using LindaEShop.Core.Services.Interfaces;
using LindaEShop.DataLayer.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace TopLearn.Web.Areas.UserPanel.Controllers
{
	[Area("UserPanel")]
	[Authorize]
	//[PermissionChecker(1)]
	public class HomeController : Controller
	{
		private IOrder _orderService;
		private IUser _userService;

		public HomeController(IOrder _order, IUser _user)
		{
			_orderService = _order;
			_userService = _user;
		}

		public IActionResult Index()
		{
			Order order;
			string userName = User.FindFirst(ClaimTypes.Email)?.Value;
			order = _orderService.GetOrderForuserPanel(userName);

			return View(order);
		}

		public IActionResult ContinueTheBuyingProcess(int id) //---id = orderId
		{

			ViewData["UserAddress"] = _userService.GetAllUserAddress();

			return View(new ContinueTheuyingProcessViewModel
			{
				OrderId = id,
			});
		}

		[HttpPost]
		public IActionResult ContinueTheBuyingProcess(ContinueTheuyingProcessViewModel continueTheuying) //---id = orderId
		{
			ViewData["UserAddress"] = _userService.GetAllUserAddress();

			if (!ModelState.IsValid || continueTheuying.BankPort == 0)
			{
				ModelState.AddModelError("BankPort", "لطفا یک درگاه را انتخاب کنید");

				return View(continueTheuying);
			}

			if (!ModelState.IsValid || continueTheuying.AddressId == 0)
			{
				ModelState.AddModelError("AddressId", "لطفا یک آدرس انتخاب کنید یا آدرس جدیدی وارد کنید");

				return View(continueTheuying);
			}

			_orderService.EditOrderdescription(continueTheuying.OrderId,continueTheuying.Description);

			return RedirectToAction("FinalyOrder", new { id = continueTheuying.OrderId, address = continueTheuying.AddressId });
		}

		public IActionResult AddNewUserAddress(int id)//---id = orderId
		{
			ViewData["orderId"] = id;

			return View();
		}

		[HttpPost]
		public IActionResult AddNewUserAddress(UserAddress userAddress, int orderId)//---id = orderId
		{
			if (ModelState.IsValid)
			{
				string userNumber = User.FindFirst(ClaimTypes.Email)?.Value;
				_userService.AddUserAddress(userAddress, userNumber);

				return RedirectToAction("ContinueTheBuyingProcess", new { id = orderId });
			}

			ViewData["orderId"] = orderId;

			return View(userAddress);
		}

		public IActionResult FinalyOrder(int id, int address)//---id = orderId
		{
			//TO DO پرداخت اینترنتی

			string userNumber = User.FindFirst(ClaimTypes.Email)?.Value;

			if (_orderService.FinalyOrder(userNumber, id, address))
			{
				//TO DO و کد پیگیری و اینا ارسال به صفحه ای برای نمایش تکمیل شدن خرید
				//TO DO ارسال اس ام اس  شامل کد پیگیری و از اینجور چیزا

				Sms.SendSms(userNumber, "کاربر گرامی خرید شما با موفقیت انجام شد .");

				return Redirect("/");
			}
			else
			{
				return BadRequest();
			}

		}

		public IActionResult DeleteDetileInvoice(int id)
		{
			_orderService.DeleteDetileInvoice(id);

			return Redirect("/UserPanel");
		}

	}
}