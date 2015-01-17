using System.ComponentModel.Composition;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Controls;
using TallerWPF.Infraestructura;
using TallerWPF.ViewModels;

namespace TallerWPF.Vistas
{
    /// <summary>
    /// Lógica de interacción para Login.xaml
    /// </summary>
    [ViewExport(RegionName = RegionNames.MainRegion, IsActiveByDefault=true)]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class Login : UserControl
    {
        public Login()
        {
            InitializeComponent();
        }

        [Import]
        LoginViewModel ViewModel
        {
            set
            {
                this.DataContext = value;
            }
        }
    }
}
