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
    public partial class ProgramAdditionForm : Form
    {
        private static ProgramAdditionForm s_Instance;

        public static ProgramAdditionForm GetInstance()
        {
            if (s_Instance == null)
            {
                s_Instance = new ProgramAdditionForm();
                s_Instance.FormClosing += OnFormClosing;
            }
            return s_Instance;
        }

        public static void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            // When the form is closed again we set the reference of the singleton to null
            s_Instance = null;
        }

        public ProgramAdditionForm()
        {
            InitializeComponent();
            this.CenterToScreen();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            GetPrograms();
        }

        private void GetPrograms()
        {
            gbExistingPrograms.Controls.Clear();
            using (SqlConnection conn = new SqlConnection())
            {
                DatabaseConnection.OpenConnection(conn);

                SqlCommand command = new SqlCommand("SELECT NAME FROM PROGRAM", conn);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    int counter = 0;
                    while (reader.Read())
                    {
                        int x = 25;
                        int y = 20 + counter * 20;
                        Label label = new Label
                        {
                            Text = reader["NAME"].ToString(),
                            Location = new Point(x, y),
                            AutoSize = true
                        };
                        gbExistingPrograms.Controls.Add(label);
                        counter++;
                    }
                }
            }
        }

        private void ResetForm()
        {
            txtProgramName.Text = "";
            GetPrograms();
        }

        private void btnCreateProgram_Click(object sender, EventArgs e)
        {
            if (txtProgramName.Text == "")
            {
                MessageBox.Show("All fields are mandatory!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (SqlConnection conn = new SqlConnection())
            {
                DatabaseConnection.OpenConnection(conn);               

                SqlCommand command = new SqlCommand("INSERT INTO PROGRAM(NAME) VALUES(@name)", conn);
                command.Parameters.AddWithValue("name", txtProgramName.Text);

                command.ExecuteNonQuery();

                MessageBox.Show("The program has been successfully added to the database!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                ResetForm();
            }
        }

        private void ProgramAdditionForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //DialogResult res = MessageBox.Show("If you have made changes, these won't be saved!", "Information", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            //e.Cancel = res.Equals(DialogResult.Cancel);
        }
    }
}
