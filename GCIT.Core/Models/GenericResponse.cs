using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GCIT.Core.Models
{
    public class GenericResponse
    {
        public string r { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public object m { get; set; }
    }

    public class Datos
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string cod_agente { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string agente { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string correo { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string telefono { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string pagina { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string value { get; set; }
    }
}
