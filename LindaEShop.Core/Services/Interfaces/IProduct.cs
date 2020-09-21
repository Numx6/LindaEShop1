using LindaEShop.Core.DTOs;
using LindaEShop.DataLayer.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;

namespace LindaEShop.Core.Services.Interfaces
{
	public interface IProduct
	{
		#region ProductGroup
		List<SelectListItem> GetAllProductGroupsSelectList();
		List<ProductGroup> GetAllproductGroups();
		#endregion

		#region Product
		int AddProduct(Product product, IFormFile imgUp, List<int> selectedColor, List<int> selectedSize);
		void EditProduct(Product product, IFormFile imgUp, List<int> selectedColor, List<int> selectedSize);
		void EditProduct(Product product);
		IndexProductAdminPanelViewModel GetAllProduct(int pageId = 1, string codeFilter = "", string nameFilter = "");
		Tuple<List<ShowProductListItemViewModel>,int> GetAllProductForShop(int pageId=1,int take=0,string filter="",string orderBy="p"
			,int startPrice=0,int endPrice=0,int selectedGroup=0);
		List<Color> GetAllColors();
		List<Size> GetAllSize();
		Product GetProductyId(int id);
		List<int> GetColorOfProduct(int productId);
		List<int> GetSizeOfProduct(int productId);
		bool CodeProductIsExist(string productCode);
		Product GetProductForShow(int productId);
		string GetColorNameById(int colorId);
		string GetSizeNameById(int sizeId);
		#endregion

		#region Product Gallary	

		int AddGallary(IFormFile galleryImg,int productId);
		List<ProductGallery> GetGallaryProduct(int productId);
		void DeleletFromGallary(int gallaryId);

		#endregion
	}
}
