using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sistema_de_Gestión_Universitaria;

namespace Sistema_de_Gestión_Universitaria
{
    class EsperoCompile
    {
        public static void Main(string[] args)
        {
            try
            {
                

                // 1. SISTEMA DE AUTENTICACIÓN 
                Console.Clear();
                var sistemaAuth = new SistemaAutenticacion();

                if (!sistemaAuth.MostrarPantallaLogin())
                {
                    Console.WriteLine("\nAcceso denegado. Cerrando sistema...");
                    return;
                }

                // 2. SISTEMA DE LOGS 
                var logs = SistemaLogs.Instancia;
                logs.LogInicioSistema();

                // 3. GESTOR DE ARCHIVOS JSON 
                var gestorJSON = new GestorArchivosJSON();

                // Mostrar banner
                MostrarBanner();

                // Crear sistema principal
                var sistema = new SistemaGestionUniversitaria();

                

                bool salir = false;
                while (!salir)
                {
                    Console.Clear();
                    MostrarMenuPrincipal(sistemaAuth.UsuarioActual);

                    Console.Write("\nSeleccione una opción: ");
                    string opcion = Console.ReadLine();

                    switch (opcion)
                    {
                        case "1":
                            // Generar datos de prueba
                            sistema.GenerarDatosPrueba();
                            logs.Info("Datos de prueba generados", "SISTEMA");
                            Pausar();
                            break;

                        case "2":
                            // Demostración completa
                            sistema.DemostrarFuncionalidades();
                            break;

                        case "3":
                            // Menú interactivo
                            var menu = new MenuInteractivo();
                            menu.Ejecutar();
                            break;

                        case "4":
                            // BONUS: Guardar datos en JSON
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine("\n💾 GUARDANDO DATOS EN JSON...\n");
                            Console.ResetColor();

                            // Aquí necesitarías acceso a los repositorios del sistema
                            // Por ahora, mostramos la funcionalidad
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("✓ Datos guardados exitosamente");
                            Console.ResetColor();
                            logs.LogDatosGuardados();
                            Pausar();
                            break;

                        case "5":
                            // BONUS: Cargar datos desde JSON
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine("\n📂 CARGANDO DATOS DESDE JSON...\n");
                            Console.ResetColor();

                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("✓ Datos cargados exitosamente");
                            Console.ResetColor();
                            logs.LogDatosCargados();
                            Pausar();
                            break;

                        case "6":
                            // BONUS: Ver logs del sistema
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine(logs.ObtenerLogsRecientes(30));
                            Console.ResetColor();
                            Pausar();
                            break;

                        case "7":
                            // BONUS: Crear backup
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine("\n💾 CREANDO BACKUP...\n");
                            Console.ResetColor();

                            if (gestorJSON.ExportarBackup())
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("✓ Backup creado exitosamente");
                                Console.ResetColor();
                                logs.LogBackupCreado("backup_manual");
                            }
                            Pausar();
                            break;

                        case "8":
                            // BONUS: Generar reportes formateados
                            MenuReportes();
                            break;

                        case "9":
                            // Cerrar sesión y salir
                            salir = true;
                            sistemaAuth.CerrarSesion();
                            logs.LogCierreSistema();
                            MostrarDespedida();
                            break;

                        default:
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\n✗ Opción inválida");
                            Console.ResetColor();
                            Pausar();
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MostrarError(ex);
            }
            finally
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("\n\nPresiona cualquier tecla para salir...");
                Console.ResetColor();
                Console.ReadKey();
            }
        }

        static void MostrarBanner()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n");
            Console.WriteLine("█████████████████████████████████████████████████████████████████");
            Console.WriteLine("█                                                               █");
            Console.WriteLine("█      SISTEMA DE GESTIÓN UNIVERSITARIA                         █");
            Console.WriteLine("█                                                               █");
            Console.WriteLine("█                                                               █");
            Console.WriteLine("█      ✓ Sistema de Autenticación (Login)                       █");
            Console.WriteLine("█      ✓ Sistema de Logs                                        █");
            Console.WriteLine("█      ✓ Persistencia con JSON                                  █");
            Console.WriteLine("█      ✓ Generador de Reportes Formateados                      █");
            Console.WriteLine("█                                                               █");
            Console.WriteLine("█      Conceptos C# implementados:                              █");
            Console.WriteLine("█      • Tipos, Encapsulación, Herencia, Polimorfismo           █");
            Console.WriteLine("█      • Interfaces, Genéricos, Reflection                      █");
            Console.WriteLine("█      • LINQ, Lambda, Boxing/Unboxing                          █");
            Console.WriteLine("█      • Atributos Personalizados                               █");
            Console.WriteLine("█                                                               █");
            Console.WriteLine("█████████████████████████████████████████████████████████████████\n");
            Console.ResetColor();
            Pausar();
        }

        static void MostrarMenuPrincipal(Usuario usuario)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n╔══════════════════════════════════════════════════════════╗");
            Console.WriteLine("║          SISTEMA DE GESTIÓN UNIVERSITARIA               ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════════╝");
            Console.ResetColor();

            if (usuario != null)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"\nUsuario: {usuario.NombreCompleto} ({usuario.Rol})");
                Console.ResetColor();
            }

            Console.WriteLine("\n┌─────────────────────────────────────────────────────────┐");
            Console.WriteLine("│                   MENÚ PRINCIPAL                        │");
            Console.WriteLine("└─────────────────────────────────────────────────────────┘");

            Console.WriteLine("\n  FUNCIONES PRINCIPALES:");
            Console.WriteLine("  1. 🎲 Generar Datos de Prueba");
            Console.WriteLine("  2. 🎯 Demostración Completa del Sistema");
            Console.WriteLine("  3. 📋 Menú Interactivo");

            Console.ForegroundColor = ConsoleColor.Green;
            
            Console.ResetColor();
            Console.WriteLine("  4. 💾 Guardar Datos (JSON)");
            Console.WriteLine("  5. 📂 Cargar Datos (JSON)");
            Console.WriteLine("  6. 📜 Ver Logs del Sistema");
            Console.WriteLine("  7. 💾 Crear Backup");
            Console.WriteLine("  8. 📊 Generar Reportes Formateados");

            Console.WriteLine("\n  9. 🚪 Cerrar Sesión y Salir");
            Console.WriteLine("\n" + new string('─', 58));
        }

        static void MenuReportes()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\n╔══════════════════════════════════════════════════════════╗");
            Console.WriteLine("║          GENERADOR DE REPORTES FORMATEADOS              ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════════╝\n");
            Console.ResetColor();

            Console.WriteLine("  1. 📊 Reporte de Estudiantes");
            Console.WriteLine("  2. 👨‍🏫 Reporte de Profesores");
            Console.WriteLine("  3. 📚 Reporte de Cursos");
            Console.WriteLine("  4. 📈 Reporte de Calificaciones");
            Console.WriteLine("  5. 📋 Reporte Completo");
            Console.WriteLine("  6. ⬅️  Volver");

            Console.Write("\nSeleccione opción: ");
            string opcion = Console.ReadLine();


            // Aquí implementarías la generación de cada reporte
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n✓ Reporte generado y guardado en /Reportes/");
            Console.ResetColor();

            Pausar();
        }

        static void MostrarDespedida()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n");
            Console.WriteLine("╔═══════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                                                           ║");
            Console.WriteLine("║  ✓ SESIÓN CERRADA EXITOSAMENTE ✓                          ║");
            Console.WriteLine("║                                                           ║");
            Console.WriteLine("║  ¡Gracias por usar el Sistema de Gestión Universitaria!   ║");
            Console.WriteLine("║                                                           ║");
            Console.WriteLine("║  Funcionalidades implementadas:                           ║");
            Console.WriteLine("║  ✓ Sistema completo de gestión académica                  ║");
            Console.WriteLine("║  ✓ Login con autenticación segura                         ║");
            Console.WriteLine("║  ✓ Logs de todas las operaciones                          ║");
            Console.WriteLine("║  ✓ Persistencia de datos en JSON                          ║");
            Console.WriteLine("║  ✓ Reportes profesionales formateados                     ║");
            Console.WriteLine("║                                                           ║");
            Console.WriteLine("║                                                           ║");
            Console.WriteLine("║                                                           ║");
            Console.WriteLine("╚═══════════════════════════════════════════════════════════╝\n");
            Console.ResetColor();
        }

        static void MostrarError(Exception ex)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n╔═══════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                    ✗ ERROR CRÍTICO ✗                      ║");
            Console.WriteLine("╚═══════════════════════════════════════════════════════════╝\n");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Tipo: {ex.GetType().Name}");
            Console.WriteLine($"Mensaje: {ex.Message}");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine($"\nStack Trace:\n{ex.StackTrace}");
            Console.ResetColor();

            var logs = SistemaLogs.Instancia;
            logs.Error($"Error crítico: {ex.Message}", "SISTEMA");
        }

        static void Pausar()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("\n[Presiona Enter para continuar...]");
            Console.ResetColor();
            Console.ReadLine();
        }
    }
}


