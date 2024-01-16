using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.Utilities.Exceptions
{
    public class EShopException: Exception
    {
        public EShopException() { }

        public EShopException(string message) { }

        protected EShopException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        
    }
}
