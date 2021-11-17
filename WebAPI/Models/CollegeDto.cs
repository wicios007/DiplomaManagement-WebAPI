using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Entities;

namespace WebAPI.Models
{
    public class CollegeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }
        //public List<DepartmentDto> Departments { get; set; }
    }
}
