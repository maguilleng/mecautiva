using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TallerWPF.Entidades;
using System.ComponentModel.Composition;
using System.Collections.ObjectModel;
using TallerWPF.Persistencia;

namespace Servicios
{
    [Export]
    public class ProductosService
    {
        private ServiciosPersistencia serviciosPersistencia;

        public ProductosService() {
            serviciosPersistencia = new ServiciosPersistencia();
        }

        public C_Servicios BuscarProducto(int Codigo)
        {
            
            var producto = new C_Servicios() 
            { 
            Serie = "111",
            Descripcion = "Balatas",
            IdMarca = 1,
            IdTipoServicio = 2,
            IdServicio = 1,
            //Modelo = "BALAXB"
            };
            return producto;
        }

        public ObservableCollection<C_Servicios> ObtenerProductos()
        {
            ObservableCollection<C_Servicios> Productos = new ObservableCollection<C_Servicios>();

            var productos = serviciosPersistencia.ObtenerCatalogoServicios();

            if(productos != null)
            {
                productos.ForEach(p => Productos.Add(p));
            }

            return Productos;
        }
    }
}
