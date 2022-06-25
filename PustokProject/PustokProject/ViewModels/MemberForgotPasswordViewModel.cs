using System;
using System.ComponentModel.DataAnnotations;

namespace PustokProject.ViewModels
{
	public class MemberForgotPasswordViewModel
	{
		[Required]
		[MaxLength(100)]
		public string Email { get; set; }
	}
}

