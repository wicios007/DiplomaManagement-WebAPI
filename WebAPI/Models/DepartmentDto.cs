using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Entities;

namespace WebAPI.Models
{
    public class DepartmentDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Initials { get; set; }
        public int CollegeId { get; set; }
        public List<Promoter> Promoters { get; set; }
        public List<Student> Students { get; set; }
    }
}
