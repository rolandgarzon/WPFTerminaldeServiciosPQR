using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WCFServicioWebPQR
{
    public class DatosCuentaCobro: BaseRespuesta
    {

        [DataMember]
        public long nuIdSuscriptor { get; set; }

        [DataMember]
        public string vaNombre { get; set; }

        [DataMember]
        public double nuSaldoPendiente { get; set; }

        [DataMember]
        public double valortotalfacturado { get; set; }

        [DataMember]
        public DateTime daFechaGeneracion { get; set; }

        [DataMember]
        public long nuIdCuentaCobro { get; set; }

        [DataMember]
        public int nuIdPais { get; set; }

        [DataMember]
        public int nuIdDepartamento { get; set; }

        [DataMember]
        public int nuIdMunicipio { get; set; }

        [DataMember]
        public string nuIdUsuario { get; set; }

        [DataMember]
        public string nuIdMaquina { get; set; }


    }
}