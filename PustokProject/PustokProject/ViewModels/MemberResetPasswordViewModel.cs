using System;
using System.ComponentModel.DataAnnotations;

namespace PustokProject.ViewModels
{
	public class MemberResetPasswordViewModel
	{
        [Required]
        [MaxLength(100)]
        public string Email { get; set; }
        [Required]
        public string Token { get; set; }
        [Required]
        [MaxLength(25)]
        [MinLength(8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [MaxLength(25)]
        [MinLength(8)]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}

