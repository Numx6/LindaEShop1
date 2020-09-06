using LindaEShop.Core.Convertor;
using LindaEShop.Core.DTOs;
using LindaEShop.Core.Generator;
using LindaEShop.Core.Security;
using LindaEShop.Core.Services.Interfaces;
using LindaEShop.DataLayer.Context;
using LindaEShop.DataLayer.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace LindaEShop.Core.Services
{
	public class ProductService : IProduct
	{
		private LindaContext _context;
		public ProductService(LindaContext context)
		{
			_context = context;
		}

		public int AddGallary(IFormFile galleryImg, int productId)
		{
			ProductGallery productGallery = new ProductGallery();
			productGallery.CreatDate = DateTime.Now;
			productGallery.ImageName = "no-photo.jpg";
			productGallery.ProductId = productId;

			if (galleryImg != null && galleryImg.IsImage())
			{
				productGallery.ImageName = NameGenerator.GenerateUniqCode() + Path.GetExtension(galleryImg.FileName);
				string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/ProductGallary", productGallery.ImageName);

				using (var stream = new FileStream(imagePath, FileMode.Create))
				{
					galleryImg.CopyTo(stream);
				}

				ImageConvertor imgResizer = new ImageConvertor();
				string thumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/ProductGallary/thumb", productGallery.ImageName);

				imgResizer.Image_resize(imagePath, thumbPath, 250);
			}
			_context.productGalleries.Add(productGallery);
			_context.SaveChanges();

			return productGallery.GalleryId;
		}

		public int AddProduct(Product product, IFormFile imgUp, List<int> selectedColor, List<int> selectedSize)
		{
			product.CreatDate = DateTime.Now;
			product.ImageName = "no-photo.jpg";
			product.IsActive = true;
			product.IsDelete = false;
			product.Visit = 0;
			product.ProductCode = product.ProductCode.Trim();

			if (imgUp != null && imgUp.IsImage())
			{
				product.ImageName = NameGenerator.GenerateUniqCode() + Path.GetExtension(imgUp.FileName);
				string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/Product", product.ImageName);

				using (var stream = new FileStream(imagePath, FileMode.Create))
				{
					imgUp.CopyTo(stream);
				}

				ImageConvertor imgResizer = new ImageConvertor();
				string thumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/Product/thumb", product.ImageName);

				imgResizer.Image_resize(imagePath, thumbPath, 250);
			}

			_context.Add(product);
			_context.SaveChanges();

			foreach (var itemColor in selectedColor) //----color to product--------
			{
				ColorToProduct colorToProduct = new ColorToProduct()
				{
					ColorId = itemColor,
					ProductId = product.Id,
				};
				_context.Add(colorToProduct);
			}

			foreach (var itemSize in selectedSize) //------size to product-----
			{
				SizeToProduct sizeToProduct = new SizeToProduct()
				{
					SizeId = itemSize,
					ProductId = product.Id
				};
				_context.Add(sizeToProduct);
			}

			_context.SaveChanges();

			return product.Id;
		}

		public bool CodeProductIsExist(string productCode)
		{
			return _context.Products.Any(u => u.ProductCode == productCode);
		}

		public void DeleletFromGallary(int gallaryId)
		{
			if (gallaryId != 0)
			{
				var img = _context.productGalleries.Find(gallaryId);
				if (img.ImageName != "no-photo.jpg")
				{
					string deleteimagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/ProductGallary", img.ImageName);
					if (File.Exists(deleteimagePath))
					{
						File.Delete(deleteimagePath);
					}

					string deletethumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/ProductGallary/thumb", img.ImageName);
					if (File.Exists(deletethumbPath))
					{
						File.Delete(deletethumbPath);
					}
				}
				_context.productGalleries.Remove(img);
				_context.SaveChanges();
			}
		}

		public void EditProduct(Product product, IFormFile imgUp, List<int> selectedColor, List<int> selectedSize)
		{

			if (imgUp != null && imgUp.IsImage())
			{
				if (product.ImageName != "no-photo.jpg")
				{
					string deleteimagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/Product", product.ImageName);
					if (File.Exists(deleteimagePath))
					{
						File.Delete(deleteimagePath);
					}

					string deletethumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/Product/thumb", product.ImageName);
					if (File.Exists(deletethumbPath))
					{
						File.Delete(deletethumbPath);
					}
				}
				product.ImageName = NameGenerator.GenerateUniqCode() + Path.GetExtension(imgUp.FileName);
				string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/Product", product.ImageName);

				using (var stream = new FileStream(imagePath, FileMode.Create))
				{
					imgUp.CopyTo(stream);
				}

				ImageConvertor imgResizer = new ImageConvertor();
				string thumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/Product/thumb", product.ImageName);

				imgResizer.Image_resize(imagePath, thumbPath, 250);
			}
			_context.Products.Update(product);

			_context.ColorToProducts.Where(u => u.ProductId == product.Id).ToList()
				.ForEach(r => _context.ColorToProducts.Remove(r));

			_context.SizeToProducts.Where(u => u.ProductId == product.Id).ToList()
				.ForEach(r => _context.SizeToProducts.Remove(r));

			foreach (var itemColor in selectedColor) //----color to product--------
			{
				ColorToProduct colorToProduct = new ColorToProduct()
				{
					ColorId = itemColor,
					ProductId = product.Id,
				};
				_context.ColorToProducts.Add(colorToProduct);
			}

			foreach (var itemSize in selectedSize) //------size to product-----
			{
				SizeToProduct sizeToProduct = new SizeToProduct()
				{
					SizeId = itemSize,
					ProductId = product.Id
				};
				_context.SizeToProducts.Add(sizeToProduct);
			}

			_context.SaveChanges();
		}

		public List<Color> GetAllColors()
		{
			return _context.Colors.ToList();
		}

		public IndexProductAdminPanelViewModel GetAllProduct(int pageId = 1, string codeFilter = ""
			, string nameFilter = "")
		{
			IQueryable<Product> result = _context.Products;

			if (!string.IsNullOrEmpty(codeFilter))
			{
				result = result.Where(u => u.ProductCode.Contains(codeFilter));
			}
			if (!string.IsNullOrEmpty(nameFilter))
			{
				result = result.Where(u => u.Name.Contains(nameFilter));
			}
			// Show Item In Page
			int take = 100;
			int skip = (pageId - 1) * take;

			IndexProductAdminPanelViewModel item = new IndexProductAdminPanelViewModel()
			{
				CurrentPage = pageId,
				PageCount = result.Count() / take,
				Products = result.OrderBy(u => u.CreatDate).Skip(skip).Take(take).OrderByDescending(c=>c.CreatDate).ToList()
			};

			return item;
		}

		public Tuple<List<ShowProductListItemViewModel>, int> GetAllProductForShop(int pageId = 1, int take = 0
			, string filter = "", string orderBy = "p", int startPrice = 0
			, int endPrice = 0, int selectedGroup = 0)
		{
			if (take == 0)
			{
				take = 4;
			}
			IQueryable<Product> result = _context.Products;
			if (!string.IsNullOrEmpty(filter))
			{
				result = result.Where(c => c.Name.Contains(filter) || c.ProductCode.Contains(filter));
			}
			switch (orderBy)
			{
				case "p": //---پیشفرض
					{ break; }
				case "m": //----محبوبترین 
					{
						result = result.OrderByDescending(u => u.Visit);
						break;
					}
				case "n": //----جدیدترین
					{
						result = result.OrderByDescending(u => u.CreatDate);
						break;
					}
			}
			if (startPrice > 0)
			{
				result = result.Where(u => u.Price >= startPrice);
			}
			if (endPrice > 0)
			{
				result = result.Where(u => u.Price <= endPrice);
			}
			if (selectedGroup > 0)
			{
				result = result.Where(u => u.ProductGroupId == selectedGroup);
			}

			int skip = (pageId - 1) * take;

			int pageCount = result.Count();
			pageCount = pageCount / take;

			var query = result.Include(c => c.ColorToProducts).Include(c => c.SizeToProducts)
				.Include(c => c.ProductGroup).Select(c => new ShowProductListItemViewModel()
				{
					Id = c.Id,
					ImageName = c.ImageName,
					IsActive = c.IsActive,
					Name = c.Name,
					Price = c.Price,
					ProductCode = c.ProductCode,
				}).Skip(skip).Take(take).ToList();

			return Tuple.Create(query, pageCount);
		}

		public List<ProductGroup> GetAllproductGroups()
		{
			return _context.ProductGroups.ToList();
		}

		public List<SelectListItem> GetAllProductGroupsSelectList()
		{
			return _context.ProductGroups
				.Select(g => new SelectListItem()
				{
					Text = g.Title,
					Value = g.Id.ToString()
				}).ToList();
		}

		public List<Size> GetAllSize()
		{
			return _context.Sizes.ToList();
		}

		public List<int> GetColorOfProduct(int productId)
		{
			return _context.ColorToProducts.Where(w => w.ProductId == productId).Select(s => s.ColorId).ToList();
		}

		public List<ProductGallery> GetGallaryProduct(int productId)
		{
			return _context.productGalleries.Where(p => p.ProductId == productId).OrderByDescending(c=>c.CreatDate).ToList();
		}

		public Product GetProductForShow(int productId)
		{
			return _context.Products.Include(c => c.ColorToProducts)
				.Include(c => c.SizeToProducts).Include(c => c.ProductGalleries)
				.FirstOrDefault(c => c.Id == productId);
		}

		public Product GetProductyId(int id)
		{
			return _context.Products.Find(id);
		}

		public List<int> GetSizeOfProduct(int productId)
		{
			return _context.SizeToProducts.Where(u => u.ProductId == productId).Select(p => p.SizeId).ToList();
		}
	}
}
