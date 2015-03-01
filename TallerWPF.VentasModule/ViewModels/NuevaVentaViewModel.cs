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
using TallerWPF.Entidades.VentasEntidades;
using TallerWPF.Infraestructura.Interfaces;
using TallerWPF.Infraestructura;
using TallerWPF.Entidades.VentasEntidades;
using System.Windows;
using TallerWPF.VentasModule.Helpers;
using System.Windows.Data;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
//using TallerWPF.Infraestructura.Dtos.Ventas;

namespace TallerWPF.VentasModule.ViewModels
{
    [Export]
    public class NuevaVentaViewModel : ValidatableBindableBase
    {
        #region ATRIBUTOS PRIVADOS

        IEventAggregator eventAggregator;
        IServicioVenta servicioVenta;

        ProductosService servicioProductos;
        PreciosService preciosService;
        TipoPagoService tipoPagoService;
        ClientesController clientesCtrl;

        #endregion       

        #region COMMANDS

        public DelegateCommand ProductoEliminadoCommand { get; set; }
        public DelegateCommand ProductoActualizadoCommand { get; set; }
        public DelegateCommand VerDetalleVentaCommand { get; set; }

        #endregion

        #region PROPIEDADES       

        bool estaPagandoVentaActual;
        public bool EstaPagandoVentaActual
        {
            get { return estaPagandoVentaActual; }
            set { 
                SetProperty(ref this.estaPagandoVentaActual, value);
                EstadoDetalleVentas = EstaPagandoVentaActual ? false : true;
            }
        }

        bool estadoDetalleVentas;
        public bool EstadoDetalleVentas
        {
            get { return estadoDetalleVentas; }
            set { SetProperty(ref this.estadoDetalleVentas, value); }
        }

        ObservableCollection<C_FormasPago> catalogoTiposPago;
        public ObservableCollection<C_FormasPago> CatalogoTiposPago
        {
            get
            {
                if (catalogoTiposPago == null)
                {
                    catalogoTiposPago = ObtenerTiposDePago();
                }
                return catalogoTiposPago;
            }
            set { SetProperty(ref this.catalogoTiposPago, value); }
        }

        C_FormasPago tipoPagoSeleccionado;
        [Required(ErrorMessage = "Debe seleccionar una Forma de Pago")]
        public C_FormasPago TipoPagoSeleccionado
        {
            get { return tipoPagoSeleccionado; }
            set { SetProperty(ref this.tipoPagoSeleccionado, value); }
        }

        string mnemonicoTipoPagoSeleccionado;
        public string MnemonicoTipoPagoSeleccionado
        {
            get { return mnemonicoTipoPagoSeleccionado; }
            set { SetProperty(ref this.mnemonicoTipoPagoSeleccionado, value); }
        }
        
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
        [CustomValidation( typeof( NuevaVentaViewModel ), "ValidarProductosSeleccionados" )]
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
        [Required (ErrorMessage = "Debe seleccionar un Cliente")]
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

        Pagos pagoVentaActual;
        public Pagos PagoVentaActual
        {
            get { return pagoVentaActual; }
            set { SetProperty(ref this.pagoVentaActual, value); }
        }

        ObservableCollection<PagosDetalle> detallesPagoVentaActual;
        public ObservableCollection<PagosDetalle> DetallesPagoVentaActual
        {
            get { return detallesPagoVentaActual; }
            set { SetProperty(ref this.detallesPagoVentaActual, value); }
        }

        PagosDetalleDto detallePagoSeleccionado;
        public PagosDetalleDto DetallePagoSeleccionado
        {
            get 
            {
                detallePagoSeleccionado = detallePagoSeleccionado ?? new PagosDetalleDto();
                return detallePagoSeleccionado;
            }
            set { SetProperty(ref this.detallePagoSeleccionado, value); }
        }

        double cantidadRecibidaPago;
        [Required]
        public double CantidadRecibidaPago
        {
            get { return cantidadRecibidaPago; }
            set 
            { 
                SetProperty(ref this.cantidadRecibidaPago, value);
                ActualizarDatosPago();
            }
        }

        PagoEfectivoHelper pagoEfectivoHelper;
        public PagoEfectivoHelper PagoEfectivoHelper
        {
            get 
            {
                pagoEfectivoHelper = pagoEfectivoHelper ?? new PagoEfectivoHelper();
                return pagoEfectivoHelper; 
            }
            set { SetProperty(ref this.pagoEfectivoHelper, value); }
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

        List<string> errores;
        public List<string> Errores
        {
            get { return ObtenerErrores(); }
            set { SetProperty(ref this.errores, value); }
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
            PreciosService preciosService,
            TipoPagoService tipoPagoService)
        {
            this.servicioVenta = servicioVenta;
            this.eventAggregator = eventAggregator;
            this.servicioProductos = servicioProductos;
            this.preciosService = preciosService;
            this.tipoPagoService = tipoPagoService;

            this.ProductoEliminadoCommand = new DelegateCommand(ActualizarImporteVenta);
            this.ProductoActualizadoCommand = new DelegateCommand(ActualizarImporteVenta);
            this.VerDetalleVentaCommand = new DelegateCommand(VerDetalleVenta);
            
            this.clientesCtrl = clientesCtrl;

            this.eventAggregator.GetEvent<ClienteSeleccionadoEvent>().Subscribe(OnClienteSeleccionado);
            this.eventAggregator.GetEvent<VehiculoSeleccionadoEvent>().Subscribe(OnVehiculoSeleccionado);
            this.eventAggregator.GetEvent<CrearNuevaVenta>().Subscribe(OnCrearNuevaVenta);
            this.eventAggregator.GetEvent<PagarVentaActualEvent>().Subscribe(OnPagarVentaActual);

            servicioVenta.ActualizarVentaActual(VentaActual, DetallesVenta);
            ActualizarDatosVenta();

            EstadoDetalleVentas = true;

            this.PropertyChanged += NuevaVentaViewModel_PropertyChanged;
        }
      
        #endregion

        #region MÉTODOS

        void VentaActual_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var p = e.PropertyName;
        }

        void NuevaVentaViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "CantidadRecibidaPago" || e.PropertyName == "Total" || e.PropertyName == "ProductoSeleccionado" || e.PropertyName == "TipoPagoSeleccionado" || e.PropertyName == "VentaActual")
            {
                if (this.CantidadRecibidaPago < VentaActual.Total)
                {
                    var err = new String[] { "La Cantidad Recibida es Menor al Total de la Venta." }.AsEnumerable();
                    this.SetErrors(() => this.CantidadRecibidaPago, err);

                    Errores = ObtenerErrores();
                    //OnErrorsChanged(new DataErrorsChangedEventArgs("CantidadRecibidaPago"));
                }
            }
        }

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

            allErrors = DetallePagoSeleccionado.GetAllErrors();
            foreach (string propertyName in allErrors.Keys)
            {
                foreach (var errorString in allErrors[propertyName])
                {
                    errors.Add(propertyName + ": " + errorString);
                }
            }

            return errors;
        }

        private ObservableCollection<C_FormasPago> ObtenerTiposDePago()
        {
            var tiposPago = tipoPagoService.ObtenerTiposPago();
            
            ObservableCollection<C_FormasPago> TiposPago = new ObservableCollection<C_FormasPago>(tiposPago);

            return TiposPago;
        }

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
                ValidateProperty("DetallesVenta");
            }
        }

        public void ActualizarVentaActual()
        {
            servicioVenta.ActualizarVentaActual(VentaActual, DetallesVenta);
        }
        
        public void ActualizarImporteVenta()
        {
            servicioVenta.ActualizarImporteVenta();
            VerificarCantidadValida();
        }

        public void VerificarCantidadValida()
        {
            if (this.CantidadRecibidaPago < VentaActual.Total)
            {
                var err = new String[] { "La Cantidad Recibida es Menor al Total de la Venta." }.AsEnumerable();
                this.SetErrors(() => this.CantidadRecibidaPago, err);

                Errores = ObtenerErrores();
                OnErrorsChanged(new DataErrorsChangedEventArgs("CantidadRecibidaPago"));
            }
            else
            {
                this.ValidateProperties();

                if(TipoPagoSeleccionado != null)
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
                }

                Errores = ObtenerErrores();
            }
        }

        public void VerDetalleVenta()
        {
            EstaPagandoVentaActual = false;
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

        public void OnPagarVentaActual(object obj)
        { 
            if(!EstaPagandoVentaActual){
                EstaPagandoVentaActual = true;
                PagoEfectivoHelper.TotalAPagar = VentaActual.Total;
                PagoEfectivoHelper.CantidadRecibidaValida = false;
                PagoEfectivoHelper.CantidadRecibida = 0;
            }
            else if (EstaPagandoVentaActual) 
            {
                this.ValidateProperties();

                if (TipoPagoSeleccionado != null)
                {
                    //this.DetallePagoSeleccionado.ValidateProperties();
                    var erroresPago = DetallePagoSeleccionado.GetAllErrors().Select(e => e.Key).ToList();
                    foreach (var error in erroresPago)
                    {
                        DetallePagoSeleccionado.ErrorsContainer.ClearErrors(error);
                    }

                    foreach (var propiedad in TipoPagoSeleccionado.ValidacionesFormasPago)
                    {
                        this.DetallePagoSeleccionado.ValidateProperty(propiedad.PropiedadValidadaPagosDetalle);
                    }                    
                }

                VerificarCantidadValida();

                if (this.HasErrors || this.DetallePagoSeleccionado.HasErrors)
                {
                    Errores = ObtenerErrores();
                }                
                
                if (!this.HasErrors)
                { 
                    Errores = ObtenerErrores();
                    MessageBox.Show("Saca la lana");
                }                
            }
        }

        public void ActualizarDatosPago()
        {
            DetallePagoSeleccionado.Cantidad = CantidadRecibidaPago;
            PagoEfectivoHelper.CantidadRecibida = CantidadRecibidaPago;
            VerificarCantidadValida();
        }

        public static ValidationResult ValidarProductosSeleccionados(ObservableCollection<VentaDetalleDto> productos, ValidationContext validationContext)
        {
            ValidationResult result = new ValidationResult("Debe agregar Productos a la Venta");

            if (productos.Count != 0)
                result = ValidationResult.Success;

            return result;
        }
        #endregion        
    }
}