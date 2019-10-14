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
            GetWorkers();
        }

        private void ResetForm()
        {
            txtProgramName.Text = "";
            cbbWorkers.SelectedItem = null;
        }

        private void GetWorkers()
        {
            using (SqlConnection conn = new SqlConnection())
            {
                DatabaseConnection.OpenConnection(conn);

                SqlCommand command = new SqlCommand("SELECT PERSON_ID AS ID, FIRSTNAME+' '+LASTNAME AS NAME FROM PERSON WHERE WORKER = 1", conn);

                cbbWorkers.DisplayMember = "Text";
                cbbWorkers.ValueMember = "Value";

                Dictionary<Object, Object> comboSource = new Dictionary<Object, Object>();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        comboSource.Add(reader["ID"], reader["NAME"]);
                    }
                }

                cbbWorkers.DataSource = new BindingSource(comboSource, null);
                cbbWorkers.DisplayMember = "Value";
                cbbWorkers.ValueMember = "Key";
                cbbWorkers.SelectedItem = null;
            }
        }

        private void btnCreateProgram_Click(object sender, EventArgs e)
        {
            if (txtProgramName.Text == "" || cbbWorkers.SelectedItem == null)
            {
                MessageBox.Show("All fields are mandatory!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (SqlConnection conn = new SqlConnection())
            {
                DatabaseConnection.OpenConnection(conn);

                SqlCommand command = new SqlCommand("INSERT INTO PROGRAM(NAME, ID_EDUCATOR) VALUES(@name, @educator)", conn);
                command.Parameters.AddWithValue("name", txtProgramName.Text);
                command.Parameters.AddWithValue("educator", ((KeyValuePair<Object, Object>)cbbWorkers.SelectedItem).Key);

                command.ExecuteNonQuery();

                MessageBox.Show("The program has been successfully added to the database!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                ResetForm();
            }
        }
    }
}
