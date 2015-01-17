using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Regions;
using TallerWPF.Infraestructura;

namespace Servicios
{
    [Export]
    public class NavigationManager
    {
        IRegionManager regionManager;
        
        [ImportingConstructor]
        public NavigationManager(IRegionManager regionManager)
        {
            this.regionManager = regionManager;
        }
        
        public void MostrarVista(string nombreVista)
        {
            switch (nombreVista)
	        {
		        case "NuevaVentaUserControl":
                    regionManager.RequestNavigate(RegionNames.MainRegion, nombreVista);
                    break;
                case "InventariosPrincipal":
                    regionManager.RequestNavigate(RegionNames.MainRegion, nombreVista);
                    break;
                case "VentasPrincipal":
                    regionManager.RequestNavigate(RegionNames.MainRegion, nombreVista);
                    break;
                case "ucClientesPrincipal":
                    regionManager.RequestNavigate(RegionNames.MainRegion, nombreVista);
                    break;
                default:
                    regionManager.RequestNavigate(RegionNames.MainRegion, nombreVista);
                    break;
	        }
        }

        public void MostrarVista(string regionName, string nombreVista)
        {
            regionManager.RequestNavigate(regionName, nombreVista);
        }
    }
}
