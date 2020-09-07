using CGIT.Core.Logging;
using CGIT.Core.Models;
using CGIT.Core.Utilities;
using GCIT.Core.Entities;
using GCIT.Core.Models;
using GCIT.Core.Modules.Remates.Interface;
using GCIT.Core.Repository.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GCIT.Core.Modules.Remates.Implementations
{
    public class RematesService : IRematesService
    {
        
        private readonly ITransactionsRepository _transactionsRepository;
        private readonly IUserCredentialsRepository _userCredentialsRepository;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _accessor;

        public RematesService(IHttpContextAccessor accessor, IConfiguration configuration, ITransactionsRepository transactionsRepository, IUserCredentialsRepository userCredentialsRepository)
        {
            _configuration = configuration;
            _transactionsRepository = transactionsRepository;
            _userCredentialsRepository = userCredentialsRepository;
            _accessor = accessor;

            Utils.settings = new AppSettings
            {
                usuario = _configuration.GetSection("appSettings").GetSection("usuario").Value,
                clave = _configuration.GetSection("appSettings").GetSection("clave").Value,
                clavecliente = _configuration.GetSection("appSettings").GetSection("clavecliente").Value,
                password = _configuration.GetSection("appSettings").GetSection("password").Value,
                SecurityKey = _configuration.GetSection("appSettings").GetSection("SecurityKey").Value,
                urlLoginGCIT = _configuration.GetSection("appSettings").GetSection("urlLoginGCIT").Value,
                urlWSA = _configuration.GetSection("appSettings").GetSection("urlWSA").Value,
            };
        }

        public GenericResponse DatosUsuario(string apikey, string usuario)
        {
            var result = new GenericResponse();

            var json = $"apikey={apikey}&usuario={usuario}";
            
            try
            {
                Log.Info("DatosUsuario started...");

                if (string.IsNullOrEmpty(apikey) || string.IsNullOrEmpty(usuario))
                    throw new Exception("Los parametros no pueden estar vacios");

                Expression<Func<UserCredentials, bool>> expression = exp => exp.ApiKey == apikey && exp.Active;
                var credential = _userCredentialsRepository.Get(expression).SingleOrDefault();

                if (credential == null)
                    throw new Exception("Apikey no coincide");

                LoginResponse login = Utils.GetLogin(usuario);

                if (login != null && login.Existe != "0")
                {

                    RegisterTransactions(new TransactionModel
                    {
                        Action = "datos_de_usuario",
                        IP = _accessor.HttpContext?.Connection?.RemoteIpAddress?.ToString(),
                        PlayerId = int.Parse(login.IDdeCliente),
                        User = login.NombreUser,
                        Date = DateTime.Now,
                        JsonRequest = json,
                        PlayerBalance = decimal.Parse(login.BalanceDisponible.Replace('.', ',')),
                        RestMethod = "200",
                        Description = "OK",

                    });

                    result = new GenericResponse
                    {
                        r = "1",
                        m = new Datos
                        {
                            cod_agente = login.IdAgente,
                            agente = login.Agente,
                            correo = login.NombreUser + "@remate.com",// login.CorreoDeCliente,
                            telefono = login.Telefono,
                            pagina = "Cordialito",
                        }
                    };
                }
                else
                {
                    throw new Exception("Usuario no existe");
                }
            }
            catch (System.Exception ex)
            {

                Log.Error(ex.Message);
                RegisterTransactions(new TransactionModel
                {
                    Action = "datos_de_usuario",
                    IP = _accessor.HttpContext?.Connection?.RemoteIpAddress?.ToString(),
                    PlayerId = -1,
                    User = usuario,
                    Date = DateTime.Now,
                    JsonRequest = json,                    
                    RestMethod = "FAILED",
                    Description = ex.Message,

                });
                result = Utils.ErrorResponse(ex);
            }

            Log.Info("DatosUsuario End...");

            return result;
        }

        public GenericResponse GetToken(string usuario)
        {
            var result = new GenericResponse();

            var json = $"usuario={usuario}";

            try
            {
                Log.Info("GetToken started...");

                if (/*string.IsNullOrEmpty(apikey) || */string.IsNullOrEmpty(usuario))
                    throw new Exception("Los parametros no pueden estar vacios");

                //Expression<Func<UserCredentials, bool>> expression = exp => exp.ApiKey == apikey && exp.Active;
                //var credential = _userCredentialsRepository.Get(expression).SingleOrDefault();

                //if (credential == null)
                //    throw new Exception("Apikey no coincide");

                LoginResponse login = Utils.GetLogin(usuario);

                if (login != null && login.Existe != "0")
                {
                    var newToken = Guid.NewGuid();

                    RegisterTransactions(new TransactionModel
                    {
                        Action = "gettoken",
                        IP = _accessor.HttpContext?.Connection?.RemoteIpAddress?.ToString(),
                        PlayerId = int.Parse(login.IDdeCliente),
                        User = login.NombreUser,
                        Token = newToken.ToString(),
                        Date = DateTime.Now,
                        JsonRequest = json,
                        PlayerBalance = decimal.Parse(login.BalanceDisponible.Replace('.', ',')),
                        RestMethod = "200",
                        Description = "OK",
                        Transaction = Utils.ConvertToTimestamp(DateTime.Now).ToString(),

                    });

                    result = new GenericResponse
                    {
                        r = "1",
                        m = newToken.ToString()
                        //m = new Datos
                        //{
                        //   value = newToken.ToString()
                        //}
                    };
                }
                else
                {
                    throw new Exception("Usuario no existe");
                }

            }
            catch (System.Exception ex)
            {

                Log.Error(ex.Message);
                RegisterTransactions(new TransactionModel
                {
                    Action = "gettoken",
                    IP = _accessor.HttpContext?.Connection?.RemoteIpAddress?.ToString(),
                    PlayerId = -1,
                    User = usuario,
                    Date = DateTime.Now,
                    JsonRequest = json,
                    RestMethod = "FAILED",
                    Description = ex.Message,

                });
                result = Utils.ErrorResponse(ex);
            }

            Log.Info("GetToken End...");

            return result;
        }

        public void RegisterTransactions(TransactionModel data)
        {
            try
            {

                var logs = new Transactions
                {
                    PlayerId = data.PlayerId,
                    User = data.User,
                    Token = data.Token ?? string.Empty,
                    IP = data.IP,
                    Transaction = data.Transaction,
                    Date = data.Date,
                    PlayerBalance = data.PlayerBalance,
                    Amount = data.Amount,
                    Action = data.Action,
                    Description = data.Description,
                    JsonRequest = data.JsonRequest,
                    RestMethod = data.RestMethod,
                    GameId = data.GameId
                };

                _transactionsRepository.Create(logs);
                _transactionsRepository.Commit();



            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<GenericResponse> TransferenciaGcit(string apikey, string usuario, decimal? monto)
        {
            var result = new GenericResponse();

            var json = $"apikey={apikey}&usuario={usuario}&monto={monto.ToString()}";

            try
            {
                Log.Info("TransferenciaGcit started...");

                if (string.IsNullOrEmpty(apikey) || string.IsNullOrEmpty(usuario) || monto==null || (monto!=null  && monto <= 0))
                    throw new Exception("Los parametros no pueden estar vacios");

                Expression<Func<UserCredentials, bool>> expression = exp => exp.ApiKey == apikey && exp.Active;
                var credential = _userCredentialsRepository.Get(expression).SingleOrDefault();

                if (credential == null)
                    throw new Exception("Apikey no coincide");

                LoginResponse login = Utils.GetLogin(usuario);

                int codigoTrans = 1;

                if (login != null && login.Existe != "0")
                {

                    //decimal balance = decimal.Parse(login.BalanceDisponible.Replace('.', ','));

                    //if(balance <=0)
                    //    throw new Exception("El usuario no posee balance suficiente");

                    var trans = await Utils.RegistrarMonto(monto.ToString(), codigoTrans, login.CuentaCliente, "Debito fondos");

                    int.TryParse(trans.ToString(), out int numtransaction);

                    if (numtransaction <= 0)
                        throw new Exception("No se pudo realizar la transaccion.");

                    RegisterTransactions(new TransactionModel
                    {
                        Action = "transferencia_gcit",
                        IP = _accessor.HttpContext?.Connection?.RemoteIpAddress?.ToString(),
                        PlayerId = int.Parse(login.IDdeCliente),
                        User = login.NombreUser,
                        Date = DateTime.Now,
                        JsonRequest = json,
                         Amount = monto,
                        PlayerBalance = decimal.Parse(login.BalanceDisponible.Replace('.', ',')),
                        RestMethod = "200",
                        Description = "OK",
                        Transaction = numtransaction.ToString(),

                    });

                    result = new GenericResponse
                    {
                        r = "1",
                        m = ""
                        //m = new Datos
                        //{                            
                        //}
                    };
                }
                else
                {
                    throw new Exception("Usuario no existe");
                }

            }
            catch (System.Exception ex)
            {

                Log.Error(ex.Message);
                RegisterTransactions(new TransactionModel
                {
                    Action = "transferencia_gcit",
                    IP = _accessor.HttpContext?.Connection?.RemoteIpAddress?.ToString(),
                    PlayerId = -1,
                    User = usuario,
                    Date = DateTime.Now,
                    JsonRequest = json,
                    RestMethod = "FAILED",
                    Description = ex.Message,

                });
                result = Utils.ErrorResponse(ex);
            }

            Log.Info("TransferenciaGcit end...");
            return result;
        }

        public async Task<GenericResponse> TransferenciaRemate(string apikey, string usuario, decimal? monto)
        {
            var result = new GenericResponse();

            var json = $"apikey={apikey}&usuario={usuario}&monto={monto.ToString()}";

            try
            {
                Log.Info("TransferenciaRemate started...");

                if (string.IsNullOrEmpty(apikey) || string.IsNullOrEmpty(usuario) || monto == null || (monto != null && monto <= 0))
                    throw new Exception("Los parametros no pueden estar vacios");

                Expression<Func<UserCredentials, bool>> expression = exp => exp.ApiKey == apikey && exp.Active;
                var credential = _userCredentialsRepository.Get(expression).SingleOrDefault();

                if (credential == null)
                    throw new Exception("Apikey no coincide");

                LoginResponse login = Utils.GetLogin(usuario);

                int codigoTrans = 2;

                if (login != null && login.Existe != "0")
                {

                    decimal balance = decimal.Parse(login.BalanceDisponible.Replace('.', ','));

                    if (balance <= 0)
                        throw new Exception("El usuario no posee balance suficiente");

                    var trans = await Utils.RegistrarMonto(monto.ToString(), codigoTrans, login.CuentaCliente, "Credito fondos");

                    int.TryParse(trans.ToString(), out int numtransaction);

                    if (numtransaction <= 0)
                        throw new Exception("No se pudo realizar la transaccion.");

                    RegisterTransactions(new TransactionModel
                    {
                        Action = "transferencia_remate",
                        IP = _accessor.HttpContext?.Connection?.RemoteIpAddress?.ToString(),
                        PlayerId = int.Parse(login.IDdeCliente),
                        User = login.NombreUser,
                        Date = DateTime.Now,
                        JsonRequest = json,
                        Amount = monto,
                        PlayerBalance = decimal.Parse(login.BalanceDisponible.Replace('.', ',')),
                        RestMethod = "200",
                        Description = "OK",
                        Transaction = numtransaction.ToString(),

                    });

                    result = new GenericResponse
                    {
                        r = "1",
                        m = ""
                        //m = new Datos
                        //{
                        //}
                    };
                }
                else
                {
                    throw new Exception("Usuario no existe");
                }

            }
            catch (System.Exception ex)
            {

                Log.Error(ex.Message);
                RegisterTransactions(new TransactionModel
                {
                    Action = "transferencia_remate",
                    IP = _accessor.HttpContext?.Connection?.RemoteIpAddress?.ToString(),
                    PlayerId = -1,
                    User = usuario,
                    Date = DateTime.Now,
                    JsonRequest = json,
                    RestMethod = "FAILED",
                    Description = ex.Message,

                });
                result = Utils.ErrorResponse(ex);
            }

            Log.Info("TransferenciaRemate end...");
            return result;
        }

        public GenericResponse VerificacionToken(string apikey, string usuario, string token)
        {
            var result = new GenericResponse();

            var json = $"apikey={apikey}&usuario={usuario}&token={token}";

            try
            {
                Log.Info("VerificacionToken started...");

                if (string.IsNullOrEmpty(apikey) || string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(token))
                    throw new Exception("Los parametros no pueden estar vacios");

                Expression<Func<UserCredentials, bool>> expression = exp => exp.ApiKey == apikey && exp.Active;
                var credential = _userCredentialsRepository.Get(expression).SingleOrDefault();

                if (credential == null)
                    throw new Exception("Apikey no coincide");

                Expression<Func<Transactions, bool>> expressionT = exp => exp.Token == token;
                var transaction = _transactionsRepository.Get(expressionT).SingleOrDefault();

                if (transaction == null)
                    throw new Exception("Token no coincide");

                LoginResponse login = Utils.GetLogin(usuario);

                if (login != null && login.Existe != "0")
                {
                    var tsp = transaction.Date;//  Utils.UnixTimeStampToDateTime(double.Parse(transaction.Transaction));

                    var diffts = (DateTime.Now - tsp).TotalSeconds;                                        

                    var ts = Utils.ConvertToTimestamp(DateTime.Now).ToString();



                    if (diffts > 60)
                    {
                        throw new Exception("Token ha expirado");
                    }

                    RegisterTransactions(new TransactionModel
                    {
                        Action = "verificacion_token",
                        IP = _accessor.HttpContext?.Connection?.RemoteIpAddress?.ToString(),
                        PlayerId = int.Parse(login.IDdeCliente),
                        User = login.NombreUser,                        
                        Date = DateTime.Now,                        
                        JsonRequest = json,
                        PlayerBalance = decimal.Parse(login.BalanceDisponible.Replace('.', ',')),
                        RestMethod = "200",
                        Description = "OK",
                        Transaction = ts,

                    });

                    result = new GenericResponse
                    {
                        r = "1",
                        m = diffts.ToString()// transaction.Transaction
                        //m = new Datos
                        //{
                        //    value = transaction.Transaction
                        //}
                    };
                }
                else
                {
                    throw new Exception("Usuario no existe");
                }
            }
            catch (System.Exception ex)
            {

                Log.Error(ex.Message);
                RegisterTransactions(new TransactionModel
                {
                    Action = "verificacion_token",
                    IP = _accessor.HttpContext?.Connection?.RemoteIpAddress?.ToString(),
                    PlayerId = -1,
                    User = usuario,
                    Date = DateTime.Now,
                    JsonRequest = json,
                    RestMethod = "FAILED",
                    Description = ex.Message,

                });
                result = Utils.ErrorResponse(ex);
            }

            Log.Info("VerificacionToken end...");
            return result;
        }
    }
}
