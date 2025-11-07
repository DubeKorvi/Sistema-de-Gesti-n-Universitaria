using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_de_Gestión_Universitaria
{
    public partial class GestorMatriculas
    {
       
        // Obtiene los 10 estudiantes con mejor promedio general.
        // Usa LINQ para ordenar y seleccionar.
        
        public List<(Estudiante estudiante, decimal promedioGeneral)> ObtenerTop10Estudiantes()
        {
            Console.WriteLine("\nObteniendo Top 10 Estudiantes...\n");

            // Agrupar matrículas por estudiante y calcular promedio general
            var estudiantesConPromedio = matriculas
                .Where(m => m.Calificaciones.Count > 0)  // Solo con calificaciones
                .GroupBy(m => m.Estudiante)              // Agrupar por estudiante
                .Select(grupo => new                     // Proyectar a nueva estructura
                {
                    Estudiante = grupo.Key,
                    PromedioGeneral = grupo.Average(m => m.ObtenerPromedio())
                })
                .OrderByDescending(x => x.PromedioGeneral)  // Ordenar descendente
                .Take(10)                                    // Tomar los primeros 10
                .Select(x => (x.Estudiante, x.PromedioGeneral))  // Convertir a tupla
                .ToList();

            return estudiantesConPromedio;
        }

      
        // Obtiene estudiantes en riesgo académico (promedio menor a 7.0).
     
        public List<(Estudiante estudiante, decimal promedio, int cursosRiesgo)>
            ObtenerEstudiantesEnRiesgo()
        {
            Console.WriteLine("\n Identificando estudiantes en riesgo académico...\n");

            const decimal PROMEDIO_MINIMO = 7.0m;

            var estudiantesEnRiesgo = matriculas
                .Where(m => m.Calificaciones.Count > 0)
                .GroupBy(m => m.Estudiante)
                .Select(grupo => new
                {
                    Estudiante = grupo.Key,
                    PromedioGeneral = grupo.Average(m => m.ObtenerPromedio()),
                    CursosRiesgo = grupo.Count(m => m.ObtenerPromedio() < PROMEDIO_MINIMO)
                })
                .Where(x => x.PromedioGeneral < PROMEDIO_MINIMO)
                .OrderBy(x => x.PromedioGeneral)  // Los de menor promedio primero
                .Select(x => (x.Estudiante, x.PromedioGeneral, x.CursosRiesgo))
                .ToList();

            return estudiantesEnRiesgo;
        }

       
        // Obtiene los cursos más populares ordenados por cantidad de estudiantes.
       
        public List<(Curso curso, int cantidadEstudiantes, decimal promedioGeneral)>
            ObtenerCursosMasPopulares()
        {
            Console.WriteLine("\nAnalizando popularidad de cursos...\n");

            var cursosPopulares = matriculas
                .GroupBy(m => m.Curso)
                .Select(grupo => new
                {
                    Curso = grupo.Key,
                    CantidadEstudiantes = grupo.Count(),
                    PromedioGeneral = grupo
                        .Where(m => m.Calificaciones.Count > 0)
                        .Any() ?
                        grupo
                            .Where(m => m.Calificaciones.Count > 0)
                            .Average(m => m.ObtenerPromedio()) : 0
                })
                .OrderByDescending(x => x.CantidadEstudiantes)
                .ThenByDescending(x => x.PromedioGeneral)
                .Select(x => (x.Curso, x.CantidadEstudiantes, x.PromedioGeneral))
                .ToList();

            return cursosPopulares;
        }

        
        // Calcula el promedio general de todos los estudiantes del sistema.
     
        public decimal ObtenerPromedioGeneral()
        {
            Console.WriteLine("\n Calculando promedio general del sistema...\n");

            var matriculasConCalificaciones = matriculas
                .Where(m => m.Calificaciones.Count > 0)
                .ToList();

            if (matriculasConCalificaciones.Count == 0)
                return 0;

            return matriculasConCalificaciones.Average(m => m.ObtenerPromedio());
        }

      
        // Agrupa estudiantes por carrera y calcula estadísticas.
       
        public Dictionary<string, EstadisticasCarrera> ObtenerEstadisticasPorCarrera()
        {
            Console.WriteLine("\n Generando estadísticas por carrera...\n");

            var estadisticas = matriculas
                .Where(m => m.Calificaciones.Count > 0)
                .GroupBy(m => m.Estudiante.Carrera)
                .ToDictionary(
                    grupo => grupo.Key,  // Key: nombre de la carrera
                    grupo => new EstadisticasCarrera  // Value: estadísticas
                    {
                        NombreCarrera = grupo.Key,
                        CantidadEstudiantes = grupo
                            .Select(m => m.Estudiante)
                            .Distinct()
                            .Count(),
                        PromedioGeneral = grupo.Average(m => m.ObtenerPromedio()),
                        MejorPromedio = grupo.Max(m => m.ObtenerPromedio()),
                        PeorPromedio = grupo.Min(m => m.ObtenerPromedio()),
                        TotalMatriculas = grupo.Count(),
                        EstudiantesAprobados = grupo
                            .Where(m => m.HaAprobado())
                            .Select(m => m.Estudiante)
                            .Distinct()
                            .Count()
                    });

            return estadisticas;
        }

        
        // Búsqueda flexible de estudiantes usando predicado personalizado.
        // Demuestra el uso de delegates con Func.
        
        public List<Estudiante> BuscarEstudiantes(Func<Estudiante, bool> criterio)
        {
            if (criterio == null)
                throw new ArgumentNullException(nameof(criterio));

            // Obtener todos los estudiantes únicos del sistema
            var todosEstudiantes = matriculas
                .Select(m => m.Estudiante)
                .Distinct()
                .ToList();

            // Aplicar el criterio de búsqueda
            return todosEstudiantes.Where(criterio).ToList();
        }

        // EXPRESIONES LAMBDA ADICIONALES - Filtros personalizados

        // Obtiene estudiantes que están tomando un curso específico.
        public List<Estudiante> ObtenerEstudiantesPorNombreCurso(string nombreCurso)
        {
            return matriculas
                .Where(m => m.Curso.Nombre.Contains(nombreCurso,
                    StringComparison.OrdinalIgnoreCase))
                .Select(m => m.Estudiante)
                .Distinct()
                .ToList();
        }

        // Obtiene cursos donde TODOS los estudiantes han aprobado.
        
        public List<Curso> ObtenerCursosConTodosAprobados()
        {
            return matriculas
                .GroupBy(m => m.Curso)
                .Where(grupo => grupo.All(m =>
                    m.Calificaciones.Count > 0 && m.HaAprobado()))
                .Select(grupo => grupo.Key)
                .ToList();
        }

        
        // Obtiene estudiantes que tienen promedio entre un rango específico.
        
        public List<Estudiante> ObtenerEstudiantesPorRangoPromedio(
            decimal minimo, decimal maximo)
        {
            return matriculas
                .Where(m => m.Calificaciones.Count > 0)
                .GroupBy(m => m.Estudiante)
                .Select(grupo => new
                {
                    Estudiante = grupo.Key,
                    Promedio = grupo.Average(m => m.ObtenerPromedio())
                })
                .Where(x => x.Promedio >= minimo && x.Promedio <= maximo)
                .Select(x => x.Estudiante)
                .ToList();
        }

        
        // Genera reporte completo con todas las estadísticas.
        
        public string GenerarReporteEstadisticas()
        {
            var reporte = new System.Text.StringBuilder();

            reporte.AppendLine("\n╔═══════════════════════════════════════════════════════════╗");
            reporte.AppendLine("║          REPORTE GENERAL DE ESTADÍSTICAS                  ║");
            reporte.AppendLine("╚═══════════════════════════════════════════════════════════╝\n");

            // Promedio general
            decimal promedioGeneral = ObtenerPromedioGeneral();
            reporte.AppendLine($" Promedio General del Sistema: {promedioGeneral:F2}\n");

            // Top 10 estudiantes
            reporte.AppendLine(" TOP 10 ESTUDIANTES\n");
            var top10 = ObtenerTop10Estudiantes();
            int pos = 1;
            foreach (var (estudiante, promedio) in top10)
            {
                reporte.AppendLine($"{pos,2}. {estudiante.Nombre} {estudiante.Apellido} " +
                    $"({estudiante.Carrera}) - Promedio: {promedio:F2}");
                pos++;
            }

            // Estudiantes en riesgo
            reporte.AppendLine("\n ESTUDIANTES EN RIESGO ACADÉMICO\n");
            var enRiesgo = ObtenerEstudiantesEnRiesgo();
            if (enRiesgo.Count == 0)
            {
                reporte.AppendLine(" No hay estudiantes en riesgo académico.");
            }
            else
            {
                foreach (var (estudiante, promedio, cursosRiesgo) in enRiesgo)
                {
                    reporte.AppendLine($"• {estudiante.Nombre} {estudiante.Apellido} " +
                        $"- Promedio: {promedio:F2} ({cursosRiesgo} curso(s) en riesgo)");
                }
            }

            // Cursos más populares
            reporte.AppendLine("\n CURSOS MÁS POPULARES\n");
            var populares = ObtenerCursosMasPopulares().Take(5);
            foreach (var (curso, cantidad, promedio) in populares)
            {
                reporte.AppendLine($"{curso.Nombre} ({curso.Codigo}) - " +
                    $"{cantidad} estudiantes - Promedio: {promedio:F2}");
            }

            // Estadísticas por carrera
            reporte.AppendLine("\n ESTADÍSTICAS POR CARRERA\n");
            var estadisticas = ObtenerEstadisticasPorCarrera();
            foreach (var kvp in estadisticas.OrderByDescending(x => x.Value.CantidadEstudiantes))
            {
                var stats = kvp.Value;
                reporte.AppendLine($" {stats.NombreCarrera}");
                reporte.AppendLine($"  Estudiantes: {stats.CantidadEstudiantes}");
                reporte.AppendLine($"  Promedio: {stats.PromedioGeneral:F2}");
                reporte.AppendLine($"  Rango: {stats.PeorPromedio:F2} - {stats.MejorPromedio:F2}");
                reporte.AppendLine($"  Aprobados: {stats.EstudiantesAprobados}");
                reporte.AppendLine();
            }

            reporte.AppendLine("═══════════════════════════════════════════════════════════");
            reporte.AppendLine($"Reporte generado: {DateTime.Now:dd/MM/yyyy HH:mm:ss}");
            reporte.AppendLine("═══════════════════════════════════════════════════════════");

            return reporte.ToString();
        }

        
        // Demuestra todas las consultas LINQ implementadas.
        
        public void DemostrarConsultasLINQ()
        {
            Console.WriteLine("\n");
            Console.WriteLine("█████████████████████████████████████████████████████████████");
            Console.WriteLine("█                                                           █");
            Console.WriteLine("█        DEMOSTRACIÓN DE CONSULTAS LINQ Y LAMBDA            █");
            Console.WriteLine("█                                                           █");
            Console.WriteLine("█████████████████████████████████████████████████████████████");

            // 1. Top 10 Estudiantes
            Console.WriteLine("\n\n=== TOP 10 ESTUDIANTES ===");
            var top10 = ObtenerTop10Estudiantes();
            int pos = 1;
            foreach (var (estudiante, promedio) in top10)
            {
                Console.WriteLine($"{pos,2}. {estudiante.Nombre} {estudiante.Apellido,-20} " +
                    $"Promedio: {promedio:F2}");
                pos++;
            }

            // 2. Estudiantes en riesgo
            Console.WriteLine("\n\n=== ESTUDIANTES EN RIESGO (Promedio < 7.0) ===");
            var enRiesgo = ObtenerEstudiantesEnRiesgo();
            if (enRiesgo.Count == 0)
            {
                Console.WriteLine("✓ No hay estudiantes en riesgo.");
            }
            else
            {
                foreach (var (estudiante, promedio, cursosRiesgo) in enRiesgo)
                {
                    Console.WriteLine($" {estudiante.Nombre} {estudiante.Apellido,-20} " +
                        $"Promedio: {promedio:F2} ({cursosRiesgo} cursos en riesgo)");
                }
            }

            // 3. Cursos más populares
            Console.WriteLine("\n\n=== CURSOS MÁS POPULARES ===");
            var populares = ObtenerCursosMasPopulares().Take(5);
            foreach (var (curso, cantidad, promedio) in populares)
            {
                Console.WriteLine($"{curso.Nombre,-30} " +
                    $"Estudiantes: {cantidad,3} | Promedio: {promedio:F2}");
            }

            // 4. Promedio general
            Console.WriteLine("\n\n=== PROMEDIO GENERAL DEL SISTEMA ===");
            decimal promedioGral = ObtenerPromedioGeneral();
            Console.WriteLine($"Promedio: {promedioGral:F2}");

            // 5. Estadísticas por carrera
            Console.WriteLine("\n\n=== ESTADÍSTICAS POR CARRERA ===");
            var estadisticas = ObtenerEstadisticasPorCarrera();
            foreach (var kvp in estadisticas)
            {
                var stats = kvp.Value;
                Console.WriteLine($"\n{stats.NombreCarrera}");
                Console.WriteLine($"   Estudiantes: {stats.CantidadEstudiantes}");
                Console.WriteLine($"   Promedio: {stats.PromedioGeneral:F2}");
                Console.WriteLine($"   Mejor: {stats.MejorPromedio:F2} | Peor: {stats.PeorPromedio:F2}");
            }

            // 6. Búsqueda con predicados (expresiones lambda)
            Console.WriteLine("\n\n=== BÚSQUEDAS CON EXPRESIONES LAMBDA ===");

            Console.WriteLine("\n1. Estudiantes mayores de 22 años:");
            var mayores22 = BuscarEstudiantes(e => e.Edad > 22);
            foreach (var est in mayores22.Take(5))
            {
                Console.WriteLine($"   {est.Nombre} {est.Apellido} ({est.Edad} años)");
            }

            Console.WriteLine("\n2. Estudiantes de Ingeniería:");
            var ingenieros = BuscarEstudiantes(e =>
                e.Carrera.Contains("Ingeniería", StringComparison.OrdinalIgnoreCase));
            Console.WriteLine($"   Total: {ingenieros.Count} estudiantes");

            Console.WriteLine("\n3. Estudiantes con nombre que empieza con 'A':");
            var nombreA = BuscarEstudiantes(e => e.Nombre.StartsWith("A"));
            foreach (var est in nombreA.Take(5))
            {
                Console.WriteLine($"  {est.Nombre} {est.Apellido}");
            }

            // 7. Expresiones lambda adicionales
            Console.WriteLine("\n\n=== EXPRESIONES LAMBDA ADICIONALES ===");

            Console.WriteLine("\n1. Estudiantes con promedio entre 8.0 y 9.0:");
            var rangoPromedio = ObtenerEstudiantesPorRangoPromedio(8.0m, 9.0m);
            Console.WriteLine($"   Total: {rangoPromedio.Count} estudiantes");

            Console.WriteLine("\n2. Cursos donde todos aprobaron:");
            var todosAprobados = ObtenerCursosConTodosAprobados();
            foreach (var curso in todosAprobados.Take(5))
            {
                Console.WriteLine($" {curso.Nombre}");
            }

            Console.WriteLine("\n\n" + new string('═', 65));
            Console.WriteLine("  FIN DE LA DEMOSTRACIÓN DE LINQ");
            Console.WriteLine(new string('═', 65) + "\n");
        }


        public class EstadisticasCarrera
        {
            public string NombreCarrera { get; set; }
            public int CantidadEstudiantes { get; set; }
            public decimal PromedioGeneral { get; set; }
            public decimal MejorPromedio { get; set; }
            public decimal PeorPromedio { get; set; }
            public int TotalMatriculas { get; set; }
            public int EstudiantesAprobados { get; set; }

            public override string ToString()
            {
                return $"{NombreCarrera}: {CantidadEstudiantes} estudiantes, " +
                       $"Promedio: {PromedioGeneral:F2}";
            }
        }
    }
}
