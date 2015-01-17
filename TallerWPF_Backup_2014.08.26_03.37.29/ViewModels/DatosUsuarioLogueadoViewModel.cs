using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Prism.Regions;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TallerWPF.Entidades;
using TallerWPF.Infraestructura;

namespace TallerWPF.ViewModels
{
    [Export]
    public class DatosUsuarioLogueadoViewModel : BindableBase
    {
        public DelegateCommand CerrarSesionCommand{get;set;}
        
        [Import]
        IEventAggregator eventAggregator;
        
        [Import]
        IRegionManager regionManager;
        
        Usuario usuarioLogueado;
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
        
        [ImportingConstructor]
        public DatosUsuarioLogueadoViewModel(IEventAggregator evtAggregator)
        {
            this.eventAggregator = evtAggregator;            
            eventAggregator.GetEvent<UsuarioLogueadoEvent>().Subscribe(OnUsuarioLogueado);
            CerrarSesionCommand = new DelegateCommand(OnCerrarSesion);
        }

        public void OnUsuarioLogueado(Usuario usuarioLogueado)
        {
            UsuarioLogueado = usuarioLogueado;
            regionManager.RequestNavigate(RegionNames.UsuarioRegion, "DatosUsuarioLogueado");
        }

        public void OnCerrarSesion()
        {
            regionManager.Regions[RegionNames.UsuarioRegion].Deactivate(regionManager.Regions[RegionNames.UsuarioRegion].ActiveViews.First());
            regionManager.Regions[RegionNames.RibbonRegion].Deactivate(regionManager.Regions[RegionNames.RibbonRegion].ActiveViews.First());
            regionManager.RequestNavigate(RegionNames.MainRegion, "Login");
        }
    }
}
