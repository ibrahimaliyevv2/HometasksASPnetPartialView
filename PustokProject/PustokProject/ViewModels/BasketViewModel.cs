using System;
using System.Collections.Generic;

namespace PustokProject.ViewModels
{
	public class BasketViewModel
	{
		public List<BasketItemViewModel> BasketItems { get; set; } = new List<BasketItemViewModel>();
		public double TotalAmount { get; set; }
	}
}

