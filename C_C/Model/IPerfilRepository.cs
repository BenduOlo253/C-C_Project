using System.Collections.Generic;

namespace C_C.Model
{
   
    public interface IPerfilRepository
    {
      
        List<Perfil> ObtenerPerfilesSugeridos(int idPerfilActual);

        
        Perfil ObtenerPerfilPorMatricula(int matricula);

        Perfil ObtenerPerfilPorId(int idPerfil);

       
        void ActualizarPerfil(Perfil perfil);

        
        void AddPerfil(string Nikname);
        void UpdatePerfil(string Nikname);
    }
}

