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
    
    public partial class ValidacionesFormasPago
    {
        public int Id { get; set; }
        public int IdFormaPago { get; set; }
        public string PropiedadValidadaPagosDetalle { get; set; }
    
        public virtual C_FormasPago C_FormasPago { get; set; }
    }
}