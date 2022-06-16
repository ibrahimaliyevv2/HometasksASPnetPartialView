using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PustokProject.Models
{
	public class Author
	{
        public int Id { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "Maximum length is 50 characher!")]
        public string FullName { get; set; }

        public DateTime BirthDate { get; set; }

        public List<Book> Books { get; set; }
    }
}

