using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CGIT.Core.Logging;
using CGIT.Core.Utilities;
using GCIT.Core.Models;
using GCIT.Core.Modules.Remates.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GCIT.Remates.WebApi.Controllers
{
    
    [ApiController]
    public class RematesController : ControllerBase
    {
        private readonly IRematesService _rematesService;

        public RematesController(IRematesService rematesService)
        {
            _rematesService = rematesService;
        }

        [HttpGet]
        [Route("api/Wallet")]        
        public async Task<GenericResponse> ExcecuteAction(string apikey, string usuario, string funcion, decimal? monto, string token )
        {
            GenericResponse result = null;

            try
            {
                switch (funcion.ToLower())
                {
                    case "gettoken":
                        result = _rematesService.GetToken(usuario);
                        break;
                    case "datos_de_usuario":
                        result = _rematesService.DatosUsuario(apikey, usuario);
                        break;
                    case "verificacion_token":
                        result = _rematesService.VerificacionToken(apikey, usuario, token);
                        break;
                    case "transferencia_gcit":
                        result = await _rematesService.TransferenciaGcit(apikey, usuario, monto);
                        break;
                    case "transferencia_remate":
                        result = await _rematesService.TransferenciaRemate(apikey, usuario, monto);
                        break;
                    default:
                        throw new Exception("Illegal method call.");
                }
                return result;
            }
            catch (Exception ex)
            {

                try
                {
                    return Utils.ErrorResponse( ex);
                }
                catch (Exception e)
                {
                    Log.Error(e.Message);
                    throw;
                }
            }
        }
    }
}