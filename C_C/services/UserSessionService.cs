using System;

namespace C_C.Services
{
   
    public class UserSessionService
    {
        
        private static readonly Lazy<UserSessionService> _instancia =
            new Lazy<UserSessionService>(() => new UserSessionService());

        public static UserSessionService Instancia => _instancia.Value;

       
        private UserSessionService() { }
        


        
        public int? IdPerfilLogueado { get; private set; }

        
        public void Login(int idPerfil)
        {
            if (idPerfil <= 0)
                throw new ArgumentException("El ID de perfil debe ser positivo.", nameof(idPerfil));

            IdPerfilLogueado = idPerfil;
           
        }

       
        public void Logout()
        {
            IdPerfilLogueado = null; 
            
        }

        
    }
}

