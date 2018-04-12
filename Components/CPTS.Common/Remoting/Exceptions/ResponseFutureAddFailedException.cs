using System;
using System.Net;

namespace CPTS.Common.Remoting.Exceptions
{
    public class ResponseFutureAddFailedException : Exception
    {
        public ResponseFutureAddFailedException(long requestSequence)
            : base(string.Format("Add remoting request response future failed. request sequence:{0}", requestSequence))
        {
        }
    }
}
