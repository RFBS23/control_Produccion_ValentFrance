using BOL;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DESIGNER
{
    public class D_Usuarios
    {
        private B_Usuarios obj_usuarios = new B_Usuarios();

        public List<Usuarios> IniciarSesion()
        {
            return obj_usuarios.IniciarSesion();
        }

        public List<Usuarios> Listar()
        {
            return obj_usuarios.Listar();
        }

        public int Registrar(Usuarios obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            if (obj.documento == "")
            {
                Mensaje += "Es necesario el numero de documento \n";
            }
            if (obj.nombres == "")
            {
                Mensaje += "Es necesario el numero de documento \n";
            }
            if (obj.apellidos == "")
            {
                Mensaje += "Es necesario el numero de documento \n";
            }
            if (obj.nombreusuario == "")
            {
                Mensaje += "Es necesario el nombre completo del usuario \n";
            }
            if (obj.clave == "")
            {
                Mensaje += "Es necesario la clave del usuario \n";
            }
            if (Mensaje != string.Empty)
            {
                return 0;
            }
            else
            {
                return obj_usuarios.Registrar(obj, out Mensaje);
            }
        }

        public bool Editar(Usuarios obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (obj.documento == "")
            {
                Mensaje += "Es necesario el documento de identificacion del usuario\n";
            }
            if (obj.nombres == "")
            {
                Mensaje += "Es necesario el numero de documento \n";
            }
            if (obj.apellidos == "")
            {
                Mensaje += "Es necesario el numero de documento \n";
            }
            if (obj.nombreusuario == "")
            {
                Mensaje += "Es necesario el nombre completo del usuario \n";
            }
            if (obj.clave == "")
            {
                Mensaje += "Es necesario la clave del usuario \n";
            }
            if (Mensaje != string.Empty)
            {
                return false;
            }
            else
            {
                return obj_usuarios.Editar(obj, out Mensaje);
            }
        }

    }
}
