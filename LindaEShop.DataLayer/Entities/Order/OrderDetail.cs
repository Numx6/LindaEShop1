using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LindaEShop.DataLayer.Entities
{
	public class OrderDetail
	{
		public OrderDetail()
		{

		}

		[Key]
		public int DetailId { get; set; }
		[Required]
		public int OrderId { get; set; }
		[Required]
		public int ProductId { get; set; }
		[Required]
		public int ColorId { get; set; }
		[Required]
		public int SizeId { get; set; }
		[Required]
		public int Count { get; set; }
		[Required]
		public int Price { get; set; }
		[Display(Name = "رنگ")]
		[MaxLength(50)]
		public string ColorName { get; set; }
		[Display(Name = "سایز")]
		[MaxLength(50)]
		public string SizeName { get; set; }

		[ForeignKey("SizeId")]
		public Size Size { get; set; }

		[ForeignKey("ColorId")]
		public Color Color { get; set; }

		[ForeignKey("OrderId")]
		public Order Order { get; set; }

		[ForeignKey("ProductId")]
		public Product Product { get; set; }
	}
}
