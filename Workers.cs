using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Dop_rabota
{
    public partial class Workers : Form
    {
        DataSet data;
        SqlDataAdapter For;
        SqlCommandBuilder FF;
        //Подключение к SQL Servery
        string connectionString = @"Data Source=MSI\SQLEXPRESS; Initial Catalog=Workers list; Integrated Security=True";
        string sql = "SELECT * FROM Work";
        public Workers()
        {
            InitializeComponent();
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.AllowUserToAddRows = false;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                For = new SqlDataAdapter(sql, connection);

                data = new DataSet();
                For.Fill(data);
                dataGridView1.DataSource = data.Tables[0];
            }
        }

        private void Workers_Load(object sender, EventArgs e)
        {

        }
        // кнопка добавления
        private void button1_Click(object sender, EventArgs e)
        {
            DataRow row = data.Tables[0].NewRow();
            data.Tables[0].Rows.Add(row);
        }
        //Удаление выделеной строки
        private void button2_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                dataGridView1.Rows.Remove(row);
            }
        }
        //Обновление таблицы
        void openchild(Panel pen, Form emptyF)
        {
            emptyF.TopLevel = false;
            emptyF.FormBorderStyle = FormBorderStyle.None;
            emptyF.Dock = DockStyle.Fill;
            pen.Controls.Add(emptyF);
            emptyF.BringToFront();
            emptyF.Show();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            openchild(panel1, new Workers());
        }
        //Сохранение изменений
        private void button3_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                For = new SqlDataAdapter(sql, connection);
                FF = new SqlCommandBuilder(For);
                For.InsertCommand = new SqlCommand(" dbo_Workers", connection);
                For.InsertCommand.CommandType = CommandType.StoredProcedure;
                For.InsertCommand.Parameters.Add(new SqlParameter("@fio_worker", SqlDbType.VarChar, 100, "fio_worker"));
                For.InsertCommand.Parameters.Add(new SqlParameter("@position_worker", SqlDbType.Int, 0, "position_worker"));

                For.Update(data);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
