﻿using System;
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
			string userNumber = User.FindFirst(ClaimTypes.Email)?.Value;
			ViewData["UserAddress"] = _userService.GetAllUserAddress(userNumber);

			return View(new ContinueTheuyingProcessViewModel
			{
				OrderId = id,
			});
		}

		[HttpPost]
		public IActionResult ContinueTheBuyingProcess(ContinueTheuyingProcessViewModel continueTheuying) //---id = orderId
		{
			string userNumber = User.FindFirst(ClaimTypes.Email)?.Value;
			ViewData["UserAddress"] = _userService.GetAllUserAddress(userNumber);

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

			_orderService.EditOrderdescription(continueTheuying.OrderId, continueTheuying.Description);

			return RedirectToAction("FinalyOrder", new { orderId = continueTheuying.OrderId, addressId = continueTheuying.AddressId });
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

		public IActionResult FinalyOrder(int orderId, int addressId)//---id = orderId
		{
			_orderService.AddAddressToOrder(orderId, addressId);
			Order order = _orderService.GetOrderByOrderId(orderId);
			string roleId= User.FindFirst(ClaimTypes.Role)?.Value;
			
			if (order.OrderDetails == null)
			{
				return Redirect("/");
			}

			#region admin pyment

			if (roleId == "1")
			{
				return RedirectToAction("OnlinePayment", "Home", new { area = "" , id =orderId});
			}

			#endregion

			#region onlin payment

			var payment = new Zarinpal.Payment("054939ee-3bc1-11ea-9822-000c295eb8fc", order.OrderSum + 15000); //----15.000---هزینه پست
			var res = payment.PaymentRequest("فروشگاه اینترنتی لیندا", "http://lindaunderwear.ir/OnlinePayment/" + orderId, "", "");

			if (res.Result.Status == 100)
			{
				return Redirect("https://zarinpal.com/pg/StartPay/" + res.Result.Authority);
			}

			return NotFound();

			#endregion

		}

		public IActionResult DeleteDetileInvoice(int OrderId,int DetailId)
		{
			_orderService.DeleteDetileInvoice(DetailId);
			_orderService.UpdatePriceOrder(OrderId);

			return Redirect("/UserPanel");
		}

	}
}