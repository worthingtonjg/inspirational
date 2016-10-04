using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace inspirational
{
	public class ImagePicker
	{
		public static string GetRandomImage(string author)
		{
			string result = string.Empty;

			result = GetAuthorImage(author);

			// No author image, so pick a random image
			if (string.IsNullOrEmpty(result))
			{
				int imageCount = 78;

				Random rand = new Random();

				int index = rand.Next(1, imageCount);

				result = "inspirationalmessageimage" + index;
			}

			return result;
		}

		private static string GetAuthorImage(string author)
		{
			var authors = new List<string>();

			authors.Add("Thomas S. Monson");
			authors.Add("Henry B. Eyring");
			authors.Add("Dieter F. Uchtdorf");
			authors.Add("Boyd K. Packer");
			authors.Add("L. Tom Perry");
			authors.Add("Russell M. Nelson");
			authors.Add("Dallin H. Oaks");
			authors.Add("M. Russell Ballard");
			authors.Add("Richard G. Scott");
			authors.Add("Robert D. Hales");
			authors.Add("Jeffrey R. Holland");
			authors.Add("David A. Bednar");
			authors.Add("Quenton L. Cook");
			authors.Add("D. Todd Christofferson");
			authors.Add("Neil L. Andersen");

			int index = authors.IndexOf(author);

			if (index >= 0)
			{
				return "leader" + (index + 1);
			}

			return null;
		}
	}
}
