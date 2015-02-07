using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.Mvvm;

namespace TallerWPF.Entidades.VehiculosEntidades
{
    public class C_ClientesDTO : BindableBase
    {


        public int IdCliente { get; set; }
        //public string RFC { get; set; }
        //public string NumeroCliente { get; set; }
        //public string Nombre { get; set; }
        public int? IdTipoPersona { get; set; }
        public string Direccion { get; set; }
        public int? IdCiudad { get; set; }
        public int? CodigoPostal { get; set; }
        public int? IdMunicipio { get; set; }
        public decimal? Movil { get; set; }
        public decimal? Casa { get; set; }
        public decimal? Trabajo { get; set; }
        public string Email { get; set; }
        public int? IdEstado { get; set; }

        public DateTime FechaAlta { get; set; }
        public DateTime FechaModificacion { get; set; }
        public int? IdUsuarioCreacion { get; set; }
        public int? IdUsuarioModificacion { get; set; }
        public int? Estatus { get; set; }

        string nombre;
        public string Nombre
        {
            get { return nombre; }
            set
            {
                SetProperty(ref this.nombre, value);
            }
        }

        string rfc;
        public string RFC
        {
            get { return rfc; }
            set
            {
                SetProperty(ref this.rfc, value);
            }
        }

        string numeroCliente;
        public string NumeroCliente
        {
            get { return numeroCliente; }
            set
            {
                SetProperty(ref this.numeroCliente, value);
            }
        }

        public  C_Ciudades C_Ciudades { get; set; }
        public  C_Municipios C_Municipios { get; set; }
        public  C_TiposPersona C_TiposPersona { get; set; }
        public  ICollection<C_Vehiculos> C_Vehiculos { get; set; }
        public  ICollection<Ventas> Ventas { get; set; }


        public C_ClientesDTO(C_Clientes cliente)
        {
            IdCliente = cliente.IdCliente;
            RFC = cliente.RFC;
            NumeroCliente = cliente.NumeroCliente;
            IdTipoPersona = cliente.IdTipoPersona;
            Direccion = cliente.Direccion;
            IdCiudad = cliente.IdCiudad;
            IdMunicipio = cliente.IdMunicipio;
            CodigoPostal = cliente.CodigoPostal;
            Movil = cliente.Movil;
            Casa = cliente.Casa;
            Trabajo = cliente.Trabajo;
            Email = cliente.Email;
            IdEstado = cliente.IdEstado;
            Nombre = cliente.Nombre;

          
            C_TiposPersona = cliente.C_TiposPersona;
            C_Municipios = cliente.C_Municipios;
            C_Ciudades = cliente.C_Ciudades;
            C_Vehiculos = cliente.C_Vehiculos;
            Ventas = cliente.Ventas;
        }
       
        public C_ClientesDTO (){
        }
    }
}
