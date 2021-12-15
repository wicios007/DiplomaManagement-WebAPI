using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class SendEmailDto
    {
        [Required]
        public int SrcId{get;set;}
        [Required]
        public int DestId{get;set;}
        [Required]
        public string Subject{get;set;}
        [Required]
        public string Content{get;set;}
    }
}
