using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PustokProject.Models
{
	public class BookImage
	{
        public int Id { get; set; }
        [Required]
        [MaxLength(35)]
        public string Name { get; set; }
        public int BookId { get; set; }

        [NotMapped]
        public List<Book> Books { get; set; }
        public bool? PosterStatus { get; set; }
        public Book Book { get; set; }
    }
}

