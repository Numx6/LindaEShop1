using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LindaEShop.DataLayer.Entities
{
	public class Size
	{
		public Size()
		{

		}

		#region prop

		[Key]
		public int SizeId { get; set; }

		[Display(Name = "سایز محصول")]
		[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
		[MaxLength(350, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
		public string SizeProduct { get; set; }

		#endregion

		#region Relations
		public virtual List<SizeToProduct> SizeToProduct { get; set; }
		public List<OrderDetail> OrderDetails { get; set; }
		#endregion
	}
}
