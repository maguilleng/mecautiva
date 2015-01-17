using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.PubSubEvents;
using Servicios;
using TallerWPF.Entidades;
using TallerWPF.Infraestructura;

namespace TallerWPF.VentasModule.ViewModels
{
    [Export(typeof(BuscarVehiculoViewModel))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class BuscarVehiculoViewModel : BindableBase
    {
        IEventAggregator eventAggregator;
        VehiculosService vehiculosService;

        C_Clientes clienteSeleccionado;
        public C_Clientes ClienteSeleccionado
        {
            get { return clienteSeleccionado; }
            set { SetProperty(ref clienteSeleccionado, value); }
        }

        C_Vehiculos vehiculoSeleccionado;
        public C_Vehiculos VehiculoSeleccionado
        {
            get { return vehiculoSeleccionado; }
            set 
            { 
                SetProperty(ref vehiculoSeleccionado, value);
                eventAggregator.GetEvent<VehiculoSeleccionadoEvent>().Publish(vehiculoSeleccionado);
            }
        }

        List<C_Vehiculos> vehiculosCliente;
        public List<C_Vehiculos> VehiculosCliente
        {
            get 
            { 
                if(vehiculosCliente == null)
                {
                    if(ClienteSeleccionado != null)
                        vehiculosCliente = ObtenerVehiculosPorIdCliente(ClienteSeleccionado.IdCliente);
                }

                return vehiculosCliente; 
            }
            set { SetProperty(ref vehiculosCliente, value); }
        }

        [ImportingConstructor]
        public BuscarVehiculoViewModel(VehiculosService vehiculosService, IEventAggregator eventAggregator)
        {
            this.vehiculosService = vehiculosService;
            this.eventAggregator = eventAggregator;

            eventAggregator.GetEvent<ClienteSeleccionadoEvent>().Subscribe(OnClienteSeleccionado);
        }

        public List<C_Vehiculos> ObtenerVehiculosPorIdCliente(int idCliente)
        {
            return vehiculosService.ObtenerVehiculosPorIdCliente(idCliente);
        }

        public void OnClienteSeleccionado(C_Clientes cliente)
        {
            ClienteSeleccionado = cliente;
            VehiculoSeleccionado = null;
            if (cliente != null)
                VehiculosCliente = ObtenerVehiculosPorIdCliente(ClienteSeleccionado.IdCliente);
            else
                VehiculosCliente = null;
        }
    }
}
