using TallerWPF.Entidades;
using System.Collections.Generic;

namespace TallerWPF.Infraestructura.Interfaces
{
    public interface IServicioCliente
    {
        void LimpiarCarteraClientes();
        void LLenarListaClientes(List<C_Clientes> Clientes);
        void LLenarListaVehiculos(List<C_Vehiculos> Vehiculos);
    }
}
