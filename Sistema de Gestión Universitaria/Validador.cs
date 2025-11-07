using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;



namespace Sistema_de_Gestión_Universitaria
{
    public class Validador
    {
        
        /// Representa un error de validación.
        
        public class ErrorValidacion
        {
            public string NombrePropiedad { get; set; }
            public string Mensaje { get; set; }
            public string TipoError { get; set; }

            public override string ToString()
            {
                return $"[{NombrePropiedad}] {Mensaje} (Tipo: {TipoError})";
            }
        }

        
        // Valida un objeto usando sus atributos personalizados.
        // Retorna lista de errores de validación (vacía si es válido).
       
      
        public static List<ErrorValidacion> Validar(object instancia)
        {
            var errores = new List<ErrorValidacion>();

            if (instancia == null)
            {
                errores.Add(new ErrorValidacion
                {
                    NombrePropiedad = "Objeto",
                    Mensaje = "La instancia es null",
                    TipoError = "NullObject"
                });
                return errores;
            }

            // Obtener tipo de la instancia
            Type tipo = instancia.GetType();

            // Obtener todas las propiedades
            PropertyInfo[] propiedades = tipo.GetProperties(
                BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo propiedad in propiedades)
            {
                // Obtener valor actual de la propiedad
                object valor = null;
                try
                {
                    valor = propiedad.GetValue(instancia);
                }
                catch (Exception ex)
                {
                    errores.Add(new ErrorValidacion
                    {
                        NombrePropiedad = propiedad.Name,
                        Mensaje = $"Error al obtener valor: {ex.Message}",
                        TipoError = "AccessError"
                    });
                    continue;
                }

                // Validar atributo [Requerido]
                var atributoRequerido = propiedad.GetCustomAttribute<RequeridoAttribute>();
                if (atributoRequerido != null)
                {
                    if (!ValidarRequerido(valor))
                    {
                        errores.Add(new ErrorValidacion
                        {
                            NombrePropiedad = propiedad.Name,
                            Mensaje = atributoRequerido.MensajeError,
                            TipoError = "Requerido"
                        });
                    }
                }

                // Validar atributo [ValidacionRango]
                var atributoRango = propiedad.GetCustomAttribute<ValidacionRangoAttribute>();
                if (atributoRango != null && valor != null)
                {
                    if (!ValidarRango(valor, atributoRango.Minimo, atributoRango.Maximo))
                    {
                        string mensaje = atributoRango.MensajeError ??
                            $"El valor debe estar entre {atributoRango.Minimo} y {atributoRango.Maximo}";

                        errores.Add(new ErrorValidacion
                        {
                            NombrePropiedad = propiedad.Name,
                            Mensaje = mensaje,
                            TipoError = "Rango"
                        });
                    }
                }

                // Validar atributo [Formato]
                var atributoFormato = propiedad.GetCustomAttribute<FormatoAttribute>();
                if (atributoFormato != null && valor != null)
                {
                    if (!ValidarFormato(valor.ToString(), atributoFormato.Patron))
                    {
                        string mensaje = atributoFormato.MensajeError ??
                            $"Formato inválido. {atributoFormato.Descripcion}";

                        errores.Add(new ErrorValidacion
                        {
                            NombrePropiedad = propiedad.Name,
                            Mensaje = mensaje,
                            TipoError = "Formato"
                        });
                    }
                }
            }

            return errores;
        }

        
        // Valida que un valor no sea null o vacío.
       
        private static bool ValidarRequerido(object valor)
        {
            if (valor == null)
                return false;

            if (valor is string texto)
                return !string.IsNullOrWhiteSpace(texto);

            return true;
        }

        
        // Valida que un valor numérico esté dentro de un rango.
       
        private static bool ValidarRango(object valor, decimal minimo, decimal maximo)
        {
            try
            {
                decimal valorDecimal = Convert.ToDecimal(valor);
                return valorDecimal >= minimo && valorDecimal <= maximo;
            }
            catch
            {
                return false;
            }
        }

        
        // Valida que un string cumpla con un patrón regex.
       
        private static bool ValidarFormato(string valor, string patron)
        {
            if (string.IsNullOrEmpty(valor) || string.IsNullOrEmpty(patron))
                return false;

            try
            {
                return Regex.IsMatch(valor, patron);
            }
            catch
            {
                return false;
            }
        }

        
        // Valida un objeto y muestra los resultados en consola.
       
        public static bool ValidarYMostrar(object instancia, string nombreObjeto = "Objeto")
        {
            Console.WriteLine($"\n╔═══════════════════════════════════════════════════════════╗");
            Console.WriteLine($"║  VALIDANDO: {nombreObjeto,-45}║");
            Console.WriteLine("╚═══════════════════════════════════════════════════════════╝\n");

            var errores = Validar(instancia);

            if (errores.Count == 0)
            {
                Console.WriteLine(" VALIDACIÓN EXITOSA - Sin errores \n");
                return true;
            }
            else
            {
                Console.WriteLine($"VALIDACIÓN FALLIDA - {errores.Count} error(es) encontrado(s) \n");

                int num = 1;
                foreach (var error in errores)
                {
                    Console.WriteLine($"{num}. {error}");
                    num++;
                }
                Console.WriteLine();
                return false;
            }
        }

        /// <summary>
        /// Demuestra el sistema de validación con atributos.
        /// </summary>
        public static void DemostrarValidacion()
        {
            Console.WriteLine("\n");
            Console.WriteLine("█████████████████████████████████████████████████████████████");
            Console.WriteLine("█                                                           █");
            Console.WriteLine("█      DEMOSTRACIÓN DE ATRIBUTOS PERSONALIZADOS             █");
            Console.WriteLine("█      Sistema de validación con Reflection                 █");
            Console.WriteLine("█                                                           █");
            Console.WriteLine("█████████████████████████████████████████████████████████████");

            // CASO 1: Estudiante VÁLIDO
            Console.WriteLine("\n\n=== CASO 1: Estudiante VÁLIDO ===");
            try
            {
                var estudianteValido = new EstudianteConValidacion(
                    "001-2024",
                    "Carlos",
                    "Ramírez",
                    new DateTime(2002, 3, 15),
                    "Ingeniería en Sistemas",
                    "2024-001"
                );

                ValidarYMostrar(estudianteValido, "Estudiante Válido");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear estudiante: {ex.Message}");
            }

            // CASO 2: Estudiante con formato INVÁLIDO de matrícula
            Console.WriteLine("\n=== CASO 2: Estudiante con formato inválido de matrícula ===");
            try
            {
                var estudianteInvalido = new EstudianteConValidacion(
                    "002-2024",
                    "María",
                    "González",
                    new DateTime(2001, 7, 20),
                    "Medicina",
                    "ABC123"  // Formato inválido
                );

                ValidarYMostrar(estudianteInvalido, "Estudiante con matrícula inválida");
            }
            catch (Exception ex)
            {
                Console.WriteLine($" Error de validación en constructor: {ex.Message}\n");
            }

            // CASO 3: Profesor VÁLIDO
            Console.WriteLine("\n=== CASO 3: Profesor VÁLIDO ===");
            try
            {
                var profesorValido = new ProfesorConValidacion(
                    "PROF-001",
                    "Juan",
                    "Pérez",
                    new DateTime(1980, 5, 10),
                    "Ciencias de la Computación",
                    TipoContrato.TiempoCompleto,
                    5000m
                );

                ValidarYMostrar(profesorValido, "Profesor Válido");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            // CASO 4: Profesor con salario fuera de rango
            Console.WriteLine("\n=== CASO 4: Profesor con salario inválido ===");
            try
            {
                var profesorInvalido = new ProfesorConValidacion(
                    "PROF-002",
                    "Ana",
                    "Martínez",
                    new DateTime(1985, 9, 25),
                    "Matemáticas",
                    TipoContrato.MedioTiempo,
                    15000m  // Fuera de rango
                );

                ValidarYMostrar(profesorInvalido, "Profesor con salario fuera de rango");
            }
            catch (Exception ex)
            {
                Console.WriteLine($" Error de validación: {ex.Message}\n");
            }

            // CASO 5: Curso VÁLIDO
            Console.WriteLine("\n=== CASO 5: Curso VÁLIDO ===");
            try
            {
                var cursoValido = new CursoConValidacion(
                    "MAT-101",
                    "Cálculo Diferencial",
                    4
                );

                ValidarYMostrar(cursoValido, "Curso Válido");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            // CASO 6: Curso con código inválido
            Console.WriteLine("\n=== CASO 6: Curso con código inválido ===");
            try
            {
                var cursoInvalido = new CursoConValidacion(
                    "matematicas101",  // Formato inválido
                    "Álgebra Lineal",
                    3
                );

                ValidarYMostrar(cursoInvalido, "Curso con código inválido");
            }
            catch (Exception ex)
            {
                Console.WriteLine($" Error de validación en constructor: {ex.Message}\n");
            }

            Console.WriteLine("\n" + new string('═', 65));
            Console.WriteLine("  FIN DE LA DEMOSTRACIÓN DE VALIDACIÓN");
            Console.WriteLine(new string('═', 65) + "\n");
        }
    }
}
