using System;
using C_C.Services; 
namespace C_C.Model
{
    public class Mensaje
    {
        public int IdMensaje { get; set; }
        public int IdChat { get; set; }
        public DateTime Fecha { get; set; }
        public bool ConfirmacionLectura { get; set; }
        public int IdPerfilRemitente { get; set; } 
        public string Texto { get; set; } 

        
        public bool EsEnviadoPorMi
        {
            get
            {
                
                int? miId = UserSessionService.Instancia.IdPerfilLogueado;
                return miId.HasValue && IdPerfilRemitente == miId.Value;
            }
        }
    }
}

