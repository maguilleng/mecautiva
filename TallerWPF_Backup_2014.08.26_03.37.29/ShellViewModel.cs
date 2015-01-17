using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Prism.Regions;
using Servicios;
using System.ComponentModel.Composition;
using System.Windows.Controls;
using TallerWPF.Entidades;
using TallerWPF.Infraestructura;
using TallerWPF.Infraestructura.Interfaces;
using Telerik.Windows.Controls;

namespace TallerWPF
{
    [Export]
    public class ShellViewModel : BindableBase
    {
        IEventAggregator eventAggregator;
        IServicioVenta servicioVenta;
        
        [Import]
        IRegionManager regionManager;

        #region Commands
        public DelegateCommand<SelectionChangedEventArgs> TabMenuCommand { get; set; }
        public DelegateCommand<string> BotonMenuCommand { get; set; }
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
        public ShellViewModel(IEventAggregator evtAggregator, IServicioVenta servicioVenta)
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
                        regionManager.RequestNavigate(RegionNames.MainRegion, "VentasPrincipal");
                        break;
                    case "ModuloInventarios":
                        regionManager.RequestNavigate(RegionNames.MainRegion, "InventariosPrincipal");
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
                case "NuevaVenta":
                    regionManager.RequestNavigate(RegionNames.MainRegion, "NuevaVentaUserControl");
                    break;
                case "LimpiarProductosVentaActual":
                    servicioVenta.LimpiarProductosVentaActual();
                    break;
                default:
                    break;
            }
        }

        public void OnUsuarioLogueado(Usuario usrLogueado)
        {
            UsuarioLogueado = usrLogueado;
            EsUsuarioLogueado = true;            
        }
    }
}
