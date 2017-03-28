﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
//WebServicePQR
namespace WCFServicioWebPQR
{
    public class Service1 : IWSServicePQR
    {
        /// <summary>
        /// metodo que conulta los datos de un cupon de pago
        /// </summary>
        /// el parametro de ingreso es el numero de cupon
        /// <param name="nuIdcuponpago"></param>
        /// retorna:
        /// numero de cupon
        /// valor
        /// fecha de generacion
        /// estado del cupon
        /// <returns></returns>
        public List<DatosCuponPago> obtenerDatosCuponPago(long nuIdcuponpago)
        {
            List<DatosCuponPago> ConsultaCuponPago = new List<DatosCuponPago>();

            System.Data.DataTable dtDatosBasicos = new DataTable();
            int nuIdPais = 57; int nuIdDepartamento = 76; int nuIdMunicipio = 834;
            string lugartrabajo = nuIdPais.ToString() + nuIdDepartamento.ToString() + nuIdMunicipio.ToString();
            int numerolugartrabajo = lugartrabajo.Length;
            string vaNumeroCuponPago = lugartrabajo + nuIdcuponpago;
            double numeroCuponpago = Convert.ToDouble(vaNumeroCuponPago);
            DatosCuponPago DatosCuponPago = new DatosCuponPago();
            try
            {
                using (SiewebDBCommand cmdDatosBasicos = new SiewebDBCommand())
                {
                    cmdDatosBasicos.QueryString = "select SubStr(cm_cuponpago.idcuponpago,8)cuponpago, cm_cuponpago.valor valor, SubStr(cm_suscriptor.idsuscriptor,8)suscriptor, trim(cm_suscriptor.nombre)||' '||trim(cm_suscriptor.apellido) nombrecompleto,cm_suscriptor.saldopendiente saldopte,cm_suscriptor.saldoafavor,cm_cuponpago.idestadocupon idestado,cm_estadocupon.descripcion estado, cm_tipocuponpago.descripcion tipo,cm_cuponpago.fechageneracion fechagen ";
                    cmdDatosBasicos.QueryString = cmdDatosBasicos.QueryString + " ,nvl(cm_factura.valortotalfacturado,0) valortotalfacturado, nvl(cm_factura.valorbienesservicios,0) valorbienesservicios, nvl(cm_factura.valortotalfacturadoglobal,0) valortotalfacturadoglobal ";
                    cmdDatosBasicos.QueryString = cmdDatosBasicos.QueryString + " from cm_cuentacobro LEFT OUTER JOIN cm_factura ";
                    cmdDatosBasicos.QueryString = cmdDatosBasicos.QueryString + "                     ON cm_factura.idcuentacobro = cm_cuentacobro.idcuentacobro ";
                    cmdDatosBasicos.QueryString = cmdDatosBasicos.QueryString + "                    ,cm_cuponpago ,cm_suscriptor,cm_estadocupon,cm_tipocuponpago ";
                    cmdDatosBasicos.QueryString = cmdDatosBasicos.QueryString + " where cm_cuentacobro.idcuentacobro=cm_cuponpago.idcuentacobro ";
                    cmdDatosBasicos.QueryString = cmdDatosBasicos.QueryString + " and cm_cuentacobro.idsuscriptor=cm_suscriptor.idsuscriptor ";
                    cmdDatosBasicos.QueryString = cmdDatosBasicos.QueryString + " and cm_cuponpago.idestadocupon=cm_estadocupon.idestadocupon ";
                    cmdDatosBasicos.QueryString = cmdDatosBasicos.QueryString + " and cm_cuponpago.ditipocuponpago=cm_tipocuponpago.idtipocuponpago ";
                    cmdDatosBasicos.QueryString = cmdDatosBasicos.QueryString + " and cm_cuponpago.idestadocupon in ('1') ";
                    cmdDatosBasicos.QueryString = cmdDatosBasicos.QueryString + " and cm_cuponpago.idpais=" + nuIdPais;
                    cmdDatosBasicos.QueryString = cmdDatosBasicos.QueryString + " and cm_cuponpago.iddepartamento=" + nuIdDepartamento;
                    cmdDatosBasicos.QueryString = cmdDatosBasicos.QueryString + " and cm_cuponpago.idmunicipio=" + nuIdMunicipio;
                    cmdDatosBasicos.QueryString = cmdDatosBasicos.QueryString + " and cm_cuponpago.idcuponpago=" + numeroCuponpago;



                    dtDatosBasicos = cmdDatosBasicos.ExecuteStringCommand();
                }

                if (dtDatosBasicos.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtDatosBasicos.Rows)
                    {
                        DatosCuponPago.nuIdCuponPago = Convert.ToInt64(dr["cuponpago"]);
                        DatosCuponPago.nuValorCuponPago = Convert.ToInt32(dr["valor"]);
                        DatosCuponPago.nuIdSuscriptor = Convert.ToInt64(dr["suscriptor"]);
                        DatosCuponPago.vaNombre = dr["nombrecompleto"].ToString();
                        DatosCuponPago.nuSaldoPendiente = Convert.ToDouble(dr["saldopte"]);
                        DatosCuponPago.valortotalfacturado = Convert.ToInt32(dr["valortotalfacturado"]);
                        DatosCuponPago.valortotalfacturadoglobal = Convert.ToInt32(dr["valortotalfacturadoglobal"]);
                        DatosCuponPago.vaTipoCuponPago = dr["tipo"].ToString();
                        DatosCuponPago.daFechaGeneracion = Convert.ToDateTime(dr["fechagen"]);
                        DatosCuponPago.nuIdEstadoCuponPago = Convert.ToInt32(dr["idestado"]);
                        DatosCuponPago.vaEstadoCuponPago = dr["estado"].ToString();

                        ConsultaCuponPago.Add(DatosCuponPago);
                    }
                    return ConsultaCuponPago;
                }
                else
                {
                    DatosCuponPago.MensajeRespuestaError = "El numero de cupon no existe o esta inactivo";
                    ConsultaCuponPago.Add(DatosCuponPago);
                    return ConsultaCuponPago;
                }
            }
            catch (OracleException e)
            {
                string errorMessage = "Code: " + e.Code + "\n" +
                                      "Message: " + e.Message;
                DatosCuponPago.MensajeRespuestaError = "Ocurrio un error no controlado contacte con el administrador";
                ConsultaCuponPago.Add(DatosCuponPago);
                return ConsultaCuponPago;
            }
        }
        /// <summary>
        /// metodo que retorna los datos del suscriptor con el fin de generar un cupon de pago
        /// </summary>
        /// el parametor de entrada es el numero de suscriptor
        /// <param name="nuIdsuscriptor"></param>
        /// <returns></returns>
        public List<DatosSuscriptor> obtenerDatosSuscriptor(long nuIdsuscriptor)
        {
            List<DatosSuscriptor> ConsultaSuscriptor = new List<DatosSuscriptor>();
            DataTable dtDatosBasicos = new DataTable();
            int nuIdPais = 57; int nuIdDepartamento = 76; int nuIdMunicipio = 834;
            string lugartrabajo = nuIdPais.ToString() + nuIdDepartamento.ToString() + nuIdMunicipio.ToString();
            int numerolugartrabajo = lugartrabajo.Length;
            string vaNumeroSuscriptor = lugartrabajo + nuIdsuscriptor;
            long numeroSuscriptor = Convert.ToInt64(vaNumeroSuscriptor);
            DatosSuscriptor DatosSuscriptor = new DatosSuscriptor();
            try
            {
                using (SiewebDBCommand cmdDatosBasicos = new SiewebDBCommand())
                {
                    //Consultar datos del suscriptor
                    cmdDatosBasicos.QueryString = "select  substr(cm_suscriptor.idsuscriptor,8) suscriptor,trim(cm_suscriptor.nombre)||' '||trim(cm_suscriptor.apellido) nombrecompleto,";
                    cmdDatosBasicos.QueryString = cmdDatosBasicos.QueryString + " TRIM(TO_CHAR(cm_suscriptor.saldopendiente, '999G999G990D99')) AS saldopendiente, Max(substr(cm_cuentacobro.idcuentacobro, 8)) cuentacobro, facturasconsaldo";
                    cmdDatosBasicos.QueryString = cmdDatosBasicos.QueryString + " from cm_cuentacobro,cm_suscriptor";
                    cmdDatosBasicos.QueryString = cmdDatosBasicos.QueryString + " where cm_cuentacobro.idsuscriptor=cm_suscriptor.idsuscriptor";
                    cmdDatosBasicos.QueryString = cmdDatosBasicos.QueryString + " and cm_suscriptor.idsuscriptor=" + numeroSuscriptor;
                    cmdDatosBasicos.QueryString = cmdDatosBasicos.QueryString + " GROUP BY cm_suscriptor.idsuscriptor,trim(cm_suscriptor.nombre)||' '||trim(cm_suscriptor.apellido),cm_suscriptor.saldopendiente,facturasconsaldo";

                    //Consultar la ultima factura
                    SiewebDBCommand cmdUltimaFactura = new SiewebDBCommand();
                    cmdUltimaFactura.QueryString = "SELECT idcuentacobro,TRIM(TO_CHAR(nvl(sum(decode(idsigno,'DB', (valor + valoriva), ";
                    cmdUltimaFactura.QueryString = cmdUltimaFactura.QueryString + "'CR', -(valor + valoriva), ";
                    cmdUltimaFactura.QueryString = cmdUltimaFactura.QueryString + "'AS', -(valor + valoriva), ";
                    cmdUltimaFactura.QueryString = cmdUltimaFactura.QueryString + "'PA', -(valor + valoriva))), 0 ),'999G999G990D99')) nuSaldoCuentaCobroAcu ";
                    cmdUltimaFactura.QueryString = cmdUltimaFactura.QueryString + "FROM cm_cartera ";
                    cmdUltimaFactura.QueryString = cmdUltimaFactura.QueryString + "WHERE IDSUSCRIPTOR = " + numeroSuscriptor + "";
                    cmdUltimaFactura.QueryString = cmdUltimaFactura.QueryString + "and idcuentacobro = (select max(idcuentacobro) from cm_cartera where IDSUSCRIPTOR = " + numeroSuscriptor + ")";
                    cmdUltimaFactura.QueryString = cmdUltimaFactura.QueryString + "GROUP BY idcuentacobro ";
                    cmdUltimaFactura.QueryString = cmdUltimaFactura.QueryString + "ORDER BY idcuentacobro";

                    //Lleno el DataReader con los datos de la consulta
                    dtDatosBasicos = cmdDatosBasicos.ExecuteStringCommand();

                    //si la consulta encuentra datos es decir que el suscriptor existe

                    if (dtDatosBasicos.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtDatosBasicos.Rows)
                        {
                            //si el suscriptor no tiene saldo pendiente retorna un mensje
                            if (dr["saldopendiente"].ToString() == "0,00")
                            {

                                DatosSuscriptor.MensajeRespuestaError = "El suscriptor no tiene saldo pendiente";
                                ConsultaSuscriptor.Add(DatosSuscriptor);
                                //return ConsultaSuscriptor;
                            }
                            //si el suscriptor tiene saldo pendiente obtiene los datos
                            else
                            {
                                DatosSuscriptor.nuIdSuscriptor = Convert.ToInt64(dr["suscriptor"]);
                                DatosSuscriptor.vaNombre = dr["nombrecompleto"].ToString();
                                DatosSuscriptor.nuSaldoPendiente = Convert.ToDouble(dr["saldopendiente"]);
                                DatosSuscriptor.nuIdCuentaCobro = Convert.ToInt64(dr["cuentacobro"]);
                                DatosSuscriptor.nuFacturasconSaldo = Convert.ToInt32(dr["facturasconsaldo"]);

                                //datatable para el valor de la ultima factura
                                DataTable dtUltimaFactura = cmdUltimaFactura.ExecuteStringCommand();

                                if (dtUltimaFactura.Rows.Count > 0)
                                {
                                    foreach (DataRow ultimaFactRecord in dtUltimaFactura.Rows)
                                    {
                                        DatosSuscriptor.nuSaldoPendienteUltimaFactura = Convert.ToDouble(ultimaFactRecord["nuSaldoCuentaCobroAcu"]);
                                    }
                                }
                                ConsultaSuscriptor.Add(DatosSuscriptor);

                            }
                        }
                        return ConsultaSuscriptor;
                    }
                    //si el numero de suscriptor ingresado no existe retorna un mensaje
                    else
                    {
                        DatosSuscriptor.MensajeRespuestaError = "El suscriptor no existe";
                        ConsultaSuscriptor.Add(DatosSuscriptor);
                        return ConsultaSuscriptor;
                    }
                }
            }
            catch (OracleException e)
            {
                string errorMessage = "Code: " + e.Code + "\n" +
                                      "Message: " + e.Message;
                DatosSuscriptor.MensajeRespuestaError = "Ocurrio un error no controlado contacte con el administrador";
                ConsultaSuscriptor.Add(DatosSuscriptor);
                return ConsultaSuscriptor;
            }
        }
        /// <summary>
        /// metodo que retorna verdadero si el suscriptor ingresado esta en proceso de facturacion 
        /// y falso en caso contrario es decir que se le puede generar un cupon de abono
        /// 
        /// </summary>
        /// parametro de entrada el numero dle suscriptor
        /// <param name="nuIdsuscriptor"></param>
        /// <returns>
        /// verdadero = esta en proceso de facturacion
        /// falso = se puede generar cupon
        /// </returns>
        public bool obtenerEstadoFacturacion(long nuIdCuponPago)
        {
            DataTable dtEstadoFacturacion = new DataTable();
            int nuIdPais = 57; int nuIdDepartamento = 76; int nuIdMunicipio = 834;
            string lugartrabajo = nuIdPais.ToString() + nuIdDepartamento.ToString() + nuIdMunicipio.ToString();
            int numerolugartrabajo = lugartrabajo.Length;
            string vaNumeroCupon = lugartrabajo + nuIdCuponPago;
            long numeroCuponPago = Convert.ToInt64(vaNumeroCupon);
            try
            {
                using (SiewebDBCommand cmdDatosBasicos = new SiewebDBCommand())
                {
                    cmdDatosBasicos.QueryString = " SELECT idcuentacobro FROM cm_cuponpago ";
                    cmdDatosBasicos.QueryString = cmdDatosBasicos.QueryString + " WHERE idcuponpago=" + numeroCuponPago;
                    cmdDatosBasicos.QueryString = cmdDatosBasicos.QueryString + " AND idcuentacobro IN (SELECT idcuentacobro FROM cm_cuentacobro ";
                    cmdDatosBasicos.QueryString = cmdDatosBasicos.QueryString + " WHERE idciclofacturacion IN (select idciclofacturacion from cm_vigencia where bloqueomovimiento='S'))";

                    dtEstadoFacturacion = cmdDatosBasicos.ExecuteStringCommand();
                    if (dtEstadoFacturacion.Rows.Count > 0)
                    {
                        //si encuentra datos es porque el suscriptor esta bloqueado para moviminetos
                        return true;
                    }
                    else
                    {
                        //si no retorna datos es porque a el suscriptor se le puede generar cupon de abono
                        return false;
                    }
                }
            }
            catch (OracleException e)
            {
                string errorMessage = "Code: " + e.Code + "\n" +
                                      "Message: " + e.Message;
                return true;
            }
        }

        /// <summary>
        /// metodo que llama la clase crea cuponpago y le envia los parametros para el registro del cupon en la tabla
        /// cm_cuponpago
        /// </summary>
        /// <param name="nuIdSuscriptor"></param>
        /// <param name="nuValorCuponPago"></param>
        /// <param name="nuIdCuentaCobro"></param>
        /// <returns>
        /// numero de cupon
        /// valor del cupon
        /// fecha de generacion
        /// </returns>
        public List<DatosCuponPago> generarDatosCuponPago(long nuIdSuscriptor, int nuValorCuponPago, long nuIdCuentaCobro)
        {
            List<DatosCuponPago> RetornaDatosCuponPago = new List<DatosCuponPago>();
            CreaCuponPago objDatosCuponPago = new CreaCuponPago();
            int nuIdTipoCuponPago = 2;
            int nuIdEstadoCuponPago = 1;
            int nuIdPais = 57; int nuIdDepartamento = 76; int nuIdMunicipio = 834;
            string lugartrabajo = nuIdPais.ToString() + nuIdDepartamento.ToString() + nuIdMunicipio.ToString();
            string vafullIdSuscriptor = lugartrabajo + nuIdSuscriptor.ToString();
            long nufullIdSuscriptor = Convert.ToInt64(vafullIdSuscriptor);
            return RetornaDatosCuponPago = objDatosCuponPago.creaCuponPago(
                                                nufullIdSuscriptor,
                                                nuValorCuponPago,
                                                nuIdTipoCuponPago,
                                                nuIdEstadoCuponPago,
                                                nuIdCuentaCobro
                                                );
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nuIdsuscriptor"></param>
        /// <returns></returns>
        public List<DatosCuentaCobro> obtenerDatosCuentaCobro(long nuIdsuscriptor)
        {
            List<DatosCuentaCobro> ConsultaFacturas = new List<DatosCuentaCobro>();
            DataTable dtDatosFacturas = new DataTable();
            int nuIdPais = 57; int nuIdDepartamento = 76; int nuIdMunicipio = 834;
            string lugartrabajo = nuIdPais.ToString() + nuIdDepartamento.ToString() + nuIdMunicipio.ToString();
            int numerolugartrabajo = lugartrabajo.Length;
            string vaNumeroSuscriptor = lugartrabajo + nuIdsuscriptor;
            long numeroSuscriptor = Convert.ToInt64(vaNumeroSuscriptor);
            DatosCuentaCobro DatosCuentaCobro = new DatosCuentaCobro();
            try
            {
                using (SiewebDBCommand cmdDatosFacturas = new SiewebDBCommand())
                {
                    //Consultar datos del suscriptor
                    cmdDatosFacturas.QueryString = "select * from";
                    cmdDatosFacturas.QueryString = cmdDatosFacturas.QueryString + " (select SubStr(idcuentacobro,8)NumeroFactura,fechaemision,valortotalfacturado";
                    cmdDatosFacturas.QueryString = cmdDatosFacturas.QueryString + " from cm_factura";
                    cmdDatosFacturas.QueryString = cmdDatosFacturas.QueryString + " where idsuscriptor=" + numeroSuscriptor;
                    cmdDatosFacturas.QueryString = cmdDatosFacturas.QueryString + " ORDER BY fechaemision desc)";
                    cmdDatosFacturas.QueryString = cmdDatosFacturas.QueryString + " where ROWNUM <= 5";


                    //Lleno el DataReader con los datos de la consulta
                    dtDatosFacturas = cmdDatosFacturas.ExecuteStringCommand();

                    //si la consulta encuentra datos es decir que el suscriptor existe

                    if (dtDatosFacturas.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtDatosFacturas.Rows)
                        {
                            DatosCuentaCobro.nuIdCuentaCobro = Convert.ToInt64(dr["NumeroFactura"]);
                            DatosCuentaCobro.daFechaGeneracion = Convert.ToDateTime(dr["fechaemision"]);
                            DatosCuentaCobro.valortotalfacturado = Convert.ToDouble(dr["valortotalfacturado"]);
                            
                            ConsultaFacturas.Add(DatosCuentaCobro);
                        }
                        return ConsultaFacturas;
                    }
                    //si el numero de suscriptor ingresado no existe retorna un mensaje
                    else
                    {
                        DatosCuentaCobro.MensajeRespuestaError = "El suscriptor no existe";
                        ConsultaFacturas.Add(DatosCuentaCobro);
                        return ConsultaFacturas;
                    }
                }
            }
            catch (OracleException e)
            {
                string errorMessage = "Code: " + e.Code + "\n" +
                                      "Message: " + e.Message;
                DatosCuentaCobro.MensajeRespuestaError = "Ocurrio un error no controlado contacte con el administrador";
                ConsultaFacturas.Add(DatosCuentaCobro);
                return ConsultaFacturas;
            }

        }

        public List<DatosSuscriptor> obtenerSuscriptorPazySalvo(long nuIdsuscriptor)
        {
            List<DatosSuscriptor> ConsultaSuscriptor = new List<DatosSuscriptor>();
            DataTable dtDatosBasicos = new DataTable();
            int nuIdPais = 57; int nuIdDepartamento = 76; int nuIdMunicipio = 834;
            string lugartrabajo = nuIdPais.ToString() + nuIdDepartamento.ToString() + nuIdMunicipio.ToString();
            int numerolugartrabajo = lugartrabajo.Length;
            string vaNumeroSuscriptor = lugartrabajo + nuIdsuscriptor;
            long numeroSuscriptor = Convert.ToInt64(vaNumeroSuscriptor);
            DatosSuscriptor DatosSuscriptor = new DatosSuscriptor();
            try
            {
                using (SiewebDBCommand cmdDatosBasicos = new SiewebDBCommand())
                {
                    //Consultar datos del suscriptor
                    cmdDatosBasicos.QueryString = "select saldopendiente,saldopdtefinanciacion";
                    cmdDatosBasicos.QueryString = cmdDatosBasicos.QueryString + " from cm_suscriptor";
                    cmdDatosBasicos.QueryString = cmdDatosBasicos.QueryString + " where cm_suscriptor.idsuscriptor=" + numeroSuscriptor;
                    
                    //Lleno el DataReader con los datos de la consulta
                    dtDatosBasicos = cmdDatosBasicos.ExecuteStringCommand();

                    //si la consulta encuentra datos es decir que el suscriptor existe

                    if (dtDatosBasicos.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtDatosBasicos.Rows)
                        {
                            //si el suscriptor no tiene saldo pendiente retorna un mensje
                            if (Convert.ToDouble(dr["saldopendiente"])>0 || Convert.ToDouble(dr["saldopdtefinanciacion"])>0)
                            {
                                DatosSuscriptor.nuSaldoPendiente = Convert.ToDouble(dr["saldopendiente"]);
                                DatosSuscriptor.nuSaldoPteFinanciacion = Convert.ToDouble(dr["saldopdtefinanciacion"]);
                                double saldototal = DatosSuscriptor.nuSaldoPendiente + DatosSuscriptor.nuSaldoPteFinanciacion;
                                DatosSuscriptor.MensajeRespuestaOk = "El suscriptor presenta saldo pendiente por valor de $ " + saldototal;
                                ConsultaSuscriptor.Add(DatosSuscriptor);
                                //return ConsultaSuscriptor;
                            }
                            //si el suscriptor tiene saldo pendiente obtiene los datos
                            else
                            {
                                DatosSuscriptor.MensajeRespuestaOk = "El suscriptor a Paz y Salvo";
                                ConsultaSuscriptor.Add(DatosSuscriptor);
                            }
                        }
                        return ConsultaSuscriptor;
                    }
                    //si el numero de suscriptor ingresado no existe retorna un mensaje
                    else
                    {
                        DatosSuscriptor.MensajeRespuestaError = "El suscriptor no existe";
                        ConsultaSuscriptor.Add(DatosSuscriptor);
                        return ConsultaSuscriptor;
                    }
                }
            }
            catch (OracleException e)
            {
                string errorMessage = "Code: " + e.Code + "\n" +
                                      "Message: " + e.Message;
                DatosSuscriptor.MensajeRespuestaError = "Ocurrio un error no controlado contacte con el administrador";
                ConsultaSuscriptor.Add(DatosSuscriptor);
                return ConsultaSuscriptor;
            }

        }
    }

}
