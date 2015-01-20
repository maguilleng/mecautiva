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
    public class ClientesService
    {
        ClientesPersistencia persistencia;
        public ClientesService()
        {
            persistencia = new ClientesPersistencia();
        }

        public List<C_Clientes> ObtenerListaClientes()
        {
            return persistencia.ObtenerListaClientes().ToList();
        }

        public List<C_Vehiculos> ObtenerListaVehiculosxCliente(int idCliente)
        {
            return persistencia.ObtenerListaVehiculosxCliente(idCliente).ToList();
        }

        public List<C_TiposPersona> ObtenerListaTiposPersona()
        {
            return persistencia.ObtenerListaTiposPersona().ToList();
        }
    }
}
