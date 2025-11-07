using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_de_Gestión_Universitaria
{
   
    // Interfaz genérica que define un contrato para objetos identificables.
   
    public interface IIdentificable
    {
        
        // Identificación única del objeto.
        // Solo lectura desde la interfaz.
        
        string Identificacion { get; }
    }
}