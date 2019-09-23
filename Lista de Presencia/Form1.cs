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
        // Dates of the current week
        private string[] m_Dates;
        // Keeps track of the cellIDs that were checked in the beginning
        private List<int> m_CheckedCells = new List<int>();
        // Manually saving the cells were changes were made (to be later saved in the database)
        private List<String> m_PresenceChanges = new List<String>();
        // To keep track of which week we are currently seeing (0 is the current one, -1 is last week and +1 next week)
        private int m_WeekDifference = 0;

        public Form1()
        {
            InitializeComponent();

            m_Dates = new string[7];
            this.CenterToScreen();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            tabControl.SelectedIndexChanged += new EventHandler(tabControl_SelectedIndexChanged);
            GetPersonData();
            dgvPresence.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvPresence.Columns["colPerson"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvPresence.CellValueChanged += dgvPresence_OnCellValueChanged;
            dgvPresence.CellMouseUp += dgvPresence_OnCellMouseUp;
        }

        private void GetPersonData()
        {
            dgvOverview.Rows.Clear();

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = "Server=USUARIO-PC\\SQLEXPRESS;Database=MALM;Trusted_Connection=true";
                conn.Open();
                Console.WriteLine("Connection opened...\n");

                SqlCommand command = new SqlCommand("SELECT HUMAN_ID, FIRSTNAME, LASTNAME, (SELECT CONVERT(varchar(10), BIRTHDAY, 103) AS [DD/MM/YYYY]) AS BIRTHDAY FROM Human", conn);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine(String.Format("{0} \t | {1} \t | {2}", reader["FIRSTNAME"], reader["LASTNAME"], reader["BIRTHDAY"]));

                        dgvOverview.Rows.Add(reader["HUMAN_ID"], reader["FIRSTNAME"], reader["LASTNAME"], reader["BIRTHDAY"]);
                    }
                }
            }
        }

        private void UpdatePresenceGridInformation()
        {
            dgvPresence.Rows.Clear();
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = "Server=USUARIO-PC\\SQLEXPRESS;Database=MALM;Trusted_Connection=true";
                conn.Open();

                // should be 1- for monday and 7- for sunday, for some reason it's one off today (18/09)
                SqlCommand command = new SqlCommand(
                    "SET DATEFIRST 1;" +
                    "SELECT CONVERT(VARCHAR, DATEADD(dd, 1-(DATEPART(dw, getdate()+@week_diff)), getdate()+@week_diff), 103), " +
                            "CONVERT(VARCHAR, DATEADD(dd, 2-(DATEPART(dw, getdate()+@week_diff)), getdate()+@week_diff), 103), " +
                            "CONVERT(VARCHAR, DATEADD(dd, 3-(DATEPART(dw, getdate()+@week_diff)), getdate()+@week_diff), 103), " +
                            "CONVERT(VARCHAR, DATEADD(dd, 4-(DATEPART(dw, getdate()+@week_diff)), getdate()+@week_diff), 103), " +
                            "CONVERT(VARCHAR, DATEADD(dd, 5-(DATEPART(dw, getdate()+@week_diff)), getdate()+@week_diff), 103), " +
                            "CONVERT(VARCHAR, DATEADD(dd, 6-(DATEPART(dw, getdate()+@week_diff)), getdate()+@week_diff), 103), " +
                            "CONVERT(VARCHAR, DATEADD(dd, 7-(DATEPART(dw, getdate()+@week_diff)), getdate()+@week_diff), 103) "
                    , conn);
                command.Parameters.Add(new SqlParameter("week_diff", m_WeekDifference * 7));

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        dgvPresence.Columns["colMonday"].HeaderText = "Mon\n" + reader[0].ToString();
                        dgvPresence.Columns["colTuesday"].HeaderText = "Tue\n" + reader[1].ToString();
                        dgvPresence.Columns["colWednesday"].HeaderText = "Wed\n" + reader[2].ToString();
                        dgvPresence.Columns["colThursday"].HeaderText = "Thu\n" + reader[3].ToString();
                        dgvPresence.Columns["colFriday"].HeaderText = "Fri\n" + reader[4].ToString();
                        dgvPresence.Columns["colSaturday"].HeaderText = "Sat\n" + reader[5].ToString();
                        dgvPresence.Columns["colSunday"].HeaderText = "Sun\n" + reader[6].ToString();

                        // Store the dates into the array to know which cell corresponds to which date
                        for (int i = 0; i < 7; i++)
                        {
                            // Convert it to the database format MM/DD/YYYY
                            string[] day = reader[i].ToString().Split('/');
                            m_Dates[i] = day[1] + '/' + day[0] + '/' + day[2];
                        }
                    }
                }

                command = new SqlCommand("SELECT HUMAN_ID AS ID, (FIRSTNAME + ' ' + LASTNAME) AS NAME FROM Human", conn);

                List<int> personIDs = new List<int>();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // colPersonID, colPerson
                        dgvPresence.Rows.Add(reader["ID"], reader["NAME"]);
                        personIDs.Add((int)reader["ID"]);
                    }
                }

                foreach (int id in personIDs)
                {
                    command = new SqlCommand("SELECT CONVERT(VARCHAR, DIA, 103) AS DIA, DATEDIFF(DAY, @week_start, DIA) AS WEEKDAY FROM (" +
                                                    "SELECT DIA FROM PRESENCE " +
                                                    "WHERE ID_HUMAN = @id " +
                                                    "AND DIA BETWEEN CONVERT(VARCHAR(30), CAST(@week_start AS DATETIME), 102)" +
                                                    "AND CONVERT(VARCHAR(30), CAST(@week_end AS DATETIME), 102))" +
                                             "AS SUB_QUERY", conn);
                    command.Parameters.Add(new SqlParameter("id", id));
                    command.Parameters.Add(new SqlParameter("week_start", m_Dates[0]));
                    command.Parameters.Add(new SqlParameter("week_end", m_Dates[6]));

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int rowIndex = personIDs.IndexOf(id);
                            int colIndex = (int)reader["WEEKDAY"] + 2;
                            // We can do this since the index in the id table corresponds to the datagridview row indexes
                            dgvPresence.Rows[personIDs.IndexOf(id)].Cells[(int)reader["WEEKDAY"] + 2].Value = true;
                            // We also add that cell id to the list so we can later only keep the changes
                            m_CheckedCells.Add(rowIndex * colIndex + colIndex);
                        }
                    }
                }

                // We clear the table of changes now so we don't keep track of the initialisation changes
                m_PresenceChanges.Clear();
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
                        UpdatePresenceGridInformation();                    
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
            DialogResult res = MessageBox.Show("Are you sure that you want to delete these human beings?", "Seguro?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if(res == DialogResult.Yes)
            {
                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = "Server=USUARIO-PC\\SQLEXPRESS;Database=MALM;Trusted_Connection=true";
                    conn.Open();

                    int deletionCounter = 0;
                    foreach (DataGridViewRow row in dgvOverview.SelectedRows)
                    {
                        // We first have to delete all the content related to that individual in the Presence table
                        SqlCommand command = new SqlCommand("DELETE FROM PRESENCE WHERE ID_HUMAN = @id", conn);
                        command.Parameters.Add(new SqlParameter("id", row.Cells["colHumanID"].Value.ToString()));

                        command.ExecuteNonQuery();

                        // Then we can safely delete the individual from the Human table
                        command = new SqlCommand("DELETE FROM HUMAN WHERE HUMAN_ID = @id", conn);
                        command.Parameters.Add(new SqlParameter("id", row.Cells["colHumanID"].Value.ToString()));

                        deletionCounter += command.ExecuteNonQuery();
                    }

                    Console.WriteLine(deletionCounter + " rows where deleted.");
                    GetPersonData();
                }
            }
        }
        
        private void dgvPresence_OnCellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                // We only want to keep track of the real changes (not the check then uncheck or vice-versa once)
                if (((bool)dgvPresence.Rows[e.RowIndex].Cells[e.ColumnIndex].Value
                     && !m_CheckedCells.Contains(e.RowIndex * e.ColumnIndex + e.ColumnIndex))
                   ||
                    (!(bool)dgvPresence.Rows[e.RowIndex].Cells[e.ColumnIndex].Value
                     && m_CheckedCells.Contains(e.RowIndex * e.ColumnIndex + e.ColumnIndex)))
                {
                    Console.WriteLine("True change");
                    m_PresenceChanges.Add(e.RowIndex + " " + e.ColumnIndex);
                }
                else
                {
                    Console.WriteLine("Not a true change");
                    m_PresenceChanges.Remove(e.RowIndex + " " + e.ColumnIndex);
                }
            }
        }

        // Ends the edition of the cell/checkbox when the user clicks instead of when the cell loses focus
        private void dgvPresence_OnCellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                dgvPresence.EndEdit();
            }
        }

        private void btnSavePresenceChanges_Click(object sender, EventArgs e)
        {
            foreach(String change in m_PresenceChanges)
            {
                String[] data = change.Split(' ');
                int[] changedCells = Array.ConvertAll<string, int>(data, int.Parse);
                //Console.WriteLine("Changed made in row " + data[0] + " and column " + data[1] + ", date: " + m_Dates[changedCells[1]-2]);

                /*
                 * TODO: 
                 * We still have to keep only the real changes (if the user undos his change we don't wanna update the database)
                 * Then we can update the database with the given changes
                 * */

                /*
                 * If a row in the table corresponds to the person and the date then the person was present.
                 * If the person wasn't present on the given date, then no row corresponds to it.
                 * Therefore we only insert and delete rows, no updates.
                **/
                
                if ((bool) dgvPresence.Rows[changedCells[0]].Cells[changedCells[1]].Value)
                {
                    /*
                     * The checkbox is checked, which means that the person was present on that day.
                     * We now have to check if the row already exists in the table. (in the case that the user unchecked and rechecked the checkbox)
                     * If the row doesn't exist we insert it into the table.
                     * */

                    using (SqlConnection conn = new SqlConnection())
                    {
                        conn.ConnectionString = "Server=USUARIO-PC\\SQLEXPRESS;Database=MALM;Trusted_Connection=true";
                        conn.Open();

                        SqlCommand command = new SqlCommand("SELECT * FROM PRESENCE WHERE DIA = convert(varchar(30),cast(@day as datetime),102) AND ID_HUMAN = @id " +
                                                            "IF @@ROWCOUNT = 0" +
                                                            "    INSERT INTO PRESENCE(DIA, ID_HUMAN) VALUES(convert(varchar(30),cast(@day as datetime),102), @id)", conn);
                        command.Parameters.Add(new SqlParameter("day", m_Dates[changedCells[1] - 2]));
                        command.Parameters.Add(new SqlParameter("id", (int) (dgvPresence.Rows[changedCells[0]].Cells["colPersonID"]).Value));

                        // We add the cell to the checked cells
                        m_CheckedCells.Add(changedCells[0] * changedCells[1] + changedCells[1]);

                        Console.WriteLine("Insert affected " + command.ExecuteNonQuery() + " rows.");                        
                    }
                }
                else
                {
                    /*
                     * The checkbox is not checked, which means that the person wasn't present on that day.
                     * We now have to check if the row exists in the table. (in the case that the user checked and unchecked the checkbox)
                     * If the row exists we delete it.
                     * */

                    using (SqlConnection conn = new SqlConnection())
                    {
                        conn.ConnectionString = "Server=USUARIO-PC\\SQLEXPRESS;Database=MALM;Trusted_Connection=true";
                        conn.Open();

                        SqlCommand command = new SqlCommand("DELETE FROM PRESENCE WHERE DIA = CONVERT(VARCHAR(30), CAST(@day AS DATETIME), 102) AND ID_HUMAN = @id", conn);
                        command.Parameters.Add(new SqlParameter("day", m_Dates[changedCells[1] - 2]));
                        command.Parameters.Add(new SqlParameter("id", (int)(dgvPresence.Rows[changedCells[0]].Cells["colPersonID"]).Value));

                        // We remove the cell from the checked cells
                        m_CheckedCells.Remove(changedCells[0] * changedCells[1] + changedCells[1]);

                        Console.WriteLine("Deletion affected " + command.ExecuteNonQuery() + " rows.");
                    }
                }
            }

            MessageBox.Show(m_PresenceChanges.Count + " changes were registered into the database.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            m_PresenceChanges.Clear();
        }

        private void btnNextWeek_Click(object sender, EventArgs e)
        {
            m_WeekDifference++;
            UpdatePresenceGridInformation();
        }

        private void btnPreviousWeek_Click(object sender, EventArgs e)
        {
            m_WeekDifference--;
            UpdatePresenceGridInformation();
        }
    }
}
