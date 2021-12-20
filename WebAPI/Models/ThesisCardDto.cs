using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Entities;

namespace WebAPI.Models
{
    public class ThesisCardDto
    {
        public Student Student{ get;set; }
        public Promoter Promoter{get;set;}
        public string Major {get;set;}          //kierunek studiów
        public string Specialization {get;set;} //specjalizacja

        public ThesisDto Thesis{get;set;}
    }
}
