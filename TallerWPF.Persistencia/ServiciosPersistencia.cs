using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TallerWPF.Entidades;
using System.Windows;
using System.Data;

namespace TallerWPF.Persistencia
{
    public class ServiciosPersistencia
    {
        PuntoVentaEntities contexto;

        public ServiciosPersistencia()
        {
            this.contexto = new PuntoVentaEntities();
        }

        public List<C_Servicios> ObtenerCatalogoServicios()
        {
            return contexto.C_Servicios.Where(s => s.SeVende.Value).ToList();
        }

        public List<C_Marcas> ObtenerListaMarcas()
        {
            using (var ctx = new PuntoVentaEntities())
            {
                return ctx.C_Marcas.ToList();
            }
        }

        public List<C_TiposServicios> ObtenerListaTiposServicios()
        { 
            using ( var ctx = new PuntoVentaEntities())
            {
              return   ctx.C_TiposServicios.ToList();
            }
        }

        public List<C_UnidadesMedida> ObtenerListaUnidadesMedida()
        { 
            using ( var ctx = new PuntoVentaEntities())
            {
              return   ctx.C_UnidadesMedida.ToList();
            }
        }

        public String GuardarServicio(C_Servicios servicio)
        {
            string mensajeExitoso = "";
            DateTime fechaTransaccion = DateTime.Now;
            try
            {
                using (var ctx = new PuntoVentaEntities())
                {
                    if (servicio.IdServicio == 0)
                    {
                        servicio.FechaAlta = fechaTransaccion;
                        ctx.C_Servicios.Add(servicio);
                        ctx.SaveChanges();
                       mensajeExitoso = "Los datos del nuevo SERVICIO han sido guardados EXITOSAMENTE";
                    }
                    else
                    {
                        servicio.FechaModificacion = fechaTransaccion;
                        ctx.C_Servicios.Attach(servicio);
                        ctx.Entry(servicio).State = EntityState.Modified;
                        mensajeExitoso = "Los datos del SERVICIO han sido actualizados EXITOSAMENTE";
                    }
                    ctx.SaveChanges();
                    return mensajeExitoso;
                }
            }

            catch (InvalidOperationException e)
            {
                return "Se produjo un error al intentar guardar los datos del SERVICIO " + e.Message;
            }
        }

        public bool buscarCodigoEnBD(string codigo)
        {
            using (var ctx = new PuntoVentaEntities())
            {
                return ctx.C_Servicios.Where(v => v.Codigo.ToUpper().Equals(codigo.ToUpper())).Any();
            }
        }
    }
}
