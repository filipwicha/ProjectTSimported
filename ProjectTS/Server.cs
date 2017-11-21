using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace ProjectTS
{
    public class Server
    {
        const string DEFAULT_SERVER = "localhost";
        const int DEFAULT_PORT = 804;

        //Server socket stuff
        Socket serverSocket;
        SocketInformation serverSocketInfo;

        public string Startup()
        {
            // The chat server always starts up on the localhost, using the default port 
            IPHostEntry hostInfo = Dns.GetHostEntry(DEFAULT_SERVER);
            IPAddress serverAddr = hostInfo.AddressList[1];
            var serverEndPoint = new IPEndPoint(serverAddr, DEFAULT_PORT);

            // Create a listener socket and bind it to the endpoint 
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            serverSocket.Bind(serverEndPoint);

            return serverSocket.LocalEndPoint.ToString();
        }

        public string Listen()
        {
            int backlog = 0;
            try
            {
                serverSocket.Listen(backlog);
                return "Server listening";
            }
            catch (Exception ex)
            {
                return "Failed to listen" + ex.ToString();
            }
        }

        public string ReceiveData()
        {
            Socket receiveSocket;
            byte[] buffer = new byte[256];

            receiveSocket = serverSocket.Accept();

            var bytesrecd = receiveSocket.Receive(buffer);

            receiveSocket.Close();

            Encoding encoding = Encoding.UTF8;

            return encoding.GetString(buffer);
        }
    }
}
