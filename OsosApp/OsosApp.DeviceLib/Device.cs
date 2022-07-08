using SimpleTCP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OsosApp.DeviceLib
{
    public class Device : IDisposable
    {
        #region Public Vars
        public string Ip { get; private set; }
        public int Port { get; private set; }


        #endregion

        #region Private Vars
        SimpleTcpClient tcpClient;
        #endregion

        #region Cons & Decons
        public Device(string ip, int port)
        {
            Ip = ip;
            Port = port;

            Init();
        }

        public void Dispose()
        {
            tcpClient.Dispose();
            tcpClient = null;
        }
        #endregion

        #region Private Methods
        void Init()
        {
            tcpClient = new SimpleTcpClient();
            tcpClient.DataReceived += TcpClient_DataReceived;
        }

        void Connect()
        {
            tcpClient.Connect(Ip, Port);
        }
        #endregion

        #region Public Methods
        public void HandShake()
        {

        }

        public void SendCommand()
        {

        }
        #endregion

        #region Events
        void TcpClient_DataReceived(object sender, Message e)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}