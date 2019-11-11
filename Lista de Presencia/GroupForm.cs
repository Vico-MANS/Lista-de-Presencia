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
using System.Data.Entity.Infrastructure;

namespace Lista_de_Presencia 
{
    public partial class GroupForm : Form 
    {
        private static GroupForm s_Instance;

        public static GroupForm GetInstance()
        {
            if (s_Instance == null)
            {
                s_Instance = new GroupForm();
                s_Instance.FormClosing += OnFormClosing;
            }
            return s_Instance;
        }

        public GroupForm()
        {
            InitializeComponent();
            this.CenterToScreen();
        }

        public static void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            // When the form is closed again we set the reference of the singleton to null
            s_Instance = null;
        }

        private void GroupForm_Load(object sender, EventArgs e)
        {
            GetServices();
            GetSupervisors();
        }

        private void GetServices()
        {
            using (SqlConnection conn = new SqlConnection())
            {
                DatabaseConnection.OpenConnection(conn);

                SqlCommand command = new SqlCommand("SELECT SERVICIO_ID AS ID, NAME FROM SERVICIO", conn);

                cbbServices.DisplayMember = "Text";
                cbbServices.ValueMember = "Value";

                Dictionary<Object, Object> comboSource = new Dictionary<Object, Object>();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        comboSource.Add(reader["ID"], reader["NAME"]);
                    }
                }

                cbbServices.DataSource = new BindingSource(comboSource, null);
                cbbServices.DisplayMember = "Value";
                cbbServices.ValueMember = "Key";
                cbbServices.SelectedItem = null;
            }
        }

        private void GetSupervisors()
        {
            using (SqlConnection conn = new SqlConnection())
            {
                DatabaseConnection.OpenConnection(conn);

                SqlCommand command = new SqlCommand("SELECT PERSON_ID AS ID, FIRSTNAME+' '+LASTNAME AS NAME FROM PERSON WHERE WORKER = 1", conn);

                cbbSupervisors.DisplayMember = "Text";
                cbbSupervisors.ValueMember = "Value";

                Dictionary<Object, Object> comboSource = new Dictionary<Object, Object>();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        comboSource.Add(reader["ID"], reader["NAME"]);
                    }
                }

                cbbSupervisors.DataSource = new BindingSource(comboSource, null);
                cbbSupervisors.DisplayMember = "Value";
                cbbSupervisors.ValueMember = "Key";
                cbbSupervisors.SelectedItem = null;
            }
        }

        private void ResetForm()
        {
            txtGroupID.Text = "";
            cbbServices.SelectedItem = null;
            cbbSupervisors.SelectedItem = null;
            dtpStartDate.Value = DateTime.Now;
            dtpEndDate.Value = DateTime.Now;
        }

        private void btnAddGroup_Click(object sender, EventArgs e)
        {
            if (txtGroupID.Text == "" || cbbServices.SelectedItem == null || cbbSupervisors.SelectedItem == null)
            {
                MessageBox.Show("All fields are mandatory!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if(dtpStartDate.Value > dtpEndDate.Value)
            {
                MessageBox.Show("Start date can't be greater than end date!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection())
                {
                    DatabaseConnection.OpenConnection(conn);

                    SqlCommand command = new SqlCommand("INSERT INTO GRUPO VALUES (@groupID, @serviceID, @supervisorID, @startDate, @endDate)", conn);
                    command.Parameters.AddWithValue("groupID", txtGroupID.Text);
                    command.Parameters.AddWithValue("serviceID", ((KeyValuePair<Object, Object>)cbbServices.SelectedItem).Key);
                    command.Parameters.AddWithValue("supervisorID", ((KeyValuePair<Object, Object>)cbbSupervisors.SelectedItem).Key);
                    command.Parameters.AddWithValue("startDate", dtpStartDate.Value);
                    command.Parameters.AddWithValue("endDate", dtpEndDate.Value);

                    command.ExecuteNonQuery();

                    MessageBox.Show("The group has been successfully added to the database!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    ResetForm();
                }
            }
            catch (SqlException ex)
            {
                // If the exception has this number it means that a primary key constraint has been violated
                if (ex.Number == 2627)
                    MessageBox.Show("A group with that ID already exists in the database", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    throw;
            }            
        }
    }
}
