using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_de_Gestión_Universitaria
{
    public class ManejadorTipos
    {
       
        // Convierte y formatea diferentes tipos de datos usando pattern matching.
        
        public static string ConvertirDatos(object dato)
        {
           
            return dato switch
            {
                // Caso 1: Si es null
                null => "Valor nulo",

                // Caso 2: Si es int
                // Aquí ocurre UNBOXING: object -> int
                int entero => $"Número entero: {entero:N0} (Tipo: System.Int32)",

                // Caso 3: Si es decimal
                decimal dec => $"Número decimal: {dec:C2} (Tipo: System.Decimal)",

                // Caso 4: Si es double
                double dbl => $"Número doble: {dbl:F4} (Tipo: System.Double)",

                // Caso 5: Si es string
                string texto => $"Texto: \"{texto}\" (Longitud: {texto.Length} caracteres)",

                // Caso 6: Si es DateTime
                DateTime fecha => $"Fecha: {fecha:dddd, dd 'de' MMMM 'de' yyyy} " +
                                 $"a las {fecha:HH:mm:ss} (Tipo: System.DateTime)",

                // Caso 7: Si es bool
                bool booleano => $"✓/✗ Booleano: {(booleano ? "Verdadero ✓" : "Falso ✗")}",

                // Caso 8: Si es Estudiante (nuestro tipo personalizado)
                Estudiante est => $"Estudiante: {est.Nombre} {est.Apellido} " +
                                 $"(ID: {est.Identificacion})",

                // Caso 9: Si es Profesor
                Profesor prof => $"Profesor: {prof.Nombre} {prof.Apellido} " +
                                $"({prof.Departamento})",

                // Caso 10: Si es Curso
                Curso curso => $"Curso: {curso.Nombre} ({curso.Codigo}) - " +
                              $"{curso.Creditos} créditos",

                // Caso 11: Array de enteros
                int[] arrayInt => $"Array de {arrayInt.Length} enteros: " +
                                 $"[{string.Join(", ", arrayInt)}]",

                // Caso default: tipo no reconocido
                _ => $"❓ Tipo desconocido: {dato.GetType().Name} - Valor: {dato}"
            };
        }

        
        // Parsea una calificación de forma segura usando TryParse.
        // Evita excepciones al intentar convertir strings inválidos.
        
        
        public static (bool exito, decimal valor) ParsearCalificacion(string entrada)
        {
            // Validación inicial
            if (string.IsNullOrWhiteSpace(entrada))
            {
                return (false, 0);
            }

            // TryParse intenta convertir sin lanzar excepción
            // Retorna true si tuvo éxito, false si falló
            // out decimal resultado: variable donde se guarda el resultado
            bool exito = decimal.TryParse(entrada, NumberStyles.Number,
                                         CultureInfo.InvariantCulture,
                                         out decimal resultado);

            if (!exito)
            {
                return (false, 0);
            }

            // Validar rango (0-10)
            if (resultado < 0 || resultado > 10)
            {
                Console.WriteLine($"Calificación fuera de rango: {resultado}");
                return (false, 0);
            }

            return (true, resultado);
        }

       
        // Demuestra el proceso de boxing y unboxing con calificaciones.
        // BOXING: Conversión implícita de tipo valor a object
        // UNBOXING: Conversión explícita de object a tipo valor
       
        public static void DemostrarBoxingUnboxing()
        {
            Console.WriteLine("\n═══════════════════════════════════════════════════════");
            Console.WriteLine("     DEMOSTRACIÓN DE BOXING Y UNBOXING");
            Console.WriteLine("═══════════════════════════════════════════════════════\n");

            // ═══ BOXING ═══
            Console.WriteLine("--- BOXING (valor -> object) ---\n");

            // 1. Boxing de decimal
            decimal calificacion = 8.5m;  // Tipo valor en el stack
            Console.WriteLine($"1. Valor original: {calificacion} (tipo: decimal)");
            Console.WriteLine($"   Ubicación: Stack (tipo valor)");

            // BOXING: El valor se copia al heap y se envuelve en un object
            object calificacionBoxed = calificacion;  // ¡Aquí ocurre el boxing!
            Console.WriteLine($"2. Después de boxing: {calificacionBoxed} (tipo: object)");
            Console.WriteLine($"   Ubicación: Heap (tipo referencia)");
            Console.WriteLine($"   El valor se COPIÓ del stack al heap\n");

            // 2. Boxing de int
            int edad = 25;
            object edadBoxed = edad;  // Boxing
            Console.WriteLine($"3. Boxing de int: {edad} -> {edadBoxed}");
            Console.WriteLine($"   Tipo original: {edad.GetType()}");
            Console.WriteLine($"   Tipo boxed: {edadBoxed.GetType()}\n");

            // ═══ UNBOXING ═══
            Console.WriteLine("--- UNBOXING (object -> valor) ---\n");

            // Unboxing CORRECTO: mismo tipo
            try
            {
                decimal calificacionUnboxed = (decimal)calificacionBoxed;  // Unboxing
                Console.WriteLine($"4. Unboxing exitoso: {calificacionUnboxed}");
                Console.WriteLine($"   object -> decimal ✓");
                Console.WriteLine($"   El valor se COPIÓ del heap al stack\n");
            }
            catch (InvalidCastException ex)
            {
                Console.WriteLine($"   Error: {ex.Message}");
            }

            // Unboxing INCORRECTO: tipo diferente (causaría excepción)
            try
            {
                Console.WriteLine("5. Intentando unboxing incorrecto (object -> int):");
                int intentoIncorrecto = (int)calificacionBoxed;  // ¡ERROR!
                Console.WriteLine($"   Valor: {intentoIncorrecto}");
            }
            catch (InvalidCastException ex)
            {
                Console.WriteLine($"   ✗ Error (esperado): {ex.Message}");
                Console.WriteLine($"   No se puede hacer unboxing a un tipo diferente\n");
            }

            // ═══ DEMOSTRACIÓN CON COLECCIONES ═══
            Console.WriteLine("--- BOXING EN COLECCIONES NO GENÉRICAS ---\n");

            // ArrayList almacena objects, causa boxing
            System.Collections.ArrayList lista = new System.Collections.ArrayList();

            decimal nota1 = 7.5m;
            decimal nota2 = 8.0m;
            decimal nota3 = 9.5m;

            Console.WriteLine("6. Agregando valores a ArrayList (no genérico):");
            lista.Add(nota1);  // BOXING: decimal -> object
            lista.Add(nota2);  // BOXING: decimal -> object
            lista.Add(nota3);  // BOXING: decimal -> object
            Console.WriteLine($"   Se realizaron {lista.Count} operaciones de boxing");

            Console.WriteLine("\n7. Recuperando valores de ArrayList:");
            decimal suma = 0;
            foreach (object item in lista)
            {
                decimal nota = (decimal)item;  // UNBOXING: object -> decimal
                suma += nota;
                Console.WriteLine($"   Unboxing: {item} (object) -> {nota} (decimal)");
            }
            Console.WriteLine($"   Promedio: {suma / lista.Count:F2}");
            Console.WriteLine($"   Se realizaron {lista.Count} operaciones de unboxing\n");

            // ═══ COMPARACIÓN CON GENÉRICOS (sin boxing) ═══
            Console.WriteLine("--- COMPARACIÓN: LISTA GENÉRICA (sin boxing) ---\n");

            var listaGenerica = new System.Collections.Generic.List<decimal>();
            listaGenerica.Add(7.5m);   // NO hay boxing
            listaGenerica.Add(8.0m);   // NO hay boxing
            listaGenerica.Add(9.5m);   // NO hay boxing

            Console.WriteLine("8. Lista genérica List<decimal>:");
            Console.WriteLine("   ✓ NO hay boxing al agregar");
            Console.WriteLine("   ✓ NO hay unboxing al recuperar");
            Console.WriteLine("   ✓ Mejor rendimiento");
            Console.WriteLine("   ✓ Type-safe en tiempo de compilación\n");

            // ═══ COSTOS DE BOXING/UNBOXING ═══
            Console.WriteLine("--- COSTOS DE RENDIMIENTO ---\n");
            Console.WriteLine("Boxing/Unboxing tiene costos:");
            Console.WriteLine("  • Alocación de memoria en el heap");
            Console.WriteLine("  • Copia de datos");
            Console.WriteLine("  • Garbage collection adicional");
            Console.WriteLine("  • Más lento que trabajar con tipos valor directamente");
            Console.WriteLine("\n¡Por eso preferimos genéricos cuando sea posible!\n");

            Console.WriteLine("═══════════════════════════════════════════════════════\n");
        }

        /// <summary>
        /// Ejemplo de conversiones seguras con múltiples tipos.
        /// </summary>
        public static void DemostrarConversiones()
        {
            Console.WriteLine("\n═══════════════════════════════════════════════════════");
            Console.WriteLine("     DEMOSTRACIÓN DE CONVERSIONES Y PATTERN MATCHING");
            Console.WriteLine("═══════════════════════════════════════════════════════\n");

            // Array de diferentes tipos (todos se convierten a object = BOXING)
            object[] datosVariados = new object[]
            {
                42,                                    // int
                3.14159m,                             // decimal
                "Programación Orientada a Objetos",   // string
                DateTime.Now,                         // DateTime
                true,                                 // bool
                new int[] { 1, 2, 3, 4, 5 },         // array
                8.75m,                                // decimal (calificación)
            };

            Console.WriteLine("Procesando datos de diferentes tipos:\n");

            int contador = 1;
            foreach (object dato in datosVariados)
            {
                // UNBOXING ocurre dentro de ConvertirDatos cuando hace pattern matching
                string resultado = ConvertirDatos(dato);
                Console.WriteLine($"{contador}. {resultado}");
                contador++;
            }

            Console.WriteLine("\n--- Parseo seguro de calificaciones ---\n");

            string[] entradasPrueba = { "8.5", "9", "10.5", "abc", "", "-1", "7.75" };

            foreach (string entrada in entradasPrueba)
            {
                var (exito, valor) = ParsearCalificacion(entrada);

                if (exito)
                {
                    Console.WriteLine($"'{entrada}' -> {valor:F2} (válido)");
                }
                else
                {
                    Console.WriteLine($"'{entrada}' -> inválido");
                }
            }

            Console.WriteLine("\n═══════════════════════════════════════════════════════\n");
        }
    }
}
