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
    /// Interaction logic for ucClientes.xaml
    /// </summary>
    [ViewExport(RegionName = RegionNames.MainRegion)]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class ucClientes : UserControl
    {

        IEventAggregator eventAggregator;
        
        [Import]
        IRegionManager regionManager;

        [ImportingConstructor]
        public ucClientes(IEventAggregator evtAggregator)
        {
            InitializeComponent();
            eventAggregator = evtAggregator;
            eventAggregator.GetEvent<GuardarEvent>().Subscribe(ValidaDatos);
            this.eventAggregator.GetEvent<NuevoClienteEvent>().Subscribe(LimpiarFormulario);
        }

        [Import]
        vmClientes ViewModel
        {
            set
            {
                this.DataContext = value;
            }
       }

        private void grdClientes_SelectionChanged(object sender, Telerik.Windows.Controls.SelectionChangeEventArgs e)
        {
            txtRFC.IsEnabled = false;
        }

        public void LimpiarFormulario(object objeto)
        {
            vmClientes VMClientes = DataContext as vmClientes;
            VMClientes.ClienteSeleccionado = null;
            

            txtCasa.Text = "";
            txtDireccion.Text = "";
            txtEmail.Text = "";
            txtMovil.Text = "";
            txtNombre.Text = "";
            txtNumeroCliente.Text = "";
            txtRFC.Text = "";
            txtTelefono.Text = "";
            txtCP.Text = "";

            cmbCiudad.SelectedIndex = 0;
            cmbEstado.SelectedIndex = 0;

            txtRFC.IsEnabled = true;

            
        }

        public void ValidaDatos(object objeto)
        {
            var vistaActiva = regionManager.Regions[RegionNames.MainRegion].ActiveViews.First();

            if (vistaActiva is ucClientes)
            {
                string mensajeErrores = "Existen errores en los datos a guardar, verifiquelos por favor";
                if (Validation.GetHasError(txtNumeroCliente))
                {
                    MessageBox.Show(mensajeErrores);
                }
                else if (Validation.GetHasError(txtRFC))
                {
                    MessageBox.Show(mensajeErrores);
                }
                else if (Validation.GetHasError(txtNombre))
                {
                    MessageBox.Show(mensajeErrores);
                }
                else if (Validation.GetHasError(txtDireccion))
                {
                    MessageBox.Show(mensajeErrores);
                }
                else if (Validation.GetHasError(cmbMunicipios))
                {
                    MessageBox.Show(mensajeErrores);
                }
                else if (Validation.GetHasError(cmbTipoPersona))
                {
                    MessageBox.Show(mensajeErrores);
                }
                else if (Validation.GetHasError(cmbEstado))
                {
                    MessageBox.Show(mensajeErrores);
                }
                else if (Validation.GetHasError(txtCasa))
                {
                    MessageBox.Show(mensajeErrores);
                }
                else if (Validation.GetHasError(txtTelefono))
                {
                    MessageBox.Show(mensajeErrores);
                }
                else if (Validation.GetHasError(txtMovil))
                {
                    MessageBox.Show(mensajeErrores);
                }
                else
                {
                   vmClientes VMClientes = DataContext as vmClientes;
                   VMClientes.GuardarDatosCommand.Execute();
                }
            }
        }
    }
}
