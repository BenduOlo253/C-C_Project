using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_C.Model
{
    
    public interface IMatchRepository
    {

        bool CrearLike(int idPerfilEmisor, int idPerfilReceptor);
    }
}

