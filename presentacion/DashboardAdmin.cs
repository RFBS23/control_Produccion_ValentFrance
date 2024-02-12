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
    public partial class DashboardAdmin : Form
    {
        private static Usuarios usuarioActual;
        
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

        private Form activeForm = null;
        private void openChildFormInPanel(Form childForm)
        {
            if (activeForm != null)
                activeForm.Close();
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelformularios.Controls.Add(childForm);
            panelformularios.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        /*fin de menu y abrir formulario*/
        private void btnmenucompras_Click(object sender, EventArgs e)
        {
            ShowSubmenu(panelmenucompras);
        }

        private void btnusuarios_Click(object sender, EventArgs e)
        {
            openChildFormInPanel(new Frmusuarios());
            hideSubmenu();
        }

        private void DashboardAdmin_Load(object sender, EventArgs e)
        {
            lblnombres.Text = usuarioActual.nombres;
            lblapellidos.Text = usuarioActual.apellidos;
            lblnivacceso.Text = usuarioActual.oNivelacceso.nombre;
            lblestado.Text = usuarioActual.estado ? "Activo" : "Inactivo";
            lblusuario.Text = usuarioActual.nombreusuario;

            /*hora*/
            ActualizarHora();
            // Puedes configurar un temporizador para actualizar la hora periódicamente
            Timer timer = new Timer();
            timer.Interval = 1000; // Intervalo en milisegundos (1 segundo)
            timer.Tick += timer1_Tick;
            timer.Start();
            /*fin hora*/

            MostrarFechaActual();
        }

        private void MostrarFechaActual()
        {
            // Obtener la fecha actual
            DateTime fechaActual = DateTime.Now;

            // Mostrar la fecha en el formato deseado (día, mes, año)
            lblfecha.Text = $"{fechaActual.Day}/{fechaActual.Month}/{fechaActual.Year}";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            ActualizarHora();
        }
        private void ActualizarHora()
        {
            DateTime horaActual = DateTime.Now;
            // Muestra la hora en el Label
            lblhora.Text = $"{horaActual.ToString("HH:mm:ss")}";
        }

        private void DashboardAdmin_FormClosed(object sender, FormClosedEventArgs e)
        {
            login iniciosesion = new login();
            iniciosesion.Show();
            this.Hide();
        }

    }
}
