using LindaEShop.DataLayer;
using LindaEShop.DataLayer.Entities;
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

	public class ShowOrderForUserPanelViewModel
	{
		public Order Order { get; set; }

		#region address

		public string Provinc { get; set; }
		public string City { get; set; }
		public string Address { get; set; }
		public string addressNo { get; set; }

		#endregion

		#region user

		public string Name { get; set; }
		public string userName { get; set; }
		public string RoleName { get; set; }

		#endregion
	}
}
