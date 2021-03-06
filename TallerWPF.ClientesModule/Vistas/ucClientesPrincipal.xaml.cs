﻿using System.ComponentModel.Composition;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Controls;
using TallerWPF.Infraestructura;
using Microsoft.Practices.Prism.Regions;
using System.Windows;
using TallerWPF.ClientesModule.ViewModels;

namespace TallerWPF.ClientesModule.Vistas
{
    /// <summary>
    /// Interaction logic for ucClientesPrincipal.xaml
    /// </summary>
    [ViewExport(RegionName = RegionNames.MainRegion)]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class ucClientesPrincipal : UserControl
    {
        public ucClientesPrincipal()
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
    }
}
