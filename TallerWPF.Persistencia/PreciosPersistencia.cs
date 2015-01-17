using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TallerWPF.Entidades;

namespace TallerWPF.Persistencia
{
    public class PreciosPersistencia
    {
        private PuntoVentaEntities contexto;

        public PreciosPersistencia()
        {
            contexto = new PuntoVentaEntities();
        }

        public Precios ObtenerPrecioActualServicio(int idServicio)
        {
            return contexto.Precios.Where(p => p.IdServicio == idServicio).OrderByDescending(p => p.FechaAlta).FirstOrDefault();
        }
    }
}
