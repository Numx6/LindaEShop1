using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LindaEShop.DataLayer.Entities
{
	public class User
	{
		public User()
		{

		}

		[Key]
		public int UserId { get; set; }

		[Display(Name = "شماره موبایل")]
		[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
		[MaxLength(200)]
		public string Number { get; set; }

		[Display(Name = "نام و نام خانوادگی")]
		[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
		[MaxLength(300, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
		public string Name { get; set; }

		[Display(Name = "کلمه عبور")]
		[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
		[MaxLength(200)]
		public string Password { get; set; }
		[MaxLength(200)]
		public string ActivCode { get; set; }
		public bool IsActive { get; set; }
		public DateTime CreateDate { get; set; }
		public int RoleId { get; set; }

		[ForeignKey("RoleId")]
		public Role Role { get; set; }
		public List<Order> Orders { get; set; }
		public List<UserAddress> userAddresses { get; set; }
	}
}
