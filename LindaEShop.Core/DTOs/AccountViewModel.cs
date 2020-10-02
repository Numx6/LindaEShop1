using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LindaEShop.Core.DTOs
{
	#region public

	public class RegisterViewModel
	{
		[Display(Name = "نام و نام خانوادگی")]
		[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
		[MaxLength(300, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
		public string Name { get; set; }

		[Display(Name = "شماره موبایل")]
		[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
		[MaxLength(200)]
		public string Number { get; set; }

		[Display(Name = "کلمه عبور")]
		[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
		[MaxLength(200)]
		public string Password { get; set; }

		[Display(Name = "تکرار کلمه عبور")]
		[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
		[MaxLength(200)]
		[Compare("Password", ErrorMessage = "کلمه های عبور مغایرت دارند")]
		public string RePassword { get; set; }
	}
	public class LogInViewModel
	{
		[Display(Name = "شماره موبایل")]
		[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
		[MaxLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
		public string Number { get; set; }

		[Display(Name = "کلمه عبور")]
		[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
		[MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
		public string Password { get; set; }

		[Display(Name = "مرا به خاطر بسپار")]
		public bool RememberMe { get; set; }
	}
	public class ForgotPasswordViewModel
	{
		[Display(Name = "شماره موبایل")]
		[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
		[MaxLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
		public string Number { get; set; }
	}
	public class ResetPasswordViewModel
	{
		public string ActiveCode { get; set; }

		[Display(Name = "کلمه عبور")]
		[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
		[MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
		public string Password { get; set; }

		[Display(Name = "تکرار کلمه عبور")]
		[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
		[MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
		[Compare("Password", ErrorMessage = "کلمه های عبور مغایرت دارند")]
		public string RePassword { get; set; }
	}

	#endregion

	#region admin
	public class SendSmsAdminViewModel
	{
		[Display(Name = "شماره موبایل")]
		[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
		[MaxLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
		public string Number { get; set; }
		
		[Display(Name = "متن پیام")]
		[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
		[MaxLength(300, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
		public string Text { get; set; }
	}
	#endregion
}
