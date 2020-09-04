using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LindaEShop.DataLayer.Entities
{
	public class Order
	{
		public Order()
		{

		}

		[Key]
		public int OrderId { get; set; }
		public int UserId { get; set; }

		[Required]
		public int OrderSum { get; set; }
		public bool IsFinaly { get; set; }
		[Required]
		public DateTime CreateDate { get; set; }

		[ForeignKey("UserId")]
		public User User { get; set; }
		public List<OrderDetail> OrderDetails { get; set; }
	}
}
