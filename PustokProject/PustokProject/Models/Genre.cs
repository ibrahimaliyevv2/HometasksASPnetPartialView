using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PustokProject.Models
{
	public class Genre
	{
		public int Id { get; set; }
		[Required]
		[MaxLength(35)]
		public string Name { get; set; }

		[NotMapped]
		public List<Book> Books { get; set; }
	}
}

