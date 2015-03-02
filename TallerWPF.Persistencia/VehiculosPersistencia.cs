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
        public List<C_Vehiculos> ObtenerVehiculosPorIdCliente(int idCliente)
        {
            using (var contexto = new PuntoVentaEntities())
            {
                return contexto.C_Vehiculos.Where(v => v.C_Clientes.IdCliente == idCliente).ToList();
            }
        }
    }
}
