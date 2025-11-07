using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_de_Gestión_Universitaria
{
    public partial class GestorMatriculas
    {
        // Lista de todas las matrículas en el sistema
        private List<Matricula> matriculas;

        // Referencias a los repositorios de estudiantes y cursos
        private Repositorio<Estudiante> repositorioEstudiantes;
        private Repositorio<Curso> repositorioCursos;

        
        // Constructor que inicializa el gestor con los repositorios necesarios.
        
        public GestorMatriculas(Repositorio<Estudiante> repoEstudiantes,
                               Repositorio<Curso> repoCursos)
        {
            matriculas = new List<Matricula>();
            repositorioEstudiantes = repoEstudiantes ?? throw new ArgumentNullException(nameof(repoEstudiantes));
            repositorioCursos = repoCursos ?? throw new ArgumentNullException(nameof(repoCursos));
        }

        
        // Matricula a un estudiante en un curso.
        // Valida que el estudiante y curso existan y que no esté ya matriculado.
        
        public Matricula MatricularEstudiante(Estudiante estudiante, Curso curso)
        {
            // Validación 1: Verificar que los objetos no sean null
            if (estudiante == null)
                throw new ArgumentNullException(nameof(estudiante), "El estudiante no puede ser null");
            if (curso == null)
                throw new ArgumentNullException(nameof(curso), "El curso no puede ser null");

            // Validación 2: Verificar que el estudiante exista en el repositorio
            if (!repositorioEstudiantes.Existe(estudiante.Identificacion))
            {
                throw new InvalidOperationException(
                    $"El estudiante {estudiante.Identificacion} no está registrado en el sistema");
            }

            // Validación 3: Verificar que el curso exista en el repositorio
            if (!repositorioCursos.Existe(curso.Codigo))
            {
                throw new InvalidOperationException(
                    $"El curso {curso.Codigo} no está registrado en el sistema");
            }

            // Validación 4: Verificar que el estudiante no esté ya matriculado en el curso
            bool yaMatriculado = matriculas.Any(m =>
                m.Estudiante.Identificacion == estudiante.Identificacion &&
                m.Curso.Codigo == curso.Codigo);

            if (yaMatriculado)
            {
                throw new InvalidOperationException(
                    $"El estudiante {estudiante.Nombre} {estudiante.Apellido} " +
                    $"ya está matriculado en el curso {curso.Nombre}");
            }

            // Si todas las validaciones pasan, crear la matrícula
            var nuevaMatricula = new Matricula(estudiante, curso, DateTime.Now);
            matriculas.Add(nuevaMatricula);

            Console.WriteLine($"Estudiante {estudiante.Nombre} {estudiante.Apellido} " +
                            $"matriculado en {curso.Nombre}");

            return nuevaMatricula;
        }

        
        // Agrega una calificación a la matrícula de un estudiante en un curso específico.
        
        public void AgregarCalificacion(string idEstudiante, string codigoCurso, decimal calificacion)
        {
            // Validación de parámetros
            if (string.IsNullOrWhiteSpace(idEstudiante))
                throw new ArgumentException("El ID del estudiante no puede estar vacío");
            if (string.IsNullOrWhiteSpace(codigoCurso))
                throw new ArgumentException("El código del curso no puede estar vacío");

            // Validación del rango de calificación (0-10)
            if (calificacion < 0 || calificacion > 10)
            {
                throw new ArgumentException(
                    $"La calificación debe estar entre 0 y 10. Valor recibido: {calificacion}");
            }

            // Buscar la matrícula correspondiente
            var matricula = matriculas.FirstOrDefault(m =>
                m.Estudiante.Identificacion == idEstudiante &&
                m.Curso.Codigo == codigoCurso);

            if (matricula == null)
            {
                throw new InvalidOperationException(
                    $"No existe matrícula para el estudiante {idEstudiante} en el curso {codigoCurso}");
            }

            // Agregar la calificación (el método de Matricula ya tiene validación)
            matricula.AgregarCalificacion(calificacion);
        }

      
        // Obtiene todas las matrículas de un estudiante específico.
       
        public List<Matricula> ObtenerMatriculasPorEstudiante(string idEstudiante)
        {
            if (string.IsNullOrWhiteSpace(idEstudiante))
                throw new ArgumentException("El ID del estudiante no puede estar vacío");

            // Filtrar matrículas del estudiante usando LINQ
            return matriculas
                .Where(m => m.Estudiante.Identificacion == idEstudiante)
                .ToList();
        }

        
        // Obtiene todos los estudiantes matriculados en un curso específico.
        
        public List<Estudiante> ObtenerEstudiantesPorCurso(string codigoCurso)
        {
            if (string.IsNullOrWhiteSpace(codigoCurso))
                throw new ArgumentException("El código del curso no puede estar vacío");

            // Filtrar matrículas del curso y obtener solo los estudiantes
            return matriculas
                .Where(m => m.Curso.Codigo == codigoCurso)
                .Select(m => m.Estudiante)
                .ToList();
        }

       
        // Genera un reporte completo de un estudiante con todas sus matrículas.
      
        public string GenerarReporteEstudiante(string idEstudiante)
        {
            if (string.IsNullOrWhiteSpace(idEstudiante))
                throw new ArgumentException("El ID del estudiante no puede estar vacío");

            // Buscar el estudiante
            var estudiante = repositorioEstudiantes.BuscarPorId(idEstudiante);
            if (estudiante == null)
            {
                throw new InvalidOperationException(
                    $"No se encontró estudiante con ID: {idEstudiante}");
            }

            // Obtener todas las matrículas del estudiante
            var matriculasEstudiante = ObtenerMatriculasPorEstudiante(idEstudiante);

            // Construir el reporte usando StringBuilder (más eficiente para concatenación)
            var reporte = new StringBuilder();

            // Encabezado del reporte
            reporte.AppendLine("═══════════════════════════════════════════════════════════");
            reporte.AppendLine("           REPORTE ACADÉMICO DE ESTUDIANTE");
            reporte.AppendLine("═══════════════════════════════════════════════════════════");
            reporte.AppendLine();

            // Información del estudiante
            reporte.AppendLine($"Estudiante: {estudiante.Nombre} {estudiante.Apellido}");
            reporte.AppendLine($"ID: {estudiante.Identificacion}");
            reporte.AppendLine($"Carrera: {estudiante.Carrera}");
            reporte.AppendLine($"Matrícula: {estudiante.NumeroMatricula}");
            reporte.AppendLine($"Edad: {estudiante.Edad} años");
            reporte.AppendLine();
            reporte.AppendLine("───────────────────────────────────────────────────────────");
            reporte.AppendLine("CURSOS MATRICULADOS:");
            reporte.AppendLine("───────────────────────────────────────────────────────────");
            reporte.AppendLine();

            if (matriculasEstudiante.Count == 0)
            {
                reporte.AppendLine("  No hay cursos matriculados.");
            }
            else
            {
                int cursoNum = 1;
                decimal sumaPromedios = 0;
                int cursosConCalificaciones = 0;

                foreach (var matricula in matriculasEstudiante)
                {
                    reporte.AppendLine($"{cursoNum}. {matricula.Curso.Nombre} ({matricula.Curso.Codigo})");
                    reporte.AppendLine($"   Créditos: {matricula.Curso.Creditos}");
                    reporte.AppendLine($"   Profesor: {(matricula.Curso.ProfesorAsignado != null ?
                        $"{matricula.Curso.ProfesorAsignado.Nombre} {matricula.Curso.ProfesorAsignado.Apellido}" :
                        "Sin asignar")}");

                    if (matricula.Calificaciones.Count > 0)
                    {
                        reporte.AppendLine($"   Calificaciones: {string.Join(", ",
                            matricula.Calificaciones.Select(c => c.ToString("F1")))}");
                        reporte.AppendLine($"   Promedio: {matricula.ObtenerPromedio():F2}");
                        reporte.AppendLine($"   Estado: {matricula.ObtenerEstado()}");

                        sumaPromedios += matricula.ObtenerPromedio();
                        cursosConCalificaciones++;
                    }
                    else
                    {
                        reporte.AppendLine($"   Estado: {matricula.ObtenerEstado()}");
                    }

                    reporte.AppendLine();
                    cursoNum++;
                }

                // Estadísticas generales
                reporte.AppendLine("───────────────────────────────────────────────────────────");
                reporte.AppendLine("ESTADÍSTICAS GENERALES:");
                reporte.AppendLine("───────────────────────────────────────────────────────────");
                reporte.AppendLine($"Total de cursos: {matriculasEstudiante.Count}");
                reporte.AppendLine($"Cursos con calificaciones: {cursosConCalificaciones}");

                if (cursosConCalificaciones > 0)
                {
                    decimal promedioGeneral = sumaPromedios / cursosConCalificaciones;
                    reporte.AppendLine($"Promedio general: {promedioGeneral:F2}");

                    int aprobados = matriculasEstudiante.Count(m => m.HaAprobado());
                    int reprobados = matriculasEstudiante.Count(m =>
                        m.Calificaciones.Count > 0 && !m.HaAprobado());

                    reporte.AppendLine($"Cursos aprobados: {aprobados}");
                    reporte.AppendLine($"Cursos reprobados: {reprobados}");
                }
            }

            reporte.AppendLine("═══════════════════════════════════════════════════════════");
            reporte.AppendLine($"Reporte generado: {DateTime.Now:MM/DD/YYYY HH:mm:ss}");
            reporte.AppendLine("═══════════════════════════════════════════════════════════");

            return reporte.ToString();
        }

        // Obtiene todas las matrículas del sistema.
        
        public List<Matricula> ObtenerTodasLasMatriculas()
        {
            return new List<Matricula>(matriculas);
        }


       
        // Obtiene la cantidad total de matrículas.
        
        public int CantidadMatriculas => matriculas.Count;

       
        //Desmatricula a un estudiante de un curso.
        
        public bool Desmatricular(string idEstudiante, string codigoCurso)
        {
            var matricula = matriculas.FirstOrDefault(m =>
                m.Estudiante.Identificacion == idEstudiante &&
                m.Curso.Codigo == codigoCurso);

            if (matricula == null)
                return false;

            matriculas.Remove(matricula);
            Console.WriteLine($"Estudiante desmatriculado del curso {codigoCurso}");
            return true;
        }
    }
}
