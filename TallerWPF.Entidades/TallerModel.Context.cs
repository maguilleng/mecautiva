﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class PuntoVentaEntities : DbContext
    {
        public PuntoVentaEntities()
            : base("name=PuntoVentaEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<C_Ciudades> C_Ciudades { get; set; }
        public DbSet<C_Clientes> C_Clientes { get; set; }
        public DbSet<C_Estados> C_Estados { get; set; }
        public DbSet<C_FormasPago> C_FormasPago { get; set; }
        public DbSet<C_Marcas> C_Marcas { get; set; }
        public DbSet<C_Municipios> C_Municipios { get; set; }
        public DbSet<C_Servicios> C_Servicios { get; set; }
        public DbSet<C_TiposPersona> C_TiposPersona { get; set; }
        public DbSet<C_TiposServicios> C_TiposServicios { get; set; }
        public DbSet<C_UnidadesMedida> C_UnidadesMedida { get; set; }
        public DbSet<C_Vehiculos> C_Vehiculos { get; set; }
        public DbSet<Facturas> Facturas { get; set; }
        public DbSet<Inventario> Inventario { get; set; }
        public DbSet<Pagos> Pagos { get; set; }
        public DbSet<PagosDetalle> PagosDetalle { get; set; }
        public DbSet<Precios> Precios { get; set; }
        public DbSet<Ventas> Ventas { get; set; }
        public DbSet<VentasDetalle> VentasDetalle { get; set; }
        public DbSet<ValidacionesFormasPago> ValidacionesFormasPago { get; set; }
        public DbSet<VentasVehiculos> VentasVehiculos { get; set; }
    }
}
