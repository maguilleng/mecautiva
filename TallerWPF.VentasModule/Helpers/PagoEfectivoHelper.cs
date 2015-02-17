using Microsoft.Practices.Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TallerWPF.VentasModule.Helpers
{
    public class PagoEfectivoHelper : BindableBase
    {
        double cantidadRecibida;
        public double CantidadRecibida
        {
            get { return cantidadRecibida; }
            set 
            { 
                SetProperty(ref this.cantidadRecibida, value);
                Cambio = CalcularCambio(this.cantidadRecibida);
                CantidadRecibidaValida = VerificarCantidadRecibidaValida(CantidadRecibida, TotalAPagar);
            }
        }

        double cambio;
        public double Cambio
        {
            get { return cambio; }
            set { SetProperty(ref this.cambio, value); }
        }

        double totalAPagar;
        public double TotalAPagar
        {
          get { return totalAPagar; }
          set { SetProperty(ref this.totalAPagar, value); }
        }

        bool cantidadRecibidaValida;
        public bool CantidadRecibidaValida
        {
            get { return cantidadRecibidaValida; }
            set { SetProperty(ref this.cantidadRecibidaValida, value); }
        }

        public double CalcularCambio(double cantidadRecibida)
        {
            double cambio = CantidadRecibida - TotalAPagar;

            if (cambio < 0)
                cambio = 0;

            return cambio;
        }

        public bool VerificarCantidadRecibidaValida(double cantidadRecibida, double totalAPagar)
        {
            bool cantidadRecibidaValida = false;
            if (cantidadRecibida >= totalAPagar)
            {
                cantidadRecibidaValida = true;
            }

            return cantidadRecibidaValida;
        }

    }
}
