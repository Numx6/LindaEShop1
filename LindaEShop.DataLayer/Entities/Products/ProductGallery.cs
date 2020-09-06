using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LindaEShop.DataLayer.Entities
{
	[Table("productGalleries", Schema = "Product")]
	public class ProductGallery
	{
		public ProductGallery()
		{

		}

		#region prop

		[Key]
		public int GalleryId { get; set; }

		[MaxLength(450)]
		[Display(Name = "تصویر ")]
		[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
		public string ImageName { get; set; }

		public int ProductId { get; set; }

		[Display(Name = "تاریخ ایجاد")]
		[DisplayFormat(DataFormatString = "{0: yyyy/MM/dd}")]
		public DateTime CreatDate { get; set; }

		#endregion

		#region Relations
		[ForeignKey("ProductId")]
		public Product Product { get; set; }

		#endregion
	}
}
