using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TallerWPF.Entidades;

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
    }
}
