﻿using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.PubSubEvents;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Servicios;
using TallerWPF.Infraestructura;
using TallerWPF.Entidades;
using TallerWPF.Entidades.VehiculosEntidades;

using TallerWPF.ClientesModule.SharedServices;
using TallerWPF.Infraestructura.Interfaces;
using System.Windows;


namespace TallerWPF.ClientesModule.ViewModels
{
    [Export]
    class vmClientes : BindableBase
    {
        IServicioCliente servicioCliente;
        ClientesService clientesCtrl = new ClientesService();

        #region COMMANDS
        #endregion

        #region CONSTRUCTORES
        public vmClientes()
        { 
        }

        [ImportingConstructor]
        public vmClientes(IServicioCliente servicioCliente)
        {
            this.servicioCliente = servicioCliente;
        }
        #endregion

        #region METODOS A LA PERSISTENCIA
        private ObservableCollection<C_ClientesDTO> ListadoClientes()
        {
            ObservableCollection<C_ClientesDTO> clientes = new ObservableCollection<C_ClientesDTO>();
             clientesCtrl.ObtenerListaClientes().ForEach(c => clientes.Add(new C_ClientesDTO(c)));
             return clientes;
        }


        public List<C_TiposPersona> ListadoTiposPersona()
        {
            return clientesCtrl.ObtenerListaTiposPersona().ToList();
        }

        public List<C_Estados> ListadoEstados()
        {
            return clientesCtrl.ObtenerListaEstados().ToList();
        }

        public ObservableCollection<C_Municipios> ListadoMunicipiosPorEstado(int idEstado)
        {
            ObservableCollection<C_Municipios> municipios = new ObservableCollection<C_Municipios>();
            clientesCtrl.ObtenerListaMunicipiosPorEstado(idEstado).ForEach(m => municipios.Add(m));
            return municipios;
        }

        public ObservableCollection<C_Ciudades> ListadoCiudadesPorMunicipio(int idMunicipio)
        {
            ObservableCollection<C_Ciudades> ciudades = new ObservableCollection<C_Ciudades>();
            clientesCtrl.ObtenerListaCiudadesPorMunicipio(idMunicipio).ForEach(c => ciudades.Add(c));
            return ciudades;
        }
        #endregion

        //Esta lista es la que toma el binding del radgrid de la cartera de clientes
        ObservableCollection<C_ClientesDTO> clientes;
        public ObservableCollection<C_ClientesDTO> Clientes
        {
            get
            {
                if (this.clientes == null)
                {
                    clientes = ListadoClientes();
                }

                return this.clientes;
            }
            set { SetProperty(ref this.clientes, value); }
        }

        C_ClientesDTO clienteSeleccionado;
        public C_ClientesDTO ClienteSeleccionado
        {
            get { return this.clienteSeleccionado; }
            set
            {
                SetProperty(ref this.clienteSeleccionado, value);
            }
        }

        //TRAEMOS LOS TIPOS DE PERSONAS
        List<C_TiposPersona> tiposPersona;
        public List<C_TiposPersona> TiposPersona
        {
              
            get
            {
                C_TiposPersona tipoPersona = new C_TiposPersona();
                tipoPersona.Descripcion = "Seleccione...";

                if (this.tiposPersona == null)
                {
                    
                    tiposPersona = ListadoTiposPersona();
                    tiposPersona.Insert(0, tipoPersona);
                }

                return this.tiposPersona;
            }
            set { SetProperty(ref this.tiposPersona, value); }
        }

        //TREAEMOS LOS ESTADOS
        List<C_Estados> estados;
        public List<C_Estados> Estados
        {

            get
            {
                C_Estados estado = new C_Estados();
                estado.Descripcion = "Seleccione...";

                if (this.estados == null)
                {

                    estados = ListadoEstados();
                    estados.Insert(0, estado);
                }

                return this.estados;
            }
            set { SetProperty(ref this.estados, value); }
        }

        C_Estados estadoSeleccionado;
        public C_Estados EstadoSeleccionado
        {
            get { return this.estadoSeleccionado; }
            set
            {
                SetProperty(ref this.estadoSeleccionado, value);

                if (estadoSeleccionado != null)
                {
                    MunicipiosPorEstado = ListadoMunicipiosPorEstado(estadoSeleccionado.IdEstado);
                }
                else
                {
                    MunicipiosPorEstado.Clear();
                }
            }
        }
        
        //TRAEMOS LOS MUNICIPIOS
        ObservableCollection<C_Municipios> municipiosPorEstado;
        public ObservableCollection<C_Municipios> MunicipiosPorEstado
        {

             get
            {
             C_Municipios municipio = new C_Municipios();
                municipio.Descripcion = "Seleccione...";
                if (this.municipiosPorEstado == null)
                {
                    if (estadoSeleccionado != null)
                    {
                        municipiosPorEstado = ListadoMunicipiosPorEstado(estadoSeleccionado.IdEstado);
                         municipiosPorEstado.Insert(0, municipio);
                    }
                }

                return this.municipiosPorEstado;
            }
            set { SetProperty(ref this.municipiosPorEstado, value); }
        }

          C_Municipios municipioSeleccionado;
        public C_Municipios MunicipioSeleccionado
        {
            get { return this.municipioSeleccionado; }
            set
            {
                SetProperty(ref this.municipioSeleccionado, value);

                if (municipioSeleccionado != null)
                {
                    CiudadesPorMunicipio = ListadoCiudadesPorMunicipio(municipioSeleccionado.IdMunicipio);
                }
                else
                {
                    CiudadesPorMunicipio.Clear();
                }
            }
        }

        //TRAEMOS LAS CIUDADES
        ObservableCollection<C_Ciudades> ciudadesPorMunicipio;
        public ObservableCollection<C_Ciudades> CiudadesPorMunicipio
        {

            get
            {
                C_Ciudades ciudad = new C_Ciudades();
                ciudad.Descripcion = "Seleccione...";
                if (this.ciudadesPorMunicipio == null)
                {
                    if (estadoSeleccionado != null)
                    {
                        ciudadesPorMunicipio = ListadoCiudadesPorMunicipio(municipioSeleccionado.IdMunicipio);
                        ciudadesPorMunicipio.Insert(0, ciudad);
                    }
                }

                return this.ciudadesPorMunicipio;
            }
            set { SetProperty(ref this.ciudadesPorMunicipio, value); }
        }
    }
}

