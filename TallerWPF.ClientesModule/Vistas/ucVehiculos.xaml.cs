﻿using System;
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
    }
}