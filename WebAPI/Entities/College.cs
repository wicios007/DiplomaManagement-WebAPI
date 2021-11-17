using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Entities
{
    public class College
    {
        public int Id{ get; set; }
        public string Name{ get; set; }
        public int AddressId { get; set; }
        public virtual Address Address { get; set; }
        public List<Department> Departments{ get; set; }
        
    }
}
