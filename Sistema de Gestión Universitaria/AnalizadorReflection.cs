using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_de_Gestión_Universitaria
{
    public class AnalizadorReflection
    {
        
        // Muestra todas las propiedades de un tipo con su información detallada.
        // Usa Reflection para obtener metadata de las propiedades.
       
        public static void MostrarPropiedades(Type tipo)
        {
            if (tipo == null)
                throw new ArgumentNullException(nameof(tipo));

            Console.WriteLine("\n╔═══════════════════════════════════════════════════════════╗");
            Console.WriteLine($"║  PROPIEDADES DE: {tipo.Name,-42}║");
            Console.WriteLine("╚═══════════════════════════════════════════════════════════╝\n");

            // GetProperties obtiene todas las propiedades públicas
            // BindingFlags permite especificar qué tipo de miembros obtener
            PropertyInfo[] propiedades = tipo.GetProperties(
                BindingFlags.Public |
                BindingFlags.Instance |
                BindingFlags.Static);

            if (propiedades.Length == 0)
            {
                Console.WriteLine("  No hay propiedades públicas.\n");
                return;
            }

            int contador = 1;
            foreach (PropertyInfo propiedad in propiedades)
            {
                Console.WriteLine($"{contador}. {propiedad.Name}");
                Console.WriteLine($"   Tipo: {propiedad.PropertyType.Name}");
                Console.WriteLine($"   Tipo completo: {propiedad.PropertyType.FullName}");

                // Verifica si tiene getter
                Console.WriteLine($"   Lectura (get): {(propiedad.CanRead ? "Sí ✓" : "No ✗")}");

                // Verifica si tiene setter
                Console.WriteLine($"   Escritura (set): {(propiedad.CanWrite ? "Sí ✓" : "No ✗")}");

                // Obtiene atributos personalizados
                var atributos = propiedad.GetCustomAttributes(true);
                if (atributos.Length > 0)
                {
                    Console.WriteLine($"   Atributos: {string.Join(", ",
                        atributos.Select(a => a.GetType().Name))}");
                }

                Console.WriteLine();
                contador++;
            }
        }

        
        //Muestra todos los métodos públicos de un tipo.
       
        public static void MostrarMetodos(Type tipo)
        {
            if (tipo == null)
                throw new ArgumentNullException(nameof(tipo));

            Console.WriteLine("\n╔═══════════════════════════════════════════════════════════╗");
            Console.WriteLine($"║  MÉTODOS DE: {tipo.Name,-46}║");
            Console.WriteLine("╚═══════════════════════════════════════════════════════════╝\n");

            // GetMethods obtiene todos los métodos públicos
            // DeclaredOnly para no incluir métodos heredados de Object
            MethodInfo[] metodos = tipo.GetMethods(
                BindingFlags.Public |
                BindingFlags.Instance |
                BindingFlags.DeclaredOnly);

            if (metodos.Length == 0)
            {
                Console.WriteLine("  No hay métodos públicos declarados.\n");
                return;
            }

            int contador = 1;
            foreach (MethodInfo metodo in metodos)
            {
                // Construye la firma del método
                var parametros = metodo.GetParameters();
                string firma = $"{metodo.ReturnType.Name} {metodo.Name}(";

                if (parametros.Length > 0)
                {
                    firma += string.Join(", ", parametros.Select(p =>
                        $"{p.ParameterType.Name} {p.Name}"));
                }

                firma += ")";

                Console.WriteLine($"{contador}. {firma}");

                // Información adicional
                if (metodo.IsVirtual)
                    Console.WriteLine("   [Virtual]");
                if (metodo.IsAbstract)
                    Console.WriteLine("   [Abstracto]");
                if (metodo.IsStatic)
                    Console.WriteLine("   [Estático]");

                Console.WriteLine();
                contador++;
            }
        }

      
        // Crea una instancia de un tipo dinámicamente usando Reflection.
        // Usa Activator.CreateInstance para crear objetos sin conocer el tipo en compilación.
       
        public static object CrearInstanciaDinamica(Type tipo, params object[] parametros)
        {
            if (tipo == null)
                throw new ArgumentNullException(nameof(tipo));

            try
            {
                Console.WriteLine($"\nCreando instancia de {tipo.Name}...");

                // Activator.CreateInstance crea una instancia del tipo
                // Busca un constructor que coincida con los parámetros
                object instancia = Activator.CreateInstance(tipo, parametros);

                Console.WriteLine($"Instancia creada exitosamente: {instancia.GetType().Name}");

                return instancia;
            }
            catch (MissingMethodException ex)
            {
                Console.WriteLine($" Error: No se encontró constructor compatible.");
                Console.WriteLine($" Detalles: {ex.Message}");
                throw;
            }
            catch (TargetInvocationException ex)
            {
                Console.WriteLine($"Error al ejecutar constructor: {ex.InnerException?.Message}");
                throw;
            }
        }

        
        // Invoca un método de una instancia dinámicamente.
        // Permite llamar métodos sin conocerlos en tiempo de compilación.
        
       
        public static object InvocarMetodo(object instancia, string nombreMetodo,
                                          params object[] parametros)
        {
            if (instancia == null)
                throw new ArgumentNullException(nameof(instancia));
            if (string.IsNullOrWhiteSpace(nombreMetodo))
                throw new ArgumentException("El nombre del método no puede estar vacío");

            Type tipo = instancia.GetType();

            try
            {
                Console.WriteLine($"\nInvocando método '{nombreMetodo}' en {tipo.Name}...");

                // Obtiene el MethodInfo del método
                MethodInfo metodo = tipo.GetMethod(nombreMetodo,
                    BindingFlags.Public | BindingFlags.Instance);

                if (metodo == null)
                {
                    throw new InvalidOperationException(
                        $"No se encontró el método '{nombreMetodo}' en {tipo.Name}");
                }

                // Invoca el método dinámicamente
                object resultado = metodo.Invoke(instancia, parametros);

                Console.WriteLine($"✓ Método invocado exitosamente");

                if (resultado != null)
                {
                    Console.WriteLine($"  Resultado: {resultado}");
                }

                return resultado;
            }
            catch (TargetInvocationException ex)
            {
                Console.WriteLine($" Error al ejecutar método: {ex.InnerException?.Message}");
                throw;
            }
        }

        // Obtiene el valor de una propiedad dinámicamente.
     
        public static object ObtenerValorPropiedad(object instancia, string nombrePropiedad)
        {
            if (instancia == null)
                throw new ArgumentNullException(nameof(instancia));

            Type tipo = instancia.GetType();
            PropertyInfo propiedad = tipo.GetProperty(nombrePropiedad);

            if (propiedad == null)
                throw new InvalidOperationException(
                    $"No existe la propiedad '{nombrePropiedad}' en {tipo.Name}");

            if (!propiedad.CanRead)
                throw new InvalidOperationException(
                    $"La propiedad '{nombrePropiedad}' no tiene getter");

            return propiedad.GetValue(instancia);
        }

      
        // Establece el valor de una propiedad dinámicamente.
       
        public static void EstablecerValorPropiedad(object instancia, string nombrePropiedad,
                                                    object valor)
        {
            if (instancia == null)
                throw new ArgumentNullException(nameof(instancia));

            Type tipo = instancia.GetType();
            PropertyInfo propiedad = tipo.GetProperty(nombrePropiedad);

            if (propiedad == null)
                throw new InvalidOperationException(
                    $"No existe la propiedad '{nombrePropiedad}' en {tipo.Name}");

            if (!propiedad.CanWrite)
                throw new InvalidOperationException(
                    $"La propiedad '{nombrePropiedad}' no tiene setter");

            propiedad.SetValue(instancia, valor);
            Console.WriteLine($"✓ Propiedad '{nombrePropiedad}' establecida a: {valor}");
        }

        
        // Analiza completamente un tipo y muestra toda su información.
        
        public static void AnalisisCompleto(Type tipo)
        {
            if (tipo == null)
                throw new ArgumentNullException(nameof(tipo));

            Console.WriteLine("\n" + new string('═', 65));
            Console.WriteLine($"  ANÁLISIS COMPLETO DE TIPO: {tipo.Name}");
            Console.WriteLine(new string('═', 65));

            // Información básica
            Console.WriteLine($"\nNombre completo: {tipo.FullName}");
            Console.WriteLine($"Espacio de nombres: {tipo.Namespace}");
            Console.WriteLine($"Es clase: {tipo.IsClass}");
            Console.WriteLine($"Es abstracta: {tipo.IsAbstract}");
            Console.WriteLine($"Es interfaz: {tipo.IsInterface}");
            Console.WriteLine($"Es sellada: {tipo.IsSealed}");

            // Tipo base
            if (tipo.BaseType != null)
            {
                Console.WriteLine($"Hereda de: {tipo.BaseType.Name}");
            }

            // Interfaces
            var interfaces = tipo.GetInterfaces();
            if (interfaces.Length > 0)
            {
                Console.WriteLine($"Implementa interfaces: {string.Join(", ",
                    interfaces.Select(i => i.Name))}");
            }

            // Constructores
            var constructores = tipo.GetConstructors();
            Console.WriteLine($"\nConstructores: {constructores.Length}");
            foreach (var ctor in constructores)
            {
                var params_ctor = ctor.GetParameters();
                Console.WriteLine($"  - ({string.Join(", ",
                    params_ctor.Select(p => $"{p.ParameterType.Name} {p.Name}"))})");
            }

            // Propiedades
            MostrarPropiedades(tipo);

            // Métodos
            MostrarMetodos(tipo);
        }

        /// <summary>
        /// Demuestra el uso de Reflection con las clases del sistema.
        /// </summary>
        public static void DemostrarReflection()
        {
            Console.WriteLine("\n");
            Console.WriteLine("█████████████████████████████████████████████████████████████");
            Console.WriteLine("█                                                           █");
            Console.WriteLine("█        DEMOSTRACIÓN DE REFLECTION                         █");
            Console.WriteLine("█        Inspección de tipos en tiempo de ejecución         █");
            Console.WriteLine("█                                                           █");
            Console.WriteLine("█████████████████████████████████████████████████████████████");

            // Análisis de Estudiante
            Console.WriteLine("\n\n=== ANÁLISIS DE LA CLASE ESTUDIANTE ===");
            AnalisisCompleto(typeof(Estudiante));

            // Análisis de Profesor
            Console.WriteLine("\n\n=== ANÁLISIS DE LA CLASE PROFESOR ===");
            AnalisisCompleto(typeof(Profesor));

            // Análisis de Curso
            Console.WriteLine("\n\n=== ANÁLISIS DE LA CLASE CURSO ===");
            AnalisisCompleto(typeof(Curso));

            // Creación dinámica de instancias
            Console.WriteLine("\n\n=== CREACIÓN DINÁMICA DE INSTANCIAS ===");

            try
            {
                // Crear un Curso dinámicamente
                Type tipoCurso = typeof(Curso);
                object cursoInstancia = CrearInstanciaDinamica(
                    tipoCurso,
                    "REFL-101",              // codigo
                    "Introducción a Reflection",  // nombre
                    3,                       // creditos
                    null                     // profesorAsignado
                );

                Console.WriteLine($"Instancia creada: {cursoInstancia}");

                // Invocar método ToString dinámicamente
                Console.WriteLine("\n--- Invocando ToString() dinámicamente ---");
                object resultadoToString = InvocarMetodo(cursoInstancia, "ToString");
                Console.WriteLine($"Resultado: {resultadoToString}");

                // Obtener valor de propiedad dinámicamente
                Console.WriteLine("\n--- Obteniendo valores de propiedades ---");
                object codigo = ObtenerValorPropiedad(cursoInstancia, "Codigo");
                Console.WriteLine($"Código del curso: {codigo}");

                object nombre = ObtenerValorPropiedad(cursoInstancia, "Nombre");
                Console.WriteLine($"Nombre del curso: {nombre}");

                // Establecer valor de propiedad dinámicamente
                Console.WriteLine("\n--- Modificando propiedades ---");
                EstablecerValorPropiedad(cursoInstancia, "Creditos", 4);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en demostración: {ex.Message}");
            }

            Console.WriteLine("\n" + new string('═', 65));
            Console.WriteLine("  FIN DE LA DEMOSTRACIÓN DE REFLECTION");
            Console.WriteLine(new string('═', 65) + "\n");
        }
    }
}

