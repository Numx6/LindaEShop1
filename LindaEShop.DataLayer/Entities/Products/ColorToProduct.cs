using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LindaEShop.DataLayer.Entities
{
	public class ColorToProduct
	{
		public ColorToProduct()
		{

		}

		#region prop

		[Key]
		public int CP_Id { get; set; }
		public int ColorId { get; set; }
		public int ProductId { get; set; }
		#endregion

		#region Relations

		public virtual Color Color { get; set; }
		public virtual Product Product { get; set; }
		#endregion
	}
}
