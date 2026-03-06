namespace Server
{
    internal class Program
    {
        static void Main()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("SERVER");
            Console.ResetColor();

            List<Product> products = null;

            while (products == null)
            {
                Console.WriteLine("Enter path to Excel file:");

                string path = Console.ReadLine().Replace("\"", "");

               

                if (!File.Exists(path))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("File not found! Try again.\n");
                    Console.ResetColor();
                    continue;
                }

                try
                {
                    var excelService = new ExcelService();
                    products = excelService.LoadProducts(path);
                }
                catch
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Error reading Excel file.\n");
                    Console.ResetColor();
                }
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Loaded {products.Count} products");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("\nProducts loaded:");
            Console.ResetColor();

            foreach (var product in products)
            {
                Console.WriteLine($"{product.Name} | Price: {product.Price} | Count: {product.Count}");
            }

            Console.WriteLine();

            var server = new ServerService(products);

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Server started on port 5000");
            Console.ResetColor();

            server.Start();

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Server stopped");
            Console.ResetColor();
        }
    }
    
}
