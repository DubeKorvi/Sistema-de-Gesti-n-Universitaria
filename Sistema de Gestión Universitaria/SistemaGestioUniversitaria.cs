using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_de_Gestión_Universitaria
{
    public class SistemaGestionUniversitaria
    {
        // Repositorios
        private Repositorio<Estudiante> repositorioEstudiantes;
        private Repositorio<Profesor> repositorioProfesores;
        private Repositorio<Curso> repositorioCursos;
        private GestorMatriculas gestorMatriculas;

        // Constructor
        public SistemaGestionUniversitaria()
        {
            repositorioEstudiantes = new Repositorio<Estudiante>();
            repositorioProfesores = new Repositorio<Profesor>();
            repositorioCursos = new Repositorio<Curso>();
            gestorMatriculas = new GestorMatriculas(repositorioEstudiantes, repositorioCursos);
        }

     

        
        // Genera datos de prueba para el sistema.
        // Crea 15+ estudiantes, 5+ profesores, 10+ cursos, 30+ matrículas.
        
        public void GenerarDatosPrueba()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n╔═══════════════════════════════════════════════════════════╗");
            Console.WriteLine("║        GENERANDO DATOS DE PRUEBA                          ║");
            Console.WriteLine("╚═══════════════════════════════════════════════════════════╝\n");
            Console.ResetColor();

            try
            {
                // ═══ CREAR PROFESORES (5+) ═══
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Creando profesores...\n");
                Console.ResetColor();

                var prof1 = new Profesor("PROF-001", "Juan", "Pérez", new DateTime(1975, 3, 15),
                    "Ciencias de la Computación", TipoContrato.TiempoCompleto, 5000m);
                var prof2 = new Profesor("PROF-002", "María", "González", new DateTime(1980, 7, 22),
                    "Matemáticas", TipoContrato.TiempoCompleto, 4800m);
                var prof3 = new Profesor("PROF-003", "Carlos", "Ramírez", new DateTime(1978, 11, 10),
                    "Física", TipoContrato.MedioTiempo, 3000m);
                var prof4 = new Profesor("PROF-004", "Ana", "Martínez", new DateTime(1982, 5, 8),
                    "Ingeniería", TipoContrato.TiempoCompleto, 5200m);
                var prof5 = new Profesor("PROF-005", "Luis", "Torres", new DateTime(1976, 9, 30),
                    "Química", TipoContrato.PorHoras, 2500m);

                repositorioProfesores.Agregar(prof1);
                repositorioProfesores.Agregar(prof2);
                repositorioProfesores.Agregar(prof3);
                repositorioProfesores.Agregar(prof4);
                repositorioProfesores.Agregar(prof5);

                // ═══ CREAR CURSOS (10+) ═══
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n📚 Creando cursos...\n");
                Console.ResetColor();

                var curso1 = new Curso("MAT-101", "Cálculo Diferencial", 4, prof2);
                var curso2 = new Curso("MAT-102", "Cálculo Integral", 4, prof2);
                var curso3 = new Curso("PRG-101", "Programación I", 5, prof1);
                var curso4 = new Curso("PRG-102", "Programación II", 5, prof1);
                var curso5 = new Curso("FIS-101", "Física I", 4, prof3);
                var curso6 = new Curso("FIS-102", "Física II", 4, prof3);
                var curso7 = new Curso("ING-101", "Ingeniería de Software", 4, prof4);
                var curso8 = new Curso("QUI-101", "Química General", 3, prof5);
                var curso9 = new Curso("MAT-201", "Álgebra Lineal", 4, prof2);
                var curso10 = new Curso("PRG-201", "Estructuras de Datos", 5, prof1);
                var curso11 = new Curso("ING-201", "Base de Datos", 4, prof4);

                repositorioCursos.Agregar(curso1);
                repositorioCursos.Agregar(curso2);
                repositorioCursos.Agregar(curso3);
                repositorioCursos.Agregar(curso4);
                repositorioCursos.Agregar(curso5);
                repositorioCursos.Agregar(curso6);
                repositorioCursos.Agregar(curso7);
                repositorioCursos.Agregar(curso8);
                repositorioCursos.Agregar(curso9);
                repositorioCursos.Agregar(curso10);
                repositorioCursos.Agregar(curso11);

                // ═══ CREAR ESTUDIANTES (15+) ═══
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n Creando estudiantes...\n");
                Console.ResetColor();

                var est1 = new Estudiante("EST-001", "Pedro", "López", new DateTime(2002, 1, 15),
                    "Ingeniería en Sistemas", "2024-001");
                var est2 = new Estudiante("EST-002", "Laura", "Fernández", new DateTime(2001, 5, 22),
                    "Ingeniería en Sistemas", "2024-002");
                var est3 = new Estudiante("EST-003", "Miguel", "Santos", new DateTime(2003, 3, 10),
                    "Ingeniería Civil", "2024-003");
                var est4 = new Estudiante("EST-004", "Sofía", "Díaz", new DateTime(2002, 8, 5),
                    "Medicina", "2024-004");
                var est5 = new Estudiante("EST-005", "Diego", "Morales", new DateTime(2001, 12, 18),
                    "Ingeniería en Sistemas", "2024-005");
                var est6 = new Estudiante("EST-006", "Valentina", "Cruz", new DateTime(2002, 6, 25),
                    "Arquitectura", "2024-006");
                var est7 = new Estudiante("EST-007", "Andrés", "Ruiz", new DateTime(2003, 2, 14),
                    "Ingeniería Eléctrica", "2024-007");
                var est8 = new Estudiante("EST-008", "Isabella", "Jiménez", new DateTime(2002, 9, 30),
                    "Ingeniería en Sistemas", "2024-008");
                var est9 = new Estudiante("EST-009", "Santiago", "Vargas", new DateTime(2001, 4, 7),
                    "Física", "2024-009");
                var est10 = new Estudiante("EST-010", "Camila", "Rojas", new DateTime(2003, 7, 20),
                    "Química", "2024-010");
                var est11 = new Estudiante("EST-011", "Mateo", "Herrera", new DateTime(2002, 11, 12),
                    "Ingeniería Civil", "2024-011");
                var est12 = new Estudiante("EST-012", "Lucía", "Mendoza", new DateTime(2001, 10, 3),
                    "Medicina", "2024-012");
                var est13 = new Estudiante("EST-013", "Sebastián", "Castro", new DateTime(2003, 1, 28),
                    "Ingeniería en Sistemas", "2024-013");
                var est14 = new Estudiante("EST-014", "Emma", "Ortiz", new DateTime(2002, 5, 16),
                    "Arquitectura", "2024-014");
                var est15 = new Estudiante("EST-015", "Daniel", "Silva", new DateTime(2001, 8, 9),
                    "Ingeniería Eléctrica", "2024-015");

                repositorioEstudiantes.Agregar(est1);
                repositorioEstudiantes.Agregar(est2);
                repositorioEstudiantes.Agregar(est3);
                repositorioEstudiantes.Agregar(est4);
                repositorioEstudiantes.Agregar(est5);
                repositorioEstudiantes.Agregar(est6);
                repositorioEstudiantes.Agregar(est7);
                repositorioEstudiantes.Agregar(est8);
                repositorioEstudiantes.Agregar(est9);
                repositorioEstudiantes.Agregar(est10);
                repositorioEstudiantes.Agregar(est11);
                repositorioEstudiantes.Agregar(est12);
                repositorioEstudiantes.Agregar(est13);
                repositorioEstudiantes.Agregar(est14);
                repositorioEstudiantes.Agregar(est15);

                // ═══ CREAR MATRÍCULAS (30+) ═══
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n Creando matrículas...\n");
                Console.ResetColor();

                // Estudiante 1 - 3 cursos
                gestorMatriculas.MatricularEstudiante(est1, curso3);
                gestorMatriculas.MatricularEstudiante(est1, curso1);
                gestorMatriculas.MatricularEstudiante(est1, curso5);

                // Estudiante 2 - 3 cursos
                gestorMatriculas.MatricularEstudiante(est2, curso3);
                gestorMatriculas.MatricularEstudiante(est2, curso4);
                gestorMatriculas.MatricularEstudiante(est2, curso9);

                // Estudiante 3 - 2 cursos
                gestorMatriculas.MatricularEstudiante(est3, curso1);
                gestorMatriculas.MatricularEstudiante(est3, curso5);

                // Estudiante 4 - 3 cursos
                gestorMatriculas.MatricularEstudiante(est4, curso8);
                gestorMatriculas.MatricularEstudiante(est4, curso1);
                gestorMatriculas.MatricularEstudiante(est4, curso5);

                // Estudiante 5 - 4 cursos
                gestorMatriculas.MatricularEstudiante(est5, curso3);
                gestorMatriculas.MatricularEstudiante(est5, curso4);
                gestorMatriculas.MatricularEstudiante(est5, curso10);
                gestorMatriculas.MatricularEstudiante(est5, curso7);

                // Estudiante 6 - 2 cursos
                gestorMatriculas.MatricularEstudiante(est6, curso1);
                gestorMatriculas.MatricularEstudiante(est6, curso5);

                // Estudiante 7 - 3 cursos
                gestorMatriculas.MatricularEstudiante(est7, curso5);
                gestorMatriculas.MatricularEstudiante(est7, curso6);
                gestorMatriculas.MatricularEstudiante(est7, curso1);

                // Estudiante 8 - 3 cursos
                gestorMatriculas.MatricularEstudiante(est8, curso3);
                gestorMatriculas.MatricularEstudiante(est8, curso1);
                gestorMatriculas.MatricularEstudiante(est8, curso11);

                // Estudiante 9 - 2 cursos
                gestorMatriculas.MatricularEstudiante(est9, curso5);
                gestorMatriculas.MatricularEstudiante(est9, curso6);

                // Estudiante 10 - 2 cursos
                gestorMatriculas.MatricularEstudiante(est10, curso8);
                gestorMatriculas.MatricularEstudiante(est10, curso1);

                // Resto de estudiantes
                gestorMatriculas.MatricularEstudiante(est11, curso1);
                gestorMatriculas.MatricularEstudiante(est11, curso5);
                gestorMatriculas.MatricularEstudiante(est12, curso8);
                gestorMatriculas.MatricularEstudiante(est13, curso3);
                gestorMatriculas.MatricularEstudiante(est13, curso10);
                gestorMatriculas.MatricularEstudiante(est14, curso1);
                gestorMatriculas.MatricularEstudiante(est15, curso5);

                // ═══ REGISTRAR CALIFICACIONES (3-4 por matrícula) ═══
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n Registrando calificaciones...\n");
                Console.ResetColor();

                var random = new Random(42); // Seed fijo para reproducibilidad

                foreach (var matricula in gestorMatriculas.ObtenerTodasLasMatriculas())
                {
                    int numCalificaciones = random.Next(3, 5); // 3 o 4 calificaciones

                    for (int i = 0; i < numCalificaciones; i++)
                    {
                        // Generar calificación entre 6.0 y 10.0
                        decimal calificacion = (decimal)(random.NextDouble() * 4.0 + 6.0);
                        calificacion = Math.Round(calificacion, 1);

                        gestorMatriculas.AgregarCalificacion(
                          matricula.Estudiante.Identificacion,    
                          matricula.Curso.Codigo,
                          calificacion);
                    }
                }

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n DATOS DE PRUEBA GENERADOS EXITOSAMENTE ");
                Console.WriteLine($"\n   {repositorioProfesores.Cantidad} profesores");
                Console.WriteLine($"   {repositorioCursos.Cantidad} cursos");
                Console.WriteLine($"   {repositorioEstudiantes.Cantidad} estudiantes");
                Console.WriteLine($"   {gestorMatriculas.CantidadMatriculas} matrículas");
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n Error al generar datos: {ex.Message}");
                Console.ResetColor();
            }
        }

       
        // Demuestra todas las funcionalidades del sistema automáticamente.
        
        public void DemostrarFuncionalidades()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("\n");
            Console.WriteLine("█████████████████████████████████████████████████████████████");
            Console.WriteLine("█                                                           █");
            Console.WriteLine("█     DEMOSTRACIÓN AUTOMÁTICA DE FUNCIONALIDADES            █");
            Console.WriteLine("█                                                           █");
            Console.WriteLine("█████████████████████████████████████████████████████████████");
            Console.ResetColor();

            PausarYContinuar();

            // 1. Consultas LINQ
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n\n╔═══════════════════════════════════════════════════════════╗");
            Console.WriteLine("║  1. DEMOSTRACIÓN DE CONSULTAS LINQ                        ║");
            Console.WriteLine("╚═══════════════════════════════════════════════════════════╝");
            Console.ResetColor();
            gestorMatriculas.DemostrarConsultasLINQ();
            PausarYContinuar();

            // 2. Reflection
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n\n╔═══════════════════════════════════════════════════════════╗");
            Console.WriteLine("║  2. DEMOSTRACIÓN DE REFLECTION                            ║");
            Console.WriteLine("╚═══════════════════════════════════════════════════════════╝");
            Console.ResetColor();
           // AnalizadorReflection.DemostrarReflection();
            PausarYContinuar();

            // 3. Atributos Personalizados
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n\n╔═══════════════════════════════════════════════════════════╗");
            Console.WriteLine("║  3. DEMOSTRACIÓN DE ATRIBUTOS PERSONALIZADOS              ║");
            Console.WriteLine("╚═══════════════════════════════════════════════════════════╝");
            Console.ResetColor();
            Validador.DemostrarValidacion();
            PausarYContinuar();

            // 4. Boxing/Unboxing y Conversiones
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n\n╔═══════════════════════════════════════════════════════════╗");
            Console.WriteLine("║  4. DEMOSTRACIÓN DE BOXING/UNBOXING                       ║");
            Console.WriteLine("╚═══════════════════════════════════════════════════════════╝");
            Console.ResetColor();
            ManejadorTipos.DemostrarBoxingUnboxing();
            PausarYContinuar();

            ManejadorTipos.DemostrarConversiones();
            PausarYContinuar();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n\n╔═══════════════════════════════════════════════════════════╗");
            Console.WriteLine("║     ✓ DEMOSTRACIÓN COMPLETADA EXITOSAMENTE ✓              ║");
            Console.WriteLine("╚═══════════════════════════════════════════════════════════╝\n");
            Console.ResetColor();
        }


        private void PausarYContinuar()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("\n[Presiona ENTER para continuar...]");
            Console.ResetColor();
            Console.ReadLine();
        }

    }
}