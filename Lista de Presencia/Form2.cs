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
    public partial class Form2 : Form
    {
        private static Form2 s_Instance;

        public static Form2 GetInstance()
        {
            if (s_Instance == null)
                s_Instance = new Form2();
            return s_Instance;
        }

        public Form2()
        {
            InitializeComponent();
            this.CenterToScreen();
        }
        
        private void Form2_Load(object sender, EventArgs e)
        {
            Console.WriteLine("Form 2 LOAD");
            LoadPrograms();
        }

        private void LoadPrograms()
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = "Server=USUARIO-PC\\SQLEXPRESS;Database=MALM;Trusted_Connection=true";
                conn.Open();

                SqlCommand command = new SqlCommand("SELECT PROGRAM_ID, NAME FROM PROGRAM", conn);

                int programCounter = 0;

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        CheckBox box = new CheckBox();
                        box.Tag = reader["PROGRAM_ID"].ToString();
                        box.Text = reader["NAME"].ToString();
                        box.AutoSize = true;
                        box.Location = new Point(10, 20 + programCounter * 25);
                        gbPrograms.Controls.Add(box);
                        programCounter++;
                    }
                }
            }
        }

        private void RetrieveAllPrograms()
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = "Server=USUARIO-PC\\SQLEXPRESS;Database=MALM;Trusted_Connection=true";
                conn.Open();
                SqlCommand command = new SqlCommand("INSERT INTO PERSON (FIRSTNAME, LASTNAME, BIRTHDAY) VALUES (@firstname, @lastname, @birthday)", conn);
                command.Parameters.Add(new SqlParameter("firstname", txtFirstname.Text));
                command.Parameters.Add(new SqlParameter("lastname", txtLastname.Text));
                command.Parameters.Add(new SqlParameter("birthday", dtpBirthday.Value));

                Console.WriteLine("Insert affected " + command.ExecuteNonQuery() + " rows.");

                ClearPersonAdditionFields();
            }
        }

        private void ClearPersonAdditionFields()
        {
            txtFirstname.Clear();
            txtLastname.Clear();
            dtpBirthday.Value = DateTime.Now;

            foreach(CheckBox cb in gbPrograms.Controls.OfType<CheckBox>())
            {
                cb.Checked = false;
            }
        }
        
        private void btnAddPerson_Click(object sender, EventArgs e)
        {
            // TODO: Make it a transaction with rollback, since we need to insert in multiple tables.

            if(txtFirstname.Text != "" && txtLastname.Text != "")
            {
                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = "Server=USUARIO-PC\\SQLEXPRESS;Database=MALM;Trusted_Connection=true";
                    conn.Open();

                    SqlCommand commandInsertPerson = new SqlCommand("INSERT INTO PERSON (FIRSTNAME, LASTNAME, BIRTHDAY) VALUES (@firstname, @lastname, @birthday)", conn);
                    commandInsertPerson.Parameters.Add(new SqlParameter("firstname", txtFirstname.Text));
                    commandInsertPerson.Parameters.Add(new SqlParameter("lastname", txtLastname.Text));
                    commandInsertPerson.Parameters.Add(new SqlParameter("birthday", dtpBirthday.Value));

                    Console.WriteLine("Insert affected " + commandInsertPerson.ExecuteNonQuery() + " rows.");
                    
                    // TODO: We need to find the ID of the person we just inserted. Use SCOPE_IDENTITY?
                    SqlCommand commandGetPersonID = new SqlCommand("SELECT MAX(PERSON_ID) AS LAST_ID FROM PERSON WHERE FIRSTNAME = @firstname AND LASTNAME = @lastname", conn);
                    commandGetPersonID.Parameters.Add(new SqlParameter("firstname", txtFirstname.Text));
                    commandGetPersonID.Parameters.Add(new SqlParameter("lastname", txtLastname.Text));

                    int personID;
                    using (SqlDataReader reader = commandGetPersonID.ExecuteReader())
                    {
                        reader.Read();
                        personID = (int) reader["LAST_ID"];
                        Console.WriteLine("Last inserted ID is: " + personID);
                    }

                    foreach (CheckBox cb in gbPrograms.Controls.OfType<CheckBox>())
                    {
                        if (cb.Checked)
                        {
                            //MessageBox.Show("Checkbox " + cb.Text + " (" + cb.Tag + "): " + cb.Checked);
                            SqlCommand commandAddProgramToPerson = new SqlCommand("INSERT INTO PERSON_PROGRAM VALUES (@personID, @programID)", conn);
                            commandAddProgramToPerson.Parameters.AddWithValue("personID", personID);
                            commandAddProgramToPerson.Parameters.AddWithValue("programID", cb.Tag);

                            commandAddProgramToPerson.ExecuteNonQuery();
                        }
                    }

                    ClearPersonAdditionFields();
                }
            }
            else
            {
                MessageBox.Show("Please fill out all the mandatory fields!");
            }
        }        
    }
}
