using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using LindaEShop.Core.Services.Interfaces;
using LindaEShop.DataLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LindaEShop.Pages.Admin.Users
{
    [Authorize(Roles = "1")]
    public class SmsAdminModel : PageModel
    {
        IUser _userService;
		public SmsAdminModel(IUser _user)
		{
            _userService = _user;
        }
        public List<SendSmsAdmin> sendSms { get; set; }
        public void OnGet()
        {
            sendSms = _userService.GetSendSmsAdmins();
        }
    }
}
