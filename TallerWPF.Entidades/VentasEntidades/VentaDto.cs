using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.Mvvm;

namespace TallerWPF.Entidades.VentasEntidades
{
    public class VentaDto : BindableBase
    {
        public int IdVenta{get;set;}
        public DateTime Fecha { get; set; }
        public string Folio { get; set; }
        
        double subtotal;
        public double Subtotal
        {
          get { return subtotal; }
          set { SetProperty(ref this.subtotal, value); }
        }

        double iva;
        public double IVA
        {
          get { return iva; }
          set { SetProperty(ref this.iva, value); }
        }

        double total;
        public double Total
        {
          get { return total; }
          set { SetProperty(ref this.total, value); }
        }

        public C_Clientes C_Clientes{get;set;}
        public C_Vehiculos C_Vehiculos{get;set;}
    }
}
