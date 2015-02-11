using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TallerWPF.Entidades;
using System.Data;
using TallerWPF.Entidades.VehiculosEntidades;
using System.Windows;

namespace TallerWPF.Persistencia
{
    public class ClientesPersistencia
    {
        #region ATRIBUTOS PRIVADOS
        private PuntoVentaEntities contexto;
        #   endregion

        #region METODOS CLIENTES
        public ClientesPersistencia()
        {
            contexto = new PuntoVentaEntities();
        }
        
        public List<C_Clientes> ObtenerListaClientes()
        {
          return contexto.C_Clientes.Include("C_Vehiculos").ToList();            
        }

        public String GuardarCliente(C_Clientes cliente)
        {
            DateTime fechaTransaccion = DateTime.Now;
            try
            {
               using (var ctx = new PuntoVentaEntities())
                    {
                        if (cliente.IdCliente == 0)
                        {
                             cliente.FechaAlta = fechaTransaccion;
                             ctx.C_Clientes.Add(cliente);
                             ctx.SaveChanges();
                        }
                        else
                        {
                             cliente.FechaModificacion = fechaTransaccion;
                             ctx.C_Clientes.Attach(cliente);
                             ctx.Entry(cliente).State = EntityState.Modified;
                        }
                        ctx.SaveChanges();
                        return "Los datos del CLIENTE han sido guardados EXITOSAMENTE";
                    }                    
                }               
            
            catch (InvalidOperationException e)
            {
                return  "Se produjo un error al intentar guardar los datos del CLIENTE " + e.Message;
            }
        }

        public List<C_TiposPersona> ObtenerListaTiposPersona()
        {
            using (var ctx = new PuntoVentaEntities())
            {
                return ctx.C_TiposPersona.ToList();
            }
        }

        public List<C_Municipios> ObtenerListaMunicipiosPorEstado(int idEstado)
        {
            using (var ctx = new PuntoVentaEntities())
            {
                return ctx.C_Municipios.Include("C_Ciudades").Where(m => m.IdEstado == idEstado).ToList();
            }
        }

        public List<C_Estados> ObtenerListaEstados()
        {
            using (var ctx = new PuntoVentaEntities())
            {
                return ctx.C_Estados.Include("C_Municipios").ToList();
            }
        }

        public List<C_Ciudades> ObtenerListaCiudadesPorMunicipio(int idMunicipio)
        {
            using (var ctx = new PuntoVentaEntities())
            {
                return ctx.C_Ciudades.Where(municipio => municipio.IdMunicipio == idMunicipio).ToList();
            }
        }

        public bool buscarRFCEnBD(string rfc)
        {
            using (var ctx = new PuntoVentaEntities())
            {
                return ctx.C_Clientes.Where(v => v.RFC.ToUpper().Equals(rfc.ToUpper())).Any();
            }
        }

        public bool buscarNumeroClienteEnBD(string numeroCliente)
        {
            using (var ctx = new PuntoVentaEntities())
            {
                return ctx.C_Clientes.Where(v => v.NumeroCliente.ToUpper().Equals(numeroCliente.ToUpper())).Any();
            }
        }
        #endregion

        #region METODOS VEHICULOS
        public List<C_Vehiculos> ObtenerListaVehiculosxCliente(int idCliente)
        {
            using (var ctx = new PuntoVentaEntities())
            {
                return ctx.C_Vehiculos.Where(v => v.IdCliente == idCliente).ToList();
            }
        }

        public string GuardarVehiculo(C_Vehiculos vehiculo)
        {
            DateTime fechaTransaccion = DateTime.Now;
            try
            {
               using (var ctx = new PuntoVentaEntities())
                    {
                        if (vehiculo.IdVehiculo == 0)
                        {
                             vehiculo.FechaAlta = fechaTransaccion;
                             ctx.C_Vehiculos.Add(vehiculo);
                             ctx.SaveChanges();
                        }
                        else
                        {
                             vehiculo.FechaModificacion = fechaTransaccion;
                             ctx.C_Vehiculos.Attach(vehiculo);
                             ctx.Entry(vehiculo).State = EntityState.Modified;
                        }
                        ctx.SaveChanges();
                        return "Los datos del VEHICULO han sido guardados EXITOSAMENTE";
                    }                    
                }               
            
            catch (InvalidOperationException e)
            {
                return  "Se produjo un error al intentar guardar los datos del VEHICULO " + e.Message;
            }
        }

        public bool buscarPlacaEnBD(string placa)
        {
            using (var ctx = new PuntoVentaEntities())
            {
                return ctx.C_Vehiculos.Where(v => v.Placas.ToUpper().Equals(placa.ToUpper())).Any();
            }
        }
        #endregion
    }
}
