using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.Mvvm;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace TallerWPF.Entidades.VehiculosEntidades
{
    public class C_ClientesDTO : ValidatableBindableBase, IDataErrorInfo
    {


        int idCliente;
        int? idTipoPersona;
        string nombre;
        string rfc;
        string numeroCliente;
        string direccion;
        public int? idCiudad;
        public int? codigoPostal;
        public int? idMunicipio;
        public decimal? movil;
        public decimal? casa;
        public decimal? trabajo;
        public string email;
        public int? idEstado;

        public DateTime FechaAlta { get; set; }
        public DateTime FechaModificacion { get; set; }
        public int? IdUsuarioCreacion { get; set; }
        public int? IdUsuarioModificacion { get; set; }
        public int? Estatus { get; set; }

        public int IdCliente
        {
            get { return idCliente; }
            set
            {
                SetProperty(ref this.idCliente, value);
            }
        }

        #region Data for ValidationInfo
        [Required]
        public int? IdTipoPersona
        {
            get { return idTipoPersona; }
            set
            {
                SetProperty(ref this.idTipoPersona, value);
            }
        }
        [Required]
        public string Nombre
        {
            get { return nombre; }
            set
            {
                SetProperty(ref this.nombre, value);
            }
        }
        [Required]
        public string RFC
        {
            get { return rfc; }
            set
            {
                SetProperty(ref this.rfc, value);
            }
        }
        [Required]
        public string NumeroCliente
        {
            get { return numeroCliente; }
            set
            {
                SetProperty(ref this.numeroCliente, value);
            }
        }
        [Required]
        public string Direccion
        {
            get { return direccion; }
            set
            {
                SetProperty(ref this.direccion, value);
            }
        }
        [Required]
        public int? IdMunicipio
        {
            get { return idMunicipio; }
            set
            {
                SetProperty(ref this.idMunicipio, value);
            }
        }
        [Required]
        public int? IdEstado
        {
            get { return idEstado; }
            set
            {
                SetProperty(ref this.idEstado, value);
            }
        }
        #endregion

        public int? IdCiudad
        {
            get { return idCiudad; }
            set
            {
                SetProperty(ref this.idCiudad, value);
            }
        }
        public int? CodigoPostal
        {
            get { return codigoPostal; }
            set
            {
                SetProperty(ref this.codigoPostal, value);
            }
        }
        public decimal? Movil
        {
            get { return movil; }
            set
            {
                SetProperty(ref this.movil, value);
            }
        }
        public decimal? Casa
        {
            get { return casa; }
            set
            {
                SetProperty(ref this.casa, value);
            }
        }
        public decimal? Trabajo
        {
            get { return trabajo; }
            set
            {
                SetProperty(ref this.trabajo, value);
            }
        }
        public string Email
        {
            get { return email; }
            set
            {
                SetProperty(ref this.email, value);
            }
        }

        public C_Ciudades C_Ciudades { get; set; }
        public  C_Municipios C_Municipios { get; set; }
        public C_TiposPersona C_TiposPersona { get; set; }
        public  ICollection<C_Vehiculos> C_Vehiculos { get; set; }
        public  ICollection<Ventas> Ventas { get; set; }


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

                if (name == "RFC")
                {
                    if (this.rfc == "" || this.rfc == null)
                    {
                        result = "El campo RFC no puede quedar vacio";
                    }
                }
                else if (name == "Nombre")
                {
                    if (this.nombre == "" || this.nombre == null)
                    {
                        result = "El campo Nombre no puede quedar vacio";
                    }
                }
                else if (name == "NumeroCliente")
                {
                    if (this.numeroCliente == "" || this.numeroCliente == null)
                    {
                        result = "El campo Numero de Cliente no puede quedar vacio";
                    }
                }
                else if (name == "IdTipoPersona")
                {
                    if (this.idTipoPersona == 0 || this.idTipoPersona == null)
                    {
                        result = "El campo Tipo de Persona no puede quedar vacio";
                    }
                }

                else if (name == "Direccion")
                {
                    if (this.numeroCliente == "" || this.numeroCliente == null)
                    {
                        result = "El campo Dirección no puede quedar vacio";
                    }
                }
                /*
                else if (name == "CodigoPostal")
                {
                    if (this.codigoPostal != null)
                    {
                        if (this.codigoPostal.Value.ToString().Length != 5 || this.codigoPostal.Value.ToString().Length != 0)
                        {
                            result = "El campo CodigoPostal tiene que contener 5 numeros";
                        }
                    }
                }
                */
                else if (name == "IdMunicipio")
                {
                    if (this.idMunicipio == 0 || this.idMunicipio == null)
                    {
                        result = "El campo Municipio no puede quedar vacio";
                    }
                }
                else if (name == "IdEstado")
                {
                    if (this.idEstado == 0 || this.idEstado == null)
                    {
                        result = "El campo Estado no puede quedar vacio";
                    }
                }
                return result;
            }
        }
        #endregion

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
