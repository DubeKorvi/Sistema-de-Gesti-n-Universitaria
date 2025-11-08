using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace Sistema_de_Gestión_Universitaria
{
    public enum RolUsuario
    {
        Administrador,
        Coordinador,
        Profesor,
        Asistente
    }

    public class Usuario
    {
        public string NombreUsuario { get; set; }
        public string HashContrasena { get; set; }
        public RolUsuario Rol { get; set; }
        public string NombreCompleto { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? UltimoAcceso { get; set; }
        public bool Activo { get; set; }

        public Usuario()
        {
            FechaCreacion = DateTime.Now;
            Activo = true;
        }
    }

    public class SistemaAutenticacion
    {
        private static readonly string ArchivoUsuarios = Path.Combine("Datos", "usuarios.json");
        private List<Usuario> _usuarios;
        public Usuario UsuarioActual { get; private set; }
        private SistemaLogs _logs;

        public bool EstaAutenticado => UsuarioActual != null;

        public SistemaAutenticacion()
        {
            _logs = SistemaLogs.Instancia;
            CargarUsuarios();

            // Crear usuario administrador por defecto si no existe
            if (_usuarios.Count == 0)
            {
                CrearUsuarioInicial();
            }
        }

        private void CrearUsuarioInicial()
        {
            var admin = new Usuario
            {
                NombreUsuario = "admin",
                HashContrasena = HashearContrasena("admin123"),
                Rol = RolUsuario.Administrador,
                NombreCompleto = "Administrador del Sistema"
            };

            _usuarios.Add(admin);
            GuardarUsuarios();
            _logs.Info("Usuario administrador inicial creado", "AUTENTICACION");
        }

       

        public bool IniciarSesion(string nombreUsuario, string contrasena)
        {
            try
            {
                var usuario = _usuarios.Find(u =>
                    u.NombreUsuario.Equals(nombreUsuario, StringComparison.OrdinalIgnoreCase) &&
                    u.Activo);

                if (usuario == null)
                {
                    _logs.Advertencia($"Intento de login fallido: usuario '{nombreUsuario}' no encontrado", "AUTENTICACION");
                    return false;
                }

                string hashIngresado = HashearContrasena(contrasena);
                if (usuario.HashContrasena != hashIngresado)
                {
                    _logs.Advertencia($"Intento de login fallido: contraseña incorrecta para '{nombreUsuario}'", "AUTENTICACION");
                    return false;
                }

                UsuarioActual = usuario;
                usuario.UltimoAcceso = DateTime.Now;
                GuardarUsuarios();

                _logs.Exito($"Usuario '{nombreUsuario}' ({usuario.Rol}) inició sesión", "AUTENTICACION");
                return true;
            }
            catch (Exception ex)
            {
                _logs.Error($"Error en inicio de sesión: {ex.Message}", "AUTENTICACION");
                return false;
            }
        }

        public void CerrarSesion()
        {
            if (UsuarioActual != null)
            {
                _logs.Info($"Usuario '{UsuarioActual.NombreUsuario}' cerró sesión", "AUTENTICACION");
                UsuarioActual = null;
            }
        }

        public bool TienePermiso(RolUsuario rolMinimo)
        {
            if (!EstaAutenticado)
                return false;

            return UsuarioActual.Rol <= rolMinimo;
        }

       

        public bool CrearUsuario(string nombreUsuario, string contrasena, RolUsuario rol, string nombreCompleto)
        {
            try
            {
                // Verificar que no exista el usuario
                if (_usuarios.Exists(u => u.NombreUsuario.Equals(nombreUsuario, StringComparison.OrdinalIgnoreCase)))
                {
                    _logs.Advertencia($"Intento de crear usuario duplicado: {nombreUsuario}", "USUARIOS");
                    return false;
                }

                var nuevoUsuario = new Usuario
                {
                    NombreUsuario = nombreUsuario,
                    HashContrasena = HashearContrasena(contrasena),
                    Rol = rol,
                    NombreCompleto = nombreCompleto
                };

                _usuarios.Add(nuevoUsuario);
                GuardarUsuarios();

                _logs.Exito($"Usuario creado: {nombreUsuario} ({rol})", "USUARIOS");
                return true;
            }
            catch (Exception ex)
            {
                _logs.Error($"Error al crear usuario: {ex.Message}", "USUARIOS");
                return false;
            }
        }

        public bool CambiarContrasena(string nombreUsuario, string contrasenaActual, string contrasenaNueva)
        {
            try
            {
                var usuario = _usuarios.Find(u => u.NombreUsuario.Equals(nombreUsuario, StringComparison.OrdinalIgnoreCase));

                if (usuario == null)
                    return false;

                string hashActual = HashearContrasena(contrasenaActual);
                if (usuario.HashContrasena != hashActual)
                {
                    _logs.Advertencia($"Intento fallido de cambio de contraseña para '{nombreUsuario}'", "USUARIOS");
                    return false;
                }

                usuario.HashContrasena = HashearContrasena(contrasenaNueva);
                GuardarUsuarios();

                _logs.Info($"Contraseña cambiada para usuario '{nombreUsuario}'", "USUARIOS");
                return true;
            }
            catch (Exception ex)
            {
                _logs.Error($"Error al cambiar contraseña: {ex.Message}", "USUARIOS");
                return false;
            }
        }

        public bool DesactivarUsuario(string nombreUsuario)
        {
            try
            {
                var usuario = _usuarios.Find(u => u.NombreUsuario.Equals(nombreUsuario, StringComparison.OrdinalIgnoreCase));

                if (usuario == null || usuario.Rol == RolUsuario.Administrador)
                    return false;

                usuario.Activo = false;
                GuardarUsuarios();

                _logs.Advertencia($"Usuario desactivado: {nombreUsuario}", "USUARIOS");
                return true;
            }
            catch (Exception ex)
            {
                _logs.Error($"Error al desactivar usuario: {ex.Message}", "USUARIOS");
                return false;
            }
        }

        public List<Usuario> ObtenerUsuarios()
        {
            return new List<Usuario>(_usuarios);
        }

      

        private string HashearContrasena(string contrasena)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(contrasena));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }


        private void CargarUsuarios()
        {
            try
            {
                if (!Directory.Exists("Datos"))
                {
                    Directory.CreateDirectory("Datos");
                }

                if (File.Exists(ArchivoUsuarios))
                {
                    string json = File.ReadAllText(ArchivoUsuarios);
                    _usuarios = JsonSerializer.Deserialize<List<Usuario>>(json) ?? new List<Usuario>();
                }
                else
                {
                    _usuarios = new List<Usuario>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al cargar usuarios: {ex.Message}");
                _usuarios = new List<Usuario>();
            }
        }

        private void GuardarUsuarios()
        {
            try
            {
                var opciones = new JsonSerializerOptions { WriteIndented = true };
                string json = JsonSerializer.Serialize(_usuarios, opciones);
                File.WriteAllText(ArchivoUsuarios, json);
            }
            catch (Exception ex)
            {
                _logs.Error($"Error al guardar usuarios: {ex.Message}", "USUARIOS");
            }
        }

        
        public bool MostrarPantallaLogin()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n");
            Console.WriteLine("╔══════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                                                          ║");
            Console.WriteLine("║          SISTEMA DE GESTIÓN UNIVERSITARIA               ║");
            Console.WriteLine("║                                                          ║");
            Console.WriteLine("║                  INICIO DE SESIÓN                        ║");
            Console.WriteLine("║                                                          ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════════╝\n");
            Console.ResetColor();

            Console.WriteLine("  Usuario por defecto: admin");
            Console.WriteLine("  Contraseña por defecto: admin123\n");
            Console.WriteLine(new string('─', 58));

            int intentos = 0;
            const int maxIntentos = 3;

            while (intentos < maxIntentos)
            {
                Console.Write("\n  Usuario: ");
                string usuario = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(usuario))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("  ✗ El usuario no puede estar vacío");
                    Console.ResetColor();
                    continue;
                }

                Console.Write("  Contraseña: ");
                string contrasena = LeerContrasenaOculta();

                if (IniciarSesion(usuario, contrasena))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"\n  ✓ Bienvenido, {UsuarioActual.NombreCompleto}!");
                    Console.WriteLine($"  Rol: {UsuarioActual.Rol}");
                    Console.ResetColor();
                    Console.WriteLine("\n  Presione Enter para continuar...");
                    Console.ReadLine();
                    return true;
                }
                else
                {
                    intentos++;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"\n  ✗ Usuario o contraseña incorrectos");
                    Console.WriteLine($"  Intentos restantes: {maxIntentos - intentos}");
                    Console.ResetColor();

                    if (intentos >= maxIntentos)
                    {
                        Console.WriteLine("\n  Demasiados intentos fallidos. Saliendo...");
                        System.Threading.Thread.Sleep(2000);
                        return false;
                    }
                }
            }

            return false;
        }

        private string LeerContrasenaOculta()
        {
            StringBuilder contrasena = new StringBuilder();
            ConsoleKeyInfo tecla;

            do
            {
                tecla = Console.ReadKey(true);

                if (tecla.Key == ConsoleKey.Backspace && contrasena.Length > 0)
                {
                    contrasena.Remove(contrasena.Length - 1, 1);
                    Console.Write("\b \b");
                }
                else if (!char.IsControl(tecla.KeyChar))
                {
                    contrasena.Append(tecla.KeyChar);
                    Console.Write("*");
                }
            }
            while (tecla.Key != ConsoleKey.Enter);

            Console.WriteLine();
            return contrasena.ToString();
        }

        
    }
}
