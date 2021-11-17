using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace WebAPI.Entities
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
