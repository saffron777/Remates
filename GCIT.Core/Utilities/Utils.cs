using CGIT.Core.Models;
using GCIT.Core.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using WSA;
//using System.Web.Script.Serialization;

namespace CGIT.Core.Utilities
{
    public static class Utils
    {
        private static readonly DateTime Epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        //public static IOptions<AppSettings> settings;
        public static AppSettings settings;
        public static long ConvertToTimestamp(DateTime value)
        {
            TimeSpan elapsedTime = value - Epoch;
            return (long)elapsedTime.TotalSeconds;
        }

        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }

        public static LoginResponse GetLogin(int? pid)
        {

            //logsWriter("Inicio Login");
            string urllogin = settings.urlLoginGCIT;// ConfigurationManager.AppSettings["urlLoginGCIT"];
            string usr = settings.usuario;// ConfigurationManager.AppSettings["usuario"];
            string pwd = settings.password;// ConfigurationManager.AppSettings["password"];

            string url = $"{urllogin}?username={usr}&password={pwd}&idusuario={pid}";

            var client = new RestClient(url);

            var request = new RestRequest("", Method.GET);

            var respuestaXml = client.Execute(request).Content;
            var serializer = new XmlSerializer(typeof(LoginResponse));
            LoginResponse result;


            using (TextReader reader = new StringReader(respuestaXml))
            {
                result = (LoginResponse)serializer.Deserialize(reader);
            }


            return result;
        }

        public static LoginResponse GetLogin(string login)
        {

            //logsWriter("Inicio Login");
            string urllogin = settings.urlLoginGCIT;// ConfigurationManager.AppSettings["urlLoginGCIT"];
            string usr = settings.usuario;// ConfigurationManager.AppSettings["usuario"];
            string pwd = settings.password;// ConfigurationManager.AppSettings["password"];

            string url = $"{urllogin}?username={usr}&password={pwd}&login={login}";

            var client = new RestClient(url);

            var request = new RestRequest("", Method.GET);

            var respuestaXml = client.Execute(request).Content;
            var serializer = new XmlSerializer(typeof(LoginResponse));
            LoginResponse result;


            using (TextReader reader = new StringReader(respuestaXml))
            {
                result = (LoginResponse)serializer.Deserialize(reader);
            }


            return result;
        }

        public async static Task<string> SaldoDisponible(string usuario)
        {

            WSAdaptadorSoapClient client = new WSAdaptadorSoapClient( WSAdaptadorSoapClient.EndpointConfiguration.WSAdaptadorSoap);
            
             string response = await client.ConsultarSaldoAsync(settings.clave, settings.clavecliente, usuario);


            return response;
            
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="monto"></param>
        ///// <param name="tipoTransaccion">1 - Deposito, 2 - Retiro, 3 - Apuesta Ganadora, 4 - Apuesta perdedora, 5 - Bono </param>
        ///// <param name="usuario"></param>
        ///// <returns></returns>
        public async static Task<int> RegistrarMonto(string monto, int tipoTransaccion, string usuario, string msg = "")
        {
            WSAdaptadorSoapClient client = new WSAdaptadorSoapClient(WSAdaptadorSoapClient.EndpointConfiguration.WSAdaptadorSoap);
            
                string response = await client.RegistrarMontoDescripAsync(settings.clave, settings.clavecliente, tipoTransaccion, usuario, monto, $"LiveBetting - {msg}");

                int trans = int.Parse(response);

                return trans;
            
        }

        public static GenericResponse ErrorResponse(Exception ex)
        {
            GenericResponse result;

            result = new GenericResponse
            {
                 r = "0",
                 m = ex.Message
                //m = new Datos { value = ex.Message }
            };
            return result;
        }

    }

    
}


