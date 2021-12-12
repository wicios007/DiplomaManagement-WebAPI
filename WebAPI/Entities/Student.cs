using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Entities
{
    public class Student : User
    {
        public string IndexNumber{ get; set; }
/*        public int? DepartmentId{get;set;}
        public virtual Department Department{get;set;}*/
        
        //public int ThesisId{get;set;}
        public virtual Thesis Thesis { get; set; }
        public List<ProposedThese> ProposedThesesList { get; set; }
        
    }
}
