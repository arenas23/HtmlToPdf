using BarcodeLib.Barcode;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SelectPdf;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace htmlToImage
{
    public class HtmlConvert
    {
        //public HtmlConvert(string codigoMunicipio, string nombreArchivo, int referencia, int almacena, dynamic parametros)
        //{
        //    CodigoMunicipio = codigoMunicipio;
        //    NombreArchivo = nombreArchivo;
        //    Referencia = referencia;
        //    Almacena = almacena;
        //    Parametros = parametros;
        //}


        public string CodigoMunicipio { get; set; }
        //public int Id_TipoAutoliquidable { get; set; }
        public string NombreArchivo { get; set; }
        public int Referencia { get; set; }
        public int Almacena { get; set; } //0-Previsualiza,1-Almacena
        public dynamic Parametros { get; set; }

        public byte[] GenerateIMG(string body)
        {
            HtmlToImage converter = new HtmlToImage();
            converter.WebPageWidth = 600;
            converter.WebPageHeight = 1000;
            Image img = converter.ConvertHtmlString(body); 
            byte[] imgBytes = (byte[])(new ImageConverter()).ConvertTo(img, typeof(byte[]));
            return imgBytes;
        }

        public byte[] GeneratePDF(string body)
        {
            PdfPageSize pageSize = PdfPageSize.A4;
            


            PdfPageOrientation pdfOrientation = (PdfPageOrientation)Enum.Parse(typeof(PdfPageOrientation), "Portrait", true);
            HtmlToPdf converter = new HtmlToPdf();
            
            converter.Options.PdfPageSize = pageSize;

            converter.Options.PdfPageOrientation = pdfOrientation;
            converter.Options.WebPageWidth = 1920;
            //converter.Options.WebPageWidth = 900;

            converter.Options.MarginTop = 20;
            converter.Options.MarginLeft = 20;
            converter.Options.MarginRight = 20;

            
     
            PdfDocument doc = converter.ConvertHtmlString(body, "");

            byte[] pdfBytes = doc.Save();
            return pdfBytes;
        }


        public string ReadFile(string path)
        {
            string body = null;
            FileStream Archivo = File.Open(path, FileMode.Open);
            using (StreamReader reader = new StreamReader(Archivo))
            {
                body = reader.ReadToEnd();
            }
            return body;
        }

        public byte[] ConvertToPDF(String path, HtmlConvert parametros)
        {
            JObject obj = parametros.Parametros;
            string body = ReadFile(path);
            //body = ReplaceData(body, obj);
            byte[] pdf = GeneratePDF(body);
            //byte[] img = GenerateIMG(body);
           // return new List<byte[]> { pdf, img};
            return pdf;
        }
    }
}
