using Microsoft.Practices.Prism.Regions;
using System.ComponentModel.Composition;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Controls;
using TallerWPF.Infraestructura;
using TallerWPF.VentasModule.ViewModels;

namespace TallerWPF.VentasModule.Vistas
{
    /// <summary>
    /// Lógica de interacción para NuevaVentaUserControl.xaml
    /// </summary>
    [ViewExport(RegionName = RegionNames.MainRegion, IsActiveByDefault=false)]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class NuevaVentaUserControl : UserControl, INavigationAware, IConfirmNavigationRequest
    {
        [ImportingConstructor]
        public NuevaVentaUserControl()
        {
            InitializeComponent();
        }

        [Import]
        NuevaVentaViewModel ViewModel
        {
            set
            {
                this.DataContext = value;
            }
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            //throw new System.NotImplementedException();
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            //throw new System.NotImplementedException();
            int i = 1;
            return;
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            //throw new System.NotImplementedException();
            int i = 0;
        }

        public void ConfirmNavigationRequest(NavigationContext navigationContext, System.Action<bool> continuationCallback)
        {
            continuationCallback(MessageBox.Show("No ha finalizado la Venta Actual, perderá los cambios si continúa navegando a otra sección.\nDesea Continuar?", "Venta No Finalizada", MessageBoxButton.YesNo) == MessageBoxResult.Yes); ;
        }

        private void grdProductosVenta_Deleted(object sender, Telerik.Windows.Controls.GridViewDeletedEventArgs e)
        {
            var vm = this.DataContext as NuevaVentaViewModel;
            vm.ProductoEliminadoCommand.Execute();
        }

        private void grdProductosVenta_RowEditEnded(object sender, Telerik.Windows.Controls.GridViewRowEditEndedEventArgs e)
        {
            var vm = this.DataContext as NuevaVentaViewModel;
            vm.ProductoActualizadoCommand.Execute();
        }
    }
}
