using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace TallerWPF.Infraestructura
{
    public class ExistenciaAColorBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            int Existencia = (int)value;
            if(Existencia <= 3)
                return new SolidColorBrush(Colors.Red);

            if (Existencia <= 8)
                return new SolidColorBrush(Colors.Orange);

            return new SolidColorBrush(Colors.Green);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
