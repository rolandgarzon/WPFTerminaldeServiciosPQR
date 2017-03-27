using System.Runtime.Serialization;


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