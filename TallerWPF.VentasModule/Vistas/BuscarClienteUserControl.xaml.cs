using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TallerWPF.Entidades;
using TallerWPF.VentasModule.ViewModels;

namespace TallerWPF.VentasModule.Vistas
{
    /// <summary>
    /// Lógica de interacción para BuscarCliente.xaml
    /// </summary>
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class BuscarClienteUserControl : UserControl
    {
        //public static DependencyProperty ClienteSeleccionadoProperty = DependencyProperty.Register("ClienteSeleccionado", typeof(C_Clientes), typeof(BuscarCliente), new PropertyMetadata(default(C_Clientes)));
        
        public C_Clientes ClienteSeleccionado
        {
            get
            {
                var vm = this.DataContext as BuscarClienteViewModel;
                return vm.ClienteSeleccionado;
            }
        } 

        [ImportingConstructor]
        public BuscarClienteUserControl(BuscarClienteViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }

        [Import]
        BuscarClienteViewModel ViewModel
        {
            set
            {
                this.DataContext = value;
            }
        }

        //public BuscarClienteUserControl()
        //{
        //    InitializeComponent();
        //}
    }
}
