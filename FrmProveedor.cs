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
    public partial class FrmProveedor : Form
    {
        Ejecucion ejecutar;
        public FrmProveedor()
        {
            InitializeComponent();
            ejecutar = new Ejecucion();
            ejecutar.actualizarGrid(this.dataGridView1, "SELECT * FROM PROVEEDOR");
        }

        private void BTN_GUARDAR_Click(object sender, EventArgs e)
        {
            ejecutar.conectar();
            ejecutar.ejecutarSQL("INSERT INTO PROVEEDOR (NOMBRE, TELEFONO) VALUES ('" + this.txtNombre.Text + "', '" + this.txtTelefono.Text + "')");
            ejecutar.actualizarGrid(this.dataGridView1, "SELECT * FROM proveedor");
            ejecutar.desconectar();
        }

        private void txtBuscarNombre_KeyUp(object sender, KeyEventArgs e)
        {
            ejecutar.conectar();
            ejecutar.actualizarGrid(this.dataGridView1, "SELECT * FROM proveedor WHERE nombre LIKE '" + txtBuscarNombre.Text + "%'");
            ejecutar.desconectar();
        }
        private int cont = 0;
        private void BTN_EDITAR_Click(object sender, EventArgs e)
        {
            if (cont == 0)
            {
                cont++;
                String sql = "SELECT * FROM proveedor WHERE id = " + txt_id.Text;
                ejecutar.conectar();
                ejecutar.actualizarGrid(this.dataGridView1, sql);
                ejecutar.desconectar();

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    // Verificar si la fila es válida y no es la fila de encabezado
                    if (!row.IsNewRow)
                    {
                        // Acceder a los valores de cada celda de la fila
                        string idUsuario = row.Cells["id"].Value.ToString();
                        string nombreProveedor = row.Cells["nombre"].Value.ToString();
                        string telefonoProveedor = row.Cells["telefono"].Value.ToString();

                        txt_id.Text = idUsuario;
                        txtNombre.Text = nombreProveedor;
                        txtTelefono.Text = telefonoProveedor;
                        break;
                    }
                    else
                    {
                        cont = 0;
                        MessageBox.Show("No se ha encontrado al proveedor", "Uy! ha ocurrido algo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else if (cont == 1)
            {
                cont = 0;
                String sql = "UPDATE proveedor SET nombre='" + txtNombre.Text + "', telefono='" + txtTelefono.Text + "' WHERE id = " + txt_id.Text;
                ejecutar.conectar();
                ejecutar.ejecutarSQL(sql);
                ejecutar.actualizarGrid(this.dataGridView1, "SELECT * FROM proveedor");
                ejecutar.desconectar();
            }
        }

        private void BTN_BORRAR_Click(object sender, EventArgs e)
        {
            try
            {
                String sql = "DELETE FROM proveedor WHERE id = " + txt_id.Text;
                ejecutar.conectar();
                ejecutar.ejecutarSQL(sql);
                ejecutar.actualizarGrid(this.dataGridView1, "SELECT * FROM proveedor");
                ejecutar.desconectar();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error!");
                Console.WriteLine("Message: " + ex.Message + "\nSource: " + ex.Source + "\nError: " + ex.StackTrace);
            }
        }
    }
}
