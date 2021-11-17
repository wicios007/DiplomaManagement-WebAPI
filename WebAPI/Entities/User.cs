using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace WebAPI.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName{ get; set; }
        public string LastName{ get; set; }
        public DateTime RegistrationDate { get; set; }
        public string PasswordHash { get; set; }
        public int RoleId { get; set; }
        public virtual Role Role { get; set; }

        /*        public string? Title { get; set; }
                public string? IndexNumber { get; set; }*/
        /*public int? ThesisId { get; set; }
        public Thesis? Thesis { get; set; }*/

        //public int CollegeId { get; set; }
        //public virtual College College { get; set; }
        //public int DepartmentId { get; set; }
        //public virtual Department Department { get; set; }

    }
}
