using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Prism.Regions;
using Servicios;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.Composition;
using System.Windows.Controls;
using TallerWPF.Entidades;
using TallerWPF.Infraestructura;
using TallerWPF.Infraestructura.Interfaces;
using Telerik.Windows.Controls;
using TallerWPF.VentasModule.Vistas;
using System.Windows;

namespace TallerWPF
{
    [Export]
    public class ShellViewModel : BindableBase
    {
        IEventAggregator eventAggregator;
        IServicioVenta servicioVenta;
        IServicioCliente servicioCliente;
        
        [Import]
        IRegionManager regionManager;

        #region Commands

        public DelegateCommand<SelectionChangedEventArgs> TabMenuCommand { get; set; }
        public DelegateCommand<string> BotonMenuCommand { get; set; }
        public Microsoft.Practices.Prism.Commands.DelegateCommand PagarVentaCommand { get; set; }

        #endregion

        #region Properties
        public string UserName { get; set; }
        
        private bool esUsuarioLogueado;
        public bool EsUsuarioLogueado 
        {
            get
            {
                return this.esUsuarioLogueado;
            }
            set
            {
                SetProperty(ref this.esUsuarioLogueado, value);
            }
        }

        private Usuario usuarioLogueado;
        public Usuario UsuarioLogueado
        {
            get
            {
                return this.usuarioLogueado;
            }
            set
            {
                SetProperty(ref this.usuarioLogueado, value);
            }
        }
        #endregion
        

        [ImportingConstructor]
        public ShellViewModel(IEventAggregator evtAggregator, IServicioVenta servicioVenta, IServicioCliente servicioCliente)
        {
            eventAggregator = evtAggregator;
            this.servicioVenta = servicioVenta;
            eventAggregator.GetEvent<UsuarioLogueadoEvent>().Subscribe(OnUsuarioLogueado);
            TabMenuCommand = new DelegateCommand<SelectionChangedEventArgs>(OnTabMenu);
            BotonMenuCommand = new DelegateCommand<string>(OnBotonMenuCommand);

            EsUsuarioLogueado = false;
        }

        public void OnTabMenu(SelectionChangedEventArgs obj)
        {
            if (obj.AddedItems.Count > 0)
            {
                var nombreModulo = "";
                RadRibbonTab item = obj.AddedItems[0] as RadRibbonTab;
                if (item != null)
                {
                    nombreModulo = item.Tag as string;
                }

                switch (nombreModulo)
                {
                    case "ModuloVentas":
                        var vistaActiva = regionManager.Regions[RegionNames.MainRegion].ActiveViews.First();
                        
                        if (vistaActiva is NuevaVentaUserControl)
                        {
                            return;
                        }
                        else
                        {
                            regionManager.RequestNavigate(RegionNames.MainRegion, "NuevaVentaUserControl");
                        }

                        break;
                    case "ModuloInventarios":
                        regionManager.RequestNavigate(RegionNames.MainRegion, "InventariosPrincipal");
                        break;
                    case "ModuloClientes":
                        regionManager.RequestNavigate(RegionNames.MainRegion, "ucClientesPrincipal");
                        break;  
                    default:
                        regionManager.RequestNavigate(RegionNames.MainRegion, "VentasPrincipal");
                        break;
                }
            }           
        }

        public void OnBotonMenuCommand(string parametroBoton)
        {
            switch (parametroBoton)
            {
                case "VentaConFactura":
                    EventoNuevaVenta(parametroBoton);
                    break;
                case "VentaSencilla":
                    EventoNuevaVenta(parametroBoton);
                    break;
                case "LimpiarProductosVentaActual":
                    servicioVenta.LimpiarProductosVentaActual();
                    break;
                case "PagarVentaActual":
                    //servicioVenta.PagarVentaActual();
                    eventAggregator.GetEvent<PagarVentaActualEvent>().Publish(null);
                    break;
                //CLIENTES
                case "CarteraClientes":
                    //Necesario mandar a guardar, editar, etc. (Se puede con los metodos?) y Necesario poder cambiar de Regiones (Exit)
                    //Dispara una región
                    regionManager.RequestNavigate(RegionNames.MainRegion, "ucClientesPrincipal");
                    break;
                case "NuevoCliente":
                    regionManager.RequestNavigate(RegionNames.MainRegion, "ucClientes");
                    eventAggregator.GetEvent<NuevoClienteEvent>().Publish(null);
                    break;
                case "Guardar":
                    eventAggregator.GetEvent<GuardarEvent>().Publish(null);
                    break;
                case "NuevoVehiculo":
                    regionManager.RequestNavigate(RegionNames.MainRegion, "ucVehiculos");
                    break;
                //end CLIENTES
                default:
                    break;
            }
        }

        public void OnUsuarioLogueado(Usuario usrLogueado)
        {
            UsuarioLogueado = usrLogueado;
            EsUsuarioLogueado = true;            
        }

        public void EventoNuevaVenta(string tipoVenta)
        {
            bool requiereFactura = tipoVenta == "VentaConFactura" ? true : false;
            var vistaActiva = regionManager.Regions[RegionNames.MainRegion].ActiveViews.First();
            if (vistaActiva is NuevaVentaUserControl)
            {
                var ventaDetalles = servicioVenta.ObtenerDetallesVenta();
                if (ventaDetalles != null && ventaDetalles.Count > 0)
                {
                    bool limpiarVenta = MessageBox.Show("No ha finalizado la Venta Actual, perderá los cambios si continúa navegando a otra sección.\nDesea Continuar?", "Venta No Finalizada", MessageBoxButton.YesNo) == MessageBoxResult.Yes;
                    if (limpiarVenta)
                        eventAggregator.GetEvent<CrearNuevaVenta>().Publish(requiereFactura);
                }
                else
                {
                    eventAggregator.GetEvent<CrearNuevaVenta>().Publish(requiereFactura);
                }               
            }
            else
            {
                eventAggregator.GetEvent<CrearNuevaVenta>().Publish(requiereFactura);
                regionManager.RequestNavigate(RegionNames.MainRegion, "NuevaVentaUserControl");
            }           
        }
    }
}
