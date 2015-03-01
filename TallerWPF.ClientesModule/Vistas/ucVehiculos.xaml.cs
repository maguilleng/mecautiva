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
using TallerWPF.Entidades;
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

        IEventAggregator eventAggregator;

        [Import]
        IRegionManager regionManager;

        [ImportingConstructor]
        public ucVehiculos(IEventAggregator evtAggregator)
        {
            InitializeComponent();
            eventAggregator = evtAggregator;
            eventAggregator.GetEvent<GuardarEvent>().Subscribe(ValidaDatos);
            this.eventAggregator.GetEvent<NuevoVehiculoEvent>().Subscribe(LimpiarFormulario);
            

            this.txtModelo.NumberFormatInfo = new System.Globalization.NumberFormatInfo();
            this.txtModelo.NumberFormatInfo.NumberGroupSeparator = "";
        }

      [Import]
        vmVehiculos ViewModelVehiculos
        {
            set
            {
                this.DataContext = value;
            }
        }

        private void grdVehiculos_SelectionChanged(object sender, Telerik.Windows.Controls.SelectionChangeEventArgs e)
        {
            txtPlaca.IsEnabled = false;
        }

        private void cmbClientes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbClientes.SelectedItem != null)
            {
                txtColor.IsEnabled = true;
                txtLinea.IsEnabled = true;
                txtMarca.IsEnabled = true;
                txtModelo.IsEnabled = true;
                txtNoEconomico.IsEnabled = true;
                txtPlaca.IsEnabled = true;
            }
            else
            {
                txtColor.IsEnabled = false;
                txtLinea.IsEnabled = false;
                txtMarca.IsEnabled = false;
                txtModelo.IsEnabled = false;
                txtNoEconomico.IsEnabled = false;
                txtPlaca.IsEnabled = false;
            }
        }

        public void LimpiarFormulario(object objeto)
        {
            vmVehiculos VMVehiculos = DataContext as vmVehiculos;
            VMVehiculos.VehiculoSeleccionado = null;

            txtColor.Text = "";
            txtLinea.Text = "";
            txtMarca.Text = "";
            txtModelo.Value = 0;
            txtNoEconomico.Text = "";
            txtPlaca.Text = "";
        }

        public void ValidaDatos(object objeto)
        {
            var vistaActiva = regionManager.Regions[RegionNames.MainRegion].ActiveViews.First();

            if (vistaActiva is ucVehiculos)
            {
                string mensajeErrores = "Existen errores en los datos a guardar, verifiquelos por favor";
            /*    if (Validation.GetHasError(txtPlaca))
                {
                    MessageBox.Show(mensajeErrores);
                }
              /*  else if (cmbClientes.SelectedItem == null)
                {
                    MessageBox.Show("Debe seleccionar un cliente");
                }*/
                /* else if (Validation.GetHasError(txtMarca))
                 {
                     MessageBox.Show("mensajeErrores");
                 }
                 else if (Validation.GetHasError(txtLinea))
                 {
                     MessageBox.Show("mensajeErrores");
                 }
                 else
                 {
                     vmVehiculos VMVehiculos = this.DataContext as vmVehiculos;
                     VMVehiculos.GuardarDatosCommand.Execute();
                 }*/
                vmVehiculos VMVehiculos = this.DataContext as vmVehiculos;
                VMVehiculos.GuardarDatosCommand.Execute();
            }
        }
    }
}
