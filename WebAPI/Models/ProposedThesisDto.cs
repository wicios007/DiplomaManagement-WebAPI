using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class ProposedThesisDto
    {
        public int Id{get;set;}
        public int StudentId{get;set;}
        public int CreatedById{get;set;}
        public string Name{get;set;}
        public string NameEnglish{get;set;}
        public string Description{get;set;}
        public bool IsAccepted{get;set;}
    }
}
