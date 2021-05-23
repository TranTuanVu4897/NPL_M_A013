using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace NPL_M_A013.Exception
{
   public class NotMatchPropertyRequireException : System.Exception
    {
        public NotMatchPropertyRequireException()
        {
        }

        public NotMatchPropertyRequireException(string message) : base(message)
        {
        }

        public NotMatchPropertyRequireException(string message, System.Exception innerException) : base(message, innerException)
        {
        }

        protected NotMatchPropertyRequireException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

    }
}
