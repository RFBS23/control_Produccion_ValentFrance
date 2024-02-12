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
            Usuarios usuariologin = new D_Usuarios().IniciarSesion().Where(u => u.correo == txtcorreo.Text && u.clave == txtclave.Text).FirstOrDefault();
            if (usuariologin != null)
            {
                if (usuariologin.oNivelacceso != null)
                {
                    switch (usuariologin.oNivelacceso.nombre.ToLower())
                    {
                        case "administrador":
                            // Usuario con nivel de acceso 'admin', muestra el Dashboard para admin
                            DashboardAdmin dashboardAdmin = new DashboardAdmin(usuariologin);
                            dashboardAdmin.Show();
                            break;

                        case "cliente":
                            // Usuario con nivel de acceso 'cliente', muestra el Dashboard para clientes
                            DashboardCliente dashboardCliente = new DashboardCliente(usuariologin);
                            dashboardCliente.Show();
                            break;

                        default:
                            // Otros casos o niveles de acceso no manejados
                            MessageBox.Show("Nivel de acceso no manejado.", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                    }
                }
                else
                {
                    // Manejo para situaciones donde el nivel de acceso es nulo
                    MessageBox.Show("Error en los datos de acceso.", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                // Cerrar el formulario actual
                this.Hide();
            }
            else
            {
                MessageBox.Show("Error al Iniciar Sesión", "ALERTA", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                Usuarios usuariologin = new D_Usuarios().IniciarSesion().Where(u => u.correo == txtcorreo.Text && u.clave == txtclave.Text).FirstOrDefault();
                if (usuariologin != null)
                {
                    if (usuariologin.oNivelacceso != null)
                    {
                        switch (usuariologin.oNivelacceso.nombre.ToLower())
                        {
                            case "administrador":
                                // Usuario con nivel de acceso 'admin', muestra el Dashboard para admin
                                DashboardAdmin dashboardAdmin = new DashboardAdmin(usuariologin);
                                dashboardAdmin.Show();
                                break;

                            case "cliente":
                                // Usuario con nivel de acceso 'cliente', muestra el Dashboard para clientes
                                DashboardCliente dashboardCliente = new DashboardCliente(usuariologin);
                                dashboardCliente.Show();
                                break;

                            default:
                                // Otros casos o niveles de acceso no manejados
                                MessageBox.Show("Nivel de acceso no manejado.", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                        }
                    }
                    else
                    {
                        // Manejo para situaciones donde el nivel de acceso es nulo
                        MessageBox.Show("Error en los datos de acceso.", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    // Cerrar el formulario actual
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Error al Iniciar Sesión", "ALERTA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                e.Handled = true;
            }
        }

        private void login_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void login_Load(object sender, EventArgs e)
        {

        }
    }
}
