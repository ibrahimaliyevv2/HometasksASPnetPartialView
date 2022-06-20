using System;
using System.ComponentModel.DataAnnotations;

namespace PustokProject.Areas.Manage.ViewModels
{
	public class AdminLoginViewModel
	{
		[Required]
		[MaxLength(30)]
		[MinLength(5)]
		public string UserName { get; set; }

		[Required]
		[MaxLength(30)]
		[MinLength(5)]
		[DataType(DataType.Password)]
		public string Password { get; set; }
	}
}

