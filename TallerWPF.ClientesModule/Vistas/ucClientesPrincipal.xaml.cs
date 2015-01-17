using System.ComponentModel.Composition;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Controls;
using TallerWPF.Infraestructura;

namespace TallerWPF.ClientesModule.Vistas
{
    /// <summary>
    /// Interaction logic for ucClientesPrincipal.xaml
    /// </summary>
    [ViewExport(RegionName = RegionNames.MainRegion)]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class ucClientesPrincipal : UserControl
    {
        public ucClientesPrincipal()
        {
            InitializeComponent();
        }
    }
}
