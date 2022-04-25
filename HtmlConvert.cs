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
            Image img = converter.ConvertHtmlString(body, ""); 
            byte[] imgBytes = (byte[])(new ImageConverter()).ConvertTo(img, typeof(byte[]));
            return imgBytes;
        }

        public byte[] GeneratePDF(string body)
        {
            PdfPageSize pageSize = PdfPageSize.Letter11x17;
            

            // (PdfPageSize)Enum.Parse(typeof(PdfPageSize), "Letter11x17", true);
            PdfPageOrientation pdfOrientation = (PdfPageOrientation)Enum.Parse(typeof(PdfPageOrientation), "Portrait", true);
            HtmlToPdf converter = new HtmlToPdf();
            //converter.Options.PdfPageSize = pageSize;
            //converter.Options.PdfPageOrientation = pdfOrientation;
            //converter.Options.WebPageWidth = 1024;
            //converter.Options.MarginTop = 20;
            //converter.Options.MarginLeft = 20;
            //converter.Options.MarginRight = 20;
            converter.Options.PdfPageSize = pageSize;
            converter.Options.PdfPageOrientation = pdfOrientation;
            converter.Options.WebPageWidth = 1920;
            converter.Options.MarginTop = 20;
            converter.Options.MarginLeft = 20;
            converter.Options.MarginRight = 20;
            converter.Options.AutoFitWidth = HtmlToPdfPageFitMode.AutoFit;
            PdfDocument doc = converter.ConvertHtmlString(body, "");

            byte[] pdfBytes = doc.Save();
            return pdfBytes;
        }

        public string ReplaceData(string body, JObject obj)
        {
            string text = body.Substring(body.IndexOf("<body>"), body.Length - body.IndexOf("<body>"));

            List<string> replace = new List<string>();
            while (text.IndexOf("{") > -1)
            {
                int start = text.IndexOf("{");
                int end = text.IndexOf("}");
                var elemento = text.Substring(start + 1, end - start - 1);
                replace.Add(elemento);
                text = text.Substring(end + 1, text.Length - end - 1);
            }
            //Reemplaza cuerpo PDF
            string objPdf = JsonConvert.SerializeObject(obj,
                 new JsonSerializerSettings
                 {
                     NullValueHandling = NullValueHandling.Ignore
                 });
            JObject objDt = JObject.Parse(objPdf);

            string total = "", fecha = "", factura = "", EAN = "", doc = "", FechaDeclarado = "";
            fecha = objDt.Property("FechaMaxima").Value.ToString();
            //factura = objDt.Property("NroRadicado").Value.ToString();
            // doc = objDt.Property("NITM").Value.ToString() == "89118000-1" ? objDt.Property("DocRepresentante").Value.ToString() : objDt.Property("Identificacion").Value.ToString();
            doc = objDt.Property("Identificacion").Value.ToString();
            total = objDt.Property("TotalAPagar").Value.ToString();
            EAN = objDt.Property("EAN").Value.ToString();
            FechaDeclarado = objDt.Property("FechaDeclarado").Value.ToString();
            body = body.Replace("{FechaDeclarado}", objDt.Property("FechaDeclarado").Value.ToString());

            foreach (var c in replace)
            {
                //sSegundoNombre = objDeclarante1.Property("SegundoNombre").Value.ToString(),
                foreach (JProperty p in obj.Properties())
                {
                    if (p.Name.Replace("\"", "") == c.Trim())
                    {
                        string old = "{" + c.Trim() + "}", newS = p.Value.ToString();
                        body = body.Replace(old, newS);
                    }
       
                }
            }

            body = body.Replace("{Code}", CodigoBarras(total, Convert.ToDateTime(fecha).ToString("yyyyMMdd"), factura + doc, EAN));
            return body;
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

        public List<byte[]> ConvertToPDF(String path, HtmlConvert parametros)
        {
            JObject obj = parametros.Parametros;
            string body = ReadFile(path);
            body = ReplaceData(body, obj);
            byte[] pdf = GeneratePDF(body);
            byte[] img = GenerateIMG(body);
            return new List<byte[]> { pdf, img };
        }

        public string CodigoBarras(string valorFactura, string fecha, string factura, string codigoBarras)
        {
            string CodeBar = "";
            int j, lengthE;
            int ma = 0, me = 999999999;
            if (!String.IsNullOrEmpty(codigoBarras))
            {

                for (j = 0; j < 50; j++)
                {
                    Linear barcode = new Linear();
                    barcode.Type = BarcodeType.EAN128;
                    barcode.Data = "(415)" + codigoBarras + "(8020)" + factura.Replace(".", "").PadLeft(8, '0') + "(3900)" + valorFactura.Replace(",", "").Replace(".", "").PadLeft(10, '0') + "(96)" + fecha;
                    barcode.UOM = UnitOfMeasure.PIXEL;
                    barcode.BarWidth = 2;
                    barcode.TopMargin = 10;
                    barcode.TextFont = new Font("Arial", 16, FontStyle.Bold);
                    byte[] base64SingleBytes = barcode.drawBarcodeAsBytes();
                    lengthE = base64SingleBytes.GetLength(0);

                    if (lengthE > ma) { ma = lengthE; }
                    if (lengthE < me) { me = lengthE; }

                    if (ma != lengthE)
                    {
                        if (me < ma)
                        {
                            byte[] IMG = base64SingleBytes;
                            CodeBar = string.Format("data:image/png;base64," + Convert.ToBase64String(IMG));
                            break;
                        }
                    }
                }
            }
            return CodeBar;
        }
    }
}
