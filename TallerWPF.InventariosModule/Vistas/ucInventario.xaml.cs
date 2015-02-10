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
using System.ComponentModel.Composition;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Controls;
using TallerWPF.Infraestructura;

namespace TallerWPF.InventariosModule.Vistas
{
    /// <summary>
    /// Interaction logic for ucClientes.xaml
    /// </summary>
    [ViewExport(RegionName = RegionNames.MainRegion)]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class ucInventario : UserControl
    {
        public ucInventario()
        {
            InitializeComponent();
        }
    }
}
