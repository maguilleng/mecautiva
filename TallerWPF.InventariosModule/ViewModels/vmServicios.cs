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
using TallerWPF.Entidades.ServiciosEntidades;

using Servicios;
using System.Windows;

namespace TallerWPF.InventariosModule.ViewModels
{
    [Export]
    class vmServicios : BindableBase
    {
        #region ATRIBUTOS PRIVAODOS
        ServiciosService serviciosCtrl = new ServiciosService();
        #endregion

        #region COMMANDS
        public DelegateCommand GuardarDatosCommand;
        #endregion

        #region CONSTRUCTORES
        
        [ImportingConstructor]
        public vmServicios()
        {
            GuardarDatosCommand = new DelegateCommand(guardarDatosServicio);
        }
        #endregion

        private ObservableCollection<C_ServiciosDTO> ListadoServicios()
        {
            ObservableCollection<C_ServiciosDTO> servicios = new ObservableCollection<C_ServiciosDTO>();

            serviciosCtrl.ObtenerListaServicios().ForEach(s => servicios.Add(new C_ServiciosDTO(s)));

            return servicios;
        }

        public List<C_TiposServicios> ListadoTiposServicios()
        {
            return serviciosCtrl.ObtenerListaTiposServicios();
        }

        public List<C_Marcas> ListadoMarcas()
        {
            return serviciosCtrl.ObtenerListaMarcas();
        }

        public List<C_UnidadesMedida> ListadoUnidadesMedida()
        {
            return serviciosCtrl.ObtenerListaUnidadesMedida();
        }

        public void guardarDatosServicio()
        {
            C_Servicios nuevoServicio = new C_Servicios();

            nuevoServicio.IdServicio = ServicioSeleccionado.IdServicio;
            nuevoServicio.Codigo = ServicioSeleccionado.Codigo;
            nuevoServicio.Descripcion = ServicioSeleccionado.Descripcion;
            nuevoServicio.IdTipoServicio = ServicioSeleccionado.IdTipoServicio;
            nuevoServicio.IdMarca = ServicioSeleccionado.IdMarca;
            nuevoServicio.IdUnidadMedida = ServicioSeleccionado.IdUnidadMedida;
            nuevoServicio.SeAlmacena = ServicioSeleccionado.SeAlmacena;
            nuevoServicio.SeCompra = ServicioSeleccionado.SeCompra;
            nuevoServicio.SeVende = ServicioSeleccionado.SeVende;
            nuevoServicio.Estatus = 1;
            MessageBox.Show(serviciosCtrl.GuardarServicio(nuevoServicio));
        }

        ObservableCollection<C_ServiciosDTO> servicios;
        public ObservableCollection<C_ServiciosDTO> Servicios
        { 
            get
            {
                if (servicios == null)
                {
                 servicios = ListadoServicios();
                }
                return servicios;
            }

            set { SetProperty(ref this.servicios, value); }
        }

        C_ServiciosDTO servicioSeleccionado;
        public C_ServiciosDTO ServicioSeleccionado
        {
            get
            {
                if (servicioSeleccionado == null)
                {
                    servicioSeleccionado = new C_ServiciosDTO()
                    {
                        Codigo = "",
                        Descripcion = "",
                        SeAlmacena = false,
                        SeCompra = false,
                        SeVende = false,
                        IdServicio = 0
                    };
                }
                return this.servicioSeleccionado;
            }
            set
            {
                SetProperty(ref this.servicioSeleccionado, value);
            }
        }

        //TREAEMOS LOS TIPOS DE SERVICIOS
        List<C_TiposServicios> tiposServicios;
        public List<C_TiposServicios> TiposServicios
        {

            get
            {
                C_TiposServicios tiposervicio = new C_TiposServicios();
                tiposervicio.Descripcion = "Seleccione...";

                if (this.tiposServicios == null)
                {

                    tiposServicios = ListadoTiposServicios();
                    tiposServicios.Insert(0, tiposervicio);
                }

                return this.tiposServicios;
            }
            set { SetProperty(ref this.tiposServicios, value); }
        }
      
        //TREAEMOS LAS MARCAS
        List<C_Marcas> marcas;
        public List<C_Marcas> Marcas
        {

            get
            {
                C_Marcas marca = new C_Marcas();
                marca.Descripcion = "Seleccione...";

                if (this.marcas == null)
                {

                    marcas = ListadoMarcas();
                    marcas.Insert(0, marca);
                }

                return this.marcas;
            }
            set { SetProperty(ref this.marcas, value); }
        }
        
      //TREAEMOS LAS UNIDADES DE MEDIDAS
      List<C_UnidadesMedida> unidadesMedida;
      public List<C_UnidadesMedida> UnidadesMedida
      {

          get
          {
              C_UnidadesMedida unidadMedida = new C_UnidadesMedida();
              unidadMedida.Descripcion = "Seleccione...";

              if (this.unidadesMedida == null)
              {

                  unidadesMedida = ListadoUnidadesMedida();
                  unidadesMedida.Insert(0, unidadMedida);
              }

              return this.unidadesMedida;
          }
          set { SetProperty(ref this.unidadesMedida, value); }
      }
    }

}
