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
        public List<Usuarios> Listar()
        {
            return obj_usuarios.Listar();
        }
    }

}
