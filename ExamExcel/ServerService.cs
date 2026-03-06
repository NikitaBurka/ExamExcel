using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class ServerService
    {
        private List<Product> products;

        public ServerService(List<Product> products)
        {
            this.products = products;
        }

        public void Start()
        {
            TcpListener server = new TcpListener(IPAddress.Any, 5000);
            server.Start();

            Console.WriteLine("Server started!");

            while (true)
            {

                var client = server.AcceptTcpClient();

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Client connected: {client.Client.RemoteEndPoint}");
                Console.ResetColor();

                Task.Run(() => HandleClient(client));
            }

        }

        private void HandleClient(TcpClient client)
        {
            var stream = client.GetStream();

            byte[] buffer = new byte[1024];
            int bytes = stream.Read(buffer, 0, buffer.Length);

            string request = Encoding.UTF8.GetString(buffer, 0, bytes);

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"Client request: {request}");
            Console.ResetColor();

            string[] names = request.Split(',');

            StringBuilder response = new StringBuilder();

            foreach (var name in names)
            {
                var product = products
                    .FirstOrDefault(p => p.Name.ToLower() == name.ToLower());

                if (product != null)
                {
                    double sum = product.Price * product.Count;

                    response.AppendLine($"{product.Name} | Price:{product.Price} | Count:{product.Count} | Sum:{sum}");
                }
                else
                {
                    response.AppendLine($"{name} not found");
                }
            }

            byte[] data = Encoding.UTF8.GetBytes(response.ToString());
            stream.Write(data, 0, data.Length);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Response sent to client\n");
            Console.ResetColor();

            client.Close();
        }
    }
}
