using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_de_Gestión_Universitaria
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class ValidacionRangoAttribute : Attribute
    {
        public decimal Minimo { get; set; }
        public decimal Maximo { get; set; }
        public string MensajeError { get; set; }

        public ValidacionRangoAttribute(double minimo, double maximo)
        {
            Minimo = (decimal)minimo;
            Maximo = (decimal)maximo;
        }
    }

    /// <summary>
    /// Atributo para campos requeridos
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class RequeridoAttribute : Attribute
    {
        public string MensajeError { get; set; }

        public RequeridoAttribute()
        {
            MensajeError = "Este campo es requerido";
        }
    }

    /// <summary>
    /// Atributo para validar formato con regex
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class FormatoAttribute : Attribute
    {
        public string Patron { get; set; }
        public string Descripcion { get; set; }
        public string MensajeError { get; set; }

        public FormatoAttribute(string patron, string descripcion = "")
        {
            Patron = patron;
            Descripcion = descripcion;
        }
    }
}
