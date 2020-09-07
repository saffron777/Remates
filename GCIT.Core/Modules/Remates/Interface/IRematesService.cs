using GCIT.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GCIT.Core.Modules.Remates.Interface
{
    public interface IRematesService
    {
        GenericResponse DatosUsuario(string apikey, string usuario);

        GenericResponse VerificacionToken(string apikey, string usuario, string token);

        Task<GenericResponse> TransferenciaGcit(string apikey, string usuario, decimal? monto);

        Task<GenericResponse> TransferenciaRemate(string apikey, string usuario, decimal? monto);

        GenericResponse GetToken(string usuario);
        void RegisterTransactions(TransactionModel data);
    }
}
