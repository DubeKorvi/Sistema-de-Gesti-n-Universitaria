using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_de_Gestión_Universitaria
{
    public class Repositorio<T> where T : IIdentificable
    {
        // Dictionary para almacenamiento eficiente
        // Key: Identificacion del objeto
        // Value: El objeto completo
        private Dictionary<string, T> items;

        
        // Constructor que inicializa el diccionario.
        
        public Repositorio()
        {
            items = new Dictionary<string, T>();
        }

       
        // Agrega un elemento al repositorio.
        // Lanza excepción si ya existe un elemento con la misma identificación.
      
        public void Agregar(T item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item), "El elemento no puede ser null");

            // Verifica si ya existe un elemento con esa identificación
            if (items.ContainsKey(item.Identificacion))
            {
                throw new InvalidOperationException(
                    $"Ya existe un elemento con la identificación '{item.Identificacion}'");
            }

            // Agrega el elemento al diccionario
            items.Add(item.Identificacion, item);

            Console.WriteLine($"✓ Elemento agregado: {item.Identificacion}");
        }

       
        
      
        public bool Eliminar(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentException("La identificación no puede estar vacía");

            bool eliminado = items.Remove(id);

            if (eliminado)
                Console.WriteLine($"Elemento eliminado: {id}");
            else
                Console.WriteLine($" No se encontró elemento con ID: {id}");

            return eliminado;
        }

     
        // Busca un elemento por su identificación.
        // Retorna el elemento si lo encuentra, null si no existe.
        
        public T BuscarPorId(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentException("La identificación no puede estar vacía");

            // TryGetValue es más eficiente que ContainsKey + indexador
            if (items.TryGetValue(id, out T elemento))
            {
                return elemento;
            }

            return default(T); // Retorna null para tipos de referencia
        }

        
        // Obtiene todos los elementos del repositorio.
        // Retorna una lista con todos los valores.
       
        // <returns>Lista con todos los elementos</returns>
        public List<T> ObtenerTodos()
        {
            // Convierte los valores del diccionario a una lista
            return items.Values.ToList();
        }

      
        // Busca elementos que cumplan con un predicado (condición).
        // Usa un delegate Func para permitir búsquedas flexibles.
       
        // DELEGATES: Son punteros a métodos. Func<T, bool> es un delegate que:
        // - Recibe un parámetro de tipo T
        // - Retorna un bool (true/false)
       
        
        public List<T> Buscar(Func<T, bool> predicado)
        {
            if (predicado == null)
                throw new ArgumentNullException(nameof(predicado),
                    "El predicado no puede ser null");

            // Usa LINQ para filtrar elementos
            return items.Values.Where(predicado).ToList();
        }

      
        // Verifica si existe un elemento con la identificación dada.
       
        
        public bool Existe(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return false;

            return items.ContainsKey(id);
        }

       
        // Obtiene la cantidad de elementos en el repositorio.
      
        public int Cantidad => items.Count;

        // Limpia todos los elementos del repositorio.
        
        public void LimpiarTodo()
        {
            int cantidad = items.Count;
            items.Clear();
            Console.WriteLine($"✓ Repositorio limpiado. {cantidad} elementos eliminados.");
        }

        
        // Actualiza un elemento existente.
        
        // <param name="item">Elemento actualizado</param>
        // <returns>true si se actualizó, false si no existía</returns>
        public bool Actualizar(T item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            if (!items.ContainsKey(item.Identificacion))
                return false;

            items[item.Identificacion] = item;
            Console.WriteLine($"✓ Elemento actualizado: {item.Identificacion}");
            return true;
        }
    }
}
