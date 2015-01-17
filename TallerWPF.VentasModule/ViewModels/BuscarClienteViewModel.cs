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
    [Export(typeof(BuscarClienteViewModel))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class BuscarClienteViewModel : BindableBase
    {
        IEventAggregator eventAggregator;
        ClientesController clientesController;
        
        C_Clientes clienteSeleccionado;
        public C_Clientes ClienteSeleccionado
        {
            get 
            { 
                return clienteSeleccionado;
            }
            set 
            {
                SetProperty(ref this.clienteSeleccionado, value);
                eventAggregator.GetEvent<ClienteSeleccionadoEvent>().Publish(this.clienteSeleccionado);
            }
        }

        List<C_Clientes> catalogoClientes;
        public List<C_Clientes> CatalogoClientes
        {
            get 
            {
                if (this.catalogoClientes == null)
                    this.catalogoClientes = ObtenerCatalogoClientes();
                
                return catalogoClientes; 
            }
            set 
            {
                SetProperty(ref this.catalogoClientes, value);          
            }
        }

        bool buscarPorIdCliente;
        public bool BuscarPorIdCliente
        {
            get { return buscarPorIdCliente; }
            set
            {
                if (buscarPorIdCliente == value)
                    return;
                SetProperty(ref this.buscarPorIdCliente, value);
            }
        }
        
        [ImportingConstructor]
        public BuscarClienteViewModel(ClientesController clientesController, IEventAggregator eventAggregator)
        {
            this.clientesController = clientesController;
            this.eventAggregator = eventAggregator;
        }

        public List<C_Clientes> ObtenerCatalogoClientes()
        {
            var clientes = clientesController.ObtenerListaClientes();
            return clientes;
        }
    }
}
