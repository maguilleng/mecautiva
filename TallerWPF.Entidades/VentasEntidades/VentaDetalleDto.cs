using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.Mvvm;

namespace TallerWPF.Entidades.VentasEntidades
{
    public class VentaDetalleDto : BindableBase
    {
        public int IdVentaDetalle { get; set; }
        public int IdVenta { get; set; }
        public int IdServicio { get; set; }

        private double cantidad;
        public double Cantidad
        {
            get { return cantidad; }
            set { SetProperty(ref this.cantidad, value); }
        }

        double precioUnitario;
        public double PrecioUnitario
        {
            get { return precioUnitario; }
            set { SetProperty(ref this.precioUnitario, value); }
        }

        double importe;
        public double Importe
        {
            get { return importe; }
            set { SetProperty(ref this.importe, value); }
        }

        public C_Servicios C_Servicios { get; set; }

        public Pagos PagoVenta { get; set; }

        public PagosDetalle DetallesPagoVenta { get; set; }

    }
}
