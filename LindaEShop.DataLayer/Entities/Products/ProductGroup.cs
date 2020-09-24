using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

		[Display(Name = "گروه اصلی")]
		public int? ParentId { get; set; }

		[Display(Name = "نام گروه")]
		public string Title { get; set; }

		[Display(Name = "نوع گروه")]
		public GroupType GroupType { get; set; }

		public List<Product> Product { get; set; }

		[ForeignKey("ParentId")]
		public List<ProductGroup> ProductGroups { get; set; }
	}
}
