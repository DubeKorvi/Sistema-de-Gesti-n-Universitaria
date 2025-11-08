using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_de_Gestión_Universitaria
{
    public enum NivelLog
    {
        INFO,
        ADVERTENCIA,
        ERROR,
        EXITO
    }

    public class SistemaLogs
    {
        private static readonly string DirectorioLogs = "Logs";
        private static readonly string ArchivoLogActual = Path.Combine(DirectorioLogs, $"log_{DateTime.Now:yyyyMMdd}.txt");
        private static SistemaLogs _instancia;
        private static readonly object _lock = new object();

        public static SistemaLogs Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    lock (_lock)
                    {
                        if (_instancia == null)
                        {
                            _instancia = new SistemaLogs();
                        }
                    }
                }
                return _instancia;
            }
        }

        private SistemaLogs()
        {
            // Crear directorio de logs si no existe
            if (!Directory.Exists(DirectorioLogs))
            {
                Directory.CreateDirectory(DirectorioLogs);
            }
        }

        public void RegistrarLog(string mensaje, NivelLog nivel = NivelLog.INFO, string categoria = "GENERAL")
        {
            try
            {
                string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                string nivelStr = nivel.ToString().PadRight(12);
                string categoriaStr = categoria.PadRight(20);
                string lineaLog = $"[{timestamp}] [{nivelStr}] [{categoriaStr}] {mensaje}";

                // Escribir en archivo
                lock (_lock)
                {
                    File.AppendAllText(ArchivoLogActual, lineaLog + Environment.NewLine);
                }

                // También mostrar en consola si es nivel ERROR
                if (nivel == NivelLog.ERROR)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"LOG ERROR: {mensaje}");
                    Console.ResetColor();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al registrar log: {ex.Message}");
            }
        }

      

        public void Info(string mensaje, string categoria = "GENERAL")
        {
            RegistrarLog(mensaje, NivelLog.INFO, categoria);
        }

        public void Advertencia(string mensaje, string categoria = "GENERAL")
        {
            RegistrarLog(mensaje, NivelLog.ADVERTENCIA, categoria);
        }

        public void Error(string mensaje, string categoria = "GENERAL")
        {
            RegistrarLog(mensaje, NivelLog.ERROR, categoria);
        }

        public void Exito(string mensaje, string categoria = "GENERAL")
        {
            RegistrarLog(mensaje, NivelLog.EXITO, categoria);
        }

        

        // Estudiantes
        public void LogEstudianteAgregado(string id, string nombre)
        {
            Exito($"Estudiante agregado: {nombre} (ID: {id})", "ESTUDIANTES");
        }

        public void LogEstudianteModificado(string id, string nombre)
        {
            Info($"Estudiante modificado: {nombre} (ID: {id})", "ESTUDIANTES");
        }

        public void LogEstudianteEliminado(string id, string nombre)
        {
            Advertencia($"Estudiante eliminado: {nombre} (ID: {id})", "ESTUDIANTES");
        }

        // Profesores
        public void LogProfesorAgregado(string id, string nombre)
        {
            Exito($"Profesor agregado: {nombre} (ID: {id})", "PROFESORES");
        }

        public void LogProfesorModificado(string id, string nombre)
        {
            Info($"Profesor modificado: {nombre} (ID: {id})", "PROFESORES");
        }

        public void LogProfesorEliminado(string id, string nombre)
        {
            Advertencia($"Profesor eliminado: {nombre} (ID: {id})", "PROFESORES");
        }

        // Cursos
        public void LogCursoAgregado(string codigo, string nombre)
        {
            Exito($"Curso agregado: {nombre} (Código: {codigo})", "CURSOS");
        }

        public void LogProfesorAsignado(string nombreProfesor, string nombreCurso)
        {
            Info($"Profesor {nombreProfesor} asignado al curso {nombreCurso}", "CURSOS");
        }

        // Matrículas
        public void LogMatriculaRealizada(string nombreEstudiante, string nombreCurso)
        {
            Exito($"Matrícula realizada: {nombreEstudiante} en {nombreCurso}", "MATRICULAS");
        }

        public void LogCalificacionRegistrada(string nombreEstudiante, string nombreCurso, double calificacion)
        {
            Info($"Calificación registrada: {nombreEstudiante} - {nombreCurso}: {calificacion:F1}", "CALIFICACIONES");
        }

        // Sistema
        public void LogInicioSistema()
        {
            Info("Sistema iniciado", "SISTEMA");
        }

        public void LogCierreSistema()
        {
            Info("Sistema cerrado", "SISTEMA");
        }

        public void LogBackupCreado(string nombreArchivo)
        {
            Exito($"Backup creado: {nombreArchivo}", "SISTEMA");
        }

        public void LogDatosCargados()
        {
            Info("Datos cargados desde archivos JSON", "SISTEMA");
        }

        public void LogDatosGuardados()
        {
            Exito("Datos guardados en archivos JSON", "SISTEMA");
        }

        

        public string ObtenerLogsRecientes(int cantidad = 50)
        {
            try
            {
                if (!File.Exists(ArchivoLogActual))
                    return "No hay logs disponibles para hoy.";

                var lineas = File.ReadAllLines(ArchivoLogActual);
                var sb = new StringBuilder();

                sb.AppendLine($"═══════════════════════════════════════════════════════════════════════════════");
                sb.AppendLine($"                    LOGS DEL SISTEMA - {DateTime.Now:dd/MM/yyyy}");
                sb.AppendLine($"═══════════════════════════════════════════════════════════════════════════════\n");

                int inicio = Math.Max(0, lineas.Length - cantidad);
                for (int i = inicio; i < lineas.Length; i++)
                {
                    sb.AppendLine(lineas[i]);
                }

                sb.AppendLine($"\n═══════════════════════════════════════════════════════════════════════════════");
                sb.AppendLine($"Total de entradas mostradas: {Math.Min(cantidad, lineas.Length)} de {lineas.Length}");

                return sb.ToString();
            }
            catch (Exception ex)
            {
                return $"Error al leer logs: {ex.Message}";
            }
        }

        public void LimpiarLogsAntiguos(int diasAMantener = 30)
        {
            try
            {
                var archivos = Directory.GetFiles(DirectorioLogs, "log_*.txt");
                DateTime fechaLimite = DateTime.Now.AddDays(-diasAMantener);

                foreach (var archivo in archivos)
                {
                    var info = new FileInfo(archivo);
                    if (info.CreationTime < fechaLimite)
                    {
                        File.Delete(archivo);
                        Info($"Log antiguo eliminado: {info.Name}", "MANTENIMIENTO");
                    }
                }
            }
            catch (Exception ex)
            {
                Error($"Error al limpiar logs antiguos: {ex.Message}", "MANTENIMIENTO");
            }
        }

        
    }
}
