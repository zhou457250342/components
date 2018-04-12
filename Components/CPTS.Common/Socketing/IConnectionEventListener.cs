﻿using System.Net;
using System.Net.Sockets;

namespace CPTS.Common.Socketing
{
    public interface IConnectionEventListener
    {
        void OnConnectionAccepted(ITcpConnection connection);
        void OnConnectionEstablished(ITcpConnection connection);
        void OnConnectionFailed(EndPoint remotingEndPoint, SocketError socketError);
        void OnConnectionClosed(ITcpConnection connection, SocketError socketError);
    }
}
