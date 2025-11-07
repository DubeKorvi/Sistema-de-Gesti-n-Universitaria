using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sistema_de_Gestión_Universitaria;

namespace Sistema_de_Gestión_Universitaria
{
    public class Profesor : Persona
    {
        private string departamento;
        private TipoContrato tipoContrato;
        private decimal salarioBase;

    
        // Departamento al que pertenece el profesor.
        
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

      
        // Tipo de contrato del profesor (Tiempo Completo, Medio Tiempo, etc.)
        
        public TipoContrato TipoContrato
        {
            get => tipoContrato;
            set => tipoContrato = value;
        }

        
        // Salario base mensual del profesor.
        // Debe ser mayor a 0.
        
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

        
        // Constructor que valida la edad mínima para profesores (25 años).
        
        public Profesor(string identificacion, string nombre, string apellido,
                       DateTime fechaNacimiento, string departamento,
                       TipoContrato tipoContrato, decimal salarioBase)
        {
            // Asignamos las propiedades heredadas
            Identificacion = identificacion;
            Nombre = nombre;
            Apellido = apellido;
            FechaNacimiento = fechaNacimiento;

            // Validación específica: edad mínima 25 años
            if (Edad < 25)
                throw new ArgumentException("El profesor debe tener al menos 25 años");

            // Asignamos las propiedades propias
            Departamento = departamento;
            TipoContrato = tipoContrato;
            SalarioBase = salarioBase;
        }

        
        // Implementación del método abstracto ObtenerRol.
        
        public override string ObtenerRol()
        {
            return "PROFESOR";
        }

       
        /// Sobrescritura de ToString para incluir información específica del profesor.
       
        public override string ToString()
        {
            return base.ToString() +
                   $"\n  Departamento: {Departamento} - Contrato: {TipoContrato} - Salario: ${SalarioBase:N2}";
        }
    }
}



