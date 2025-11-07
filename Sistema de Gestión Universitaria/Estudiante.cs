using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sistema_de_Gestión_Universitaria;

namespace Sistema_de_Gestión_Universitaria
{
    public class Estudiante : Persona
    {
        private string carrera;
        private string numeroMatricula;

        
        /// Carrera que estudia el estudiante.
       
        public string Carrera
        {
            get => carrera;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("La carrera no puede estar vacía");
                carrera = value;
            }
        }

       
        /// Número único de matrícula del estudiante.
        
        public string NumeroMatricula
        {
            get => numeroMatricula;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("El número de matrícula no puede estar vacío");
                numeroMatricula = value;
            }
        }

        
        /// Constructor que valida la edad mínima para estudiantes (15 años).
       
        public Estudiante(string identificacion, string nombre, string apellido,
                         DateTime fechaNacimiento, string carrera, string numeroMatricula)
        {
            // Asignamos las propiedades heredadas
            Identificacion = identificacion;
            Nombre = nombre;
            Apellido = apellido;
            FechaNacimiento = fechaNacimiento;

            
            if (Edad < 15)
                throw new ArgumentException("El estudiante debe tener al menos 15 años");

          
            Carrera = carrera;
            NumeroMatricula = numeroMatricula;
        }

      
        public override string ObtenerRol()
        {
            return "ESTUDIANTE";
        }

        
        public override string ToString()
        {
            return base.ToString() + $"\n  Carrera: {Carrera} - Matrícula: {NumeroMatricula}";
        }
    }
}
