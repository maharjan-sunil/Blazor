using System.Text.Json;
using Toronto.Models;

namespace Toronto.Service
{
    public class ProductService
    {
        public ProductService(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
        }

        public IWebHostEnvironment WebHostEnvironment { get; }
        //provides information about the web hosting where application is hosted

        private string FileName
        {
            get
            {
                return Path.Combine(WebHostEnvironment.WebRootPath, "data", "Product.json");
            }
        }

        public IEnumerable<Product> GetProducts()
        {
            using(var reader = File.OpenText(FileName))
            {
                var list = JsonSerializer.Deserialize<Product[]>(reader.ReadToEnd(),
                    new JsonSerializerOptions
                {
                        PropertyNameCaseInsensitive = true,
                });
                return list;
            }
        }

        public void UpdateTax(int Id, int Tax)
        {
            var products = GetProducts();

            var query = products.First(x => x.Id == Id);

            if (query.Tax == null)
                query.Tax = new[] { Tax };

            else
            {
                var taxes = query.Tax.ToList();
                taxes.Add(Tax);
                query.Tax = taxes.ToArray();
            }

            using (var file = File.OpenWrite(FileName)) {
                JsonSerializer.Serialize<IEnumerable<Product>>(
                    new Utf8JsonWriter(file, new JsonWriterOptions
                    {
                        SkipValidation = true,
                        Indented = true,
                    }), products);
            };
                
        }
    }
}
