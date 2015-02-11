using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Servicios;

namespace TallerWPF.InventariosModule.ValidationRules
{
    public class ValidaCodigo : ValidationRule
    {
        public ValidaCodigo()
        { }

        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            ServiciosService serviciosCtrl = new ServiciosService();
            bool existeCodigo = serviciosCtrl.buscarCodigoEnBD(value.ToString());
            if (!existeCodigo)
                return new ValidationResult(true, null);

            return new ValidationResult(false, "El Codigo ya esta dado de alta, verifique sus datos");
        }
    }
}
