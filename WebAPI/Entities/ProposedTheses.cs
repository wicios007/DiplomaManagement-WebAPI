using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Entities
{
    public class ProposedTheses
    {
        public int Id { get; set; }
        public int StudentId{ get; set; }
        public Student Student{ get; set; }
        public string Description{ get; set; }
        public bool IsAccepted { get; set; }
    }
}
