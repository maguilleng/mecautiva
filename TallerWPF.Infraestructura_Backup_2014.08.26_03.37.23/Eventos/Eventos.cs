using System;
using Microsoft.Practices.Prism.PubSubEvents;
using TallerWPF.Entidades;

namespace TallerWPF.Infraestructura
{
    public class UsuarioLogueadoEvent : PubSubEvent<Usuario>{}
    public class CerrarSesionEvent : PubSubEvent<object>{}
}
