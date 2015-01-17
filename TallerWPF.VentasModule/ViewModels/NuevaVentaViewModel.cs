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
        IEventAggregator eventAggregator;
        IServicioVenta servicioVenta;       
        ProductosService servicioProductos;
        PreciosService preciosService;
        ClientesController clientesCtrl;

        public DelegateCommand ProductoEliminadoCommand { get; set; }
        public DelegateCommand ProductoActualizadoCommand { get; set; }
        

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
                }
                return this.ventaActual;
            }
            set { SetProperty(ref this.ventaActual, value); }
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

            VentaActual = new VentaDto();
            servicioVenta.ActualizarVentaActual(VentaActual, DetallesVenta);
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

                    VentaDetalleDto ventaDetalle = new VentaDetalleDto()
                    {
                        C_Servicios = c_servicioAgregado,
                        Cantidad = 1,
                        PrecioUnitario = precioActual.Precio,
                        Importe = precioActual.Precio
                    };

                    DetallesVenta.Add(ventaDetalle);
                }
                ActualizarImporteVenta();
            }
        }

        public void ActualizarImporteVenta()
        {
            servicioVenta.ActualizarImporteVenta(VentaActual, DetallesVenta);
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

        #endregion        
    }
}