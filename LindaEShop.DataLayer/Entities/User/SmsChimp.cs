using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LindaEShop.DataLayer.Entities
{
	[Table("SmsChimps", Schema = "User")]
	public class SmsChimp
	{
		[Key]
		public int Id { get; set; }

		[MaxLength(25)]
		public string Number { get; set; }

		public DateTime CreatDate { get; set; }
	}
}
