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

namespace Lista_de_Presencia {
    public partial class SearchParentForm : Form 
    {
        public enum PARENT_TYPE { FATHER, MOTHER };

        private static SearchParentForm s_Instance;

        public static SearchParentForm GetInstance(PARENT_TYPE type)
        {
            // We close the last form if the parent type we searched for is not the same
            if (s_Instance != null && !s_Instance.ParentType.Equals(type))
                s_Instance.Close();
            if (s_Instance == null)
                s_Instance = new SearchParentForm(type);
            return s_Instance;
        }

        public static SearchParentForm GetInstance()
        {
            return s_Instance;
        }

        public int ParentID { get; private set; }
        public PARENT_TYPE ParentType { get; private set; }
        public bool Valid { get; private set; }

        public SearchParentForm(PARENT_TYPE type)
        {
            InitializeComponent();
            this.CenterToScreen();
            ParentType = type;
            btnValidate.Enabled = false;
            Valid = false;
            this.Text = "Search " + type.ToString().ToLower();
        }

        private void SearchParentForm_Load(object sender, EventArgs e)
        {
            txtSearchText.Select();
        }

        private void SearchParentForm_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void SearchParentForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            s_Instance = null;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtSearchText.Text != "")
            {
                dgvResults.Rows.Clear();

                using (SqlConnection conn = new SqlConnection())
                {
                    DatabaseConnection.OpenConnection(conn);

                    SqlCommand command = new SqlCommand("SELECT PERSON_ID, FIRSTNAME, LASTNAME " +
                                                        "FROM PERSON " +
                                                        "WHERE FIRSTNAME LIKE @search OR LASTNAME LIKE @search", conn);
                    command.Parameters.AddWithValue("search", txtSearchText.Text + "%");

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            dgvResults.Rows.Add(reader["PERSON_ID"], reader["FIRSTNAME"], reader["LASTNAME"]);
                        }
                    }
                    dgvResults.ClearSelection();
                }
            }
        }

        private void txtSearchText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
                btnSearch.PerformClick();
        }

        private void dgvResults_SelectionChanged(object sender, EventArgs e)
        {
            btnValidate.Enabled = dgvResults.SelectedRows.Count > 0;
        }

        private void btnValidate_Click(object sender, EventArgs e)
        {
            ParentID = (int)dgvResults.SelectedRows[0].Cells[0].Value;
            Valid = true;
            this.Close();
        }
    }
}
