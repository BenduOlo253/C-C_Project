using System.Collections.Generic;

namespace C_C.Model
{
    
    public class Perfil
    {
        
        public int IdPerfil { get; set; }
        public string NikName { get; set; }
        public string Biografia { get; set; }
        public List<string> Fotos { get; set; } 

        
        public int Matricula { get; set; }
        public string Nombre { get; set; }
        public string Amaterno { get; set; }
        public string Apaterno { get; set; }
        public int Edad { get; set; }
        public char Genero { get; set; }
        public string Carrera { get; set; }
        public string Email { get; set; }

        
        public char PreferenciaGenero { get; set; }
        public int EdadMinima { get; set; }
        public int EdadMaxima { get; set; }

        
        public Perfil()
        {
            Fotos = new List<string>();
        }
    }
}

