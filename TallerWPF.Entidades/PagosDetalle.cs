//------------------------------------------------------------------------------
// <auto-generated>
//    Este código se generó a partir de una plantilla.
//
//    Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//    Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TallerWPF.Entidades
{
    using System;
    using System.Collections.Generic;
    
    public partial class PagosDetalle
    {
        public int IdPagoDetalle { get; set; }
        public int IdPago { get; set; }
        public int IdFormaPago { get; set; }
        public string Institucion { get; set; }
        public string Referencia { get; set; }
        public double Cantidad { get; set; }
        public Nullable<System.DateTime> FechaAlta { get; set; }
        public Nullable<System.DateTime> FechaModificacion { get; set; }
        public Nullable<int> Estatus { get; set; }
        public Nullable<int> IdUsuarioCreacion { get; set; }
        public Nullable<int> IdUsuarioModificacion { get; set; }
    
        public virtual C_FormasPago C_FormasPago { get; set; }
        public virtual Pagos Pagos { get; set; }
    }
}
