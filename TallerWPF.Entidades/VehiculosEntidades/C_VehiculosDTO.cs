using System;
using System.Globalization;
using System.Threading;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.Mvvm;
using System.ComponentModel;

namespace TallerWPF.Entidades.VehiculosEntidades
{
   
    public class C_VehiculosDTO : ValidatableBindableBase , IDataErrorInfo
    {
        int idVehiculo;
        string placas;
        string marca;
        string linea;
        int? modelo;
        string color;
        string noEconomico;
        int idCliente;

        public DateTime FechaAlta { get; set; }
        public DateTime FechaModificacion { get; set; }
        public int? IdUsuarioCreacion { get; set; }
        public int? IdUsuarioModificacion { get; set; }
        public int? Estatus { get; set; }


        public int IdVehiculo
        {
            get { return idVehiculo; }
            set
            {
                SetProperty(ref this.idVehiculo, value);
            }
        }

        #region Data for ValidationInfo
        [Required]
        public string Placas
        {
            get { return placas; }
            set { SetProperty(ref this.placas, value);
            }
        }

        [Required]
        public string Marca
        {
            get { return marca; }
            set
            {
                SetProperty(ref this.marca, value);
            }
        }

        [Required]
        public string Linea
        {
            get { return linea; }
            set
            {
                SetProperty(ref this.linea, value);
            }
        }

        public int? Modelo
        {
            get { return modelo; }
            set
            {
                SetProperty(ref this.modelo, value);
            }
        }
        #endregion

        public string Color
        {
            get { return color; }
            set
            {
                SetProperty(ref this.color, value);
            }
        }

        public string NoEconomico
        {
            get { return noEconomico; }
            set
            {
                SetProperty(ref this.noEconomico, value);
            }
        }

        public int IdCliente
         {
            get { return idCliente; }
            set
            {
                SetProperty(ref this.idCliente, value);
            }
        }

        public C_Clientes Cliente { get; set; }
        public C_Vehiculos Vehiculo { get; set; }


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

                if (name == "Placas")
                {
                    if (this.placas == "" || this.placas == null)
                    {
                        result = "El campo Placa no puede quedar vacio";
                    }
                }
                else if(name == "Marca")
                {
                    if (this.marca == "" || this.marca == null)
                    {
                        result = "El campo Marca no puede quedar vacio";
                    }
                }
                else if (name == "Linea")
                {
                    if (this.linea == "" || this.linea == null)
                    {
                        result = "El campo Linea no puede quedar vacio";
                    }
                }
                else if (name == "Modelo")
                {
                    if (this.modelo < 1950 || this.modelo > DateTime.Now.Year + 1)
                    {
                        result = "El Modelo es incorrecto";
                    }
                }
                return result;
            }
        }
        #endregion


        public C_VehiculosDTO (C_Vehiculos vehiculo)
        {
            IdVehiculo = vehiculo.IdVehiculo;
            Marca = vehiculo.Marca;
            Linea = vehiculo.Linea;
            Modelo = vehiculo.Modelo;
            Color = vehiculo.Color;
            NoEconomico = vehiculo.NoEconomico;
            IdCliente = vehiculo.IdCliente;
            Placas = vehiculo.Placas;
        }

        public C_VehiculosDTO()
        { 
        
        }

    }
}
