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
    public partial class PersonInformationForm : Form
    {
        private static PersonInformationForm s_Instance;

        public static PersonInformationForm GetInstance(int personID)
        {
            if(s_Instance != null)
                Console.WriteLine("Current ID: " + s_Instance.m_PersonID + " & wanted ID: " + personID);
            if (s_Instance == null || (s_Instance != null && s_Instance.m_PersonID != personID))
            {
                // We close the last form
                if (s_Instance != null)
                    s_Instance.Close();
                s_Instance = new PersonInformationForm(personID);
                s_Instance.FormClosing += OnFormClosing;
            }
            return s_Instance;
        }

        // If this is false the changes are from the user himself and not from the initialisation
        private bool m_Initialisation;
        private int m_PersonID;

        private List<int> m_WeeklyPresenceCheckedCells = new List<int>();
        private List<int> m_WeeklyPresenceChanges = new List<int>();

        public static void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            // When the form is closed again we set the reference of the singleton to null
            s_Instance = null;
        }

        private void PersonInformationForm_Load(object sender, EventArgs e)
        {
            // We get rid of the focus on elements
            this.ActiveControl = label1;
            dgvWeeklyDetail.ClearSelection();
        }

        public PersonInformationForm(int personID)
        {
            InitializeComponent();
            this.CenterToScreen();

            m_PersonID = personID;
            bool worker = RetrievePersonInformation();

            if (!worker)
            {
                gbWeeklyPresence.Visible = true;
                dgvWeeklyDetail.Rows.Clear();
                dgvWeeklyDetail.Rows.Add(m_PersonID, txtFirstname.Text + " " + txtLastname.Text);
                RetrieveWeeklyPresenceInformation();
            }
            else
            {
                gbWeeklyPresence.Visible = false;
            }
        }

        private bool RetrievePersonInformation()
        {
            using (SqlConnection conn = new SqlConnection())
            {
                DatabaseConnection.OpenConnection(conn);

                SqlCommand command = new SqlCommand("SELECT * FROM PERSON WHERE PERSON_ID = @id", conn);
                command.Parameters.AddWithValue("id", m_PersonID);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    // Should only be one retrieved row
                    reader.Read();

                    txtFirstname.Text = reader["FIRSTNAME"].ToString();
                    txtLastname.Text = reader["LASTNAME"].ToString();
                    dtpBirthday.Value = Convert.ToDateTime(reader["BIRTHDAY"]);

                    return (bool)reader["WORKER"];
                }
            }
        }

        private void RetrieveWeeklyPresenceInformation()
        {
            using (SqlConnection conn = new SqlConnection())
            {
                DatabaseConnection.OpenConnection(conn);

                m_Initialisation = true;
                m_WeeklyPresenceCheckedCells.Clear();
                m_WeeklyPresenceChanges.Clear();

                SqlCommand command = new SqlCommand("SELECT WEEK_DAY FROM WEEKLY_PRESENCE WHERE ID_PERSON = @id", conn);
                command.Parameters.Add(new SqlParameter("id", (int)dgvWeeklyDetail.Rows[0].Cells["colWeekPresPersonID"].Value));

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // +1 'cause we have personID and personName in the first two columns of the dgv
                        dgvWeeklyDetail.Rows[0].Cells[(int)reader["WEEK_DAY"] + 1].Value = true;
                        m_WeeklyPresenceCheckedCells.Add((int)reader["WEEK_DAY"]);
                    }
                }

                m_Initialisation = false;
            }
        }

        private void dgvWeeklyDetail_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (m_Initialisation)
                return;

            if (e.RowIndex != -1)
            {
                if (((bool)dgvWeeklyDetail.Rows[e.RowIndex].Cells[e.ColumnIndex].Value && !m_WeeklyPresenceCheckedCells.Contains(e.ColumnIndex - 1))
                    || (!(bool)dgvWeeklyDetail.Rows[e.RowIndex].Cells[e.ColumnIndex].Value && m_WeeklyPresenceCheckedCells.Contains(e.ColumnIndex - 1)))
                    m_WeeklyPresenceChanges.Add(e.ColumnIndex - 1);
                else
                    m_WeeklyPresenceChanges.Remove(e.ColumnIndex - 1);
                
                btnSaveWeeklyChanges.Enabled = m_WeeklyPresenceChanges.Count > 0;
            }
        }

        private void dgvWeeklyDetail_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                dgvWeeklyDetail.EndEdit();
            }
        }

        private void btnSaveWeeklyChanges_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                DatabaseConnection.OpenConnection(conn);

                foreach (int weekday in m_WeeklyPresenceChanges)
                {
                    SqlCommand command;

                    // If the cell is checked we insert into the database
                    if((bool)dgvWeeklyDetail.Rows[0].Cells[weekday + 1].Value)
                        command = new SqlCommand("INSERT INTO WEEKLY_PRESENCE VALUES (@id, @weekday)", conn);
                    // Else we remove the data from the database
                    else
                        command = new SqlCommand("DELETE FROM WEEKLY_PRESENCE WHERE ID_PERSON = @id AND WEEK_DAY = @weekday", conn);

                    command.Parameters.Add(new SqlParameter("id", (int)dgvWeeklyDetail.Rows[0].Cells["colWeekPresPersonID"].Value));
                    command.Parameters.Add(new SqlParameter("weekday", weekday));

                    Console.WriteLine("Change affected " + command.ExecuteNonQuery() + " row.");
                    //m_WeeklyPresenceChanges.Clear();
                }

                // After the insert into the database we can update the checked cells list
                foreach(int weekday in m_WeeklyPresenceChanges)
                {                    
                    // If the cell is checked we add it to the list
                    if ((bool)dgvWeeklyDetail.Rows[0].Cells[weekday + 1].Value)
                        m_WeeklyPresenceCheckedCells.Add(weekday);
                    // Else we remove it from the list
                    else
                        m_WeeklyPresenceCheckedCells.Remove(weekday);
                }
                m_WeeklyPresenceChanges.Clear();
                btnSaveWeeklyChanges.Enabled = false;

                MessageBox.Show("All weekly presence changes are saved.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
