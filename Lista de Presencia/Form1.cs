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
            tabControl.SelectedIndexChanged += new EventHandler(tabControl_SelectedIndexChanged);
            GetPersonData();
            dgvPresence.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvPresence.Columns["colPerson"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
        }

        private void GetPersonData()
        {
            dgvOverview.Rows.Clear();

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

                        dgvOverview.Rows.Add(reader["FIRSTNAME"], reader["LASTNAME"], reader["BIRTHDAY"]);
                    }
                }
            }
        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch((sender as TabControl).SelectedIndex){
                // Overview tab
                case 0:
                    break;
                // Presence tab
                case 1:
                    using (SqlConnection conn = new SqlConnection())
                    {
                        conn.ConnectionString = "Server=USUARIO-PC\\SQLEXPRESS;Database=MALM;Trusted_Connection=true";
                        conn.Open();

                        SqlCommand command = new SqlCommand("SET DATEFIRST 1; " +
                            "SELECT CONVERT(VARCHAR, DATEADD(dd, (DATEPART(dw, getdate())), getdate()-1), 103), " +
                            "CONVERT(VARCHAR, DATEADD(dd, (DATEPART(dw, getdate())), getdate()), 103), " +
                            "CONVERT(VARCHAR, DATEADD(dd, (DATEPART(dw, getdate())), getdate()+1), 103), " +
                            "CONVERT(VARCHAR, DATEADD(dd, (DATEPART(dw, getdate())), getdate()+2), 103), " +
                            "CONVERT(VARCHAR, DATEADD(dd, (DATEPART(dw, getdate())), getdate()+3), 103), " +
                            "CONVERT(VARCHAR, DATEADD(dd, (DATEPART(dw, getdate())), getdate()+4), 103), " +
                            "CONVERT(VARCHAR, DATEADD(dd, (DATEPART(dw, getdate())), getdate()+5), 103) " +
                            "FROM Human", conn);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Console.WriteLine("Monday: " + reader[0]);
                                Console.WriteLine("Tuesday: " + reader[1]);
                                Console.WriteLine("Wednesday: " + reader[2]);
                                Console.WriteLine("Thursday: " + reader[3]);
                                Console.WriteLine("Friday: " + reader[4]);
                                Console.WriteLine("Saturday: " + reader[5]);
                                Console.WriteLine("Sunday: " + reader[6]);

                                dgvPresence.Columns["colMonday"].HeaderText = "Mon\n" + reader[0].ToString();
                                dgvPresence.Columns["colTuesday"].HeaderText = "Tue\n" + reader[1].ToString();
                                dgvPresence.Columns["colWednesday"].HeaderText = "Wed\n" + reader[2].ToString();
                                dgvPresence.Columns["colThursday"].HeaderText = "Thu\n" + reader[3].ToString();
                                dgvPresence.Columns["colFriday"].HeaderText = "Fri\n" + reader[4].ToString();
                                dgvPresence.Columns["colSaturday"].HeaderText = "Sat\n" + reader[5].ToString();
                                dgvPresence.Columns["colSunday"].HeaderText = "Sun\n" + reader[6].ToString();
                            }
                        }

                            command = new SqlCommand("SELECT (FIRSTNAME + ' ' + LASTNAME) AS NAME FROM Human", conn);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                dgvPresence.Rows.Add(reader["NAME"]);
                            }
                        }
                    }
                    break;
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
                foreach (DataGridViewRow row in dgvOverview.SelectedRows)
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

        private void dgvPresence_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
