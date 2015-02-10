using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Servicios;

namespace TallerWPF.ClientesModule.ValidationRules
{
    public class ValidaNumeroCliente : ValidationRule
    {
        public ValidaNumeroCliente()
        { }

        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            ClientesService clientesCtrl = new ClientesService();
            bool existeNumeroCliente = clientesCtrl.buscarNumeroClienteEnBD(value.ToString());
            if (!existeNumeroCliente)
                return new ValidationResult(true, null);

            return new ValidationResult(false, "El Numero de cliente ya esta dado de alta, verifique sus datos");
        }
    }
}
