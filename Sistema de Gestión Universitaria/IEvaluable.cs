using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_de_Gestión_Universitaria
{
    public interface IEvaluable
    {
       
        // Agrega una calificación al objeto evaluable.
        
        void AgregarCalificacion(decimal calificacion);

       
        // Obtiene el promedio de todas las calificaciones.
        
        decimal ObtenerPromedio();

        
        // Determina si el objeto evaluable ha sido aprobado.
        
        bool HaAprobado();
    }
}
