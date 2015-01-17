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
    public class VehiculosService
    {
        VehiculosPersistencia persistencia;

        public VehiculosService()
        {
            persistencia = new VehiculosPersistencia();
        }

        public List<C_Vehiculos> ObtenerVehiculosPorIdCliente(int idCliente)
        {
            return persistencia.ObtenerVehiculosPorIdCliente(idCliente);
        }
    }
}
