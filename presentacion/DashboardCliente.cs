using DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace presentacion
{
    public partial class DashboardCliente : Form
    {
        private static Usuarios usuarioActual;
        public DashboardCliente(Usuarios objusuario = null)
        {
            InitializeComponent();
            usuarioActual = objusuario;
        }

        private void DashboardCliente_Load(object sender, EventArgs e)
        {

        }
    }
}
