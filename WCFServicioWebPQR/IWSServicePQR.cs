using System;
using System.Collections.Generic;
using System.ServiceModel;


namespace WCFServicioWebPQR
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IWSServicePQR
    {

        [OperationContract]
        List<DatosSuscriptor> obtenerDatosSuscriptor(long nuIdsuscriptor);

        [OperationContract]
        List<DatosSuscriptor> obtenerSuscriptorPazySalvo(long nuIdsuscriptor);

        [OperationContract]
        List<DatosCuentaCobro> obtenerDatosCuentaCobro(long nuIdsuscriptor);

        [OperationContract]
        List<DatosCuponPago> obtenerDatosCuponPago(long nuIdcuponpago);

        [OperationContract]
        Boolean obtenerEstadoFacturacion(long nuIdcuponpago);

        [OperationContract]
        List<DatosCuponPago> generarDatosCuponPago(long nuIdSuscriptor, int nuValorCuponPago, long nuIdCuentaCobro);

    }

}
