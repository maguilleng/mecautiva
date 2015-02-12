using System;
using Microsoft.Practices.Prism.PubSubEvents;
using TallerWPF.Entidades;

namespace TallerWPF.Infraestructura
{
    public class UsuarioLogueadoEvent : PubSubEvent<Usuario>{}
    public class CerrarSesionEvent : PubSubEvent<object>{}
    public class ClienteSeleccionadoEvent : PubSubEvent<C_Clientes> { }
    public class VehiculoSeleccionadoEvent : PubSubEvent<C_Vehiculos> { }

    public class CrearNuevaVenta : PubSubEvent<bool> { }

    //Componentes Compartidos
    public class GuardarEvent : PubSubEvent<object> { }

    //Modulo Clientes
    public class NuevoClienteEvent : PubSubEvent<object> { }
    public class NuevoVehiculoEvent : PubSubEvent<object> { }

    //Modulo Ventas
    public class NuevoServicioEvent : PubSubEvent<object> { }

    //Modulo Ventas
    public class PagarVentaActualEvent : PubSubEvent<object> { }
}
