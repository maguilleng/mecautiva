using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TallerWPF.Infraestructura.Interfaces;
using System.ComponentModel.Composition;
using TallerWPF.Entidades;
using System.Collections;


namespace TallerWPF.ClientesModule.SharedServices
{
    [Export(typeof(IServicioCliente))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class ServicioCliente : IServicioCliente, IDisposable
    {
        public List<C_Clientes> ListaClientes { get; set; }
        public List<C_Vehiculos> ListaVehiculos { get; set; }

        [ImportingConstructor]
        public ServicioCliente()
        {

        }

        public void LimpiarCarteraClientes()
        {
            ListaClientes = null;
        }

        public void LLenarListaClientes(List<C_Clientes> Clientes)
        {
            ListaClientes = Clientes;
        }

        public void LLenarListaVehiculos(List<C_Vehiculos> Vehiculos)
        {
            ListaVehiculos = Vehiculos;
        }

        #region Dispose
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing) return;

            if (ListaClientes != null)
                ListaClientes = null;
        }

        // Use C# destructor syntax for finalization code.
        ~ServicioCliente()
        {
            Dispose(false);
        }
        #endregion
    }
}
