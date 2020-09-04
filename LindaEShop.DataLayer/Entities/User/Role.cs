using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LindaEShop.DataLayer.Entities
{
	public class Role
	{
        [Key]
        public int RoleId { get; set; }

        [Display(Name = "عنوان نقش")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string RoleTitle { get; set; }

		public List<User> Users { get; set; }
	}
}
