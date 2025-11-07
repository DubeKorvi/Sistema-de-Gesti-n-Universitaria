using System;


namespace Sistema_de_Gestión_Universitaria
{
    public abstract class Persona : IIdentificable
    {
       
        private string identificacion;
        private string nombre;
        private string apellido;
        private DateTime fechaNacimiento;

       
        public string Identificacion
        {
            get => identificacion;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("La identificación no puede estar vacía");
                identificacion = value;
            }
        }

        
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

     
        public string Apellido
        {
            get => apellido;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("El apellido no puede estar vacío");
                apellido = value;
            }
        }

    
        public DateTime FechaNacimiento
        {
            get => fechaNacimiento;
            set
            {
                if (value > DateTime.Now)
                    throw new ArgumentException("La fecha de nacimiento no puede ser futura");
                fechaNacimiento = value;
            }
        }

       
        public int Edad
        {
            get
            {
                int edad = DateTime.Now.Year - fechaNacimiento.Year;

                
                if (DateTime.Now.Month < fechaNacimiento.Month ||
                    (DateTime.Now.Month == fechaNacimiento.Month &&
                     DateTime.Now.Day < fechaNacimiento.Day))
                {
                    edad--;
                }

                return edad;
            }
        }

        
        public abstract string ObtenerRol();

        
        public override string ToString()
        {
            return $"[{ObtenerRol()}] {Nombre} {Apellido} - ID: {Identificacion} - Edad: {Edad} años";
     
        }









    }

}


