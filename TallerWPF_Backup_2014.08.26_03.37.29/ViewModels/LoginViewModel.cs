using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Prism.Regions;
using Servicios;
using System.ComponentModel.Composition;
using System.Windows.Controls;
using TallerWPF.Entidades;
using TallerWPF.Infraestructura;
using Telerik.Windows.Controls;

namespace TallerWPF.ViewModels
{
    [Export]
    public class LoginViewModel : BindableBase
    {
        IEventAggregator eventAggregator;

        [Import]
        IRegionManager regionManager;
        
        [Import]
        UsuarioService usuarioService;
        
        #region Commands

        public DelegateCommand<object> IniciarSesionCommand { get; set; }
        public Microsoft.Practices.Prism.Commands.DelegateCommand CancelarInicioSesionCommand { get; set; }

        #endregion
        
        #region Properties       

        string strUsuario;
        string StrUsuario 
        {
            get
            {
                return strUsuario;
            }
            set
            {
                SetProperty(ref this.strUsuario, value);
            }
        }

        string strContraseña;
        string StrContraseña 
        {
            get
            {
                return strContraseña;
            }
            set 
            {
                SetProperty(ref this.strContraseña, value);
            }
        }

        Usuario usuarioLogueado;
        Usuario UsuarioLogueado { get; set; }
        
        #endregion
        
        [ImportingConstructor]
        public LoginViewModel(IEventAggregator evtAggregator, IRegionManager regionManager)
        {
            eventAggregator = evtAggregator;
            this.regionManager = regionManager;
            IniciarSesionCommand = new DelegateCommand<object>(IniciarSesion);
        }
        
        public void IniciarSesion(object passBox)
        {
            PasswordBox passwordBox = passBox as PasswordBox;

            UsuarioLogueado = usuarioService.LoginUsuario(StrUsuario, passwordBox.Password);

            if (UsuarioLogueado != null)
            {
                eventAggregator.GetEvent<UsuarioLogueadoEvent>().Publish(UsuarioLogueado);
                regionManager.RequestNavigate(RegionNames.RibbonRegion, "RibbonView");
                regionManager.RequestNavigate(RegionNames.MainRegion, "VentasPrincipal");
            }
        }
    }
}
