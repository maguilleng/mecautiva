using System.Collections.ObjectModel;
using TallerWPF.Entidades;
using TallerWPF.Entidades.VentasEntidades;
namespace TallerWPF.Infraestructura.Interfaces
{
    public interface IServicioVenta
    {
        VentaDto ObtenerVentaActual();
        void ActualizarVentaActual(VentaDto ventaActual, ObservableCollection<VentaDetalleDto> detalles);
        void ActualizarImporteVenta(VentaDto ventaActual, ObservableCollection<VentaDetalleDto> detalles);
        void LimpiarProductosVentaActual();
        bool EstaRealizandoVenta();
    }
}
