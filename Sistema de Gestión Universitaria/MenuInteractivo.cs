using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_de_Gestión_Universitaria
{
    public class MenuInteractivo
    {
        private Repositorio<Estudiante> repositorioEstudiantes;
        private Repositorio<Profesor> repositorioProfesores;
        private Repositorio<Curso> repositorioCursos;
        private GestorMatriculas gestorMatriculas;

        public MenuInteractivo()
        {
            repositorioEstudiantes = new Repositorio<Estudiante>();
            repositorioProfesores = new Repositorio<Profesor>();
            repositorioCursos = new Repositorio<Curso>();
            gestorMatriculas = new GestorMatriculas(repositorioEstudiantes, repositorioCursos);
        }

        public void Ejecutar()
        {
            bool salir = false;

            MostrarBienvenida();

            while (!salir)
            {
                try
                {
                    MostrarMenuPrincipal();
                    string opcion = LeerEntrada("Seleccione una opción");

                    switch (opcion)
                    {
                        case "1":
                            MenuGestionEstudiantes();
                            break;
                        case "2":
                            MenuGestionProfesores();
                            break;
                        case "3":
                            MenuGestionCursos();
                            break;
                        case "4":
                            MenuMatricularEstudiante();
                            break;
                        case "5":
                            MenuRegistrarCalificaciones();
                            break;
                        case "6":
                            MenuVerReportes();
                            break;
                        case "7":
                            MenuAnalisisReflection();
                            break;
                        case "8":
                            salir = true;
                            MostrarDespedida();
                            break;
                        default:
                            MostrarError("Opción inválida. Por favor, seleccione un número del 1 al 8.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    MostrarError($"Error: {ex.Message}");
                    PausarYContinuar();
                }
            }
        }

        #region MENÚS

        private void MostrarMenuPrincipal()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n╔══════════════════════════════════════════════════════════╗");
            Console.WriteLine("║          SISTEMA DE GESTIÓN UNIVERSITARIA               ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════════╝");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\n┌─────────────────────────────────────────────────────────┐");
            Console.WriteLine("│                     MENÚ PRINCIPAL                      │");
            Console.WriteLine("└─────────────────────────────────────────────────────────┘\n");
            Console.ResetColor();

            Console.WriteLine("  1. 👨‍🎓 Gestionar Estudiantes");
            Console.WriteLine("  2. 👨‍🏫 Gestionar Profesores");
            Console.WriteLine("  3. 📚 Gestionar Cursos");
            Console.WriteLine("  4. 📝 Matricular Estudiante en Curso");
            Console.WriteLine("  5. 📊 Registrar Calificaciones");
            Console.WriteLine("  6. 📋 Ver Reportes");
            Console.WriteLine("  7. 🔍 Análisis con Reflection");
            Console.WriteLine("  8. 🚪 Salir");

            Console.WriteLine("\n" + new string('─', 58));
        }

        private void MenuGestionEstudiantes()
        {
            bool volver = false;

            while (!volver)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n╔══════════════════════════════════════════════════════════╗");
                Console.WriteLine("║             GESTIÓN DE ESTUDIANTES                      ║");
                Console.WriteLine("╚══════════════════════════════════════════════════════════╝\n");
                Console.ResetColor();

                Console.WriteLine("  1. ➕ Agregar Estudiante");
                Console.WriteLine("  2. 📋 Listar Estudiantes");
                Console.WriteLine("  3. 🔍 Buscar Estudiante");
                Console.WriteLine("  4. ✏️  Modificar Estudiante");
                Console.WriteLine("  5. ❌ Eliminar Estudiante");
                Console.WriteLine("  6. ⬅️  Volver al Menú Principal");
                Console.WriteLine("\n" + new string('─', 58));

                string opcion = LeerEntrada("Seleccione una opción");

                switch (opcion)
                {
                    case "1":
                        AgregarEstudiante();
                        break;
                    case "2":
                        ListarEstudiantes();
                        break;
                    case "3":
                        BuscarEstudiante();
                        break;
                    case "4":
                        ModificarEstudiante();
                        break;
                    case "5":
                        EliminarEstudiante();
                        break;
                    case "6":
                        volver = true;
                        break;
                    default:
                        MostrarError("Opción inválida.");
                        PausarYContinuar();
                        break;
                }
            }
        }

        private void MenuGestionProfesores()
        {
            bool volver = false;

            while (!volver)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n╔══════════════════════════════════════════════════════════╗");
                Console.WriteLine("║             GESTIÓN DE PROFESORES                       ║");
                Console.WriteLine("╚══════════════════════════════════════════════════════════╝\n");
                Console.ResetColor();

                Console.WriteLine("  1. ➕ Agregar Profesor");
                Console.WriteLine("  2. 📋 Listar Profesores");
                Console.WriteLine("  3. 🔍 Buscar Profesor");
                Console.WriteLine("  4. ✏️  Modificar Profesor");
                Console.WriteLine("  5. ❌ Eliminar Profesor");
                Console.WriteLine("  6. ⬅️  Volver al Menú Principal");
                Console.WriteLine("\n" + new string('─', 58));

                string opcion = LeerEntrada("Seleccione una opción");

                switch (opcion)
                {
                    case "1":
                        AgregarProfesor();
                        break;
                    case "2":
                        ListarProfesores();
                        break;
                    case "3":
                        BuscarProfesor();
                        break;
                    case "4":
                        ModificarProfesor();
                        break;
                    case "5":
                        EliminarProfesor();
                        break;
                    case "6":
                        volver = true;
                        break;
                    default:
                        MostrarError("Opción inválida.");
                        PausarYContinuar();
                        break;
                }
            }
        }

        private void MenuGestionCursos()
        {
            bool volver = false;

            while (!volver)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("\n╔══════════════════════════════════════════════════════════╗");
                Console.WriteLine("║             GESTIÓN DE CURSOS                           ║");
                Console.WriteLine("╚══════════════════════════════════════════════════════════╝\n");
                Console.ResetColor();

                Console.WriteLine("  1. ➕ Agregar Curso");
                Console.WriteLine("  2. 📋 Listar Cursos");
                Console.WriteLine("  3. 👨‍🏫 Asignar Profesor a Curso");
                Console.WriteLine("  4. ⬅️  Volver al Menú Principal");
                Console.WriteLine("\n" + new string('─', 58));

                string opcion = LeerEntrada("Seleccione una opción");

                switch (opcion)
                {
                    case "1":
                        AgregarCurso();
                        break;
                    case "2":
                        ListarCursos();
                        break;
                    case "3":
                        AsignarProfesorACurso();
                        break;
                    case "4":
                        volver = true;
                        break;
                    default:
                        MostrarError("Opción inválida.");
                        PausarYContinuar();
                        break;
                }
            }
        }

        private void MenuMatricularEstudiante()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n╔══════════════════════════════════════════════════════════╗");
            Console.WriteLine("║          MATRICULAR ESTUDIANTE EN CURSO                 ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════════╝\n");
            Console.ResetColor();

            if (repositorioEstudiantes.Cantidad == 0)
            {
                MostrarError("No hay estudiantes registrados.");
                PausarYContinuar();
                return;
            }

            if (repositorioCursos.Cantidad == 0)
            {
                MostrarError("No hay cursos registrados.");
                PausarYContinuar();
                return;
            }

            // Mostrar estudiantes
            Console.WriteLine("ESTUDIANTES DISPONIBLES:\n");
            var estudiantes = repositorioEstudiantes.ObtenerTodos();
            for (int i = 0; i < estudiantes.Count; i++)
            {
                Console.WriteLine($"  {i + 1}. {estudiantes[i].Nombre} {estudiantes[i].Apellido} - ID: {estudiantes[i].Identificacion}");
            }

            string idEstudiante = LeerEntrada("\nIngrese el ID del estudiante");
            var estudiante = repositorioEstudiantes.BuscarPorId(idEstudiante);

            if (estudiante == null)
            {
                MostrarError("Estudiante no encontrado.");
                PausarYContinuar();
                return;
            }

            // Mostrar cursos
            Console.WriteLine("\nCURSOS DISPONIBLES:\n");
            var cursos = repositorioCursos.ObtenerTodos();
            for (int i = 0; i < cursos.Count; i++)
            {
                Console.WriteLine($"  {i + 1}. {cursos[i].Nombre} - Código: {cursos[i].Codigo}");
            }

            string codigoCurso = LeerEntrada("\nIngrese el código del curso");
            var curso = repositorioCursos.BuscarPorId(codigoCurso);

            if (curso == null)
            {
                MostrarError("Curso no encontrado.");
                PausarYContinuar();
                return;
            }

            try
            {
                gestorMatriculas.MatricularEstudiante(estudiante, curso);
                MostrarExito($"Estudiante {estudiante.Nombre} matriculado exitosamente en {curso.Nombre}");
            }
            catch (Exception ex)
            {
                MostrarError($"Error al matricular: {ex.Message}");
            }

            PausarYContinuar();
        }

        private void MenuRegistrarCalificaciones()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n╔══════════════════════════════════════════════════════════╗");
            Console.WriteLine("║          REGISTRAR CALIFICACIONES                       ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════════╝\n");
            Console.ResetColor();

            if (gestorMatriculas.CantidadMatriculas == 0)
            {
                MostrarError("No hay matrículas registradas.");
                PausarYContinuar();
                return;
            }

            string idEstudiante = LeerEntrada("Ingrese el ID del estudiante");
            var estudiante = repositorioEstudiantes.BuscarPorId(idEstudiante);

            if (estudiante == null)
            {
                MostrarError("Estudiante no encontrado.");
                PausarYContinuar();
                return;
            }

            // Mostrar matrículas del estudiante
            var matriculas = gestorMatriculas.ObtenerMatriculasPorEstudiante(idEstudiante);

            if (matriculas.Count == 0)
            {
                MostrarError("El estudiante no tiene matrículas.");
                PausarYContinuar();
                return;
            }

            Console.WriteLine($"\nMATRÍCULAS DE {estudiante.Nombre} {estudiante.Apellido}:\n");
            for (int i = 0; i < matriculas.Count; i++)
            {
                Console.WriteLine($"  {i + 1}. {matriculas[i].Curso.Nombre} - Código: {matriculas[i].Curso.Codigo}");
            }

            string codigoCurso = LeerEntrada("\nIngrese el código del curso");

            string calificacionStr = LeerEntrada("Ingrese la calificación (0-10)");

            var (exito, calificacion) = ManejadorTipos.ParsearCalificacion(calificacionStr);

            if (!exito)
            {
                MostrarError("Calificación inválida. Debe ser un número entre 0 y 10.");
                PausarYContinuar();
                return;
            }

            try
            {
                gestorMatriculas.AgregarCalificacion(idEstudiante, codigoCurso, calificacion);
                MostrarExito($"Calificación {calificacion:F1} registrada exitosamente");
            }
            catch (Exception ex)
            {
                MostrarError($"Error: {ex.Message}");
            }

            PausarYContinuar();
        }

        private void MenuVerReportes()
        {
            bool volver = false;

            while (!volver)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("\n╔══════════════════════════════════════════════════════════╗");
                Console.WriteLine("║                  REPORTES                               ║");
                Console.WriteLine("╚══════════════════════════════════════════════════════════╝\n");
                Console.ResetColor();

                Console.WriteLine("  1. 📊 Reporte de Estudiante");
                Console.WriteLine("  2. 📈 Estadísticas Generales");
                Console.WriteLine("  3. 🏆 Top 10 Estudiantes");
                Console.WriteLine("  4. ⚠️  Estudiantes en Riesgo");
                Console.WriteLine("  5. 📚 Cursos Más Populares");
                Console.WriteLine("  6. ⬅️  Volver al Menú Principal");
                Console.WriteLine("\n" + new string('─', 58));

                string opcion = LeerEntrada("Seleccione una opción");

                switch (opcion)
                {
                    case "1":
                        GenerarReporteEstudiante();
                        break;
                    case "2":
                        MostrarEstadisticasGenerales();
                        break;
                    case "3":
                        MostrarTop10Estudiantes();
                        break;
                    case "4":
                        MostrarEstudiantesEnRiesgo();
                        break;
                    case "5":
                        MostrarCursosPopulares();
                        break;
                    case "6":
                        volver = true;
                        break;
                    default:
                        MostrarError("Opción inválida.");
                        PausarYContinuar();
                        break;
                }
            }
        }

        private void MenuAnalisisReflection()
        {
            Console.Clear();
            AnalizadorReflection.DemostrarReflection();
            PausarYContinuar();
        }

        #endregion

        #region OPERACIONES ESTUDIANTES

        private void AgregarEstudiante()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n╔══════════════════════════════════════════════════════════╗");
            Console.WriteLine("║             AGREGAR NUEVO ESTUDIANTE                    ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════════╝\n");
            Console.ResetColor();

            try
            {
                string id = LeerEntrada("ID del estudiante");
                string nombre = LeerEntrada("Nombre");
                string apellido = LeerEntrada("Apellido");

                Console.Write("Fecha de nacimiento (DD/MM/YYYY): ");
                DateTime fechaNac = DateTime.ParseExact(Console.ReadLine(), "dd/MM/yyyy", null);

                string carrera = LeerEntrada("Carrera");
                string matricula = LeerEntrada("Número de matrícula (formato: YYYY-NNN)");

                var estudiante = new Estudiante(id, nombre, apellido, fechaNac, carrera, matricula);
                repositorioEstudiantes.Agregar(estudiante);

                MostrarExito("Estudiante agregado exitosamente");
            }
            catch (FormatException)
            {
                MostrarError("Formato de fecha inválido. Use DD/MM/YYYY");
            }
            catch (Exception ex)
            {
                MostrarError($"Error: {ex.Message}");
            }

            PausarYContinuar();
        }

        private void ListarEstudiantes()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n╔══════════════════════════════════════════════════════════╗");
            Console.WriteLine("║             LISTA DE ESTUDIANTES                        ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════════╝\n");
            Console.ResetColor();

            var estudiantes = repositorioEstudiantes.ObtenerTodos();

            if (estudiantes.Count == 0)
            {
                Console.WriteLine("  No hay estudiantes registrados.\n");
            }
            else
            {
                Console.WriteLine($"  Total: {estudiantes.Count} estudiante(s)\n");
                Console.WriteLine(new string('─', 58));

                foreach (var est in estudiantes)
                {
                    Console.WriteLine($"\n  ID: {est.Identificacion}");
                    Console.WriteLine($"  Nombre: {est.Nombre} {est.Apellido}");
                    Console.WriteLine($"  Edad: {est.Edad} años");
                    Console.WriteLine($"  Carrera: {est.Carrera}");
                    Console.WriteLine($"  Matrícula: {est.NumeroMatricula}");
                    Console.WriteLine(new string('─', 58));
                }
            }

            PausarYContinuar();
        }

        private void BuscarEstudiante()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n╔══════════════════════════════════════════════════════════╗");
            Console.WriteLine("║             BUSCAR ESTUDIANTE                           ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════════╝\n");
            Console.ResetColor();

            string id = LeerEntrada("Ingrese el ID del estudiante");
            var estudiante = repositorioEstudiantes.BuscarPorId(id);

            if (estudiante == null)
            {
                MostrarError("Estudiante no encontrado.");
            }
            else
            {
                Console.WriteLine("\n" + new string('─', 58));
                Console.WriteLine($"  ID: {estudiante.Identificacion}");
                Console.WriteLine($"  Nombre: {estudiante.Nombre} {estudiante.Apellido}");
                Console.WriteLine($"  Edad: {estudiante.Edad} años");
                Console.WriteLine($"  Carrera: {estudiante.Carrera}");
                Console.WriteLine($"  Matrícula: {estudiante.NumeroMatricula}");
                Console.WriteLine(new string('─', 58));
            }

            PausarYContinuar();
        }

        private void ModificarEstudiante()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n╔══════════════════════════════════════════════════════════╗");
            Console.WriteLine("║             MODIFICAR ESTUDIANTE                        ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════════╝\n");
            Console.ResetColor();

            string id = LeerEntrada("Ingrese el ID del estudiante a modificar");
            var estudiante = repositorioEstudiantes.BuscarPorId(id);

            if (estudiante == null)
            {
                MostrarError("Estudiante no encontrado.");
                PausarYContinuar();
                return;
            }

            Console.WriteLine($"\nEstudiante encontrado: {estudiante.Nombre} {estudiante.Apellido}\n");
            Console.WriteLine("Ingrese los nuevos datos (presione Enter para mantener el valor actual):\n");

            try
            {
                Console.Write($"Nombre [{estudiante.Nombre}]: ");
                string nombre = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(nombre))
                    estudiante.Nombre = nombre;

                Console.Write($"Apellido [{estudiante.Apellido}]: ");
                string apellido = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(apellido))
                    estudiante.Apellido = apellido;

                Console.Write($"Carrera [{estudiante.Carrera}]: ");
                string carrera = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(carrera))
                    estudiante.Carrera = carrera;

                repositorioEstudiantes.Actualizar(estudiante);
                MostrarExito("Estudiante modificado exitosamente");
            }
            catch (Exception ex)
            {
                MostrarError($"Error: {ex.Message}");
            }

            PausarYContinuar();
        }

        private void EliminarEstudiante()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n╔══════════════════════════════════════════════════════════╗");
            Console.WriteLine("║             ELIMINAR ESTUDIANTE                         ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════════╝\n");
            Console.ResetColor();

            string id = LeerEntrada("Ingrese el ID del estudiante a eliminar");
            var estudiante = repositorioEstudiantes.BuscarPorId(id);

            if (estudiante == null)
            {
                MostrarError("Estudiante no encontrado.");
                PausarYContinuar();
                return;
            }

            Console.WriteLine($"\n¿Está seguro que desea eliminar a {estudiante.Nombre} {estudiante.Apellido}?");
            Console.Write("Escriba 'SI' para confirmar: ");
            string confirmacion = Console.ReadLine();

            if (confirmacion?.ToUpper() == "SI")
            {
                repositorioEstudiantes.Eliminar(id);
                MostrarExito("Estudiante eliminado exitosamente");
            }
            else
            {
                Console.WriteLine("Operación cancelada.");
            }

            PausarYContinuar();
        }

        #endregion

        #region OPERACIONES PROFESORES

        private void AgregarProfesor()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n╔══════════════════════════════════════════════════════════╗");
            Console.WriteLine("║             AGREGAR NUEVO PROFESOR                      ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════════╝\n");
            Console.ResetColor();

            try
            {
                string id = LeerEntrada("ID del profesor");
                string nombre = LeerEntrada("Nombre");
                string apellido = LeerEntrada("Apellido");

                Console.Write("Fecha de nacimiento (DD/MM/YYYY): ");
                DateTime fechaNac = DateTime.ParseExact(Console.ReadLine(), "dd/MM/yyyy", null);

                string departamento = LeerEntrada("Departamento");

                Console.WriteLine("\nTipos de Contrato:");
                Console.WriteLine("  1. Tiempo Completo");
                Console.WriteLine("  2. Medio Tiempo");
                Console.WriteLine("  3. Por Horas");
                Console.WriteLine("  4. Contratado");
                string tipoStr = LeerEntrada("Seleccione tipo de contrato (1-4)");

                TipoContrato tipo = tipoStr switch
                {
                    "1" => TipoContrato.TiempoCompleto,
                    "2" => TipoContrato.MedioTiempo,
                    "3" => TipoContrato.PorHoras,
                    "4" => TipoContrato.Contratado,
                    _ => TipoContrato.TiempoCompleto
                };

                string salarioStr = LeerEntrada("Salario base");
                decimal salario = decimal.Parse(salarioStr);

                var profesor = new Profesor(id, nombre, apellido, fechaNac, departamento, tipo, salario);
                repositorioProfesores.Agregar(profesor);

                MostrarExito("Profesor agregado exitosamente");
            }
            catch (FormatException)
            {
                MostrarError("Formato inválido. Verifique los datos ingresados.");
            }
            catch (Exception ex)
            {
                MostrarError($"Error: {ex.Message}");
            }

            PausarYContinuar();
        }

        private void ListarProfesores()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n╔══════════════════════════════════════════════════════════╗");
            Console.WriteLine("║             LISTA DE PROFESORES                         ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════════╝\n");
            Console.ResetColor();

            var profesores = repositorioProfesores.ObtenerTodos();

            if (profesores.Count == 0)
            {
                Console.WriteLine("  No hay profesores registrados.\n");
            }
            else
            {
                Console.WriteLine($"  Total: {profesores.Count} profesor(es)\n");
                Console.WriteLine(new string('─', 58));

                foreach (var prof in profesores)
                {
                    Console.WriteLine($"\n  ID: {prof.Identificacion}");
                    Console.WriteLine($"  Nombre: {prof.Nombre} {prof.Apellido}");
                    Console.WriteLine($"  Departamento: {prof.Departamento}");
                    Console.WriteLine($"  Contrato: {prof.TipoContrato}");
                    Console.WriteLine($"  Salario: ${prof.SalarioBase:N2}");
                    Console.WriteLine(new string('─', 58));
                }
            }

            PausarYContinuar();
        }

        private void BuscarProfesor()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n╔══════════════════════════════════════════════════════════╗");
            Console.WriteLine("║             BUSCAR PROFESOR                             ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════════╝\n");
            Console.ResetColor();

            string id = LeerEntrada("Ingrese el ID del profesor");
            var profesor = repositorioProfesores.BuscarPorId(id);

            if (profesor == null)
            {
                MostrarError("Profesor no encontrado.");
            }
            else
            {
                Console.WriteLine("\n" + new string('─', 58));
                Console.WriteLine($"  ID: {profesor.Identificacion}");
                Console.WriteLine($"  Nombre: {profesor.Nombre} {profesor.Apellido}");
                Console.WriteLine($"  Departamento: {profesor.Departamento}");
                Console.WriteLine($"  Contrato: {profesor.TipoContrato}");
                Console.WriteLine($"  Salario: ${profesor.SalarioBase:N2}");
                Console.WriteLine(new string('─', 58));
            }

            PausarYContinuar();
        }

        private void ModificarProfesor()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n╔══════════════════════════════════════════════════════════╗");
            Console.WriteLine("║             MODIFICAR PROFESOR                          ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════════╝\n");
            Console.ResetColor();

            string id = LeerEntrada("Ingrese el ID del profesor a modificar");
            var profesor = repositorioProfesores.BuscarPorId(id);

            if (profesor == null)
            {
                MostrarError("Profesor no encontrado.");
                PausarYContinuar();
                return;
            }

            Console.WriteLine($"\nProfesor encontrado: {profesor.Nombre} {profesor.Apellido}\n");
            Console.WriteLine("Ingrese los nuevos datos (presione Enter para mantener el valor actual):\n");

            try
            {
                Console.Write($"Nombre [{profesor.Nombre}]: ");
                string nombre = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(nombre))
                    profesor.Nombre = nombre;

                Console.Write($"Apellido [{profesor.Apellido}]: ");
                string apellido = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(apellido))
                    profesor.Apellido = apellido;

                Console.Write($"Departamento [{profesor.Departamento}]: ");
                string departamento = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(departamento))
                    profesor.Departamento = departamento;

                Console.Write($"Salario [{profesor.SalarioBase}]: ");
                string salarioStr = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(salarioStr))
                    profesor.SalarioBase = decimal.Parse(salarioStr);

                repositorioProfesores.Actualizar(profesor);
                MostrarExito("Profesor modificado exitosamente");
            }
            catch (Exception ex)
            {
                MostrarError($"Error: {ex.Message}");
            }

            PausarYContinuar();
        }

        private void EliminarProfesor()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n╔══════════════════════════════════════════════════════════╗");
            Console.WriteLine("║             ELIMINAR PROFESOR                           ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════════╝\n");
            Console.ResetColor();

            string id = LeerEntrada("Ingrese el ID del profesor a eliminar");
            var profesor = repositorioProfesores.BuscarPorId(id);

            if (profesor == null)
            {
                MostrarError("Profesor no encontrado.");
                PausarYContinuar();
                return;
            }

            Console.WriteLine($"\n¿Está seguro que desea eliminar a {profesor.Nombre} {profesor.Apellido}?");
            Console.Write("Escriba 'SI' para confirmar: ");
            string confirmacion = Console.ReadLine();

            if (confirmacion?.ToUpper() == "SI")
            {
                repositorioProfesores.Eliminar(id);
                MostrarExito("Profesor eliminado exitosamente");
            }
            else
            {
                Console.WriteLine("Operación cancelada.");
            }

            PausarYContinuar();
        }

        #endregion

        #region OPERACIONES CURSOS

        private void AgregarCurso()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("\n╔══════════════════════════════════════════════════════════╗");
            Console.WriteLine("║             AGREGAR NUEVO CURSO                         ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════════╝\n");
            Console.ResetColor();

            try
            {
                string codigo = LeerEntrada("Código del curso (formato: ABC-123)");
                string nombre = LeerEntrada("Nombre del curso");
                string creditosStr = LeerEntrada("Créditos (1-6)");
                int creditos = int.Parse(creditosStr);

                var curso = new Curso(codigo, nombre, creditos);
                repositorioCursos.Agregar(curso);

                MostrarExito("Curso agregado exitosamente");
            }
            catch (FormatException)
            {
                MostrarError("Formato de número inválido.");
            }
            catch (Exception ex)
            {
                MostrarError($"Error: {ex.Message}");
            }

            PausarYContinuar();
        }

        private void ListarCursos()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("\n╔══════════════════════════════════════════════════════════╗");
            Console.WriteLine("║             LISTA DE CURSOS                             ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════════╝\n");
            Console.ResetColor();

            var cursos = repositorioCursos.ObtenerTodos();

            if (cursos.Count == 0)
            {
                Console.WriteLine("  No hay cursos registrados.\n");
            }
            else
            {
                Console.WriteLine($"  Total: {cursos.Count} curso(s)\n");
                Console.WriteLine(new string('─', 58));

                foreach (var curso in cursos)
                {
                    Console.WriteLine($"\n  Código: {curso.Codigo}");
                    Console.WriteLine($"  Nombre: {curso.Nombre}");
                    Console.WriteLine($"  Créditos: {curso.Creditos}");

                    if (curso.ProfesorAsignado != null)
                    {
                        Console.WriteLine($"  Profesor: {curso.ProfesorAsignado.Nombre} {curso.ProfesorAsignado.Apellido}");
                    }
                    else
                    {
                        Console.WriteLine("  Profesor: Sin asignar");
                    }
                    Console.WriteLine(new string('─', 58));
                }
            }

            PausarYContinuar();
        }

        private void AsignarProfesorACurso()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("\n╔══════════════════════════════════════════════════════════╗");
            Console.WriteLine("║          ASIGNAR PROFESOR A CURSO                       ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════════╝\n");
            Console.ResetColor();

            if (repositorioCursos.Cantidad == 0)
            {
                MostrarError("No hay cursos registrados.");
                PausarYContinuar();
                return;
            }

            if (repositorioProfesores.Cantidad == 0)
            {
                MostrarError("No hay profesores registrados.");
                PausarYContinuar();
                return;
            }

            // Mostrar cursos
            Console.WriteLine("CURSOS DISPONIBLES:\n");
            var cursos = repositorioCursos.ObtenerTodos();
            for (int i = 0; i < cursos.Count; i++)
            {
                string profActual = cursos[i].ProfesorAsignado != null
                    ? $"{cursos[i].ProfesorAsignado.Nombre} {cursos[i].ProfesorAsignado.Apellido}"
                    : "Sin asignar";
                Console.WriteLine($"  {i + 1}. {cursos[i].Nombre} ({cursos[i].Codigo}) - Profesor: {profActual}");
            }

            string codigoCurso = LeerEntrada("\nIngrese el código del curso");
            var curso = repositorioCursos.BuscarPorId(codigoCurso);

            if (curso == null)
            {
                MostrarError("Curso no encontrado.");
                PausarYContinuar();
                return;
            }

            // Mostrar profesores
            Console.WriteLine("\nPROFESORES DISPONIBLES:\n");
            var profesores = repositorioProfesores.ObtenerTodos();
            for (int i = 0; i < profesores.Count; i++)
            {
                Console.WriteLine($"  {i + 1}. {profesores[i].Nombre} {profesores[i].Apellido} - {profesores[i].Departamento}");
            }

            string idProfesor = LeerEntrada("\nIngrese el ID del profesor");
            var profesor = repositorioProfesores.BuscarPorId(idProfesor);

            if (profesor == null)
            {
                MostrarError("Profesor no encontrado.");
                PausarYContinuar();
                return;
            }

            curso.ProfesorAsignado = profesor;
            repositorioCursos.Actualizar(curso);

            MostrarExito($"Profesor {profesor.Nombre} {profesor.Apellido} asignado al curso {curso.Nombre}");
            PausarYContinuar();
        }

        #endregion

        #region REPORTES

        private void GenerarReporteEstudiante()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\n╔══════════════════════════════════════════════════════════╗");
            Console.WriteLine("║          REPORTE DE ESTUDIANTE                          ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════════╝\n");
            Console.ResetColor();

            string id = LeerEntrada("Ingrese el ID del estudiante");

            try
            {
                string reporte = gestorMatriculas.GenerarReporteEstudiante(id);
                Console.WriteLine(reporte);
            }
            catch (Exception ex)
            {
                MostrarError($"Error: {ex.Message}");
            }

            PausarYContinuar();
        }

        private void MostrarEstadisticasGenerales()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\n╔══════════════════════════════════════════════════════════╗");
            Console.WriteLine("║          ESTADÍSTICAS GENERALES                         ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════════╝\n");
            Console.ResetColor();

            string reporte = gestorMatriculas.GenerarReporteEstadisticas();
            Console.WriteLine(reporte);

            PausarYContinuar();
        }

        private void MostrarTop10Estudiantes()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\n╔══════════════════════════════════════════════════════════╗");
            Console.WriteLine("║          TOP 10 MEJORES ESTUDIANTES                     ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════════╝\n");
            Console.ResetColor();

            var top10 = gestorMatriculas.ObtenerTop10Estudiantes();

            if (top10.Count == 0)
            {
                Console.WriteLine("  No hay datos suficientes para generar el ranking.\n");
            }
            else
            {
                int pos = 1;
                foreach (var (estudiante, promedio) in top10)
                {
                    string medalla = pos switch
                    {
                        1 => "🥇",
                        2 => "🥈",
                        3 => "🥉",
                        _ => "  "
                    };

                    Console.WriteLine($"{medalla} {pos,2}. {estudiante.Nombre} {estudiante.Apellido,-20} - Promedio: {promedio:F2}");
                    Console.WriteLine($"      Carrera: {estudiante.Carrera}");
                    Console.WriteLine();
                    pos++;
                }
            }

            PausarYContinuar();
        }

        private void MostrarEstudiantesEnRiesgo()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n╔══════════════════════════════════════════════════════════╗");
            Console.WriteLine("║          ESTUDIANTES EN RIESGO ACADÉMICO                ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════════╝\n");
            Console.ResetColor();

            var enRiesgo = gestorMatriculas.ObtenerEstudiantesEnRiesgo();

            if (enRiesgo.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("  ✓ No hay estudiantes en riesgo académico.\n");
                Console.ResetColor();
            }
            else
            {
                Console.WriteLine($"  Total: {enRiesgo.Count} estudiante(s) en riesgo\n");
                Console.WriteLine(new string('─', 58));

                foreach (var (estudiante, promedio, cursosRiesgo) in enRiesgo)
                {
                    Console.WriteLine($"\n  ⚠️  {estudiante.Nombre} {estudiante.Apellido}");
                    Console.WriteLine($"      Promedio: {promedio:F2}");
                    Console.WriteLine($"      Cursos en riesgo: {cursosRiesgo}");
                    Console.WriteLine($"      Carrera: {estudiante.Carrera}");
                    Console.WriteLine(new string('─', 58));
                }
            }

            PausarYContinuar();
        }

        private void MostrarCursosPopulares()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\n╔══════════════════════════════════════════════════════════╗");
            Console.WriteLine("║          CURSOS MÁS POPULARES                           ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════════╝\n");
            Console.ResetColor();

            var populares = gestorMatriculas.ObtenerCursosMasPopulares();

            if (populares.Count == 0)
            {
                Console.WriteLine("  No hay datos de cursos.\n");
            }
            else
            {
                Console.WriteLine($"  Total: {populares.Count} curso(s)\n");
                Console.WriteLine(new string('─', 58));

                int pos = 1;
                foreach (var (curso, cantidad, promedio) in populares)
                {
                    Console.WriteLine($"\n  {pos}. {curso.Nombre} ({curso.Codigo})");
                    Console.WriteLine($"     Estudiantes matriculados: {cantidad}");
                    Console.WriteLine($"     Promedio del curso: {promedio:F2}");
                    Console.WriteLine($"     Créditos: {curso.Creditos}");

                    if (curso.ProfesorAsignado != null)
                    {
                        Console.WriteLine($"     Profesor: {curso.ProfesorAsignado.Nombre} {curso.ProfesorAsignado.Apellido}");
                    }

                    Console.WriteLine(new string('─', 58));
                    pos++;
                }
            }

            PausarYContinuar();
        }

        #endregion

        #region UTILIDADES

        private void MostrarBienvenida()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n");
            Console.WriteLine("█████████████████████████████████████████████████████████████████");
            Console.WriteLine("█                                                               █");
            Console.WriteLine("█      SISTEMA DE GESTIÓN UNIVERSITARIA                         █");
            Console.WriteLine("█                                                               █");
            Console.WriteLine("█      Bienvenido al sistema de administración académica        █");
            Console.WriteLine("█                                                               █");
            Console.WriteLine("█████████████████████████████████████████████████████████████████\n");
            Console.ResetColor();

            Console.WriteLine("  Presione Enter para continuar...");
            Console.ReadLine();
        }

        private void MostrarDespedida()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n");
            Console.WriteLine("╔══════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                                                          ║");
            Console.WriteLine("║   ¡Gracias por usar el Sistema de Gestión!              ║");
            Console.WriteLine("║                                                          ║");
            Console.WriteLine("║   Hasta pronto                                           ║");
            Console.WriteLine("║                                                          ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════════╝\n");
            Console.ResetColor();
        }

        private string LeerEntrada(string mensaje)
        {
            Console.Write($"\n{mensaje}: ");
            string entrada = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(entrada))
            {
                throw new ArgumentException($"{mensaje} no puede estar vacío");
            }

            return entrada.Trim();
        }

        private void MostrarExito(string mensaje)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n✓ {mensaje}\n");
            Console.ResetColor();
        }

        private void MostrarError(string mensaje)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n✗ {mensaje}\n");
            Console.ResetColor();
        }

        private void PausarYContinuar()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("\n[Presiona Enter para continuar...]");
            Console.ResetColor();
            Console.ReadLine();
        }

        #endregion
    }
}
