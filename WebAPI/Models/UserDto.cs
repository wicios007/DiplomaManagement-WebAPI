﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class UserDto
    {
        public int Id{get;set;}
        public string FirstName { get; set;}
        public string LastName {get; set;}
        public string Email {get; set;}
        public int DepartmentId{get;set;}
        public string IndexNumber{get;set;}
        public string Title{get;set;}

    }
}
