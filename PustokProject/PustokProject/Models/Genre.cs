using System;
using System.ComponentModel.DataAnnotations;

namespace PustokProject.Models
{
	public class Genre
	{
		public int Id { get; set; }
		[Required]
		[MaxLength(35)]
		public string Name { get; set; }
	}
}

