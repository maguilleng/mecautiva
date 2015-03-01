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
using TallerWPF.Entidades;
using TallerWPF.Entidades.VehiculosEntidades;

using Servicios;
using System.Windows;

using System.ComponentModel.DataAnnotations;

namespace TallerWPF.ClientesModule.ViewModels
{
    [Export]
    class vmVehiculos : ValidatableBindableBase
    {

        #region ATRIBUTOS PRIVAODOS
        ClientesService clientesCtrl = new ClientesService();
        #endregion

        #region COMMANDS
        public DelegateCommand GuardarDatosCommand;
        #endregion

        #region CONSTRUCTORES
        
        [ImportingConstructor]
        public vmVehiculos()
        {
            GuardarDatosCommand = new DelegateCommand(guardarDatosVehiculo);
        }
        #endregion

        #region METODOS A LA PERSISTENCIA

        public void guardarDatosVehiculo()
        {
            this.ValidateProperties();

           
            

            if (ClienteSeleccionado.HasErrors)
            {
                Errores = ObtenerErrores();
                MessageBox.Show("Existen errores");
            }
            else {

                C_Vehiculos nuevoVehiculo = new C_Vehiculos();

                nuevoVehiculo.IdVehiculo = vehiculoSeleccionado.IdVehiculo;
                nuevoVehiculo.Placas = VehiculoSeleccionado.Placas;
                nuevoVehiculo.Marca = VehiculoSeleccionado.Marca;
                nuevoVehiculo.Linea = VehiculoSeleccionado.Linea;
                nuevoVehiculo.IdCliente = ClienteSeleccionado.IdCliente;
                nuevoVehiculo.Color = VehiculoSeleccionado.Color;
                nuevoVehiculo.Modelo = VehiculoSeleccionado.Modelo;
                nuevoVehiculo.NoEconomico = VehiculoSeleccionado.NoEconomico;
                nuevoVehiculo.Estatus = 1;
                MessageBox.Show(clientesCtrl.GuardarVehiculo(nuevoVehiculo));
            }
            
        }

        private ObservableCollection<C_VehiculosDTO> ListadoVehiculos(int idCliente)
        {
            ObservableCollection<C_VehiculosDTO> vehiculos = new ObservableCollection<C_VehiculosDTO>();

            clientesCtrl.ObtenerListaVehiculosxCliente(idCliente).ForEach(v => vehiculos.Add(new C_VehiculosDTO(v)));

            return vehiculos;
        }
        #endregion

        #region PROPIEDADES
        ObservableCollection<C_ClientesDTO> clientes;
        public ObservableCollection<C_ClientesDTO> Clientes
        {

            get
            {
                if (this.clientes == null)
                {
                    vmClientes ViewModelClientes = new vmClientes();
                    clientes = ViewModelClientes.Clientes;
                }

                return this.clientes;
            }
            set { SetProperty(ref this.clientes, value); }
        }

        
        C_ClientesDTO clienteSeleccionado;
        public C_ClientesDTO ClienteSeleccionado
        {  // Aqui deberia ir el MOdelLocator
            get
            {
                return this.clienteSeleccionado;
            }
            set
            {
                SetProperty(ref this.clienteSeleccionado, value);

                VerificarCantidadValida();

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
        
        ObservableCollection<C_VehiculosDTO> vehiculosxCliente;
        public ObservableCollection<C_VehiculosDTO> VehiculosxCliente
        {
            get
            {
                if (this.vehiculosxCliente == null)
                {
                    vmClientes ViewModelClientes = new vmClientes();
                    if (ViewModelClientes.ClienteSeleccionado != null)
                    {
                        vehiculosxCliente = ListadoVehiculos(ViewModelClientes.ClienteSeleccionado.IdCliente);
                    }
                }

                return this.vehiculosxCliente;
            }
            set { SetProperty(ref this.vehiculosxCliente, value); }
        }

        C_VehiculosDTO vehiculoSeleccionado;
        public C_VehiculosDTO VehiculoSeleccionado
        {
            get
            {
                if (vehiculoSeleccionado == null)
                {
                    vehiculoSeleccionado = new C_VehiculosDTO()
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
            }
        }

        #region PROPIEDAD

        List<string> errores;
        public List<string> Errores
        {
            get { return ObtenerErrores(); }
            set { SetProperty(ref this.errores, value); }
        }
        public void VerificarCantidadValida()
        {
            if (this.ClienteSeleccionado == null)
            {
                var err = new String[] { "No existe un cliente seleccionado." }.AsEnumerable();
                this.SetErrors(() => this.ClienteSeleccionado, err);

                Errores = ObtenerErrores();
                // OnErrorsChanged(new DataErrorsChangedEventArgs("CantidadRecibidaPago"));
            }
            else
            {
                this.ValidateProperties();

                /*   if (TipoPagoSeleccionado != null)
                   {
                       //DetallePagoSeleccionado.ValidateProperties();
                       var erroresPago = DetallePagoSeleccionado.GetAllErrors().Select(e => e.Key).ToList();
                       foreach (var error in erroresPago)
                       {
                           DetallePagoSeleccionado.ErrorsContainer.ClearErrors(error);
                       }

                       foreach (var propiedad in TipoPagoSeleccionado.ValidacionesFormasPago)
                       {
                           this.DetallePagoSeleccionado.ValidateProperty(propiedad.PropiedadValidadaPagosDetalle);
                       }
                   }*/

                Errores = ObtenerErrores();
            }
        }
        #endregion

        private List<string> ObtenerErrores()
        {
            List<string> errors = new List<string>();
            Dictionary<string, List<string>> allErrors = this.GetAllErrors();
            foreach (string propertyName in allErrors.Keys)
            {
                foreach (var errorString in allErrors[propertyName])
                {
                    errors.Add(errorString);
                }
            }

           // allErrors = ClienteSeleccionado.GetAllErrors();
            foreach (string propertyName in allErrors.Keys)
            {
                foreach (var errorString in allErrors[propertyName])
                {
                    errors.Add(propertyName + ": " + errorString);
                }
            }

            return errors;
        }

    }
        #endregion
}
