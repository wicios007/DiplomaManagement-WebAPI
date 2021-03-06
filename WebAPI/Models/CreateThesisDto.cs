using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class CreateThesisDto
    {
        [Required]
        public string Name { get; set; }
        public string NameEnglish { get; set; }
        public string Description { get; set; }
        public int StudentId{get;set;}
        public int PromoterId{get;set;}
    }
}
