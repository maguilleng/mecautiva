using System.ComponentModel.Composition;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Controls;
using TallerWPF.Infraestructura;

namespace TallerWPF.VentasModule.Vistas
{
    /// <summary>
    /// Lógica de interacción para VentasPrincipal.xaml
    /// </summary>
    [ViewExport(RegionName = RegionNames.MainRegion, IsActiveByDefault=false)]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class VentasPrincipal : UserControl
    {
        public VentasPrincipal()
        {
            InitializeComponent();
        }
    }
}
