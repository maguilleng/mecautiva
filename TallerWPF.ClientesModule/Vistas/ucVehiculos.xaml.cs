using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Controls;
using TallerWPF.Infraestructura;
using Microsoft.Practices.Prism.Regions;
using System.Windows;
using TallerWPF.ClientesModule.ViewModels;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.ServiceLocation;

using System.Threading.Tasks;
namespace TallerWPF.ClientesModule.Vistas
{
    /// <summary>
    /// Interaction logic for ucVehiculos.xaml
    /// </summary>
    [ViewExport(RegionName = RegionNames.MainRegion)]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class ucVehiculos : UserControl
    {
        public ucVehiculos()
        {
            InitializeComponent();
        }

        [Import]
        vmClientes ViewModel
        {
            set
            {
                this.DataContext = value;
            }
        }

        private void grdVehiculos_SelectionChanged(object sender, Telerik.Windows.Controls.SelectionChangeEventArgs e)
        {
           
        }

        private void cmbClientes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbClientes.SelectedItem != null)
            {
                txtColor.IsEnabled = true;
                txtLinea.IsEnabled = true;
                txtMarca.IsEnabled = true;
                txtModelo.IsEnabled = true;
                txtNoEconomica.IsEnabled = true;
                txtPlaca.IsEnabled = true;
            }
            else
            {
                txtColor.IsEnabled = false;
                txtLinea.IsEnabled = false;
                txtMarca.IsEnabled = false;
                txtModelo.IsEnabled = false;
                txtNoEconomica.IsEnabled = false;
                txtPlaca.IsEnabled = false;
            }
        }
    }
}
