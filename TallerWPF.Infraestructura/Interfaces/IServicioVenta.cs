using System.Collections.ObjectModel;
using TallerWPF.Entidades;
using TallerWPF.Entidades.VentasEntidades;
namespace TallerWPF.Infraestructura.Interfaces
{
    public interface IServicioVenta
    {
        VentaDto ObtenerVentaActual();
        ObservableCollection<VentaDetalleDto> ObtenerDetallesVenta();
        void ActualizarVentaActual(VentaDto ventaActual, ObservableCollection<VentaDetalleDto> detalles);
        void ActualizarImporteVenta();
        void LimpiarProductosVentaActual();
        void PagarVentaActual();
        bool EstaRealizandoVenta();
    }
}
