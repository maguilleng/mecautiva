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
        private PuntoVentaEntities contexto;

        public ClientesPersistencia()
        {
           contexto = new PuntoVentaEntities();
        }
        
        public List<C_Clientes> ObtenerListaClientes()
        {
               return contexto.C_Clientes.Include("C_Vehiculos").ToList();    
        }

        public List<C_Vehiculos> ObtenerListaVehiculosxCliente(int idCliente)
        {
            using (var ctx = new PuntoVentaEntities())
            {
                return ctx.C_Vehiculos.Where(v => v.IdCliente == idCliente).ToList();
            }       
        }

        public void registrar_nuevo_cliente(C_Clientes nuevo_cliente)
        {

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

        public string GuardarVehiculo(C_Vehiculos vehiculo)
        {            
            try
            {
               using (var ctx = new PuntoVentaEntities())
                    {
                        if (vehiculo.IdVehiculo == 0)
                        {
                         ctx.C_Vehiculos.Add(vehiculo);
                         ctx.SaveChanges();
                        }
                        else{
                        ctx.C_Vehiculos.Attach(vehiculo);
                        ctx.Entry(vehiculo).State = EntityState.Modified;
                        }
                        ctx.SaveChanges();
                        return "El cliente a sido Guardado Exitosamente";
                    }                    
                }               
            
            catch (InvalidOperationException e)
            {
                return  "Se produjo un error al intentar guardar el nuevo cliente: " + e.Message;
            }
        }

        public bool buscarPlacaEnBD(string placa)
        {
            using (var ctx = new PuntoVentaEntities())
            {
                return ctx.C_Vehiculos.Where(v => v.Placas.ToUpper().Equals(placa.ToUpper())).Any();
            }
        }
    }
}
