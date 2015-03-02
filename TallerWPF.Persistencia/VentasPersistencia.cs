using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TallerWPF.Entidades;

namespace TallerWPF.Persistencia
{
    public class VentasPersistencia
    {
        public bool GuardarVenta(Ventas venta)
        {
            bool exito = false;
            int resultado = 0;

            try
            {                
                using (var contexto = new PuntoVentaEntities())
                {                    
                    if (venta.IdVenta == 0)
                    {
                        contexto.Ventas.Add(venta);
                        resultado = contexto.SaveChanges();
                        exito = true;
                    }
                    else
                    { 
                    
                    }
                }
            }
            catch (Exception ex)
            {
                var e = ex;
                exito = false;
            }

            return exito;
        }
    }
}
