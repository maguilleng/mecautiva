using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TallerWPF.Entidades;
using TallerWPF.Entidades.VentasEntidades;
using TallerWPF.Infraestructura.Dtos.Ventas;
using TallerWPF.Persistencia;

namespace Servicios
{
    [Export]
    public class VentasService
    {
        #region ATRIBUTOS PRIVADOS

        VentasPersistencia ventasPersistencia;

        #endregion

        #region CONSTRUCTOR
        public VentasService()
        {
            ventasPersistencia = new VentasPersistencia();
        }
        #endregion

        #region METODOS
        public bool GuardarVenta(C_FormasPago formaPago, VentaDto venta,
            List<VentaDetalleDto> detallesVenta, C_Clientes cliente, C_Vehiculos vehiculo, PagosDetalleDto detallePago)
        {            
            venta.Cliente = cliente;
            venta.Vehiculo = vehiculo;
            
            Ventas VentaGuardar = ConvertirVentaDto(venta);

            List<VentasDetalle> DetallesVenta = new List<VentasDetalle>();
            detallesVenta.ForEach(dv => DetallesVenta.Add(ConvertirDetalleVentaDto(dv)));


            DetallesVenta.ForEach(dv => dv.Ventas = VentaGuardar);
            VentaGuardar.VentasDetalle = DetallesVenta;

            Pagos PagoPrincipal = ObtenerPagoVacio();
            PagosDetalle DetallePago = ConvertirPagoDetalleDto(detallePago);
            DetallePago.Pagos = PagoPrincipal;
            DetallePago.IdFormaPago = formaPago.IdFormaPago;

            List<PagosDetalle> DetallesPago = new List<PagosDetalle>();
            DetallesPago.Add(DetallePago);

            PagoPrincipal.Ventas = VentaGuardar;
            PagoPrincipal.PagosDetalle = DetallesPago;

            VentaGuardar.Pagos = new List<Pagos>();
            VentaGuardar.Pagos.Add(PagoPrincipal);

            bool exito = ventasPersistencia.GuardarVenta(VentaGuardar);

            return exito;
        }


        public Ventas ConvertirVentaDto(VentaDto ventaDto)
        {
            int? idVehiculo = null;

            if (ventaDto.Vehiculo != null)
                idVehiculo = ventaDto.Vehiculo.IdVehiculo;
            
            Ventas Venta = new Ventas()
            {
                Fecha = DateTime.Now,
                Folio = "F001",
                Subtotal = ventaDto.Subtotal,
                IVA = ventaDto.IVA,
                Total = ventaDto.Total,
                TotalLetras = "TotalLetras",
                IdCliente = ventaDto.Cliente.IdCliente,
                IdVehiculo = idVehiculo,
                Kilometraje = 0,
                FechaAlta = DateTime.Now,
                FechaModificacion = DateTime.Now,
                IdUsuarioCreacion = 1,
                IdUsuarioModificacion = 1,
                Estatus = 1,
                EsFactura = ventaDto.EsFactura
            };

            return Venta;
        }

        public VentasDetalle ConvertirDetalleVentaDto(VentaDetalleDto detalleDto)
        {
            VentasDetalle ventaDetalle = new VentasDetalle()
            {
                Cantidad = detalleDto.Cantidad,
                PrecioUnitario = detalleDto.PrecioUnitario,
                Importe = detalleDto.Importe,
                IdServicio = detalleDto.IdServicio,
                FechaAlta = DateTime.Now,
                FechaModificacion = DateTime.Now,
                IdUsuarioCreacion = 1,
                IdUsuarioModificacion = 1,
                Estatus = 1
            };

            return ventaDetalle;
        }

        public PagosDetalle ConvertirPagoDetalleDto(PagosDetalleDto pagoDetalleDto)
        {
            PagosDetalle pagoDetalle = new PagosDetalle()
            {
                IdFormaPago = pagoDetalleDto.IdFormaPago,
                Institucion = pagoDetalleDto.Institucion,
                Referencia = pagoDetalleDto.Referencia,
                Cantidad = pagoDetalleDto.Cantidad,
                FechaAlta = DateTime.Now,
                FechaModificacion = DateTime.Now,
                Estatus = 1,
                IdUsuarioCreacion = 1,
                IdUsuarioModificacion = 1
            };

            return pagoDetalle;
        }

        public Pagos ObtenerPagoVacio()
        {
            Pagos pago = new Pagos()
            {
                FechaAlta = DateTime.Now,
                FechaModificacion = DateTime.Now,
                IdUsuarioCreacion = 1,
                IdUsuarioModificacion = 1,
                Estatus = 1
            };

            return pago;
        }

        #endregion       
    }
}
