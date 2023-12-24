using CATACHORI.modelos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CATACHORI
{
    public partial class FrmUsuario : Form
    {

        Ejecucion ejecutar;
        Usuario usuario;
        string sql;
        public FrmUsuario()
        {
            InitializeComponent();
            ejecutar = new Ejecucion();
            ejecutar.actualizarGrid(this.dataGridView1 , "SELECT * FROM usuario");
        }

        private void button1_Click(object sender, EventArgs e) //BTN_BUSCAR
        {
            ejecutar.conectar();
            ejecutar.actualizarGrid(this.dataGridView1, "SELECT * FROM usuario");

            ejecutar.desconectar();

        }

        private void FrmUsuario_Load(object sender, EventArgs e)
        {

        }

        private void BTN_GUARDAR_Click(object sender, EventArgs e)
        {
            usuario = new Usuario(txtNombre.Text, txtApellido.Text, txtGenero.Text);

            sql = "INSERT INTO usuario (nombre, apellido, genero) values ('" + usuario.nombre + "', '" + usuario.apellido + "', '" + usuario.genero + "' )";
            ejecutar.conectar();
            ejecutar.ejecutarSQL(sql);
            ejecutar.desconectar();
            ejecutar.actualizarGrid(this.dataGridView1, "SELECT * FROM usuario");

        }

        private void BTN_BORRAR_Click(object sender, EventArgs e)
        {
            try
            {
                sql = "DELETE FROM usuario WHERE id = " + txt_id.Text;
                ejecutar.conectar();
                ejecutar.ejecutarSQL(sql);
                ejecutar.actualizarGrid(this.dataGridView1, "SELECT * FROM usuario");
                ejecutar.desconectar();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error!");
                Console.WriteLine("Message: " + ex.Message + "\nSource: " + ex.Source + "\nError: " + ex.StackTrace);
            }

        }

       

        private void txtBuscarNombre_KeyUp(object sender, KeyEventArgs e)
        {
            ejecutar.conectar();
            ejecutar.actualizarGrid(dataGridView1, "SELECT * FROM usuario WHERE nombre LIKE '" + txtBuscarNombre.Text + "%'");
            ejecutar.desconectar();
        }
        private int cont = 0;

        private void BTN_EDITAR_Click(object sender, EventArgs e)
        {
            if (cont == 0)
            {
                cont++;
                sql = "SELECT * FROM usuario WHERE id = " + txt_id.Text;
                ejecutar.conectar();
                //ejecutar.ejecutarSQL(sql);
                ejecutar.actualizarGrid(this.dataGridView1, sql);
                ejecutar.desconectar();

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    // Verificar si la fila es válida y no es la fila de encabezado
                    if (!row.IsNewRow)
                    {
                        // Acceder a los valores de cada celda de la fila
                        string idUsuario = row.Cells["id"].Value.ToString();
                        string nombreUsuario = row.Cells["nombre"].Value.ToString();
                        string apellidoUsuario = row.Cells["apellido"].Value.ToString();
                        string generoUsuario = row.Cells["genero"].Value.ToString();

                        txt_id.Text = idUsuario;
                        txtNombre.Text = nombreUsuario;
                        txtApellido.Text = apellidoUsuario;
                        txtGenero.Text = generoUsuario;
                        break;
                    }
                    else
                    {
                        cont = 0;
                        MessageBox.Show("No se ha encontrado al usuario", "Uy! ha ocurrido algo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else if(cont == 1)
            {
                cont = 0;
                sql = "UPDATE usuario SET nombre='" + txtNombre.Text + "', apellido='" + txtApellido.Text + "', genero='" + txtGenero.Text + "' WHERE id = " + txt_id.Text;
                ejecutar.conectar();
                ejecutar.ejecutarSQL(sql);
                ejecutar.actualizarGrid(this.dataGridView1, "SELECT * FROM usuario");
                ejecutar.desconectar();
            }
            
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                // Verificar si la fila es válida y no es la fila de encabezado
                if (!row.IsNewRow)
                {
                    // Acceder a los valores de cada celda de la fila
                    string idUsuario = row.Cells["id"].Value.ToString();
                    string nombreUsuario = row.Cells["nombre"].Value.ToString();
                    string apellidoUsuario = row.Cells["apellido"].Value.ToString();
                    string generoUsuario = row.Cells["genero"].Value.ToString();

                    txt_id.Text = idUsuario;
                    txtNombre.Text = nombreUsuario;
                    txtApellido.Text = apellidoUsuario;
                    txtGenero.Text = generoUsuario;
                    break;
                }
                else
                {
                    cont = 0;
                    MessageBox.Show("No se ha encontrado al usuario", "Uy! ha ocurrido algo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}
