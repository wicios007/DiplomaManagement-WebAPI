using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace WebAPI.Entities
{
    public class User : IdentityUser<int>
    {
        public string FirstName{ get; set; }
        public string LastName{ get; set; }
        public DateTime RegistrationDate { get; set; }

        /*public int? ThesisId { get; set; }
        public Thesis? Thesis { get; set; }*/

        public int? DepartmentId { get; set; }
        public virtual Department Department { get; set; }
        public virtual Promoter Promoter{get;set;}
        public virtual Student Student{get;set;}

    }
}
