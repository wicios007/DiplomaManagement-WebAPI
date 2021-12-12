using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Entities
{
    public class ProposedThese
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string NameEnglish { get; set; }
        public string Description { get; set; }
        public bool IsAccepted { get; set; }
        public int? StudentId { get; set; }
        public virtual Student Student { get; set; }
        public int? DepartmentId{get;set;}
        public virtual Department Department{get;set;}
        public List<ProposedTheseComment> Comments { get; set; }
    }
}
