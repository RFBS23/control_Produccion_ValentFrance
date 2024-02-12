using DAL;
using DESIGNER;
using presentacion.Utilidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace presentacion
{
    public partial class Frmusuarios : Form
    {        
        public Frmusuarios()
        {
            InitializeComponent();
        }

        private void Frmusuarios_Load(object sender, EventArgs e)
        {
            txtdocumento.Select();

            txtestado.Items.Add(new OpcionesComboBox() { Valor = 1, Texto = "Activo" });
            txtestado.Items.Add(new OpcionesComboBox() { Valor = 0, Texto = "Inactivo" });
            txtestado.DisplayMember = "Texto";
            txtestado.ValueMember = "Valor";
            txtestado.SelectedIndex = 0;

            List<Nivelacceso> listanivelesaccesos = new D_Nivelacceso().Listar();
            foreach (Nivelacceso item in listanivelesaccesos)
            {
                cbnivelacceso.Items.Add(new OpcionesComboBox() { Valor = item.idnivelacceso, Texto = item.nombre });
            }
            cbnivelacceso.DisplayMember = "Texto";
            cbnivelacceso.ValueMember = "Valor";
            cbnivelacceso.SelectedIndex = 0;
            
            foreach (DataGridViewColumn columna in dgUsuarios.Columns)
            {
                if (columna.Visible == true)
                {
                    listabuscar.Items.Add(new OpcionesComboBox() { Valor = columna.Name, Texto = columna.HeaderText });
                }
                listabuscar.DisplayMember = "Texto";
                listabuscar.ValueMember = "Valor";
                listabuscar.SelectedIndex = 0;
            }

            List<Usuarios> listaUsuario = new D_Usuarios().Listar();
            foreach (Usuarios item in listaUsuario)
            {
                dgUsuarios.Rows.Add(new object[] { "", item.idusuario, item.documento, item.nombres, item.apellidos, item.nombreusuario, item.correo, item.clave,
                    item.oNivelacceso.idnivelacceso, 
                    item.oNivelacceso.nombre, 
                    item.estado == true ? 1 : 0, 
                    item.estado == true ? "Activo" : "Inactivo" });
            }
        }

        private async void txtdocumento_TextChanged(object sender, EventArgs e)
        {
            if (txtdocumento.Text.Length == 8)
            {
                // URL de la API con el número de DNI
                string apiUrl = $"https://api.apis.net.pe/v1/dni?numero={txtdocumento.Text}";
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        // Realizar la solicitud a la API
                        HttpResponseMessage response = await client.GetAsync(apiUrl);

                        // Verificar si la solicitud fue exitosa
                        if (response.IsSuccessStatusCode)
                        {
                            // Leer la respuesta como cadena JSON
                            string jsonResponse = await response.Content.ReadAsStringAsync();

                            // Deserializar la respuesta JSON para obtener la información del DNI
                            dynamic json = Newtonsoft.Json.JsonConvert.DeserializeObject(jsonResponse);

                            if (json != null)
                            {
                                txtnombre.Text = $"{json.nombres}";
                                txtapellidos.Text = $"{json.apellidoPaterno} {json.apellidoMaterno}";
                            }
                            else
                            {
                                MessageBox.Show("La respuesta de la API no tiene el formato esperado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            MessageBox.Show($"No se ha podido encontrar el dni buscado..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al obtener la información del DNI: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnbuscar_Click(object sender, EventArgs e)
        {
            String columnaFiltro = ((OpcionesComboBox)listabuscar.SelectedItem).Valor.ToString();
            if (dgUsuarios.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dgUsuarios.Rows)
                {
                    if (row.Cells[columnaFiltro].Value.ToString().Trim().ToUpper().Contains(txtbusqueda.Text.Trim().ToUpper()))
                        row.Visible = true;
                    else
                        row.Visible = false;
                }
            }
        }

        private void Limpiar()
        {
            txtindice.Text = "-1";
            txtid.Text = "0";

            txtdocumento.Text = "";
            txtnombre.Text = "";
            txtapellidos.Text = "";
            txtnombreusuario.Text = "";
            txtcorreo.Text = "";
            txtclave.Text = "";
            txtestado.SelectedIndex = 0;
            cbnivelacceso.SelectedIndex = 0;

            txtdocumento.Enabled = true;

            txtdocumento.Select();
        }

        private void btnlimpiarbusqueda_Click(object sender, EventArgs e)
        {
            txtbusqueda.Text = "";
            foreach(DataGridViewRow row in dgUsuarios.Rows)
            {
                row.Visible = true;
            }
            txtbusqueda.Select();
        }

        private void btnagregar_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;

            Usuarios objusuario = new Usuarios()
            {
                idusuario = Convert.ToInt32(txtid.Text),
                documento = txtdocumento.Text,
                nombres = txtnombre.Text,
                apellidos = txtapellidos.Text,
                nombreusuario = txtnombreusuario.Text,
                correo = txtcorreo.Text,
                clave = txtclave.Text,
                oNivelacceso = new Nivelacceso() { idnivelacceso = Convert.ToInt32(((OpcionesComboBox)cbnivelacceso.SelectedItem).Valor) },
                estado = Convert.ToInt32(((OpcionesComboBox)txtestado.SelectedItem).Valor) == 1 ? true : false
            };

            if (btnagregar.Text == "Agregar")
            {
                int idusuariogenerado = new D_Usuarios().Registrar(objusuario, out mensaje);

                if (idusuariogenerado != 0)
                {
                    dgUsuarios.Rows.Add(new object[] {"", idusuariogenerado, txtdocumento.Text, txtnombre.Text, txtapellidos.Text, txtnombreusuario.Text, txtcorreo.Text, txtclave.Text,
                        ((OpcionesComboBox)cbnivelacceso.SelectedItem).Valor.ToString(),
                        ((OpcionesComboBox)cbnivelacceso.SelectedItem).Texto.ToString(),
                        ((OpcionesComboBox)txtestado.SelectedItem).Valor.ToString(),
                        ((OpcionesComboBox)txtestado.SelectedItem).Texto.ToString(),
                    });
                    Limpiar();
                }
                else
                {
                    MessageBox.Show(mensaje);
                }
            }
            else if (btnagregar.Text == "Editar")
            {
                bool resultado = new D_Usuarios().Editar(objusuario, out mensaje);
                if (resultado)
                {
                    DataGridViewRow row = dgUsuarios.Rows[Convert.ToInt32(txtindice.Text)];
                    row.Cells["idusuario"].Value = Convert.ToInt32(txtid.Text);
                    row.Cells["documento"].Value = txtdocumento.Text;
                    row.Cells["nombres"].Value = txtnombre.Text;
                    row.Cells["apellidos"].Value = txtapellidos.Text;
                    row.Cells["nombreusuario"].Value = txtnombreusuario.Text;
                    row.Cells["correo"].Value = txtcorreo.Text;
                    row.Cells["clave"].Value = txtclave.Text;
                    row.Cells["idnivelacceso"].Value = ((OpcionesComboBox)cbnivelacceso.SelectedItem).Valor.ToString();
                    row.Cells["nombreacceso"].Value = ((OpcionesComboBox)cbnivelacceso.SelectedItem).Texto.ToString();
                    row.Cells["valorestado"].Value = ((OpcionesComboBox)txtestado.SelectedItem).Valor.ToString();
                    row.Cells["estado"].Value = ((OpcionesComboBox)txtestado.SelectedItem).Texto.ToString();
                    Limpiar();
                }
                else
                {
                    MessageBox.Show(mensaje);
                }

                // Después de editar, cambia el texto del botón a "Guardar" para futuras operaciones de agregar
                btnagregar.Text = "Agregar";
                txtdocumento.Enabled = true;
            }
        }

        private void dgUsuarios_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            if (e.ColumnIndex == 0)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);
                var w = Properties.Resources.seleccionar.Width;
                var h = Properties.Resources.seleccionar.Height;
                var x = e.CellBounds.Left + (e.CellBounds.Width - w) / 2;
                var y = e.CellBounds.Top + (e.CellBounds.Height - h) / 2;

                e.Graphics.DrawImage(Properties.Resources.seleccionar, new Rectangle(x, y, w, h));
                e.Handled = true;
            }
        }

        private void dgUsuarios_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgUsuarios.Columns[e.ColumnIndex].Name == "btnseleccionar")
            {
                int indice = e.RowIndex;
                if (indice >= 0)
                {
                    txtindice.Text = indice.ToString();
                    txtid.Text = dgUsuarios.Rows[indice].Cells["idusuario"].Value.ToString();
                    txtdocumento.Text = dgUsuarios.Rows[indice].Cells["documento"].Value.ToString();
                    txtnombre.Text = dgUsuarios.Rows[indice].Cells["nombres"].Value.ToString();
                    txtapellidos.Text = dgUsuarios.Rows[indice].Cells["apellidos"].Value.ToString();
                    txtnombreusuario.Text = dgUsuarios.Rows[indice].Cells["nombreusuario"].Value.ToString();
                    txtcorreo.Text = dgUsuarios.Rows[indice].Cells["correo"].Value.ToString();
                    txtclave.Text = dgUsuarios.Rows[indice].Cells["clave"].Value.ToString();

                    foreach (OpcionesComboBox ocb in cbnivelacceso.Items)
                    {
                        if (Convert.ToInt32(ocb.Valor) == Convert.ToInt32(dgUsuarios.Rows[indice].Cells["idnivelacceso"].Value))
                        {
                            int indice_combo = cbnivelacceso.Items.IndexOf(ocb);
                            cbnivelacceso.SelectedIndex = indice_combo;
                            break;
                        }
                    }
                    
                    foreach (OpcionesComboBox oc in txtestado.Items)
                    {
                        if (Convert.ToInt32(oc.Valor) == Convert.ToInt32(dgUsuarios.Rows[indice].Cells["valorestado"].Value))
                        {
                            int indice_combo = txtestado.Items.IndexOf(oc);
                            txtestado.SelectedIndex = indice_combo;
                            break;
                        }
                    }
                    txtdocumento.Enabled = false;
                    btnagregar.Text = "Editar";
                }
            }
        }

        private void btnlimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }



    }
}
