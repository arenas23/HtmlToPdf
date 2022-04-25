// See https://aka.ms/new-console-template for more information

using htmlToImage;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

Console.WriteLine("Hello, World!");
HtmlConvert pepe = new HtmlConvert();
PDF pdf = new PDF("enero","2022","2022-05-02","66666","12345678","2022-05-05","200000000","50000","50000","78787","854484","478787","70000","7888","50000","7000","pepe","115222131","2389566","arnas9808@hotmail.com","2022-05-05","454545");

HtmlConvert parametros= new HtmlConvert();
parametros.Parametros = JObject.Parse(JsonConvert.SerializeObject(pdf));
pepe.ConvertToPDF("C:/Users/carlos.velez/Desktop/PdfReteICA.html",parametros);