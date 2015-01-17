using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.ComponentModel.Composition;
using TallerWPF.Infraestructura;
using TallerWPF.ViewModels;
using Telerik.Windows.Controls.RibbonView;

namespace TallerWPF.Vistas
{
    /// <summary>
    /// Lógica de interacción para RibbonView.xaml
    /// </summary>
    [ViewExport(RegionName = RegionNames.RibbonRegion, IsActiveByDefault = false)]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class RibbonView : UserControl
    {
        public RibbonView()
        {
            InitializeComponent();
        }
    }
}
