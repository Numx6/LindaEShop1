using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LindaEShop.DataLayer.Entities
{
	public class SizeToProduct
	{
		public SizeToProduct()
		{

		}

		#region prop

		[Key]
		public int SP_Id { get; set; }
		public int SizeId { get; set; }
		public int ProductId { get; set; }

		#endregion

		#region Relations

		public virtual Product Product { get; set; }
		public virtual Size Size { get; set; }

		#endregion
	}
}
