using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LindaEShop.Core.Services.Interfaces;
using LindaEShop.DataLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LindaEShop.Pages.Admin.Users
{
    public class IndexModel : PageModel
    {
        IUser _userService;
		public IndexModel(IUser user)
		{
            _userService = user;
		}
		public List<User> Users { get; set; }
		public void OnGet()
        {
            Users = _userService.GetAllUsers();
        }
    }
}
