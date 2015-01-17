using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TallerWPF.Infraestructura.Interfaces;
using System.ComponentModel.Composition;
using TallerWPF.Entidades;
using System.Collections;

namespace TallerWPF.VentasModule.SharedServices
{
    [Export(typeof(IServicioVenta))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class ServicioVenta : IServicioVenta, IDisposable
    {
        Venta VentaActual { get; set; }                

        [ImportingConstructor]
        public ServicioVenta()
        { 
        
        }

        public Venta ObtenerVentaActual()
        {
            return VentaActual;
        }

        public void ActualizarVentaActual(Venta ventaActual)
        {
            VentaActual = ventaActual;
        }

        public bool EstaRealizandoVenta()
        {
            if (VentaActual != null)
            {
                return true;
            }
            return false;
        }

        public void ActualizarImporteVenta(Venta ventaParam) 
        {
            ventaParam.Subtotal = CalcularSubtotalProductos(ventaParam.Productos);
            ventaParam.Iva = CalcularIvaDelSubtotal(ventaParam.Subtotal);
            ventaParam.Total = CalcularTotalVenta(ventaParam);
        }

        protected double CalcularSubtotalProductos(IList<Producto> listaProductos)
        {
            double subtotal = 0;
            foreach(var producto in listaProductos)
            {
                producto.Importe = producto.PrecioUnitario * producto.Cantidad;
                subtotal = subtotal + producto.Importe;
            }

            return subtotal;
        }

        protected double CalcularIvaDelSubtotal(double subtotalVenta)
        {
            return subtotalVenta * 0.16;
        }

        protected double CalcularTotalVenta(Venta venta)
        {
            return venta.Subtotal + venta.Iva;
        }

        public void LimpiarProductosVentaActual()
        {
            VentaActual.Productos.Clear();
            ActualizarImporteVenta(VentaActual);
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
