using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Entities
{
    public class Promoter : User
    {
        public string Title { get; set; }
/*        public int? DepartmentId{get;set;}
        public virtual Department Department{get;set;}*/
        public List<ProposedThese> ProposedThesesList {get;set;}
        public List<Thesis> Theses { get; set; }

    }
}
