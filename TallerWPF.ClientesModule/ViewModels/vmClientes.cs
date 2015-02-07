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
using TallerWPF.Entidades.VehiculosEntidades;
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
            this.eventAggregator.GetEvent<NuevoClienteEvent>().Subscribe(LimpiarCliente);
        }


        public void MensajeExitoso(object objeto)
        {
            C_Vehiculos nuevoVehiculo = new C_Vehiculos();
            nuevoVehiculo.Placas = VehiculoSeleccionado.Placas;
            nuevoVehiculo.Marca = VehiculoSeleccionado.Marca;
            nuevoVehiculo.Linea = VehiculoSeleccionado.Linea;
            nuevoVehiculo.IdCliente = ClienteSeleccionado.IdCliente;
            nuevoVehiculo.Color = VehiculoSeleccionado.Color;
            nuevoVehiculo.Modelo = VehiculoSeleccionado.Modelo;
            nuevoVehiculo.NoEconomico = VehiculoSeleccionado.NoEconomico;
            MessageBox.Show(clientesCtrl.GuardarVehiculo(nuevoVehiculo));            
        }

        public void LimpiarCliente(object objeto)
        {
    //        ClienteSeleccionado = null;
        }

        //Metodos para la persistencia
        private ObservableCollection<C_ClientesDTO> ListadoClientes()
        {
            ObservableCollection<C_ClientesDTO> clientes = new ObservableCollection<C_ClientesDTO>();
             clientesCtrl.ObtenerListaClientes().ForEach(c => clientes.Add(new C_ClientesDTO(c)));
             return clientes;
        }

        private ObservableCollection<C_VehiculosDTO> ListadoVehiculos(int idCliente)
        {
            ObservableCollection<C_VehiculosDTO> vehiculos = new ObservableCollection<C_VehiculosDTO>();
            
            clientesCtrl.ObtenerListaVehiculosxCliente(idCliente).ForEach(v => vehiculos.Add( new C_VehiculosDTO( v)));

            return vehiculos;
        }

        public List<C_TiposPersona> ListadoTiposPersona()
        {
            return clientesCtrl.ObtenerListaTiposPersona().ToList();
        }

        public List<C_Estados> ListadoEstados()
        {
            return clientesCtrl.ObtenerListaEstados().ToList();
        }

        public ObservableCollection<C_Municipios> ListadoMunicipiosPorEstado(int idEstado)
        {
            ObservableCollection<C_Municipios> municipios = new ObservableCollection<C_Municipios>();
            clientesCtrl.ObtenerListaMunicipiosPorEstado(idEstado).ForEach(m => municipios.Add(m));
            return municipios;
        }

        public ObservableCollection<C_Ciudades> ListadoCiudadesPorMunicipio(int idMunicipio)
        {
            ObservableCollection<C_Ciudades> ciudades = new ObservableCollection<C_Ciudades>();
            clientesCtrl.ObtenerListaCiudadesPorMunicipio(idMunicipio).ForEach(c => ciudades.Add(c));
            return ciudades;
        }

        //Esta lista es la que toma el binding del radgrid de la cartera de clientes
        ObservableCollection<C_ClientesDTO> clientes;
        public ObservableCollection<C_ClientesDTO> Clientes
        {
            get
            {
                if (this.clientes == null)
                {
                    clientes = ListadoClientes();
                }

                return this.clientes;
            }
            set { SetProperty(ref this.clientes, value); }
        }

        //ANTES FUNCIONABA CORRECTAMENTE
        
        C_ClientesDTO clienteSeleccionado;
        public C_ClientesDTO ClienteSeleccionado
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
        
        /*
        C_ClientesDTO clienteSeleccionado;
        public C_ClientesDTO ClienteSeleccionado
        {

            get
            {
                if (clienteSeleccionado == null)
                {
                    C_Clientes cliente = new C_Clientes();
                    clienteSeleccionado = new C_ClientesDTO(cliente)
                    {
                     /*  IdCliente = 0,
                        NumeroCliente = "",
                        Nombre = "",
                        IdTipoPersona = 0,
                        Direccion = "",
                        IdCiudad = 0,
                        CodigoPostal = 0,
                        IdMunicipio = 0,
                        Movil = 0,
                        Trabajo = 0,
                        Casa = 0,
                        Email = "",
                        IdEstado = 0
                    };

                }
                return this.clienteSeleccionado;
            }
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
        }*/
        //MODULO VEHICULOS
        //Esta lista es la que toma el binding del radgrid filtrado por el id del cliente
        //ANTES con la entidad del model
       /* ObservableCollection<C_Vehiculos> vehiculosxCliente;
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
        }*/

        //Ahora con la entidad del DTO
        ObservableCollection<C_VehiculosDTO> vehiculosxCliente;
        public ObservableCollection<C_VehiculosDTO> VehiculosxCliente
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

        string placaActual;
        public string PlacaActual
        {
            get { return placaActual; }
            set { placaActual = value; 
            
            }
        }

        //VEHICULO seleccionado ahora
        C_VehiculosDTO vehiculoSeleccionado;
        public C_VehiculosDTO VehiculoSeleccionado
        {       
            get
              {
                  if (vehiculoSeleccionado == null)
                  {
                      C_Vehiculos vehiculo = new C_Vehiculos();
                      vehiculoSeleccionado = new C_VehiculosDTO(vehiculo)
                      {
                          Placas = "",
                          Marca = "",
                          Linea = "",
                          Modelo = 0,
                          Color = "",
                          NoEconomico = "",
                          IdCliente = 0 
                      };
                      
                  }
                  return this.vehiculoSeleccionado;
            }
            set
            {
                SetProperty(ref this.vehiculoSeleccionado, value);
                PlacaActual = VehiculoSeleccionado.Placas;
            }
        }

      

        public bool ValidarPlaca(string placa)
        {
            return clientesCtrl.ValidarPlaca(placa);
        }

        //TRAEMOS LOS TIPOS DE PERSONAS

        List<C_TiposPersona> tiposPersona;
        public List<C_TiposPersona> TiposPersona
        {
              
            get
            {
                C_TiposPersona tipoPersona = new C_TiposPersona();
                tipoPersona.Descripcion = "Seleccione...";

                if (this.tiposPersona == null)
                {
                    
                    tiposPersona = ListadoTiposPersona();
                    tiposPersona.Insert(0, tipoPersona);
                }

                return this.tiposPersona;
            }
            set { SetProperty(ref this.tiposPersona, value); }
        }

        //TREAEMOS LOS ESTADOS
        List<C_Estados> estados;
        public List<C_Estados> Estados
        {

            get
            {
                C_Estados estado = new C_Estados();
                estado.Descripcion = "Seleccione...";

                if (this.estados == null)
                {

                    estados = ListadoEstados();
                    estados.Insert(0, estado);
                }

                return this.estados;
            }
            set { SetProperty(ref this.estados, value); }
        }

        C_Estados estadoSeleccionado;
        public C_Estados EstadoSeleccionado
        {
            get { return this.estadoSeleccionado; }
            set
            {
                SetProperty(ref this.estadoSeleccionado, value);

                if (estadoSeleccionado != null)
                {
                    MunicipiosPorEstado = ListadoMunicipiosPorEstado(estadoSeleccionado.IdEstado);
                }
                else
                {
                    MunicipiosPorEstado.Clear();
                }
            }
        }
        
        //TRAEMOS LOS MUNICIPIOS
        ObservableCollection<C_Municipios> municipiosPorEstado;
        public ObservableCollection<C_Municipios> MunicipiosPorEstado
        {

             get
            {
             C_Municipios municipio = new C_Municipios();
                municipio.Descripcion = "Seleccione...";
                if (this.municipiosPorEstado == null)
                {
                    if (estadoSeleccionado != null)
                    {
                        municipiosPorEstado = ListadoMunicipiosPorEstado(estadoSeleccionado.IdEstado);
                         municipiosPorEstado.Insert(0, municipio);
                    }
                }

                return this.municipiosPorEstado;
            }
            set { SetProperty(ref this.municipiosPorEstado, value); }
        }

          C_Municipios municipioSeleccionado;
        public C_Municipios MunicipioSeleccionado
        {
            get { return this.municipioSeleccionado; }
            set
            {
                SetProperty(ref this.municipioSeleccionado, value);

                if (municipioSeleccionado != null)
                {
                    CiudadesPorMunicipio = ListadoCiudadesPorMunicipio(municipioSeleccionado.IdMunicipio);
                }
                else
                {
                    CiudadesPorMunicipio.Clear();
                }
            }
        }

        //TRAEMOS LOS MUNICIPIOS
        ObservableCollection<C_Ciudades> ciudadesPorMunicipio;
        public ObservableCollection<C_Ciudades> CiudadesPorMunicipio
        {

            get
            {
                C_Ciudades ciudad = new C_Ciudades();
                ciudad.Descripcion = "Seleccione...";
                if (this.ciudadesPorMunicipio == null)
                {
                    if (estadoSeleccionado != null)
                    {
                        ciudadesPorMunicipio = ListadoCiudadesPorMunicipio(municipioSeleccionado.IdMunicipio);
                        ciudadesPorMunicipio.Insert(0, ciudad);
                    }
                }

                return this.ciudadesPorMunicipio;
            }
            set { SetProperty(ref this.ciudadesPorMunicipio, value); }
        }
    }
}

