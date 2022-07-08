using SimpleTCP;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OsosApp
{
    public partial class Form1 : Form
    {
        string hostName = "5.26.165.81";
        int port = 8090;

        TcpClient socketConnection;
        Thread clientReceiveThread;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void ConnectToTcpServer()
        {
            try
            {
                clientReceiveThread = new Thread(new ThreadStart(ListenForData));
                clientReceiveThread.IsBackground = true;
                clientReceiveThread.Start();
            }
            catch (Exception e)
            {
                //Debug.Log("On client connect exception " + e);
            }
        }

        /// <summary> 	
        /// Runs in background clientReceiveThread; Listens for incomming data. 	
        /// </summary>     
        private void ListenForData()
        {
            try
            {
                socketConnection = new TcpClient(hostName, port);
                Byte[] bytes = new Byte[1024];

                SendMessage();
                while (true)
                {
                    // Get a stream object for reading 				
                    using (NetworkStream stream = socketConnection.GetStream())
                    {
                        int length;
                        // Read incomming stream into byte arrary. 					
                        while ((length = stream.Read(bytes, 0, bytes.Length)) != 0)
                        {
                            var incommingData = new byte[length];
                            Array.Copy(bytes, 0, incommingData, 0, length);
                            // Convert byte array to string message. 						
                            string serverMessage = Encoding.ASCII.GetString(incommingData);

                            //Debug.Log("server message received as: " + serverMessage);
                        }
                    }
                }
            }
            catch (SocketException socketException)
            {
                //Debug.Log("Socket exception: " + socketException);
            }
        }

        /// <summary> 	
        /// Send message to server using socket connection. 	
        /// </summary> 	
        private void SendMessage()
        {
            if (socketConnection == null)
            {
                return;
            }
            try
            {
                // Get a stream object for writing. 			
                NetworkStream stream = socketConnection.GetStream();
                if (stream.CanWrite)
                {
                    string clientMessage = "/?21000185\r\n";
                    // Convert string message to byte array.                 
                    byte[] clientMessageAsByteArray = Encoding.ASCII.GetBytes(clientMessage);
                    // Write byte array to socketConnection stream.                 
                    stream.Write(clientMessageAsByteArray, 0, clientMessageAsByteArray.Length);
                    //Debug.Log("Client sent his message - should be received by server");
                }
            }
            catch (SocketException socketException)
            {
                //Debug.Log("Socket exception: " + socketException);
            }
        }

        void adsasdads()
        {
            SimpleTCP.SimpleTcpClient client = new SimpleTCP.SimpleTcpClient();
            client.DataReceived += Client_DataReceived;
        }

        private void Client_DataReceived(object sender, SimpleTCP.Message e)
        {
            throw new NotImplementedException();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ConnectToTcpServer();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (frmModem frm = new frmModem())
            {
                frm.ShowDialog();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SimpleTcpClient tcpClient = new SimpleTcpClient();
            tcpClient.DataReceived += TcpClient_DataReceived;

            SimpleTcpClient sssss = tcpClient.Connect("5.26.165.81", 8090);

            // /?21000185[0D][0A]
            //13,10
            //int unicode = 65;
            //char character = (char)unicode;
            //string text = character.ToString();

            tcpClient.Write("/?21000185\r\n");
        }

        private void TcpClient_DataReceived(object sender, SimpleTCP.Message e)
        {
            
        }
    }
}