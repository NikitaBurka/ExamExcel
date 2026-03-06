using System.Net.Sockets;
using System.Text;

class Program
{
    static void Main()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("PRODUCT CLIENT");
        Console.ResetColor();

        string defaultIp = "127.0.0.1";

        Console.WriteLine($"Enter server IP (default {defaultIp}):");

        string ip = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(ip))
            ip = defaultIp;

        while (true)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Enter product names (Apple,Banana) or 'exit':");
            Console.ResetColor();

            string request = Console.ReadLine();

            if (request.ToLower() == "exit")
            {
                break;
            }
                

            try
            {
                TcpClient client = new TcpClient();
                client.Connect(ip, 5000);

                var stream = client.GetStream();

                byte[] data = Encoding.UTF8.GetBytes(request);
                stream.Write(data, 0, data.Length);

                byte[] buffer = new byte[1024];
                int bytes = stream.Read(buffer);

                string response = Encoding.UTF8.GetString(buffer, 0, bytes);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Server response:");
                Console.ResetColor();

                Console.WriteLine(response);

                client.Close();
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Connection error");
                Console.ResetColor();
            }

            Console.WriteLine();
        }

        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine("Client stopped");
        Console.ResetColor();
    }
}
