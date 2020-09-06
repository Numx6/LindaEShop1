using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LindaEShop.DataLayer.Entities
{
	public class ProductGroup
	{
		public ProductGroup()
		{

		}

		[Key]
		public int Id { get; set; }
		[Display(Name = "نام گروه")]
		public string Title { get; set; }
		public List<Product> Product { get; set; }
	}
}
