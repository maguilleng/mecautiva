using System
;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Servicios;

namespace TallerWPF.ClientesModule.ValidationRules
{
   
    public class ValidaPlaca : ValidationRule
    {
        public ValidaPlaca()
        {}

        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
             ClientesService clientesCtrl = new ClientesService();
               bool existePlaca =  clientesCtrl.buscarPlacaEnBD(value.ToString());
            if (!existePlaca)
                return new ValidationResult(true, null);

            return new ValidationResult(false, "La placa ya esta de alta, verifique sus datos");
        }
    }
}
