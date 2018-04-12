using System;
using System.Net;

namespace CPTS.Common.Remoting.Exceptions
{
    public class RemotingServerUnAvailableException : Exception
    {
        public RemotingServerUnAvailableException(EndPoint serverEndPoint)
            : base(string.Format("Remoting server is unavailable, server address:{0}", serverEndPoint))
        {
        }
    }
}
