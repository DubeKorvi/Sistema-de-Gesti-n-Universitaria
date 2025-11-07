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
                // Mostrar banner inicial
                MostrarBanner();

                // Crear instancia del sistema
                var sistema = new SistemaGestionUniversitaria();

                // Generar datos de prueba
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("\n════════════════════════════════════════════════════════════");
                Console.WriteLine("  INICIANDO SISTEMA - Generando datos de prueba...");
                Console.WriteLine("════════════════════════════════════════════════════════════\n");
                Console.ResetColor();

                sistema.GenerarDatosPrueba();

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n[Presiona ENTER para iniciar la demostración completa...]");
                Console.ResetColor();
                Console.ReadLine();

                // Demostrar todas las funcionalidades
                sistema.DemostrarFuncionalidades();

                // Opción para menú interactivo
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("\n╔═══════════════════════════════════════════════════════════╗");
                Console.WriteLine("║  ¿Desea iniciar el menú interactivo?                      ║");
                Console.WriteLine("╚═══════════════════════════════════════════════════════════╝");
                Console.ResetColor();
                Console.Write("\nEscriba 'SI' para abrir el menú interactivo: ");
                string respuesta = Console.ReadLine();

                if (respuesta?.ToUpper() == "SI")
                {
                    var menu = new MenuInteractivo();
                    menu.Ejecutar();
                }

                // Mostrar mensaje final
                MostrarDespedida();
            }
            catch (Exception ex)
            {
                MostrarError(ex);
            }
            finally
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("\nPresiona cualquier tecla para salir...");
                Console.ResetColor();
                Console.ReadKey();
            }
        }

        private static void MostrarBanner()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n");
            Console.WriteLine("█████████████████████████████████████████████████████████████████");
            Console.WriteLine("█                                                               █");
            Console.WriteLine("█      SISTEMA DE GESTIÓN UNIVERSITARIA                         █");
            Console.WriteLine("█      Tarea Práctica de C#                                     █");
            Console.WriteLine("█                                                               █");
            Console.WriteLine("█      Conceptos demostrados:                                   █");
            Console.WriteLine("█      • Tipos y Encapsulación                                  █");
            Console.WriteLine("█      • Herencia y Polimorfismo                                █");
            Console.WriteLine("█      • Interfaces y Genéricos                                 █");
            Console.WriteLine("█      • Reflection y Atributos Personalizados                  █");
            Console.WriteLine("█      • LINQ y Expresiones Lambda                              █");
            Console.WriteLine("█      • Boxing/Unboxing y Conversiones                         █");
            Console.WriteLine("█                                                               █");
            Console.WriteLine("█████████████████████████████████████████████████████████████████\n");
            Console.ResetColor();
        }

        private static void MostrarDespedida()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n");
            Console.WriteLine("╔═══════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                                                           ║");
            Console.WriteLine("║  ✓ DEMOSTRACIÓN COMPLETADA EXITOSAMENTE ✓                 ║");
            Console.WriteLine("║                                                           ║");
            Console.WriteLine("║  ¡Gracias por usar el Sistema de Gestión Universitaria!  ║");
            Console.WriteLine("║                                                           ║");
            Console.WriteLine("║  Todas las funcionalidades han sido demostradas:         ║");
            Console.WriteLine("║  ✓ Generación de datos de prueba                         ║");
            Console.WriteLine("║  ✓ Consultas LINQ y Lambda                                ║");
            Console.WriteLine("║  ✓ Reflection                                             ║");
            Console.WriteLine("║  ✓ Atributos personalizados                               ║");
            Console.WriteLine("║  ✓ Boxing/Unboxing                                        ║");
            Console.WriteLine("║                                                           ║");
            Console.WriteLine("╚═══════════════════════════════════════════════════════════╝\n");
            Console.ResetColor();
        }

        private static void MostrarError(Exception ex)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n╔═══════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                    ✗ ERROR CRÍTICO ✗                      ║");
            Console.WriteLine("╚═══════════════════════════════════════════════════════════╝\n");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Tipo de error: {ex.GetType().Name}");
            Console.WriteLine($"Mensaje: {ex.Message}");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine($"\nStack Trace:");
            Console.WriteLine(ex.StackTrace);
            Console.ResetColor();

            if (ex.InnerException != null)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"\nExcepción interna: {ex.InnerException.Message}");
                Console.ResetColor();
            }
        }
    }

}
