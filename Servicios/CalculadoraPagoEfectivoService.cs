using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TallerWPF.Entidades.VentasEntidades;

namespace Servicios
{
    [Export]
    public class CalculadoraPagoEfectivoService
    {
        public CalculadoraPagoEfectivoService()
        { 
            
        }

        public void ActualizarDatosPagoEfectivo(PagoEfectivoDto pagoEfectivo)
        {
            pagoEfectivo.CantidadRecibidaValida = VerificarCantidadRecibidaValida(pagoEfectivo);

            pagoEfectivo.Cambio = CalcularCambio(pagoEfectivo);
        }

        private bool VerificarCantidadRecibidaValida(PagoEfectivoDto pagoEfectivo)
        { 
            bool EsCantidadValida = false;

            if(pagoEfectivo.CantidadRecibida >= pagoEfectivo.TotalVenta)
                EsCantidadValida = true;

            return EsCantidadValida;
        }

        private double CalcularCambio(PagoEfectivoDto pagoEfectivo)
        {
            double cambio = pagoEfectivo.CantidadRecibida - pagoEfectivo.TotalVenta;

            return cambio;
        }
    }
}
