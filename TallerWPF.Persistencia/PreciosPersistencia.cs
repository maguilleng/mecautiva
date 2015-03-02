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

        public Precios ObtenerPrecioActualServicio(int idServicio)
        {
            using (var contexto = new PuntoVentaEntities())
            {
                return contexto.Precios.Where(p => p.IdServicio == idServicio).OrderByDescending(p => p.FechaAlta).FirstOrDefault();
            }
        }
    }
}
