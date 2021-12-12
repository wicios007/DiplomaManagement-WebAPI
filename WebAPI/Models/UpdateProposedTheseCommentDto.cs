using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class UpdateProposedTheseCommentDto
    {
        public int PromoterId{get;set;}
        public int StudentId{get;set;}
        public string Comment{get;set;}
    }
}
