using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.PubSubEvents;
using Servicios;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TallerWPF.Entidades;
using TallerWPF.Infraestructura.Interfaces;

namespace TallerWPF.VentasModule.ViewModels
{
    [Export]
    public class NuevaVentaViewModel : BindableBase
    {
        IEventAggregator eventAggregator;
        IServicioVenta servicioVenta;
        ProductosService servicioProductos;

        public DelegateCommand ProductoEliminadoCommand { get; set; }
        public DelegateCommand ProductoActualizadoCommand { get; set; }
        

        #region PROPIEDADES

        Venta ventaActual;
        public Venta VentaActual
        {
            get
            {
                if (ventaActual == null)
                {
                    ventaActual = new Venta()
                    {
                        FechaVenta = DateTime.Now,
                        IdVenta = 0,
                        Subtotal = 0,
                        Iva = 0,
                        Total = 0
                    };
                }
                return this.ventaActual;
            }
            set { SetProperty(ref this.ventaActual, value); }
        }

        ObservableCollection<Producto> productos;
        public ObservableCollection<Producto> Productos
        {
            get
            {
                if (productos == null)
                {
                    productos = servicioProductos.ObtenerProductos();
                }
                return productos;
            }
            set { SetProperty(ref this.productos, value); }
        }

        Producto productoSeleccionado;
        public Producto ProductoSeleccionado
        {
            get { return productoSeleccionado; }
            set 
            {
                SetProperty(ref this.productoSeleccionado, value);
                VerificarProductoEnCarrito(this.productoSeleccionado);                                
            }
        }


        ObservableCollection<Cliente> clientes;
        public ObservableCollection<Cliente> Clientes
        {
            get
            {
                if (this.clientes == null)
                {
                    this.clientes = ListadoClientes();
                }

                return this.clientes;
            }
            set { SetProperty(ref this.clientes, value); }
        }

        Cliente clienteSeleccionado;
        public Cliente ClienteSeleccionado
        {
            get { return this.clienteSeleccionado; }
            set { SetProperty(ref this.clienteSeleccionado, value); }
        }

        Vehiculo vehiculoSeleccionado;
        public Vehiculo VehiculoSeleccionado
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

        bool buscarPorDescripcionProducto;
        public bool BuscarPorDescripcionProducto
        {
            get { return buscarPorDescripcionProducto; }
            set 
            {
                if (buscarPorDescripcionProducto == value) 
                    return;

                SetProperty(ref this.buscarPorDescripcionProducto, value); 
            }
        }

        string productosSearchText;
        public string ProductosSearchText
        {
            get { return productosSearchText; }
            set 
            { 
                SetProperty(ref this.productosSearchText, value);
            }
        }
        
        #endregion

        #region CONSTRUCTOR

        [ImportingConstructor]
        public NuevaVentaViewModel(IServicioVenta servicioVenta, IEventAggregator eventAggregator, ProductosService servicioProductos)
        {
            this.servicioVenta = servicioVenta;
            this.eventAggregator = eventAggregator;
            this.servicioProductos = servicioProductos;
            this.ProductoEliminadoCommand = new DelegateCommand(ActualizarImporteVenta);
            this.ProductoActualizadoCommand = new DelegateCommand(ActualizarImporteVenta);

            MockupVenta();
        }

        #endregion

        #region MÉTODOS
        
        public void MockupVenta()
        {
            VentaActual = new Venta();
            VentaActual.Productos = new ObservableCollection<Producto>();
            servicioVenta.ActualizarVentaActual(VentaActual);
        }

        public List<Vehiculo> VehiculosCliente()
        {
            List<Vehiculo> Vehiculos = new List<Vehiculo>();

            for (int i = 0; i <= 5; i++)
            {
                var vehi = new Vehiculo()
                {
                    IdVehiculo = i,
                    Placas = "TAB-00" + i,
                    Marca = "CHEVROLET",
                    Modelo = "CAMARO",
                    Kilometraje = 10000 + i,
                    Tipo = "Automovil"
                };

                Vehiculos.Add(vehi);
            }

            return Vehiculos;
        }

        public ObservableCollection<Cliente> ListadoClientes()
        {
            ObservableCollection<Cliente> ListaClientes = new ObservableCollection<Cliente>();
            var cl1 = new Cliente()
            {
                IdCliente = 1,
                Nombre = "Público en General",
                Direccion = "Venta en Sucursal",
                Rfc = "PUBLICO",
                Telefono = 9932790918,
                Vehiculos = VehiculosCliente()
            };

            var cl2 = new Cliente()
            {
                IdCliente = 2,
                Nombre = "Manuel Guillén",
                Direccion = "Cd. Pemex",
                Rfc = "GUGM8303311A1",
                Telefono = 9932000019,
                Vehiculos = VehiculosCliente()
            };

            var cl3 = new Cliente()
            {
                IdCliente = 3,
                Nombre = "Haziel Isaí",
                Direccion = "La Curva",
                Rfc = "GULH1004021A1",
                Telefono = 9932000020,
                Vehiculos = VehiculosCliente()
            };

            var cl4 = new Cliente()
            {
                IdCliente = 3,
                Nombre = "Karina López",
                Direccion = "Activo Macuspana",
                Rfc = "LOAK8209121A1",
                Telefono = 9932000021,
                Vehiculos = VehiculosCliente()
            };

            ListaClientes.Add(cl1);
            ListaClientes.Add(cl2);
            ListaClientes.Add(cl3);
            ListaClientes.Add(cl4);

            return ListaClientes;
        }

        public void VerificarProductoEnCarrito(Producto productoParam)
        {
            if (productoParam != null)
            {
                var productoExistente = VentaActual.Productos.Where(p => p.IdProducto == productoParam.IdProducto).SingleOrDefault();
                if (productoExistente != null)
                {
                    productoExistente.Cantidad = productoExistente.Cantidad + productoParam.Cantidad;
                    productoExistente.Importe = productoExistente.PrecioUnitario * productoExistente.Cantidad;                    
                }
                else
                {
                    Producto nuevoProducto = new Producto(){
                                                    IdProducto = productoParam.IdProducto,
                                                    Codigo = productoParam.Codigo,
                                                    Marca = productoParam.Marca,
                                                    Modelo = productoParam.Modelo,
                                                    Categoria = productoParam.Categoria,
                                                    Cantidad = productoParam.Cantidad,
                                                    Descripcion = productoParam.Descripcion,
                                                    PrecioUnitario = productoParam.PrecioUnitario,
                                                    Importe = productoParam.PrecioUnitario * productoParam.Cantidad
                                                    };
                    VentaActual.Productos.Add(nuevoProducto);
                }
                ActualizarImporteVenta();
            }
        }

        public void ActualizarImporteVenta()
        {
            servicioVenta.ActualizarImporteVenta(VentaActual);
        }

        public bool PuedePagarVenta()
        {
            return VentaActual.Productos.Count > 0;
        }
        
        #endregion        
    }
}
