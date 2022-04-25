using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace htmlToImage
{
    public class PDF
    {
        public string PeriodoBogota { get; set; }
        public string Anio { get; set; }
        public string FechaMaxima { get; set; }
        public string Radicacion { get; set; }//RadicadoCorrige
        public string NumeroDeclaracionCorrige { get; set; }//RadicadoCorrige
        public string FechaElaboracion { get; set; } //FechaRadicadoCorrige
        public string TotalIngresosOrdinarios { get; set; }
        public string Sobretasa { get; set; }
        public string TotalImpuestosACargo { get; set; }
        public string TotalImpuestosCargo { get; set; }
        public string Retenciones { get; set; }
        public string Retencion { get; set; }
        public string ValorAPagar { get; set; }
        public string InteresesMora { get; set; }
        public string TotalAPagar { get; set; }/**/
        public string TotalDeducciones { get; set; }
        public string RazonSocial { get; set; }
        public string Identificacion { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public string FechaDeclarado { get; set; }
        public string EAN { get; set; }

        public PDF(string periodoBogota, string anio, string fechaMaxima, string radicacion, string numeroDeclaracionCorrige, string fechaElaboracion, string totalIngresosOrdinarios, string sobretasa, string totalImpuestosACargo, string totalImpuestosCargo, string retenciones, string retencion, string valorAPagar, string interesesMora, string totalAPagar, string totalDeducciones, string razonSocial, string identificacion, string telefono, string correo,string fechaDeclarado,string ean)
        {
            PeriodoBogota = periodoBogota;
            Anio = anio;
            FechaMaxima = fechaMaxima;
            Radicacion = radicacion;
            NumeroDeclaracionCorrige = numeroDeclaracionCorrige;
            FechaElaboracion = fechaElaboracion;
            TotalIngresosOrdinarios = totalIngresosOrdinarios;
            Sobretasa = sobretasa;
            TotalImpuestosACargo = totalImpuestosACargo;
            TotalImpuestosCargo = totalImpuestosCargo;
            Retenciones = retenciones;
            Retencion = retencion;
            ValorAPagar = valorAPagar;
            InteresesMora = interesesMora;
            TotalAPagar = totalAPagar;
            TotalDeducciones = totalDeducciones;
            RazonSocial = razonSocial;
            Identificacion = identificacion;
            Telefono = telefono;
            Correo = correo;
            FechaDeclarado = fechaDeclarado;
            EAN = ean;
        }



        //public PDF GetDataFactPreferencial(int id, IDbConnection db)
        //{
        //    string readSp = "GenerarDataPDFFacturaPreferencial";
        //    //[dbo].[GenerarDataPDFFacturaPreferencial]
        //    //string readSp = "GenerarDataPDFFacturaPreferencialPivot";
        //    var queryParameters = new DynamicParameters();
        //    queryParameters.Add("@Id_Declaracion", id);

        //    //queryParameters.Add("@IdDeclaracion", id);
        //    return db.Query<PDF>(readSp, queryParameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
        //}

    }
}
