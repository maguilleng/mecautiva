using System.ComponentModel.Composition;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Controls;
using TallerWPF.Infraestructura;
using Microsoft.Practices.Prism.Regions;
using System.Windows;
using TallerWPF.InventariosModule.ViewModels;

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

        [Import]
        vmServicios ViewModel
        {
            set
            {
                this.DataContext = value;
            }
        }
    }
}
