using System;
using Microsoft.Practices.Prism.PubSubEvents;
using TallerWPF.Entidades;

namespace TallerWPF.Infraestructura
{
    public class UsuarioLogueadoEvent : PubSubEvent<Usuario>{}
    public class CerrarSesionEvent : PubSubEvent<object>{}
    public class ClienteSeleccionadoEvent : PubSubEvent<C_Clientes> { }
    public class VehiculoSeleccionadoEvent : PubSubEvent<C_Vehiculos> { }
}
