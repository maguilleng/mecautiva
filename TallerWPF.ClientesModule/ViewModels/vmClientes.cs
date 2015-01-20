using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.PubSubEvents;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TallerWPF.Infraestructura;
using TallerWPF.Entidades;
using Servicios;
using TallerWPF.ClientesModule.SharedServices;
using TallerWPF.Infraestructura.Interfaces;
using System.Windows;


namespace TallerWPF.ClientesModule.ViewModels
{
    [Export]
    class vmClientes : BindableBase
    {
        IServicioCliente servicioCliente;
        IEventAggregator eventAggregator;
        ClientesService clientesCtrl = new ClientesService();



        [ImportingConstructor]
        public vmClientes(IServicioCliente servicioCliente, IEventAggregator evtAggregator)
        {
            this.servicioCliente = servicioCliente;
            eventAggregator = evtAggregator;
            eventAggregator.GetEvent<GuardarEvent>().Subscribe(MensajeExitoso);
        }


        public void MensajeExitoso(object objeto)
        {
            MessageBox.Show("Ejemplo exitoso");
        }

        //Metodos para la persistencia
        private List<C_Clientes> ListadoClientes()
        {
            return clientesCtrl.ObtenerListaClientes().ToList();
        }

        private ObservableCollection<C_Vehiculos> ListadoVehiculos(int idCliente)
        {
            ObservableCollection<C_Vehiculos> vehiculos = new ObservableCollection<C_Vehiculos>();
            clientesCtrl.ObtenerListaVehiculosxCliente(idCliente).ForEach(v => vehiculos.Add(v));

            return vehiculos;
        }

        public List<C_TiposPersona> ListadoTiposPersona()
        {
            return clientesCtrl.ObtenerListaTiposPersona().ToList();
        }

        //Esta lista es la que toma el binding del radgrid de la cartera de clientes
        List<C_Clientes> clientes;
        public List<C_Clientes> Clientes
        {
            get
            {
                if (this.clientes == null)
                {
                    clientes = ListadoClientes();
                    servicioCliente.LLenarListaClientes(Clientes);
                }

                return this.clientes;
            }
            set { SetProperty(ref this.clientes, value); }
        }

        C_Clientes clienteSeleccionado;
        public C_Clientes ClienteSeleccionado
        {
            get { return this.clienteSeleccionado; }
            set
            {
                SetProperty(ref this.clienteSeleccionado, value);

                if (clienteSeleccionado != null)
                {
                    VehiculosxCliente = ListadoVehiculos(clienteSeleccionado.IdCliente);
                }
                else
                {
                    VehiculosxCliente.Clear();
                }
            }
        }

        //Esta lista es la que toma el binding del radgrid filtrado por el id del cliente
        ObservableCollection<C_Vehiculos> vehiculosxCliente;
        public ObservableCollection<C_Vehiculos> VehiculosxCliente
        {
            get
            {
                if (this.vehiculosxCliente == null)
                {
                    if (clienteSeleccionado != null)
                    {
                        vehiculosxCliente = ListadoVehiculos(clienteSeleccionado.IdCliente);
                    }
                }

                return this.vehiculosxCliente;
            }
            set { SetProperty(ref this.vehiculosxCliente, value); }
        }

        C_Vehiculos vehiculoSeleccionado;
        public C_Vehiculos VehiculoSeleccionado
        {
            get { return this.vehiculoSeleccionado; }
            set { SetProperty(ref this.vehiculoSeleccionado, value); }
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


        //TRAEMOS LOS TIPOS DE PERSONAS
        List<C_TiposPersona> tiposPersona;
        public List<C_TiposPersona> TiposPersona
        {

            get
            {
                if (this.tiposPersona == null)
                {
                    tiposPersona = ListadoTiposPersona();
                }

                return this.tiposPersona;
            }
            set { SetProperty(ref this.tiposPersona, value); }
        }
    }
}

