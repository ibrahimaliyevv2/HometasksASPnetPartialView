using System;
using System.Collections.Generic;
using PustokProject.Models;

namespace PustokProject.ViewModels
{
	public class HomeViewModel
	{
		public List<Slider> Sliders { get; set; }
		public List<Book> DiscountedBooks { get; set; }
		public List<Book> FeaturedBooks { get; set; }
		public List<Book> NewBooks { get; set; }
	}
}

