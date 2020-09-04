using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LindaEShop.DataLayer.Entities
{
	public class Color
	{
		public Color()
		{

		}

		#region prop

		[Key]
		public int ColorId { get; set; }

		[Display(Name = "رنگ محصول")]
		[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
		[MaxLength(350, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
		public string ColorName { get; set; }

		#endregion

		#region Relations
		public virtual List<ColorToProduct> ColorToProducts { get; set; }
		#endregion
	}
}
