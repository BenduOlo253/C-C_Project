
using System;

namespace C_C.Model
{
   
    public class ChatPreview
    {
        

        
        public int IdChat { get; set; }

        
        public string Nombre { get; set; }

        
        public string FotoUrl { get; set; }

        
        public int IdOtroPerfil { get; set; }
        public string UltimoMensaje { get; set; }
        public DateTime FechaUltimoMensaje { get; set; }
    }
}