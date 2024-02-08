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
    public partial class DashboardAdmin : Form
    {
        private static Usuarios usuarioActual;
        private Form activeForm = null;
        
        public DashboardAdmin(Usuarios objusuario = null)
        {
            InitializeComponent();
            usuarioActual = objusuario;
            hideSubmenu();
        }

        /*diseño de menu y abrir formularios*/
        private void hideSubmenu()
        {
            panelmenucompras.Visible = false;
        }

        private void ShowSubmenu(Panel submenu)
        {
            if(submenu.Visible == false)
            {
                hideSubmenu();
                submenu.Visible = true;
            }else
                submenu.Visible = false;
        }

        private void abrirFormulario (Form FormularioAbierto)
        {
            if (ActiveForm != null)
                activeForm.Close();
            activeForm = FormularioAbierto;
            FormularioAbierto.TopLevel = false;
            FormularioAbierto.FormBorderStyle = FormBorderStyle.None;
            FormularioAbierto.Dock = DockStyle.Fill;
            panelformularios.Controls.Add(FormularioAbierto);
            panelformularios.Tag = FormularioAbierto;
            FormularioAbierto.BringToFront();
            FormularioAbierto.Show();
        }

        /*fin de menu y abrir formulario*/
        private void btnmenucompras_Click(object sender, EventArgs e)
        {
            ShowSubmenu(panelmenucompras);
        }

        private void btnusuarios_Click(object sender, EventArgs e)
        {
            abrirFormulario(new Frmusuarios());
            hideSubmenu();
        }

    }
}
