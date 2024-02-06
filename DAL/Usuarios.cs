using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Usuarios
    {
        public int idusuario { get; set; }
        public string documento { get; set; }
        public string nombres { get; set; }
        public string apellidos { get; set; }
        public string nombreusuario { get; set; }
        public string correo { get; set; }
        public string clave { get; set; }
        public Nivelacceso oNivelacceso { get; set; }
        public int estado { get; set; }
        public string fecharegistro { get; set; }
    }
}
