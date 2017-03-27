using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WCFServicioWebPQR
{
    [DataContract]
    public class BaseRespuesta
    {
        [DataMember]
        public string MensajeRespuestaOk { get; set; }
        [DataMember]
        public string MensajeRespuestaError { get; set; }
    }
}