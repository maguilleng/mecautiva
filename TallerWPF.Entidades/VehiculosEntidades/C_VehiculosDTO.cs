using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.Mvvm;

namespace TallerWPF.Entidades.VehiculosEntidades
{
    public class C_VehiculosDTO : BindableBase
    {
        public int idVehiculo { get; set; } //Creo no es necesario
        string placas;
        string marca;
        string linea;
        int? modelo;
        string color;
        string noEconomico;
        int? idCliente;

        public DateTime FechaAlta { get; set; }
        public DateTime FechaModificacion { get; set; }
        public int? IdUsuarioCreacion { get; set; }
        public int? IdUsuarioModificacion { get; set; }
        public int? Estatus { get; set; }

        
        public string Placas
        {
            get { return placas; }
            set { SetProperty(ref this.placas, value);
            }
        }

        public string Marca
        {
            get { return marca; }
            set
            {
                SetProperty(ref this.marca, value);
            }
        }
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

        public int? IdCliente
         {
            get { return idCliente; }
            set
            {
                SetProperty(ref this.idCliente, value);
            }
        }

        public C_Clientes Cliente { get; set; }
        public C_Vehiculos Vehiculo { get; set; }

        public C_VehiculosDTO (C_Vehiculos vehiculo)
        {
            idVehiculo = vehiculo.IdVehiculo;
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
