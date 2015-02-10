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
        private PuntoVentaEntities contexto;

        public TipoPagoPersistencia()
        {
            contexto = new PuntoVentaEntities();
        }

        public List<C_FormasPago> ObtenerTiposPago()
        {
            return contexto.C_FormasPago.ToList();
        }
    }
}
