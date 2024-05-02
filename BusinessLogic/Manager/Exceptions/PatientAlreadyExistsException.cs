using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UPB.BusinessLogic.Manager.Exceptions
{
    internal class PatientAlreadyExistsException : Exception
    {
        public PatientAlreadyExistsException() { }

        public PatientAlreadyExistsException(string message) : base(message) { }

        public string getError()
        {
            return "Patient already Registered!!";
        }
    }
}
