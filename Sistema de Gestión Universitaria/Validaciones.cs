using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_de_Gestión_Universitaria
{

    public class EstudianteConValidacion : Persona
    {
        private string carrera;
        private string numeroMatricula;

        [Requerido(MensajeError = "La carrera es obligatoria")]
        public string Carrera
        {
            get => carrera;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("La carrera no puede estar vacía");
                carrera = value;
            }
        }

        [Requerido]
        [Formato(@"^\d{4}-\d{3}$", "Formato esperado: YYYY-NNN (ej: 2024-001)",
                MensajeError = "El formato de matrícula es inválido")]
        public string NumeroMatricula
        {
            get => numeroMatricula;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("El número de matrícula no puede estar vacío");
                numeroMatricula = value;
            }
        }

        public EstudianteConValidacion(string identificacion, string nombre, string apellido,
                                      DateTime fechaNacimiento, string carrera, string numeroMatricula)
        {
            Identificacion = identificacion;
            Nombre = nombre;
            Apellido = apellido;
            FechaNacimiento = fechaNacimiento;

            if (Edad < 15)
                throw new ArgumentException("El estudiante debe tener al menos 15 años");

            Carrera = carrera;
            NumeroMatricula = numeroMatricula;
        }

        public override string ObtenerRol()
        {
            return "ESTUDIANTE";
        }
    }



    public class ProfesorConValidacion : Persona
    {
        private string departamento;
        private TipoContrato tipoContrato;
        private decimal salarioBase;

        [Requerido(MensajeError = "El departamento es obligatorio")]
        public string Departamento
        {
            get => departamento;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("El departamento no puede estar vacío");
                departamento = value;
            }
        }

        public TipoContrato TipoContrato
        {
            get => tipoContrato;
            set => tipoContrato = value;
        }

        [ValidacionRango(500, 10000, MensajeError = "El salario debe estar entre $500 y $10,000")]
        public decimal SalarioBase
        {
            get => salarioBase;
            set
            {
                if (value <= 0)
                    throw new ArgumentException("El salario debe ser mayor a 0");
                salarioBase = value;
            }
        }

        public ProfesorConValidacion(string identificacion, string nombre, string apellido,
                                    DateTime fechaNacimiento, string departamento,
                                    TipoContrato tipoContrato, decimal salarioBase)
        {
            Identificacion = identificacion;
            Nombre = nombre;
            Apellido = apellido;
            FechaNacimiento = fechaNacimiento;

            if (Edad < 25)
                throw new ArgumentException("El profesor debe tener al menos 25 años");

            Departamento = departamento;
            TipoContrato = tipoContrato;
            SalarioBase = salarioBase;
        }

        public override string ObtenerRol()
        {
            return "PROFESOR";
        }
    }


    public class CursoConValidacion : IIdentificable
    {
        private string codigo;
        private string nombre;
        private int creditos;

        [Requerido]
        [Formato(@"^[A-Z]{3}-\d{3}$", "Formato: ABC-123 (ej: MAT-101)",
                MensajeError = "El código debe seguir el formato ABC-123")]
        public string Codigo
        {
            get => codigo;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("El código no puede estar vacío");
                codigo = value;
            }
        }

        public string Identificacion => Codigo;

        [Requerido(MensajeError = "El nombre del curso es obligatorio")]
        public string Nombre
        {
            get => nombre;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("El nombre no puede estar vacío");
                nombre = value;
            }
        }

        [ValidacionRango(1, 6, MensajeError = "Los créditos deben estar entre 1 y 6")]
        public int Creditos
        {
            get => creditos;
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Los créditos deben ser mayores a 0");
                creditos = value;
            }
        }

        public Profesor ProfesorAsignado { get; set; }

        public CursoConValidacion(string codigo, string nombre, int creditos,
                                 Profesor profesorAsignado = null)
        {
            Codigo = codigo;
            Nombre = nombre;
            Creditos = creditos;
            ProfesorAsignado = profesorAsignado;
        }
    }
}

