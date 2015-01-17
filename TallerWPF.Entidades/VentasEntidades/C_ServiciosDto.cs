using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TallerWPF.Entidades.VentasEntidades
{
    public class C_ServiciosDto
    {
        public int IdServicio { get; set; }
        public string Descripcion { get; set; }
        public int idUnidadMedida { get; set; }
        public bool SeAlmacena { get; set; }
        public bool SeCompra { get; set; }
        public bool SeVende { get; set; }
        public int IdMarca { get; set; }
        public int IdTipoServicio { get; set; }
        public DateTime FechaAlta { get; set; }
        public DateTime FechaModificacion { get; set; }
        public int IdUsuarioCreacion { get; set; }
        public int IdUsuarioModificacion { get; set; }
        public int Estatus { get; set; }
        public string Codigo { get; set; }
        
        public C_Marcas C_Marcas { get; set; }
        public C_TiposServicios C_TiposServicios { get; set; }
        public Precios PrecioActual { get; set; }
        
        public C_ServiciosDto(C_Servicios servicio)
        {
            IdServicio = servicio.IdServicio;
            Descripcion = servicio.Descripcion;
            idUnidadMedida = servicio.IdUnidadMedida;
            SeAlmacena = servicio.SeAlmacena.Value;
            SeCompra = servicio.SeCompra.Value;
            SeVende = servicio.SeVende.Value;
            IdMarca = servicio.IdMarca;
            IdTipoServicio = servicio.IdTipoServicio.Value;
            FechaAlta = servicio.FechaAlta.Value;
            FechaModificacion = servicio.FechaModificacion.Value;
            IdUsuarioCreacion = servicio.IdUsuarioCreacion.Value;
            IdUsuarioModificacion = servicio.IdUsuarioModificacion.Value;
            Estatus = servicio.Estatus;
            Codigo = servicio.Codigo;

            C_Marcas = servicio.C_Marcas;
            C_TiposServicios = servicio.C_TiposServicios;
        }
    }
}
