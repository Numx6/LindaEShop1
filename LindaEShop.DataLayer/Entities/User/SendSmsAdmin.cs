using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LindaEShop.DataLayer.Entities
{
	[Table("SendSmsAdmins", Schema = "User")]
	public class SendSmsAdmin
	{
		[Key]
		public int Id { get; set; }

		[Display(Name = "شماره موبایل")]
		[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
		[MaxLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
		public string Number { get; set; }

		[Display(Name = "متن پیام")]
		[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
		[MaxLength(300, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
		public string Text { get; set; }
		public DateTime CreatDate { get; set; }
	}
}
