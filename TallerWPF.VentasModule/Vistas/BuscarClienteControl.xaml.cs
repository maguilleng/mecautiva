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
using Microsoft.Practices.ServiceLocation;
using TallerWPF.Entidades;
using TallerWPF.VentasModule.ViewModels;

namespace TallerWPF.VentasModule.Vistas
{
    /// <summary>
    /// Lógica de interacción para BuscarClienteControl.xaml
    /// </summary>
    [Export(typeof(BuscarClienteControl))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class BuscarClienteControl : UserControl, IPartImportsSatisfiedNotification
    {        
        public BuscarClienteControl()
        {
            InitializeComponent();
            this.DataContext = ServiceLocator.Current.GetInstance<BuscarClienteViewModel>();
        }

        public void OnImportsSatisfied()
        {
        }
    }
}
