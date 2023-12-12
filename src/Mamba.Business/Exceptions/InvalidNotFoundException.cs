using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mamba.Business.Exceptions
{
    public class InvalidNotFoundException:Exception
    {
        public string PropertyName { get; set; }

        public InvalidNotFoundException()
        {

        }
        public InvalidNotFoundException(string propertyName, string message) : base(message)
        {
            PropertyName = propertyName;
        }
    }
}
