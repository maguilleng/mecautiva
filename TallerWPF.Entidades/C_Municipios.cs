//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TallerWPF.Entidades
{
    using System;
    using System.Collections.Generic;
    
    public partial class C_Municipios
    {
        public C_Municipios()
        {
            this.C_Ciudades = new HashSet<C_Ciudades>();
            this.C_Clientes = new HashSet<C_Clientes>();
        }
    
        public int IdMunicipio { get; set; }
        public int IdEstado { get; set; }
        public string Descripcion { get; set; }
        public int Estatus { get; set; }
    
        public virtual ICollection<C_Ciudades> C_Ciudades { get; set; }
        public virtual ICollection<C_Clientes> C_Clientes { get; set; }
        public virtual C_Estados C_Estados { get; set; }
    }
}
