using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TallerWPF.Entidades;
using TallerWPF.Persistencia;

namespace Servicios
{
    [Export]
    public class ServiciosService
    {
        ServiciosPersistencia persistencia = new ServiciosPersistencia();

        public ServiciosService()
        {
        
        }

        public List<C_TiposServicios> ObtenerListaTiposServicios()
        {
            return persistencia.ObtenerListaTiposServicios();
        }

        public List<C_Servicios> ObtenerListaServicios()
        {
           return persistencia.ObtenerCatalogoServicios();
        }

        public List<C_Marcas> ObtenerListaMarcas()
        {
            return persistencia.ObtenerListaMarcas();
        }

        public List<C_UnidadesMedida> ObtenerListaUnidadesMedida()
        {
            return persistencia.ObtenerListaUnidadesMedida();
        }

        public String GuardarServicio(C_Servicios servicio)
        {
            return persistencia.GuardarServicio(servicio);
        }

        public bool buscarCodigoEnBD (string codigo)
        {
            return persistencia.buscarCodigoEnBD(codigo);
        }
    }
}
