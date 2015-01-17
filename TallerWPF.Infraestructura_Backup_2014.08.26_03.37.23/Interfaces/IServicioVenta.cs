using TallerWPF.Entidades;
namespace TallerWPF.Infraestructura.Interfaces
{
    public interface IServicioVenta
    {
        Venta ObtenerVentaActual();
        void ActualizarVentaActual(Venta ventaActual);
        void ActualizarImporteVenta(Venta ventaActual);
        void LimpiarProductosVentaActual();
        bool EstaRealizandoVenta();
    }
}
