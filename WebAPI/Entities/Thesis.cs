using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Entities
{
    public class Thesis
    {
        public int Id{ get; set; }
        public string Name{ get; set; }
        public string NameEnglish{ get; set; }
        public string Description { get; set; }
        //public bool IsTaken { get; set; }

        public int? StudentId { get; set; }
        public virtual Student Student { get; set; }

        public int? PromoterId { get; set; }
        public virtual Promoter Promoter { get; set; }
        
        public int? DepartmentId{get;set;}
        public virtual Department Department{get;set;}




    }
}
