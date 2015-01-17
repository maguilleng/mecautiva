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
    public class PreciosService
    {
        private PreciosPersistencia preciosPersistencia;

        public PreciosService()
        {
            preciosPersistencia = new PreciosPersistencia();
        }

        public Precios ObtenerPrecioActualServicio(int idServicio)
        {
            return preciosPersistencia.ObtenerPrecioActualServicio(idServicio);
        }
    }
}
