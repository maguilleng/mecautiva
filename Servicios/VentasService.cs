using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TallerWPF.Entidades;
using TallerWPF.Entidades.VentasEntidades;

namespace Servicios
{
    [Export]
    public class VentasService
    {
        public bool GuardarVenta(VentaDto venta, List<VentaDetalleDto>detallesVenta, List<PagosDetalle> detallesPago)
        {
            bool exito = false;



            return exito;
        }

        public Ventas ConvertirVentaDto(VentaDto ventaDto)
        {
            Ventas Venta = new Ventas() { 
                
            };

            return Venta;
        }

        //public List<VentasDetalle> ConvertirDetallesVentaDto(List<VentaDetalleDto> detallesVentaDto)
        //{ 
            
        //}

        //public VentasDetalle ConvertirDetalleVentaDto(VentaDetalleDto ventaDetalle)
        //{
        //    VentasDetalle venta= new VentasDetalle() { 
                
        //    };
        //}
    }
}
