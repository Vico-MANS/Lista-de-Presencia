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

namespace Lista_de_Presencia
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            GetPersonData();
        }

        private void GetPersonData()
        {
            dataGridView1.Rows.Clear();

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = "Server=USUARIO-PC\\SQLEXPRESS;Database=MALM;Trusted_Connection=true";
                conn.Open();
                Console.WriteLine("Connection opened...\n");

                SqlCommand command = new SqlCommand("SELECT FIRSTNAME, LASTNAME, (SELECT CONVERT(varchar(10), BIRTHDAY, 103) AS [DD/MM/YYYY]) AS BIRTHDAY FROM Human", conn);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine(String.Format("{0} \t | {1} \t | {2}", reader["FIRSTNAME"], reader["LASTNAME"], reader["BIRTHDAY"]));

                        dataGridView1.Rows.Add(reader["FIRSTNAME"], reader["LASTNAME"], reader["BIRTHDAY"]);
                    }
                }
            }
        }

        private void btnAddPerson_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Add Person");
            
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = "Server=USUARIO-PC\\SQLEXPRESS;Database=MALM;Trusted_Connection=true";
                conn.Open();
                SqlCommand command = new SqlCommand("INSERT INTO Human (FIRSTNAME, LASTNAME, BIRTHDAY) VALUES (@firstname, @lastname, @birthday)", conn);
                command.Parameters.Add(new SqlParameter("firstname", txtFirstname.Text));
                command.Parameters.Add(new SqlParameter("lastname", txtLastname.Text));
                command.Parameters.Add(new SqlParameter("birthday", dtpBirthday.Value));

                Console.WriteLine("Insert affected " + command.ExecuteNonQuery() + " rows.");

                GetPersonData();
            }
        }

        private void btn_DeleteSelected(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = "Server=USUARIO-PC\\SQLEXPRESS;Database=MALM;Trusted_Connection=true";
                conn.Open();

                int deletionCounter = 0;
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    SqlCommand command = new SqlCommand("DELETE FROM Human WHERE FIRSTNAME = @firstname AND LASTNAME = @lastname", conn);
                    command.Parameters.Add(new SqlParameter("firstname", row.Cells["colFirstName"].Value.ToString()));
                    command.Parameters.Add(new SqlParameter("lastname", row.Cells["colLastName"].Value.ToString()));

                    deletionCounter += command.ExecuteNonQuery();
                }

                Console.WriteLine(deletionCounter + " rows where deleted.");
                GetPersonData();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
