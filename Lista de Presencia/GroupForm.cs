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
            GetSupervisors();
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
    }
}
