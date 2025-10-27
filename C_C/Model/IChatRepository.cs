
using C_C.Model;
using System.Collections.Generic;
using System.Threading.Tasks; 

namespace C_C.Model
{
    public interface IChatRepository
    {
        
        Task<List<ChatPreview>> ObtenerChatsAsync(int idPerfilUsuario);

        
        Task<List<Mensaje>> ObtenerMensajesAsync(int idChat);

        
        Task EnviarMensajeAsync(Mensaje nuevoMensaje);
    }
}