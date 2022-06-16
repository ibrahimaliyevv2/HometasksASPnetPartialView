using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace PustokProject.Models
{
	public class Slider
	{
        public int Id { get; set; }
        public int Order { get; set; }
        [Required]
        [MaxLength(50)]
        public string Title { get; set; }
        [MaxLength(50)]
        public string SubTitle { get; set; }
        [MaxLength(250)]
        public string Desc { get; set; }
        [MaxLength(50)]
        public string BtnText { get; set; }
        [MaxLength(250)]
        public string BtnUrl { get; set; }
        [MaxLength(100)]
        public string Image { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
    }
}

