using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TallerWPF.Infraestructura.ComponentesCompartidos
{
    /// <summary>
    /// Interaction logic for ucToolBarCRUD.xaml
    /// </summary>
    public partial class ucToolBarCRUD : UserControl
    {
        public ucToolBarCRUD()
        {
            InitializeComponent();
        }

        public void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Hello MessageBox");
        }
    }
}
