using SimpleTCP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OsosApp.DeviceLib
{
    public abstract class DeviceBase
    {
        internal SimpleTcpClient tcpClient;

        public string Ip { get; private set; }
        public int Port { get; private set; }

        public DeviceBase(string ip, int port)
        {
            Ip = ip;
            Port = port;

            tcpClient = new SimpleTcpClient();
        }

        public void Connect()
        {
            tcpClient.Connect(Ip, Port);
        }
    }
}