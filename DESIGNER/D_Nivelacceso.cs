using BOL;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DESIGNER
{
    public class D_Nivelacceso
    {
        private B_Nivelacceso objd_nivelacceso = new B_Nivelacceso();
        public List<Nivelacceso> Listar()
        {
            return objd_nivelacceso.Listar();
        }
    }
}
