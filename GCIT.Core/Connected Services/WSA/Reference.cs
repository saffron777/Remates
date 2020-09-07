//------------------------------------------------------------------------------
// <generado automáticamente>
//     Este código fue generado por una herramienta.
//     //
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </generado automáticamente>
//------------------------------------------------------------------------------

namespace WSA
{
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://WSadaGCIT.com/", ConfigurationName="WSA.WSAdaptadorSoap")]
    public interface WSAdaptadorSoap
    {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://WSadaGCIT.com/ConsultarSaldo", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<string> ConsultarSaldoAsync(string clave, string Clavecliente, string usuario);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://WSadaGCIT.com/ConsultarSaldoBono", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<string> ConsultarSaldoBonoAsync(string clave, string Clavecliente, string usuario);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://WSadaGCIT.com/GenerarTicket", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<string> GenerarTicketAsync(string clave, string Clavecliente, string usuario, decimal monto);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://WSadaGCIT.com/RegistrarMonto", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<string> RegistrarMontoAsync(string clave, string Clavecliente, int tipoTransaccion, string usuario, string monto);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://WSadaGCIT.com/RegistrarMontoDescrip", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<string> RegistrarMontoDescripAsync(string clave, string Clavecliente, int tipoTransaccion, string usuario, string monto, string descripcion);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://WSadaGCIT.com/RegistrarMontoDesWeb", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<string> RegistrarMontoDesWebAsync(string clave, string Clavecliente, int tipoTransaccion, string usuario, string monto, string descripcion, int idusuario, int idcliente, int operacion, string tipoap);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://WSadaGCIT.com/Verificarlogin", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<string> VerificarloginAsync(string clave, string Clavecliente, string usuario, string Claveusuario);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    public interface WSAdaptadorSoapChannel : WSA.WSAdaptadorSoap, System.ServiceModel.IClientChannel
    {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    public partial class WSAdaptadorSoapClient : System.ServiceModel.ClientBase<WSA.WSAdaptadorSoap>, WSA.WSAdaptadorSoap
    {
        
    /// <summary>
    /// Implemente este método parcial para configurar el punto de conexión de servicio.
    /// </summary>
    /// <param name="serviceEndpoint">El punto de conexión para configurar</param>
    /// <param name="clientCredentials">Credenciales de cliente</param>
    static partial void ConfigureEndpoint(System.ServiceModel.Description.ServiceEndpoint serviceEndpoint, System.ServiceModel.Description.ClientCredentials clientCredentials);
        
        public WSAdaptadorSoapClient(EndpointConfiguration endpointConfiguration) : 
                base(WSAdaptadorSoapClient.GetBindingForEndpoint(endpointConfiguration), WSAdaptadorSoapClient.GetEndpointAddress(endpointConfiguration))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public WSAdaptadorSoapClient(EndpointConfiguration endpointConfiguration, string remoteAddress) : 
                base(WSAdaptadorSoapClient.GetBindingForEndpoint(endpointConfiguration), new System.ServiceModel.EndpointAddress(remoteAddress))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public WSAdaptadorSoapClient(EndpointConfiguration endpointConfiguration, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(WSAdaptadorSoapClient.GetBindingForEndpoint(endpointConfiguration), remoteAddress)
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public WSAdaptadorSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress)
        {
        }
        
        public System.Threading.Tasks.Task<string> ConsultarSaldoAsync(string clave, string Clavecliente, string usuario)
        {
            return base.Channel.ConsultarSaldoAsync(clave, Clavecliente, usuario);
        }
        
        public System.Threading.Tasks.Task<string> ConsultarSaldoBonoAsync(string clave, string Clavecliente, string usuario)
        {
            return base.Channel.ConsultarSaldoBonoAsync(clave, Clavecliente, usuario);
        }
        
        public System.Threading.Tasks.Task<string> GenerarTicketAsync(string clave, string Clavecliente, string usuario, decimal monto)
        {
            return base.Channel.GenerarTicketAsync(clave, Clavecliente, usuario, monto);
        }
        
        public System.Threading.Tasks.Task<string> RegistrarMontoAsync(string clave, string Clavecliente, int tipoTransaccion, string usuario, string monto)
        {
            return base.Channel.RegistrarMontoAsync(clave, Clavecliente, tipoTransaccion, usuario, monto);
        }
        
        public System.Threading.Tasks.Task<string> RegistrarMontoDescripAsync(string clave, string Clavecliente, int tipoTransaccion, string usuario, string monto, string descripcion)
        {
            return base.Channel.RegistrarMontoDescripAsync(clave, Clavecliente, tipoTransaccion, usuario, monto, descripcion);
        }
        
        public System.Threading.Tasks.Task<string> RegistrarMontoDesWebAsync(string clave, string Clavecliente, int tipoTransaccion, string usuario, string monto, string descripcion, int idusuario, int idcliente, int operacion, string tipoap)
        {
            return base.Channel.RegistrarMontoDesWebAsync(clave, Clavecliente, tipoTransaccion, usuario, monto, descripcion, idusuario, idcliente, operacion, tipoap);
        }
        
        public System.Threading.Tasks.Task<string> VerificarloginAsync(string clave, string Clavecliente, string usuario, string Claveusuario)
        {
            return base.Channel.VerificarloginAsync(clave, Clavecliente, usuario, Claveusuario);
        }
        
        public virtual System.Threading.Tasks.Task OpenAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginOpen(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndOpen));
        }
        
        public virtual System.Threading.Tasks.Task CloseAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginClose(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndClose));
        }
        
        private static System.ServiceModel.Channels.Binding GetBindingForEndpoint(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.WSAdaptadorSoap))
            {
                System.ServiceModel.BasicHttpBinding result = new System.ServiceModel.BasicHttpBinding();
                result.MaxBufferSize = int.MaxValue;
                result.ReaderQuotas = System.Xml.XmlDictionaryReaderQuotas.Max;
                result.MaxReceivedMessageSize = int.MaxValue;
                result.AllowCookies = true;
                return result;
            }
            if ((endpointConfiguration == EndpointConfiguration.WSAdaptadorSoap12))
            {
                System.ServiceModel.Channels.CustomBinding result = new System.ServiceModel.Channels.CustomBinding();
                System.ServiceModel.Channels.TextMessageEncodingBindingElement textBindingElement = new System.ServiceModel.Channels.TextMessageEncodingBindingElement();
                textBindingElement.MessageVersion = System.ServiceModel.Channels.MessageVersion.CreateVersion(System.ServiceModel.EnvelopeVersion.Soap12, System.ServiceModel.Channels.AddressingVersion.None);
                result.Elements.Add(textBindingElement);
                System.ServiceModel.Channels.HttpTransportBindingElement httpBindingElement = new System.ServiceModel.Channels.HttpTransportBindingElement();
                httpBindingElement.AllowCookies = true;
                httpBindingElement.MaxBufferSize = int.MaxValue;
                httpBindingElement.MaxReceivedMessageSize = int.MaxValue;
                result.Elements.Add(httpBindingElement);
                return result;
            }
            throw new System.InvalidOperationException(string.Format("No se pudo encontrar un punto de conexión con el nombre \"{0}\".", endpointConfiguration));
        }
        
        private static System.ServiceModel.EndpointAddress GetEndpointAddress(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.WSAdaptadorSoap))
            {
                return new System.ServiceModel.EndpointAddress("http://ca02-vm03.sagcit.com/wsadaptador/WsAdaMain.asmx");
            }
            if ((endpointConfiguration == EndpointConfiguration.WSAdaptadorSoap12))
            {
                return new System.ServiceModel.EndpointAddress("http://ca02-vm03.sagcit.com/wsadaptador/WsAdaMain.asmx");
            }
            throw new System.InvalidOperationException(string.Format("No se pudo encontrar un punto de conexión con el nombre \"{0}\".", endpointConfiguration));
        }
        
        public enum EndpointConfiguration
        {
            
            WSAdaptadorSoap,
            
            WSAdaptadorSoap12,
        }
    }
}
