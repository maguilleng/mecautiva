using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Servicios;

namespace TallerWPF.ClientesModule.ValidationRules
{
    public class ValidaRFC : ValidationRule
    {
        public ValidaRFC()
        {}

        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
             ClientesService clientesCtrl = new ClientesService();
               bool existeRFC =  clientesCtrl.buscarRFCEnBD(value.ToString());
               if (!existeRFC)
                return new ValidationResult(true, null);

            return new ValidationResult(false, "El RFC ya esta dado de alta, verifique sus datos");
        }
    }
}
