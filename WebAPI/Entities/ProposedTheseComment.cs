using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Entities
{
    public class ProposedTheseComment
    {
        public int Id{get;set;}
        public int ProposedTheseId{get;set;}
        public virtual ProposedThese ProposedThese{get;set;}
        public int? DepartmentId{get;set;}
        public virtual Department Department{get;set;}
        public int? PromoterId{get;set;}
        public virtual Promoter Promoter{get;set;}
        public int? StudentId{get;set;}
        public virtual Student Student{get;set;}
        public string Comment{get;set;}

    }
}
