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
    public partial class FrmProduucto : Form
    {
        Ejecucion ejecutar;
        public FrmProduucto()
        {
            InitializeComponent();
            ejecutar = new Ejecucion();
            ejecutar.actualizarGrid(this.dataGridView1, "SELECT * FROM producto");
            ejecutar.llenarComboBox(this.cbProveedores, "SELECT * FROM PROVEEDOR");
        }

        private void BTN_GUARDAR_Click(object sender, EventArgs e)
        {
            //TODO: COMPLETAR SQL PARA INSERTAR PRODUCTOS CON SU RESPECTIVO PROVEEDOR
            ejecutar.conectar();
            ejecutar.ejecutarSQL("INSERT INTO PRODUCTO (nombre, precio_compra, precio_venta, stock, proveedor) VALUES ('" +
                txtNombre.Text + "', '" +
                txtPrecioCompra.Text + "', '" +
                txtPrecioVenta.Text + "', '" + 
                txtStock.Text + "', '" +
                cbProveedores.SelectedValue + "')");

            ejecutar.actualizarGrid(this.dataGridView1, "SELECT * FROM PRODUCTO");

            ejecutar.desconectar();
        }

        private void BTN_BUSCAR_Click(object sender, EventArgs e)
        {
            ejecutar.conectar();
            ejecutar.actualizarGrid(this.dataGridView1, "SELECT * FROM producto");

            ejecutar.desconectar();
        }
        int cont = 0;
        private void BTN_EDITAR_Click(object sender, EventArgs e)
        {
            if (cont == 0)
            {
                cont++;
                String sql = "SELECT * FROM producto WHERE id = " + txt_id.Text;
                ejecutar.conectar();
                ejecutar.actualizarGrid(this.dataGridView1, sql);
                ejecutar.desconectar();

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    // Verificar si la fila es válida y no es la fila de encabezado
                    if (!row.IsNewRow)
                    {
                        // Acceder a los valores de cada celda de la fila
                        string id = row.Cells["id"].Value.ToString();
                        string nombreProducto = row.Cells["nombre"].Value.ToString();
                        string precioCompra = row.Cells["precio_compra"].Value.ToString();
                        string precioVenta = row.Cells["precio_venta"].Value.ToString();
                        string stock = row.Cells["stock"].Value.ToString();

                        txt_id.Text = id;
                        txtNombre.Text = nombreProducto;
                        txtPrecioCompra.Text = precioCompra;
                        txtPrecioVenta.Text = precioVenta;
                        txtStock.Text = stock;
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
                String sql = "UPDATE producto SET nombre='" + txtNombre.Text + "', proveedor='" + cbProveedores.SelectedValue +"',precio_compra='" + txtPrecioCompra.Text + "', precio_venta='"+ txtPrecioVenta.Text +"', stock='"+txtStock.Text+"' WHERE id = " + txt_id.Text;
                ejecutar.conectar();
                ejecutar.ejecutarSQL(sql);
                ejecutar.actualizarGrid(this.dataGridView1, "SELECT * FROM producto");
                ejecutar.desconectar();
            }
        }

        private void BTN_BORRAR_Click(object sender, EventArgs e)
        {
            try
            {
                String sql = "DELETE FROM producto WHERE id = " + txt_id.Text;
                ejecutar.conectar();
                ejecutar.ejecutarSQL(sql);
                ejecutar.actualizarGrid(this.dataGridView1, "SELECT * FROM producto");
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
