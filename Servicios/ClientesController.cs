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
    public class ClientesController
    {
        ClientesPersistencia persistencia;
        public ClientesController()
        {
            persistencia = new ClientesPersistencia();
        }

        public List<C_Clientes> ObtenerListaClientes()
        {
            return persistencia.ObtenerListaClientes().ToList();
        }
    }
}
