// See https://aka.ms/new-console-template for more information

using htmlToImage;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;


string appPath = @"Desktop\file.pdf";
string basePath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
string fullPath = System.IO.Path.Combine(basePath, appPath);



HtmlConvert pepe = new HtmlConvert();
PDF pdf = new PDF();

HtmlConvert parametros = new HtmlConvert();
parametros.Parametros = JObject.Parse(JsonConvert.SerializeObject(pdf));

//List<byte[]> files = new List<byte[]>();
byte[] files;
//byte[] result = null;
//string resultPDF = null;

Console.WriteLine("escriba la ruta del archivo");
string ruta = Console.ReadLine();

files = pepe.ConvertToPDF(ruta, parametros);



FileStream stream = new FileStream(fullPath, FileMode.CreateNew);
BinaryWriter writer = new BinaryWriter(stream);
writer.Write(files, 0, files.Length);
writer.Close();

Process.Start(@"C:\Program Files (x86)\Microsoft\Edge\Application\msedge.exe", fullPath);

//result = files;
//resultPDF = Convert.ToBase64String(result) + "split" + Convert.ToBase64String(files[1]);
//resultPDF = Convert.ToBase64String(result);
//resultPDF.Replace("\"", "");
//Console.WriteLine(resultPDF);


