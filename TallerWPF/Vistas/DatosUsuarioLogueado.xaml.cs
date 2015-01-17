using System.ComponentModel.Composition;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Controls;
using TallerWPF.Infraestructura;
using TallerWPF.ViewModels;

namespace TallerWPF.Vistas
{
    /// <summary>
    /// Lógica de interacción para DatosUsuarioLogueado.xaml
    /// </summary>
    [ViewExport(RegionName = RegionNames.UsuarioRegion, IsActiveByDefault = false)]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class DatosUsuarioLogueado : UserControl
    {
        public DatosUsuarioLogueado()
        {
            InitializeComponent();
        }

        [Import]
        DatosUsuarioLogueadoViewModel ViewModel
        {
            get
            {
                return this.DataContext as DatosUsuarioLogueadoViewModel;
            }
            set 
            {
                this.DataContext = value;
            }
        }

        private void btnCerrarSesion_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var vm = this.ViewModel as DatosUsuarioLogueadoViewModel;
            vm.CerrarSesionCommand.Execute();
        }
    }
}
