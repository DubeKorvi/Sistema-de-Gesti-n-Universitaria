using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_de_Gestión_Universitaria
{
    public partial class Curso : IIdentificable
    {
        private string codigo;
        private string nombre;
        private int creditos;
        private Profesor profesorAsignado;

        // Implementación de la propiedad requerida por IIdentificable  
        public string Identificacion => Codigo;

        // Código único del curso (ej: "MAT-101").  
        public string Codigo
        {
            get => codigo;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("El código del curso no puede estar vacío");
                codigo = value;
            }
        }

        // Nombre descriptivo del curso.  
        public string Nombre
        {
            get => nombre;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("El nombre del curso no puede estar vacío");
                nombre = value;
            }
        }

        // Cantidad de créditos que vale el curso.  
        // Debe ser mayor a 0.  
        public int Creditos
        {
            get => creditos;
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Los créditos deben ser mayores a 0");
                creditos = value;
            }
        }

        // Profesor asignado para impartir el curso.  
        // Puede ser null si aún no se ha asignado.  
        public Profesor ProfesorAsignado
        {
            get => profesorAsignado;
            set => profesorAsignado = value;
        }

        // Constructor del curso.  
        public Curso(string codigo, string nombre, int creditos, Profesor profesorAsignado = null)
        {
            Codigo = codigo;
            Nombre = nombre;
            Creditos = creditos;
            ProfesorAsignado = profesorAsignado;
        }

        // Representación en texto del curso.  
        public override string ToString()
        {
            string profesor = ProfesorAsignado != null
                ? $"Prof. {ProfesorAsignado.Nombre} {ProfesorAsignado.Apellido}"
                : "Sin profesor asignado";

            return $"[{Codigo}] {Nombre} ({Creditos} créditos) - {profesor}";
        }
    }

}
