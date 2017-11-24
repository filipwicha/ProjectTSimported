using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTS
{
    class Program
    {
        static Server server = new Server();
        static Client client = new Client();
        
        static void Main(string[] args)
        {
            string serverInfo = server.Startup();
            Console.WriteLine("Server started at:" + serverInfo);

            serverInfo = server.Listen();
            Console.WriteLine(serverInfo);

            while(true)
            {
                client.SendData(Convert.ToInt32(Console.ReadLine()));

                serverInfo = server.ReceiveData();
                Console.WriteLine(serverInfo);
            }
            

            Console.ReadKey();
            //exit 
        }
    }
}
