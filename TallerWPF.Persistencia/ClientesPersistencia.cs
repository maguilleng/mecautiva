using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TallerWPF.Entidades;

namespace TallerWPF.Persistencia
{
    public class ClientesPersistencia
    {
        private PuntoVentaEntities contexto;

        public ClientesPersistencia()
        {
            contexto = new PuntoVentaEntities();
        }

        //public IQueryable<cliente> ObtenerListaClientes()
        public List<C_Clientes> ObtenerListaClientes()
        {
            return contexto.C_Clientes.Include("C_Vehiculos").ToList();
        }

        public void registrar_nuevo_cliente(C_Clientes nuevo_cliente)
        {           
            
        }
    }
}
