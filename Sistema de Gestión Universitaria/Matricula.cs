using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_de_Gestión_Universitaria
{
    public class Matricula : IEvaluable
    {
        // Constante que define la nota mínima para aprobar
        private const decimal NOTA_MINIMA_APROBACION = 7.0m;

        private Estudiante estudiante;
        private Curso curso;
        private DateTime fechaMatricula;
        private List<decimal> calificaciones;

        
        /// Estudiante que está matriculado.
        
        public Estudiante Estudiante
        {
            get => estudiante;
            set => estudiante = value ?? throw new ArgumentNullException(nameof(value),
                "El estudiante no puede ser null");
        }

       
        // Curso en el que está matriculado.
      
        public Curso Curso
        {
            get => curso;
            set => curso = value ?? throw new ArgumentNullException(nameof(value),
                "El curso no puede ser null");
        }

        
        // Fecha en la que se realizó la matrícula.
      
        public DateTime FechaMatricula
        {
            get => fechaMatricula;
            set => fechaMatricula = value;
        }

       
        // Lista de todas las calificaciones obtenidas en el curso.
        // Se expone como solo lectura para proteger la colección interna.
        
        public IReadOnlyList<decimal> Calificaciones => calificaciones.AsReadOnly();

        
        // Constructor de la matrícula.
       
        public Matricula(Estudiante estudiante, Curso curso, DateTime fechaMatricula)
        {
            Estudiante = estudiante;
            Curso = curso;
            FechaMatricula = fechaMatricula;
            calificaciones = new List<decimal>();
        }


        
        // Agrega una calificación a la lista.
        // Valida que la calificación esté entre 0 y 10.
      
        public void AgregarCalificacion(decimal calificacion)
        {
            // Validación: calificación debe estar entre 0 y 10
            if (calificacion < 0 || calificacion > 10)
            {
                throw new ArgumentException("La calificación debe estar entre 0 y 10");
            }

            calificaciones.Add(calificacion);

            // Opcional: mensaje de confirmación
            Console.WriteLine($"✓ Calificación {calificacion:F1} agregada para " +
                            $"{Estudiante.Nombre} en {Curso.Nombre}");
        }

        
        // Calcula y retorna el promedio de todas las calificaciones.
        // Si no hay calificaciones, retorna 0.
        // Usa LINQ para calcular el promedio.
      
        public decimal ObtenerPromedio()
        {
            // Si no hay calificaciones, retorna 0
            if (calificaciones.Count == 0)
                return 0;

            // Usa LINQ para calcular el promedio
            return calificaciones.Average();
        }

        
        // Determina si el estudiante ha aprobado el curso.
        // Para aprobar debe tener al menos una calificación y el promedio >= 7.0
      
        public bool HaAprobado()
        {
            // Debe tener al menos una calificación
            if (calificaciones.Count == 0)
                return false;

            // El promedio debe ser mayor o igual a la nota mínima
            return ObtenerPromedio() >= NOTA_MINIMA_APROBACION;
        }

        
        // Obtiene el estado actual de la matrícula.
        // Retorna: "Aprobado", "Reprobado" o "En Curso"
        
        public string ObtenerEstado()
        {
            // Si no hay calificaciones, está en curso
            if (calificaciones.Count == 0)
                return "En Curso";

            // Si ha aprobado, retorna "Aprobado", sino "Reprobado"
            return HaAprobado() ? "Aprobado" : "Reprobado";
        }

        
        // Representación en texto de la matrícula con toda su información.
       
        public override string ToString()
        {
            string califs = calificaciones.Count > 0
                ? string.Join(", ", calificaciones.Select(c => c.ToString("F1")))
                : "Sin calificaciones";

            return $"Matrícula: {Estudiante.Nombre} {Estudiante.Apellido}\n" +
                   $"  Curso: {Curso.Nombre} ({Curso.Codigo})\n" +
                   $"  Fecha: {FechaMatricula:dd/MM/yyyy}\n" +
                   $"  Calificaciones: {califs}\n" +
                   $"  Promedio: {ObtenerPromedio():F2}\n" +
                   $"  Estado: {ObtenerEstado()}";
        }
    }
}


