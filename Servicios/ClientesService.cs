using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TallerWPF.Entidades;
using TallerWPF.Entidades.VehiculosEntidades;
using TallerWPF.Persistencia;

namespace Servicios
{
    [Export]
    public class ClientesService
    {
        #region ATRIBUTOS PRIVADOS
        ClientesPersistencia persistencia;
        #endregion

        #region CONSTRUCTORES
        public ClientesService()
        {
            persistencia = new ClientesPersistencia();
        }
        #endregion

        #region METODOS CLIENTES
        public List<C_Clientes> ObtenerListaClientes()
        {
            return persistencia.ObtenerListaClientes().ToList();
        }

        public List<C_TiposPersona> ObtenerListaTiposPersona()
        {
            return persistencia.ObtenerListaTiposPersona().ToList();
        }

        public List<C_Municipios> ObtenerListaMunicipiosPorEstado(int  idEstado)
        {
            return persistencia.ObtenerListaMunicipiosPorEstado(idEstado).ToList();
        }

        public List<C_Estados> ObtenerListaEstados()
        {
            return persistencia.ObtenerListaEstados().ToList();
        }

        public List<C_Ciudades> ObtenerListaCiudadesPorMunicipio(int idMunicipio)
        {
            return persistencia.ObtenerListaCiudadesPorMunicipio(idMunicipio);
        }

        public bool buscarRFCEnBD(string rfc)
        {
            return persistencia.buscarRFCEnBD(rfc);
        }

        public bool buscarNumeroClienteEnBD(string noCliente)
        {
            return persistencia.buscarNumeroClienteEnBD(noCliente);
        }

        public String GuardarCliente(C_Clientes cliente)
        {
            return persistencia.GuardarCliente(cliente);
        }
#endregion

        #region METODOS VEHICULOS

        public List<C_Vehiculos> ObtenerListaVehiculosxCliente(int idCliente)
        {
            return persistencia.ObtenerListaVehiculosxCliente(idCliente).ToList();
        }

        public String GuardarVehiculo(C_Vehiculos vehiculo)
        {
            return persistencia.GuardarVehiculo(vehiculo);
        }
        
        public bool buscarPlacaEnBD(string placa)
        {
         return persistencia.buscarPlacaEnBD(placa);
        }

        #endregion 
    }
}
