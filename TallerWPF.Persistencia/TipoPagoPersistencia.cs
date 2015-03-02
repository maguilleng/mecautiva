using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TallerWPF.Entidades;

namespace TallerWPF.Persistencia
{
    public class TipoPagoPersistencia
    {
        public List<C_FormasPago> ObtenerTiposPago()
        {
            using (var contexto = new PuntoVentaEntities())
            {
                return contexto.C_FormasPago.Include("ValidacionesFormasPago").ToList();
            }
        }
    }
}
