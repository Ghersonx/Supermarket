using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Data;

namespace CATACHORI
{
    class Ejecucion : Conexion
    {
        public Ejecucion()
        {
            
        }

        public void ejecutarSQL(string sql)
        {
            try
            {
                SqlCommand sentencia = new SqlCommand(sql, miConexion);
                int numero = sentencia.ExecuteNonQuery();

                if (numero > 0)
                {
                    MessageBox.Show("OPERACION REALIZADA EXITOSAMENTE", "LA BASE SE HA ESPECIFICADO",
                    MessageBoxButtons.OK);
                }
                else
                {
                    MessageBox.Show("NO SE PUEDE ACTUALIZAR LA BD", "LA BASE NO SE HA ESPECIFICADO",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Verifique que los campos requeridos hayan sido especificados", "Error al intentar realizar la operación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine("Message: " + ex.Message + "\nSource: " + ex.Source + "\nError: " + ex.StackTrace);
            }
        }

        public void actualizarGrid(DataGridView dg, String sql)
        {
            try
            {
                this.conectar();
                //creamos el data set
                System.Data.DataSet miDataSet = new System.Data.DataSet();
                //creamos el adaptador de datos
                SqlDataAdapter miAdaptador = new SqlDataAdapter(sql, miConexion);

                //llenar data set 
                miAdaptador.Fill(miDataSet, "usuario");

                //asignar el valor adecuado a las  propiedades del datagrid

                dg.DataSource = miDataSet;
                dg.DataMember = "usuario";
                //desconecto de la bd
                this.desconectar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se ha podido realizar la acción", "Error al intentar actualizar la tabla", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine("Message: " + ex.Message + "\nSource: " + ex.Source + "\nError: " + ex.StackTrace);
            }
        }

        public void llenarComboBox(ComboBox comboBox, String sql)
        {
            try
            {
                this.conectar();
                // Crear el data set
                System.Data.DataSet miDataSet = new System.Data.DataSet();
                // Crear el adaptador de datos
                SqlDataAdapter miAdaptador = new SqlDataAdapter(sql, miConexion);

                // Llenar el data set con los datos del adaptador
                miAdaptador.Fill(miDataSet, "proveedor");

                // Obtener el DataTable del DataSet
                DataTable dataTable = miDataSet.Tables["proveedor"];

                // Asignar el DataTable como origen de datos del ComboBox
                comboBox.DataSource = dataTable;
                comboBox.DisplayMember = "nombre";
                comboBox.ValueMember = "id";

                // Desconectar de la base de datos
                this.desconectar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se ha podido realizar la acción", "Error al intentar actualizar la tabla", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine("Message: " + ex.Message + "\nSource: " + ex.Source + "\nError: " + ex.StackTrace);
            }
        }
    }
}
