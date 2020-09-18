using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LindaEShop.Core.DTOs
{
	public class ContinueTheuyingProcessViewModel
	{
		public int OrderId { get; set; }

		[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
		public int AddressId { get; set; }

		[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
		public int BankPort { get; set; }

		[Display(Name = "توضیحات")]
		[MaxLength(1500)]
		public string Description { get; set; }
	}
}
