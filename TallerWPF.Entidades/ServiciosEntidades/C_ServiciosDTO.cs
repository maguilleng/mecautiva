using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.Mvvm;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
namespace TallerWPF.Entidades.ServiciosEntidades
{
   public class C_ServiciosDTO : BindableBase , IDataErrorInfo
    {
        int idServicio;
        string codigo;
        int? idTipoServicio;
        string descripcion;
        int idUnidadMedida;
        int idMarca;
        bool? seAlmacena;
        bool? seCompra;
        bool? seVende;

        public DateTime FechaAlta { get; set; }
        public DateTime FechaModificacion { get; set; }
        public int? IdUsuarioCreacion { get; set; }
        public int? IdUsuarioModificacion { get; set; }
        public int? Estatus { get; set; }

        public C_Marcas C_Marcas { get; set; }
        public C_TiposServicios C_TiposServicios { get; set; }
        public C_UnidadesMedida C_UnidadesMedida { get; set; }

        public int IdServicio
        {
            get { return idServicio; }
            set
            {
                SetProperty(ref this.idServicio, value);
            }
        }
        [Required]
        public string Codigo
        {
            get { return codigo; }
            set
            {
                SetProperty(ref this.codigo, value);
            }
        }
        [Required]
        public int? IdTipoServicio
        {
            get { return idTipoServicio; }
            set
            {
                SetProperty(ref this.idTipoServicio, value);
            }
        }
        [Required]
        public string Descripcion
        {
            get { return descripcion; }
            set
            {
                SetProperty(ref this.descripcion, value);
            }
        }
        [Required]
        public int IdUnidadMedida
        {
            get { return idUnidadMedida; }
            set
            {
                SetProperty(ref this.idUnidadMedida, value);
            }
        }
        [Required]
        public int IdMarca
        {
            get { return idMarca; }
            set
            {
                SetProperty(ref this.idMarca, value);
            }
        }
        public bool? SeAlmacena
        {
            get { return seAlmacena; }
            set
            {
                SetProperty(ref this.seAlmacena, value);
            }
        }
        public bool? SeCompra
        {
            get { return seCompra; }
            set
            {
                SetProperty(ref this.seCompra, value);
            }
        }
        public bool? SeVende
        {
            get { return seVende; }
            set
            {
                SetProperty(ref this.seVende, value);
            }
        }

         #region IDataErrorInfo implementation
        //Get the error
        public string Error
        {
            get
            {
                return null;
            }
        }

        public string this[string name]
        {
            get
            {
                string result = null;

                if (name == "Descripcion")
                {
                    if (this.descripcion == "" || this.descripcion == null)
                    {
                        result = "El campo Tipo de Servicio no puede quedar vacio";
                    }
                }
                else if (name == "Codigo")
                {
                    if (this.codigo == "" || this.codigo == null)
                    {
                        result = "El campo Codigo no puede quedar vacio";
                    }
                }
                else if (name == "IdTipoServicio")
                {
                    if (this.idTipoServicio == 0 || this.idTipoServicio == null)
                    {
                        result = "El campo Codigo no puede quedar vacio";
                    }
                }
                else if (name == "IdMarca")
                {
                    if (this.idMarca < 1 )
                    {
                        result = "El campo Marca no puede quedar vacio";
                    }
                }
                else if (name == "IdUnidadMedida")
                {
                    if (this.idUnidadMedida < 1)
                    {
                        result = "El campo Unidad de Medida no puede quedar vacio";
                    }
                }
                return result;
            }
        }
        #endregion

        #region CONSTRUCTORES
        public C_ServiciosDTO()
        {
            
        }

        public C_ServiciosDTO(C_Servicios servicio)
        {
            IdServicio = servicio.IdServicio;
            Codigo = servicio.Codigo;
            IdTipoServicio = servicio.IdTipoServicio;
            Descripcion = servicio.Descripcion;
            IdMarca = servicio.IdMarca;
            IdUnidadMedida = servicio.IdUnidadMedida;
            SeAlmacena = servicio.SeAlmacena;
            SeCompra = servicio.SeCompra;
            SeVende = servicio.SeVende;

            C_TiposServicios = servicio.C_TiposServicios;
            C_UnidadesMedida = servicio.C_UnidadesMedida;
            C_Marcas = servicio.C_Marcas;
        }

        #endregion

    }
}
