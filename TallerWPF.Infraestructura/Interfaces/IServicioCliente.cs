using TallerWPF.Entidades.VehiculosEntidades;
using System.Collections.Generic;
using TallerWPF.Entidades;

namespace TallerWPF.Infraestructura.Interfaces
{
    public interface IServicioCliente
    {
        void LimpiarCarteraClientes();
        //void ActualizarVentaActual(VehiculosDTO vehiculoActual);
        void LLenarListaClientes(List<C_Clientes> Clientes);
        void LLenarListaVehiculos(List<C_Vehiculos> Vehiculos);
    }
}
