using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using LindaEShop.Core;
using LindaEShop.Core.DTOs;
using LindaEShop.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LindaEShop.Pages.Admin.Users
{
	[Authorize(Roles = "1")]
	public class SendSmsAdminModel : PageModel
    {
		IUser _userService;
		public SendSmsAdminModel(IUser _user)
		{
			_userService = _user;
		}

		[BindProperty]
		public SendSmsAdminViewModel sendSms { get; set; }
		public void OnGet()
        {
			ViewData["IsSuccess"] = 0;
		}
		public IActionResult OnPost()
		{
			if (ModelState.IsValid)
			{
				Sms.SendSms(sendSms.Number.Trim(),sendSms.Text.Trim());
				_userService.AddSendSms(sendSms);
				ViewData["IsSuccess"] = 1;

				return Page();
			}

			return Page();
		}
	}
}
