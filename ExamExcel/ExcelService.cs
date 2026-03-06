using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClosedXML.Excel;


namespace Server
{
    public class ExcelService
    {
        public List<Product> LoadProducts(string path)
        {
            var products = new List<Product>();

            var workbook = new XLWorkbook(path);
            var worksheet = workbook.Worksheet(1);

            var rows = worksheet.RowsUsed().Skip(1);

            foreach (var row in rows)
            {
                products.Add(new Product
                {
                    Name = row.Cell(1).GetString(),
                    Price = row.Cell(2).GetDouble(),
                    Count = row.Cell(3).GetValue<int>()
                });
            }

            return products;
        }
    }
}
