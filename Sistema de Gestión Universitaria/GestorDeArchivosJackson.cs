using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Sistema_de_Gestión_Universitaria
{
    public class GestorArchivosJSON
    {
        private static readonly string DirectorioBase = "Datos";
        private static readonly JsonSerializerOptions OpcionesJson = new JsonSerializerOptions
        {
            WriteIndented = true,
            PropertyNameCaseInsensitive = true,
            Converters = { new JsonStringEnumConverter() }
        };

        public GestorArchivosJSON()
        {
            if (!Directory.Exists(DirectorioBase))
            {
                Directory.CreateDirectory(DirectorioBase);
            }
        }

        public bool GuardarEstudiantes(List<Estudiante> estudiantes)
        {
            try
            {
                string archivo = Path.Combine(DirectorioBase, "estudiantes.json");
                string json = JsonSerializer.Serialize(estudiantes, OpcionesJson);
                File.WriteAllText(archivo, json);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al guardar estudiantes: {ex.Message}");
                return false;
            }
        }

        public bool GuardarProfesores(List<Profesor> profesores)
        {
            try
            {
                string archivo = Path.Combine(DirectorioBase, "profesores.json");
                string json = JsonSerializer.Serialize(profesores, OpcionesJson);
                File.WriteAllText(archivo, json);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al guardar profesores: {ex.Message}");
                return false;
            }
        }

        public bool GuardarCursos(List<Curso> cursos)
        {
            try
            {
                string archivo = Path.Combine(DirectorioBase, "cursos.json");
                string json = JsonSerializer.Serialize(cursos, OpcionesJson);
                File.WriteAllText(archivo, json);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al guardar cursos: {ex.Message}");
                return false;
            }
        }

        public bool GuardarMatriculas(List<Matricula> matriculas)
        {
            try
            {
                string archivo = Path.Combine(DirectorioBase, "matriculas.json");
                string json = JsonSerializer.Serialize(matriculas, OpcionesJson);
                File.WriteAllText(archivo, json);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al guardar matrículas: {ex.Message}");
                return false;
            }
        }

        public List<Estudiante> CargarEstudiantes()
        {
            try
            {
                string archivo = Path.Combine(DirectorioBase, "estudiantes.json");
                if (!File.Exists(archivo))
                    return new List<Estudiante>();

                string json = File.ReadAllText(archivo);
                return JsonSerializer.Deserialize<List<Estudiante>>(json, OpcionesJson) ?? new List<Estudiante>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al cargar estudiantes: {ex.Message}");
                return new List<Estudiante>();
            }
        }

        public List<Profesor> CargarProfesores()
        {
            try
            {
                string archivo = Path.Combine(DirectorioBase, "profesores.json");
                if (!File.Exists(archivo))
                    return new List<Profesor>();

                string json = File.ReadAllText(archivo);
                return JsonSerializer.Deserialize<List<Profesor>>(json, OpcionesJson) ?? new List<Profesor>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al cargar profesores: {ex.Message}");
                return new List<Profesor>();
            }
        }

        public List<Curso> CargarCursos()
        {
            try
            {
                string archivo = Path.Combine(DirectorioBase, "cursos.json");
                if (!File.Exists(archivo))
                    return new List<Curso>();

                string json = File.ReadAllText(archivo);
                return JsonSerializer.Deserialize<List<Curso>>(json, OpcionesJson) ?? new List<Curso>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al cargar cursos: {ex.Message}");
                return new List<Curso>();
            }
        }

        public List<Matricula> CargarMatriculas()
        {
            try
            {
                string archivo = Path.Combine(DirectorioBase, "matriculas.json");
                if (!File.Exists(archivo))
                    return new List<Matricula>();

                string json = File.ReadAllText(archivo);
                return JsonSerializer.Deserialize<List<Matricula>>(json, OpcionesJson) ?? new List<Matricula>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al cargar matrículas: {ex.Message}");
                return new List<Matricula>();
            }
        }

        public bool GuardarTodosDatos(
            List<Estudiante> estudiantes,
            List<Profesor> profesores,
            List<Curso> cursos,
            List<Matricula> matriculas)
        {
            bool exito = true;
            exito &= GuardarEstudiantes(estudiantes);
            exito &= GuardarProfesores(profesores);
            exito &= GuardarCursos(cursos);
            exito &= GuardarMatriculas(matriculas);
            return exito;
        }

        public bool ExportarBackup(string nombreArchivo = null)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(nombreArchivo))
                {
                    nombreArchivo = $"backup_{DateTime.Now:yyyyMMdd_HHmmss}.json";
                }

                string rutaBackup = Path.Combine(DirectorioBase, "Backups");
                if (!Directory.Exists(rutaBackup))
                {
                    Directory.CreateDirectory(rutaBackup);
                }

                var backup = new
                {
                    FechaBackup = DateTime.Now,
                    Estudiantes = CargarEstudiantes(),
                    Profesores = CargarProfesores(),
                    Cursos = CargarCursos(),
                    Matriculas = CargarMatriculas()
                };

                string archivo = Path.Combine(rutaBackup, nombreArchivo);
                string json = JsonSerializer.Serialize(backup, OpcionesJson);
                File.WriteAllText(archivo, json);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear backup: {ex.Message}");
                return false;
            }
        }
    }
}