using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Formulario_4
{
    public class Registros
    {
        public Guid IdEncargado { get; set; }
        public DateTime fecha { get; set; }
        public int horas { get; set; }
    }

    public class Actividades {
        public Guid id { get; set; }
        public string Horas { get; set; }
        public string FechaInicio { get; set; }
        public string FechaFinal { get; set; }
        public string Proyecto { get; set; }
        public string Cliente { get; set; }
        public string Actividad { get; set; }
    }
}