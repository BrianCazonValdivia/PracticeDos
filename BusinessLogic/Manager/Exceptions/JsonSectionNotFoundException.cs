using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UPB.BusinessLogic.Manager.Exceptions
{
    public class JsonSectionNotFoundException : Exception
    {
        public JsonSectionNotFoundException() { }

        public JsonSectionNotFoundException(string message) : base(message) { }

        public string getError()
        {
            return "JsonSection Not Found";
        }
    }
}
