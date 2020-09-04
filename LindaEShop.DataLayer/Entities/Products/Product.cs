using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LindaEShop.DataLayer.Entities
{
	public class Product
	{
		public Product()
		{

		}

		[Key]
		public int Id { get; set; }

		[Display(Name = "نام محصول")]
		[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
		[MaxLength(350, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
		public string Name { get; set; }
		
		[Display(Name = "کد محصول")]
		[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
		[MaxLength(150, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
		public string ProductCode { get; set; }

		[Display(Name = "توضیحات کوتاه")]
		[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
		[MaxLength(450, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
		public string ShortDescription { get; set; }

		[Display(Name = "توضیحات")]
		public string Description { get; set; }

		[Display(Name = "قیمت")]
		[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
		public int Price { get; set; }
		
		[Display(Name = "بازدید")]
		public int Visit { get; set; }

		[MaxLength(450)]
		[Display(Name = "تصویر اصلی")]
		public string ImageName { get; set; }

		[Display(Name = "تاریخ ایجاد")]
		[DisplayFormat(DataFormatString = "{0: yyyy/MM/dd}")]
		public DateTime CreatDate { get; set; }

		[Display(Name = "فعال")]
		public bool IsActive { get; set; }

		[Display(Name = "حذف شده")]
		public bool IsDelete { get; set; }
		public int ProductGroupId { get; set; }

		public ProductGroup ProductGroup { get; set; }
		public List<ColorToProduct> ColorToProducts { get; set; }
		public List<SizeToProduct> SizeToProducts { get; set; }
		public List<OrderDetail> OrderDetails { get; set; }
	}
}
