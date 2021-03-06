using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Entities
{
    public class Department
    {
        public int Id{ get; set; }
        public string Name{ get; set; }
        public string Initials { get; set; }
        public List<Thesis> Theses { get; set; }
        public List<ProposedThese> ProposedTheses{get;set;} //added
        public List<Student> Students { get; set; }
        public List<Promoter> Promoters { get; set; }

    }
}
