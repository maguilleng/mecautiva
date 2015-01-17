using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TallerWPF.Entidades;

namespace TallerWPF.Persistencia
{
    public class VehiculosPersistencia
    {
        private PuntoVentaEntities contexto;

        public VehiculosPersistencia()
        {
            contexto = new PuntoVentaEntities();
        }

        public List<C_Vehiculos> ObtenerVehiculosPorIdCliente(int idCliente)
        {
            return contexto.C_Vehiculos.Where(v => v.C_Clientes.IdCliente == idCliente).ToList();
        }
    }
}
