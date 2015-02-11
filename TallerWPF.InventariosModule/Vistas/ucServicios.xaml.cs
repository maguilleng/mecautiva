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
using TallerWPF.Entidades;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.ServiceLocation;

using System.Threading.Tasks;
using TallerWPF.InventariosModule.ViewModels;

namespace TallerWPF.InventariosModule.Vistas
{
    /// Interaction logic for ucClientes.xaml
    /// </summary>
    [ViewExport(RegionName = RegionNames.MainRegion)]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class ucServicios : UserControl
    {

        IEventAggregator eventAggregator;

        [Import]
        IRegionManager regionManager;

        [ImportingConstructor]
        public ucServicios(IEventAggregator evtAggregator)
        {
            InitializeComponent();
            eventAggregator = evtAggregator;
            eventAggregator.GetEvent<GuardarEvent>().Subscribe(ValidaDatos);    
        }

        [Import]
        vmServicios ViewModel
        {
            set
            {
                this.DataContext = value;
            }
        }

        private void grdVehiculos_SelectionChanged(object sender, Telerik.Windows.Controls.SelectionChangeEventArgs e)
        {
            txtCodigo.IsEnabled = false;
        }

        public void ValidaDatos(object objeto)
        {
            var vistaActiva = regionManager.Regions[RegionNames.MainRegion].ActiveViews.First();

            if (vistaActiva is ucServicios)
            {
                string mensajeErrores = "Existen errores en los datos a guardar, verifiquelos por favor";
                if (Validation.GetHasError(txtCodigo))
                {
                    MessageBox.Show(mensajeErrores);
                }
                else if (Validation.GetHasError(txtDescripcion))
                {
                    MessageBox.Show(mensajeErrores);
                }
                else if (Validation.GetHasError(cmbTipoServicio))
                {
                    MessageBox.Show(mensajeErrores);
                }
                else if (Validation.GetHasError(cmbMarca))
                {
                    MessageBox.Show(mensajeErrores);
                }
                else if (Validation.GetHasError(cmbUnidadMedida))
                {
                    MessageBox.Show(mensajeErrores);
                }
                else
                {
                    vmServicios VMServicios = this.DataContext as vmServicios;
                    VMServicios.GuardarDatosCommand.Execute();
                }
            }
        }
    }
}
