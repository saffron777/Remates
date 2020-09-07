using System;
using System.Collections.Generic;
using System.Text;

namespace GCIT.Core.Models
{
    public class GenericRequest
    {
        public string apikey { get; set; }
        public string usuario { get; set; }
        public string funcion { get; set; }
        public decimal? monto { get; set; }
        public string token { get; set; }
    }
}
