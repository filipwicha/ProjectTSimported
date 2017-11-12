using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace ProjectTS
{
    public class ClientServer
    {
        const string DEFAULT_SERVER = "localhost";
        const int DEFAULT_PORT = 804;

        //Server socket stuff
        Socket serverSocket;
        SocketInformation serverSocketInfo;

        //Client socket stuff 
        Socket clientSocket;
        SocketInformation clientSocketInfo;

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

        public bool SendData(string textdata)
        {
            if (string.IsNullOrEmpty(textdata))
            {
                return false;
            }

            if (textdata.Trim().ToLower() == "exit")
            {
                return true;
            }

            // The chat client always starts up on the localhost, using the default port 
            IPHostEntry hostInfo = Dns.GetHostEntry(DEFAULT_SERVER);
            IPAddress serverAddr = hostInfo.AddressList[1];
            var clientEndPoint = new IPEndPoint(serverAddr, DEFAULT_PORT);

            // Create a client socket and connect it to the endpoint 
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            clientSocket.Connect(clientEndPoint);

            byte[] byData = Encoding.ASCII.GetBytes(textdata);
            clientSocket.Send(byData);
            clientSocket.Close();

            return false;
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
