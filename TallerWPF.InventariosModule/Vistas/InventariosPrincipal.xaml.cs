using System.ComponentModel.Composition;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Controls;
using TallerWPF.Infraestructura;

namespace TallerWPF.InventariosModule.Vistas
{
    /// <summary>
    /// Lógica de interacción para InventariosPrincipal.xaml
    /// </summary>
    [ViewExport(RegionName = RegionNames.MainRegion)]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class InventariosPrincipal : UserControl
    {
        public InventariosPrincipal()
        {
            InitializeComponent();
        }
    }
}
