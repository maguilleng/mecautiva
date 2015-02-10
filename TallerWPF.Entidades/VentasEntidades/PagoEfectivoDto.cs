using Microsoft.Practices.Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TallerWPF.Entidades.VentasEntidades
{
    public class PagoEfectivoDto : BindableBase
    {
        public double TotalVenta { get; set; }
        
        public double CantidadRecibida { get; set; }

        public double Cambio { get; set; }

        public bool CantidadRecibidaValida { get; set; }
    }
}
