using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Models
{
    public class Patient
    {
        public string Name { get; set; }
        public string Lastname { get; set; }
        public int CI { get; set; }
        public string Bloodtype { get; set; }

        public Patient() { }

        public Patient(string name, string lastname, int cI, string bloodtype)
        {
            Name = name;
            Lastname = lastname;
            CI = cI;
            Bloodtype = bloodtype;
        }
    }
}
