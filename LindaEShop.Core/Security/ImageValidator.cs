using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace LindaEShop.Core.Security
{
	public static class ImageValidator
	{
		public static bool IsImage(this IFormFile file)
		{
			try
			{
				var img = System.Drawing.Image.FromStream(file.OpenReadStream());
				return true;
			}
			catch (Exception)
			{

				return false;
			}
		}
	}
}
