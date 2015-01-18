using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TallerWPF.Infraestructura.Interfaces;
using System.ComponentModel.Composition;
using TallerWPF.Entidades;
using System.Collections;
using TallerWPF.Entidades.VentasEntidades;
using System.Collections.ObjectModel;

namespace TallerWPF.VentasModule.SharedServices
{
    [Export(typeof(IServicioVenta))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class ServicioVenta : IServicioVenta, IDisposable
    {
        //VentaDto VentaActual { get; set; }
        VentaDto VentaActual { get; set; }
        ObservableCollection<VentaDetalleDto> DetallesVenta { get; set; }

        [ImportingConstructor]
        public ServicioVenta()
        { 
        }

        public void CrearNuevaVenta(bool requiereFactura)
        {
            VentaActual = new VentaDto()
            {
                EsFactura = requiereFactura,
                Cliente = null,
                Vehiculo = null,
                VentaPagada = false
            };

            DetallesVenta = new ObservableCollection<VentaDetalleDto>();
        }

        public VentaDto ObtenerVentaActual()
        {
            return VentaActual;
        }

        public ObservableCollection<VentaDetalleDto> ObtenerDetallesVenta()
        {
            return DetallesVenta;
        }

        public void ActualizarVentaActual(VentaDto ventaActual, ObservableCollection<VentaDetalleDto> detalles)
        {
            VentaActual = ventaActual;
            DetallesVenta = detalles;
        }

        public bool EstaRealizandoVenta()
        {
            if (VentaActual != null)
            {
                return true;
            }
            return false;
        }

        public void ActualizarImporteVenta()
        {
            VentaActual.Subtotal = CalcularSubtotalServicios(DetallesVenta);
            VentaActual.IVA = Convert.ToDouble(CalcularIvaDelSubtotal(VentaActual.Subtotal));
            VentaActual.Total = Convert.ToDouble(CalcularTotalVenta(VentaActual));
        }

        protected double CalcularSubtotalServicios(ICollection<VentaDetalleDto> detallesVenta)
        {
            double subtotal = 0;
            foreach (var detalle in detallesVenta)
            {
                detalle.Importe = detalle.PrecioUnitario * detalle.Cantidad;
                subtotal = subtotal + detalle.Importe;
            }

            return subtotal;
        }

        protected double? CalcularIvaDelSubtotal(double? subtotalVenta)
        {
            return subtotalVenta * 0.16;
        }

        protected double? CalcularTotalVenta(VentaDto venta)
        {
            return venta.Subtotal + venta.IVA;
        }

        public void LimpiarProductosVentaActual()
        {
            DetallesVenta.Clear();
            ActualizarImporteVenta();
        }

        public void PagarVentaActual()
        {
            int i = 0;
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

            if (VentaActual != null)
                VentaActual = null;
        }

        // Use C# destructor syntax for finalization code.
        ~ServicioVenta()
        {
            Dispose(false);
        }
        #endregion        
    

        
    }
}
