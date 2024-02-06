using DAL;
using DESIGNER;
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
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
            txtcorreo.Select();

            txtcorreo.KeyDown += txtcorreo_KeyDown;
        }

        private void btniniciarsesion_Click(object sender, EventArgs e)
        {
            Usuarios usuariologin = new D_Usuarios().Listar().Where(u => u.correo == txtcorreo.Text && u.clave == txtclave.Text).FirstOrDefault();
            if (usuariologin != null)
            {
                Dashboard inicio = new Dashboard(usuariologin);
                inicio.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Credenciales incorrectas. Verifique su correo y/o clave.", "ALERTA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtcorreo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtclave.Focus();
                e.Handled = true;
            }
        }

        private void txtclave_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Usuarios usuariologin = new D_Usuarios().Listar().Where(u => u.correo == txtcorreo.Text && u.clave == txtclave.Text).FirstOrDefault();
                if (usuariologin != null)
                {
                    Dashboard inicio = new Dashboard(usuariologin);
                    inicio.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Credenciales incorrectas. Verifique su correo y/o clave.", "ALERTA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                e.Handled = true;
            }
        }

        private void login_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

    }
}
