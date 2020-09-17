using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LindaEShop.DataLayer.Entities
{
	[Table("UserAddresses", Schema = "User")]
	public class UserAddress
	{
		[Key]
		public int AddressId { get; set; }

		[Display(Name = "استان")]
		[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
		[MaxLength(100)]
		public string Province { get; set; }

		[Display(Name = "شهر")]
		[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
		[MaxLength(100)]
		public string City { get; set; }

		[Display(Name = "آدرس")]
		[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
		[MaxLength(2000)]
		public string Address { get; set; }

		[Display(Name = "کد پستی")]
		[MaxLength(15)]
		public string AddressNo { get; set; }

		public DateTime CreatDate { get; set; }

		public int UserId { get; set; }

		[ForeignKey("UserId")]
		public User User { get; set; }
	}
}
