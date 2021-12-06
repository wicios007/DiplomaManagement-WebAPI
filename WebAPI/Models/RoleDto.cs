using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Entities;

namespace WebAPI.Models
{
    public class RoleDto
    {       
        public List<RoleValue> roleValue {get; }
        public List<string> Name { get; }
    }
}
