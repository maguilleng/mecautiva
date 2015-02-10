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
    public class TipoPagoService
    {
        private TipoPagoPersistencia tipoPagoPersistencia;

        public TipoPagoService()
        {
            tipoPagoPersistencia = new TipoPagoPersistencia();
        }

        public List<C_FormasPago> ObtenerTiposPago()
        {
            return tipoPagoPersistencia.ObtenerTiposPago();
        }
    }
}
