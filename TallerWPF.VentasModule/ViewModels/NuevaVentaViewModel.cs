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
using TallerWPF.Infraestructura;
using TallerWPF.Entidades.VentasEntidades;

namespace TallerWPF.VentasModule.ViewModels
{
    [Export]
    public class NuevaVentaViewModel : BindableBase
    {
        #region ATRIBUTOS PRIVADOS

        IEventAggregator eventAggregator;
        IServicioVenta servicioVenta;
        ProductosService servicioProductos;
        PreciosService preciosService;
        ClientesController clientesCtrl;

        #endregion       

        #region COMMANDS

        public DelegateCommand ProductoEliminadoCommand { get; set; }
        public DelegateCommand ProductoActualizadoCommand { get; set; }

        #endregion

        #region PROPIEDADES       

        VentaDto ventaActual;
        public VentaDto VentaActual
        {
            get
            {
                if (ventaActual == null)
                {
                    ventaActual = new VentaDto()
                    {
                        Fecha = DateTime.Now,
                        IdVenta = 0,
                        Subtotal = 0,
                        IVA = 0,
                        Total = 0
                    };
                    ActualizarDatosVenta();
                }
                return this.ventaActual;
            }
            set 
            { 
                SetProperty(ref this.ventaActual, value);
            }
        }

        ObservableCollection<VentaDetalleDto> detallesVenta;
        public ObservableCollection<VentaDetalleDto> DetallesVenta
        {
            get 
            {
                if (detallesVenta == null)
                    detallesVenta = new ObservableCollection<VentaDetalleDto>();

                return detallesVenta; 
            }
            set { SetProperty(ref this.detallesVenta, value); }
        }

        ObservableCollection<C_Servicios> productos;
        public ObservableCollection<C_Servicios> Productos
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

        C_Servicios productoSeleccionado;
        public C_Servicios ProductoSeleccionado
        {
            get { return productoSeleccionado; }
            set 
            {
                SetProperty(ref this.productoSeleccionado, value);
                VerificarProductoEnCarrito(this.productoSeleccionado);                                
            }
        }

        C_Clientes clienteSeleccionado;
        public C_Clientes ClienteSeleccionado
        {
            get { return this.clienteSeleccionado; }
            set 
            { 
                SetProperty(ref this.clienteSeleccionado, value); 
            }
        }

        C_Vehiculos vehiculoSeleccionado;
        public C_Vehiculos VehiculoSeleccionado
        {
            get { return vehiculoSeleccionado; }
            set { SetProperty(ref vehiculoSeleccionado, value); }
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

        string tituloVenta;
        public string TituloVenta
        {
            get { return tituloVenta; }
            set { SetProperty(ref this.tituloVenta, value); }
        }

        #endregion

        #region CONSTRUCTOR

        [ImportingConstructor]
        public NuevaVentaViewModel(IServicioVenta servicioVenta, 
            IEventAggregator eventAggregator, 
            ProductosService servicioProductos, 
            ClientesController clientesCtrl,
            PreciosService preciosService)
        {
            this.servicioVenta = servicioVenta;
            this.eventAggregator = eventAggregator;
            this.servicioProductos = servicioProductos;
            this.preciosService = preciosService;
            this.ProductoEliminadoCommand = new DelegateCommand(ActualizarImporteVenta);
            this.ProductoActualizadoCommand = new DelegateCommand(ActualizarImporteVenta);
            this.clientesCtrl = clientesCtrl;

            this.eventAggregator.GetEvent<ClienteSeleccionadoEvent>().Subscribe(OnClienteSeleccionado);
            this.eventAggregator.GetEvent<VehiculoSeleccionadoEvent>().Subscribe(OnVehiculoSeleccionado);
            this.eventAggregator.GetEvent<CrearNuevaVenta>().Subscribe(OnCrearNuevaVenta);

            servicioVenta.ActualizarVentaActual(VentaActual, DetallesVenta);
            ActualizarDatosVenta();
        }

        #endregion

        #region MÉTODOS

        public void VerificarProductoEnCarrito(C_Servicios c_servicioAgregado)
        {
            if (c_servicioAgregado != null)
            {
                var ventaDetalleExistente = DetallesVenta.Where(vd => vd.C_Servicios.IdServicio == c_servicioAgregado.IdServicio).SingleOrDefault();
                
                if (ventaDetalleExistente != null)
                {
                    ventaDetalleExistente.Cantidad = ventaDetalleExistente.Cantidad + 1;
                    ventaDetalleExistente.Importe = ventaDetalleExistente.PrecioUnitario * ventaDetalleExistente.Cantidad;
                }
                else
                {
                    var precioActual = preciosService.ObtenerPrecioActualServicio(c_servicioAgregado.IdServicio);

                    var precioUnitario = VentaActual.EsFactura ? precioActual.Precio : precioActual.PrecioNota;

                    VentaDetalleDto ventaDetalle = new VentaDetalleDto()
                    {
                        C_Servicios = c_servicioAgregado,
                        Cantidad = 1,
                        PrecioUnitario = precioUnitario,
                        Importe = precioActual.Precio
                    };

                    DetallesVenta.Add(ventaDetalle);
                }
                ActualizarImporteVenta();
            }
        }

        public void ActualizarVentaActual()
        {
            servicioVenta.ActualizarVentaActual(VentaActual, DetallesVenta);
        }
        
        public void ActualizarImporteVenta()
        {
            servicioVenta.ActualizarImporteVenta();
        }

        public bool PuedePagarVenta()
        {
            return DetallesVenta.Count > 0;
        }

        public void OnClienteSeleccionado(C_Clientes cliente)
        { 
            if(cliente != null)
            {
                ClienteSeleccionado = cliente;
            }
        }

        public void OnVehiculoSeleccionado(C_Vehiculos vehiculo)
        {
            VehiculoSeleccionado = vehiculo;
        }

        public void OnCrearNuevaVenta(bool requiereFactura)
        {
            DetallesVenta = new ObservableCollection<VentaDetalleDto>();
            VentaActual = new VentaDto()
            {
                EsFactura = requiereFactura,
                Fecha = DateTime.Now,
                IdVenta = 0,
                Subtotal = 0,
                IVA = 0,
                Total = 0
            };

            servicioVenta.ActualizarVentaActual(VentaActual, DetallesVenta);

            ActualizarDatosVenta();
        }

        public void ActualizarDatosVenta()
        {
            TituloVenta = VentaActual.EsFactura ? "Venta Con Factura" : "Venta Con Nota Sencilla";
        }
        #endregion        
    }
}