using System;
using System.Collections.Generic;

namespace PustokProject.ViewModels
{
	public class BookCommentPostViewModel
	{
		public List<BasketItemViewModel> BasketItems { get; set; } = new List<BasketItemViewModel>();
		public double TotalAmount { get; set; }
        public int BookId { get; internal set; }
        public int Rate { get; internal set; }
    }
}

