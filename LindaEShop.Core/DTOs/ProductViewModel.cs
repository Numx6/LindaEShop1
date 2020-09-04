using LindaEShop.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LindaEShop.Core.DTOs
{
	public class IndexProductAdminPanelViewModel{
		public List<Product> Products { get; set; }
		public int CurrentPage { get; set; }
		public int PageCount { get; set; }
	}
	public class ShowProductListItemViewModel
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string ProductCode { get; set; }
		public int Price { get; set; }
		public string ImageName { get; set; }
		public bool IsActive { get; set; }
	}
}
