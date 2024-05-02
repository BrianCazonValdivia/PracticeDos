using BusinessLogic.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UPB.BusinessLogic.Manager.Exceptions
{
    public class PatientNotFoundException : Exception
    {
        public PatientNotFoundException() { }

        public PatientNotFoundException(string message) : base(message) { }
    }
}
