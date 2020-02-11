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
    public partial class MainForm : Form
    {
        // Dates of the current week
        private string[] m_DatesWeekFormat;
        // To keep track of which week we are currently seeing (0 is the current one, -1 is last week and +1 next week)
        private int m_WeekDifference = 0;

        // Dates of the current month
        private string[] m_DatesMonthFormat;
        // To keep track of which month we are currently seeing (1 is january, 12 december)
        private int m_CurrentMonth;
        // To keep track of the year
        private int m_YearDifference = 0;

        // Keeps track of the cellIDs that were checked in the beginning
        private List<int> m_PresenceCheckedCells = new List<int>();
        // Manually saving the cells were changes were made (to be later saved in the database)
        private List<String> m_PresenceChanges = new List<String>();

        private List<int> m_WeeklyPresenceCheckedCells = new List<int>();
        private List<int> m_WeeklyPresenceChanges = new List<int>();

        // If this is false the changes are from the user himself and not from the initialisation
        private bool m_Initialisation;

        // Program ID for the presence tab
        private int m_ProgramID = -1;

        // Determines how the presence table is displayed
        private enum RANGE { WEEK, MONTH };
        private RANGE m_ViewRange;

        public MainForm()
        {
            InitializeComponent();

            m_DatesWeekFormat = new string[7];
            m_DatesMonthFormat = new string[31];

            m_ViewRange = RANGE.WEEK;
            m_CurrentMonth = DateTime.Today.Month;
            this.CenterToScreen();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            tabControl.SelectedIndexChanged += new EventHandler(tabControl_SelectedIndexChanged);

            dgvPresenceWeekFormat.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvPresenceWeekFormat.Columns["colPerson"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvPresenceWeekFormat.CellValueChanged += dgvPresenceWeekFormat_OnCellValueChanged;
            dgvPresenceWeekFormat.CellMouseUp += dgvPresenceWeekFormat_OnCellMouseUp;
            
            // Workaround so that the tab control index changed event gets called even on initialisation
            tabControl.SelectedIndex = 1;
            tabControl.SelectedIndex = 0;
        }

        private void GetPersonData()
        {
            dgvOverview.Rows.Clear();

            using (SqlConnection conn = new SqlConnection())
            {
                DatabaseConnection.OpenConnection(conn);

                SqlCommand command = new SqlCommand("SELECT PERSON_ID, FIRSTNAME, LASTNAME, (SELECT CONVERT(varchar(10), BIRTHDAY, 103) AS [DD/MM/YYYY]) AS BIRTHDAY FROM PERSON", conn);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        //Console.WriteLine(String.Format("{0} \t | {1} \t | {2}", reader["FIRSTNAME"], reader["LASTNAME"], reader["BIRTHDAY"]));
                        dgvOverview.Rows.Add(reader["PERSON_ID"], reader["FIRSTNAME"], reader["LASTNAME"], reader["BIRTHDAY"]);
                    }
                }
            }
        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_Initialisation = true;
            switch ((sender as TabControl).SelectedIndex){
                // Overview tab
                case 0:
                    InitOverviewTab();
                    // We get rid of the focus
                    dgvOverview.ClearSelection();
                    break;
                // Presence tab
                case 1:
                    InitPresenceTab();
                    break;
            }
            m_Initialisation = false;
        }

        private void InitPresenceTab()
        {
            GetPrograms();
            gbPresence.Visible = false;
        }

        private void InitOverviewTab()
        {
            GetPersonData();
            // Used to get all the kids from a group
            GetGroups();
        }

        private void GetGroups()
        {
            using (SqlConnection conn = new SqlConnection())
            {
                DatabaseConnection.OpenConnection(conn);

                SqlCommand command = new SqlCommand("SELECT GRUPO_ID AS ID, NAME FROM GRUPO", conn);

                cbbGroupIDs.DisplayMember = "Text";
                cbbGroupIDs.ValueMember = "Value";

                Dictionary<Object, Object> comboSource = new Dictionary<Object, Object>();

                bool empty = true;
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        comboSource.Add(reader["ID"], reader["NAME"]);
                        empty = false;
                    }
                }

                if (!empty)
                {
                    cbbGroupIDs.DataSource = new BindingSource(comboSource, null);
                    cbbGroupIDs.DisplayMember = "Value";
                    cbbGroupIDs.ValueMember = "Key";
                    cbbGroupIDs.SelectedItem = null;
                }
            }
        }

        private void UpdatePresenceGridInformationWeekFormat()
        {
            lblRangeInfo.Text = "";

            dgvPresenceMonthFormat.Visible = false;
            dgvPresenceWeekFormat.Visible = true;
            using (SqlConnection conn = new SqlConnection())
            {
                DatabaseConnection.OpenConnection(conn);

                m_Initialisation = true;
                dgvPresenceWeekFormat.Rows.Clear();
                m_PresenceCheckedCells.Clear();

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
                        dgvPresenceWeekFormat.Columns["colMonday"].HeaderText = "Mon\n" + reader[0].ToString();
                        dgvPresenceWeekFormat.Columns["colTuesday"].HeaderText = "Tue\n" + reader[1].ToString();
                        dgvPresenceWeekFormat.Columns["colWednesday"].HeaderText = "Wed\n" + reader[2].ToString();
                        dgvPresenceWeekFormat.Columns["colThursday"].HeaderText = "Thu\n" + reader[3].ToString();
                        dgvPresenceWeekFormat.Columns["colFriday"].HeaderText = "Fri\n" + reader[4].ToString();
                        dgvPresenceWeekFormat.Columns["colSaturday"].HeaderText = "Sat\n" + reader[5].ToString();
                        dgvPresenceWeekFormat.Columns["colSunday"].HeaderText = "Sun\n" + reader[6].ToString();

                        // Store the dates into the array to know which cell corresponds to which date
                        for (int i = 0; i < 7; i++)
                        {
                            // Convert it to the database format MM/DD/YYYY
                            string[] day = reader[i].ToString().Split('/');
                            m_DatesWeekFormat[i] = day[1] + '/' + day[0] + '/' + day[2];
                        }
                    }
                }

                command = new SqlCommand("SELECT PERSON_ID AS ID, (FIRSTNAME + ' ' + LASTNAME) AS NAME FROM PERSON WHERE WORKER != 1 AND PERSON_ID IN "+
                                            " (SELECT ID_PERSON FROM PERSON_GRUPO WHERE ID_GRUPO IN "+
                                                " (SELECT GRUPO_ID FROM GRUPO WHERE ID_SERVICIO IN "+
                                                    " (SELECT SERVICIO_ID FROM SERVICIO WHERE ID_PROGRAM = @programID)))", conn);
                command.Parameters.AddWithValue("programID", m_ProgramID);

                List<int> personIDs = new List<int>();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // colPresPersonID, colPerson
                        dgvPresenceWeekFormat.Rows.Add(reader["ID"], reader["NAME"]);
                        personIDs.Add((int)reader["ID"]);
                    }
                }

                foreach (int id in personIDs)
                {
                    command = new SqlCommand("SELECT CONVERT(VARCHAR, DIA, 103) AS DIA, DATEDIFF(DAY, @week_start, DIA) AS WEEKDAY FROM (" +
                                                    "SELECT DIA FROM PRESENCE " +
                                                    "WHERE ID_PERSON = @id " +
                                                    "AND DIA BETWEEN CONVERT(VARCHAR(30), CAST(@week_start AS DATETIME), 102)" +
                                                    "AND CONVERT(VARCHAR(30), CAST(@week_end AS DATETIME), 102))" +
                                             "AS SUB_QUERY", conn);
                    command.Parameters.AddWithValue("id", id);
                    command.Parameters.AddWithValue("week_start", m_DatesWeekFormat[0]);
                    command.Parameters.AddWithValue("week_end", m_DatesWeekFormat[6]);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int rowIndex = personIDs.IndexOf(id);
                            int colIndex = (int)reader["WEEKDAY"] + 2;
                            // We can do this since the index in the id table corresponds to the datagridview row indexes
                            dgvPresenceWeekFormat.Rows[rowIndex].Cells[colIndex].Value = true;
                            // We also add that cell id to the list so we can later only keep the changes
                            m_PresenceCheckedCells.Add(rowIndex * dgvPresenceWeekFormat.ColumnCount + colIndex);

                            //Console.WriteLine("Adding to checked cells row " + rowIndex + " and column " + colIndex +" cellIndex: "+(rowIndex * dgvPresence.ColumnCount + colIndex));
                        }
                    }
                    
                    // TODO: Find a better way to do this 'cause it doesn't change from week to week, therefore no need to ask the database again
                    command = new SqlCommand("SELECT WEEK_DAY FROM WEEKLY_PRESENCE WHERE ID_PERSON = @id", conn);
                    command.Parameters.AddWithValue("id", id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int rowIndex = personIDs.IndexOf(id);
                            int colIndex = (int)reader["WEEK_DAY"] + 1;
                            dgvPresenceWeekFormat.Rows[rowIndex].Cells[colIndex].Style.BackColor = Color.LightGreen;
                        }
                    }
                }

                // We clear the table of changes now so we don't keep track of the initialisation changes
                m_PresenceChanges.Clear();
                
                m_Initialisation = false;
                gbPresence.Visible = true;
                dgvPresenceWeekFormat.ClearSelection();
            }
        }

        private List<DateTime> GetDatesInMonth(int year, int month)
        {
            return Enumerable.Range(1, DateTime.DaysInMonth(year, month))  // Days: 1, 2 ... 31 etc.
                             .Select(day => new DateTime(year, month, day)) // Map each day to a date
                             .ToList(); // Load dates into a list
        }

        private void UpdatePresenceGridInformationMonthFormat()
        {
            m_Initialisation = true;
            m_PresenceChanges.Clear();

            lblRangeInfo.Text = m_CurrentMonth.ToString("D2")+"/"+(DateTime.Today.Year + m_YearDifference).ToString();

            dgvPresenceWeekFormat.Visible = false;
            dgvPresenceMonthFormat.Visible = true;

            dgvPresenceMonthFormat.Columns.Clear();
            dgvPresenceMonthFormat.RowHeadersWidth = 24;

            DataGridViewColumn personIDColumn = new DataGridViewTextBoxColumn();
            personIDColumn.Name = "colPresPersonID";
            personIDColumn.Visible = false;
            dgvPresenceMonthFormat.Columns.Add(personIDColumn);

            DataGridViewColumn personNameColumn = new DataGridViewTextBoxColumn();
            personNameColumn.Name = "colPerson";
            personNameColumn.HeaderText = "Person";
            personNameColumn.Width = 200;
            personNameColumn.ReadOnly = true;
            dgvPresenceMonthFormat.Columns.Add(personNameColumn);
            dgvPresenceMonthFormat.Columns["colPerson"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;

            dgvPresenceMonthFormat.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Get the days in the given month
            List<DateTime> days = GetDatesInMonth(DateTime.Today.Year + m_YearDifference, m_CurrentMonth);
            foreach (DateTime day in days)
            {
                DataGridViewColumn column = new DataGridViewCheckBoxColumn();
                column.Name = day.Day.ToString();
                column.HeaderText = day.Day.ToString("D2");
                column.Width = 22;
                dgvPresenceMonthFormat.Columns.Add(column);

                // We show the weekends
                if(day.DayOfWeek == DayOfWeek.Saturday || day.DayOfWeek == DayOfWeek.Sunday)
                    dgvPresenceMonthFormat.Columns[dgvPresenceMonthFormat.ColumnCount-1].DefaultCellStyle.BackColor = Color.LightGray;

                // Database format MM/DD/YYYY
                m_DatesMonthFormat[day.Day-1] = day.Month.ToString()+"/"+day.Day.ToString()+"/"+day.Year.ToString();
            }

            using (SqlConnection conn = new SqlConnection())
            {
                DatabaseConnection.OpenConnection(conn);

                // Get the people of the program
                SqlCommand command = new SqlCommand("SELECT PERSON_ID AS ID, (FIRSTNAME + ' ' + LASTNAME) AS NAME FROM PERSON WHERE WORKER != 1 AND PERSON_ID IN " +
                                                        " (SELECT ID_PERSON FROM PERSON_GRUPO WHERE ID_GRUPO IN " +
                                                            " (SELECT GRUPO_ID FROM GRUPO WHERE ID_SERVICIO IN " +
                                                                " (SELECT SERVICIO_ID FROM SERVICIO WHERE ID_PROGRAM = @programID)))", conn);
                command.Parameters.AddWithValue("programID", m_ProgramID);

                List<int> personIDs = new List<int>();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // colPresPersonID, colPerson
                        dgvPresenceMonthFormat.Rows.Add(reader["ID"], reader["NAME"]);
                        personIDs.Add((int)reader["ID"]);
                    }
                }

                // Key = day of the week, value = list of persons that should be present
                Dictionary<int, List<int>> weekdaysOfPresence = new Dictionary<int, List<int>>();

                foreach (int id in personIDs)
                {
                    command = new SqlCommand("SELECT CONVERT(VARCHAR, DIA, 103) AS DIA, DATEDIFF(DAY, @month_start, DIA) AS MONTHDAY FROM (" +
                                                    "SELECT DIA FROM PRESENCE " +
                                                    "WHERE ID_PERSON = @id " +
                                                    "AND DIA BETWEEN CONVERT(VARCHAR(30), CAST(@month_start AS DATETIME), 102)" +
                                                    "AND CONVERT(VARCHAR(30), CAST(@month_end AS DATETIME), 102))" +
                                             "AS SUB_QUERY", conn);
                    command.Parameters.AddWithValue("id", id);
                    command.Parameters.AddWithValue("month_start", days[0].ToString("MM/dd/yyyy"));
                    command.Parameters.AddWithValue("month_end", days[days.Count-1].ToString("MM/dd/yyyy"));
                                        
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int rowIndex = personIDs.IndexOf(id);
                            int colIndex = (int)reader["MONTHDAY"] + 2;
                            // We can do this since the index in the id table corresponds to the datagridview row indexes
                            dgvPresenceMonthFormat.Rows[rowIndex].Cells[colIndex].Value = true;
                            // We also add that cell id to the list so we can later only keep the changes
                            m_PresenceCheckedCells.Add(rowIndex * dgvPresenceMonthFormat.ColumnCount + colIndex);
                        }
                    }

                    command = new SqlCommand("SELECT WEEK_DAY FROM WEEKLY_PRESENCE WHERE ID_PERSON = @id", conn);
                    command.Parameters.AddWithValue("id", id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (weekdaysOfPresence.ContainsKey((int)reader["WEEK_DAY"]))
                                weekdaysOfPresence[(int)reader["WEEK_DAY"]].Add(id);
                            else
                                weekdaysOfPresence.Add((int)reader["WEEK_DAY"], new List<int>{id});
                        }
                    }
                }

                foreach (DateTime day in days)
                {
                    if (weekdaysOfPresence.ContainsKey((int)day.DayOfWeek))
                    {
                        foreach(int personID in weekdaysOfPresence[(int)day.DayOfWeek])
                        {
                            dgvPresenceMonthFormat.Rows[personIDs.IndexOf(personID)].Cells[days.IndexOf(day)+2].Style.BackColor = Color.LightGreen;
                        }
                    }
                }
            }

            m_Initialisation = false;
        }

        private void btn_DeleteSelected(object sender, EventArgs e)
        {
            if (dgvOverview.SelectedRows.Count == 0)
            {
                MessageBox.Show("No rows are selected!");
                return;
            }

            DialogResult res = MessageBox.Show("Are you sure that you want to delete these human beings?", "Seguro?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if(res == DialogResult.Yes)
            {
                using (SqlConnection conn = new SqlConnection())
                {
                    DatabaseConnection.OpenConnection(conn);
                    int deletionCounter = 0;
                    foreach (DataGridViewRow row in dgvOverview.SelectedRows)
                    {
                        SqlTransaction transaction = conn.BeginTransaction();
                        try
                            {
                            // We first have to delete all the content related to that individual in the Presence table
                            SqlCommand command = new SqlCommand("DELETE FROM PRESENCE WHERE ID_PERSON = @id", conn, transaction);
                            command.Parameters.Add(new SqlParameter("id", row.Cells["colOverPersonID"].Value.ToString()));
                            command.ExecuteNonQuery();

                            // And all the content in the Person_Program table
                            command = new SqlCommand("DELETE FROM PERSON_PROGRAM WHERE ID_PERSON = @id", conn, transaction);
                            command.Parameters.Add(new SqlParameter("id", row.Cells["colOverPersonID"].Value.ToString()));
                            command.ExecuteNonQuery();

                            // And all the content in the Program table
                            command = new SqlCommand("DELETE FROM PROGRAM WHERE ID_EDUCATOR = @id", conn, transaction);
                            command.Parameters.Add(new SqlParameter("id", row.Cells["colOverPersonID"].Value.ToString()));
                            command.ExecuteNonQuery();

                            // And all the content in the Weekly_Presence table
                            command = new SqlCommand("DELETE FROM WEEKLY_PRESENCE WHERE ID_PERSON = @id", conn, transaction);
                            command.Parameters.Add(new SqlParameter("id", row.Cells["colOverPersonID"].Value.ToString()));
                            command.ExecuteNonQuery();

                            // Then we can safely delete the individual from the Person table
                            command = new SqlCommand("DELETE FROM PERSON WHERE PERSON_ID = @id", conn, transaction);
                            command.Parameters.Add(new SqlParameter("id", row.Cells["colOverPersonID"].Value.ToString()));

                            deletionCounter += command.ExecuteNonQuery();

                            transaction.Commit();
                        }
                        catch (Exception excep)
                        {
                            transaction.Rollback();

                            Console.WriteLine(excep);
                            MessageBox.Show("An error has occured!\nThe person couldn't be deleted from the database...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                    Console.WriteLine(deletionCounter + " rows where deleted.");
                    GetPersonData();
                }              
            }
        }
        
        private void dgvPresenceWeekFormat_OnCellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            // We only want to do something if we aren't in the initialisation step, only if the user is making changes
            if (m_Initialisation)
                return;
            
            if (e.RowIndex != -1)
            {
                // We only want to keep track of the real changes (not the check then uncheck or vice-versa once)
                if (((bool)dgvPresenceWeekFormat.Rows[e.RowIndex].Cells[e.ColumnIndex].Value
                     && !m_PresenceCheckedCells.Contains(e.RowIndex * dgvPresenceWeekFormat.ColumnCount + e.ColumnIndex))
                   ||
                    (!(bool)dgvPresenceWeekFormat.Rows[e.RowIndex].Cells[e.ColumnIndex].Value
                     && m_PresenceCheckedCells.Contains(e.RowIndex * dgvPresenceWeekFormat.ColumnCount + e.ColumnIndex)))
                {
                    //Console.WriteLine("True change");
                    m_PresenceChanges.Add(e.RowIndex + " " + e.ColumnIndex);
                }
                else
                {
                    //Console.WriteLine("Not a true change");
                    m_PresenceChanges.Remove(e.RowIndex + " " + e.ColumnIndex);
                }                
            }
        }

        // Ends the edition of the cell/checkbox when the user clicks instead of when the cell loses focus
        private void dgvPresenceWeekFormat_OnCellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                dgvPresenceWeekFormat.EndEdit();
            }
        }

        private void dgvPresenceWeekFormat_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1 || e.ColumnIndex < 2)
                return;

            // This works, but the problem is that the cell click event is not always called, if we click too fast.
            if (dgvPresenceWeekFormat.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == null || (bool)dgvPresenceWeekFormat.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == false)
                dgvPresenceWeekFormat.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = true;
            else
                dgvPresenceWeekFormat.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = false;
        }

        private void dgvPresenceMonthFormat_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            // We only want to do something if we aren't in the initialisation step, only if the user is making changes
            if (m_Initialisation)
                return;

            if(e.RowIndex != -1)
            {
                //Console.WriteLine("Day clicked: " + m_DatesMonthFormat[e.ColumnIndex-2]);

                // We only want to keep track of the real changes (not the check then uncheck or vice-versa once)
                if (((bool)dgvPresenceMonthFormat.Rows[e.RowIndex].Cells[e.ColumnIndex].Value
                     && !m_PresenceCheckedCells.Contains(e.RowIndex * dgvPresenceMonthFormat.ColumnCount + e.ColumnIndex))
                   ||
                    (!(bool)dgvPresenceMonthFormat.Rows[e.RowIndex].Cells[e.ColumnIndex].Value
                     && m_PresenceCheckedCells.Contains(e.RowIndex * dgvPresenceMonthFormat.ColumnCount + e.ColumnIndex)))
                {
                    //Console.WriteLine("True change");
                    m_PresenceChanges.Add(e.RowIndex + " " + e.ColumnIndex);
                }
                else
                {
                    //Console.WriteLine("Not a true change");
                    m_PresenceChanges.Remove(e.RowIndex + " " + e.ColumnIndex);
                }
            }
        }

        private void dgvPresenceMonthFormat_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                dgvPresenceMonthFormat.EndEdit();
            }
        }

        private void dgvPresenceMonthFormat_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1 || e.ColumnIndex < 2)
                return;

            // This works, but the problem is that the cell click event is not always called, if we click too fast.
            if (dgvPresenceMonthFormat.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == null || (bool)dgvPresenceMonthFormat.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == false)
                dgvPresenceMonthFormat.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = true;
            else
                dgvPresenceMonthFormat.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = false;
        }

        private void btnSavePresenceChanges_Click(object sender, EventArgs e)
        {
            if (m_ViewRange.Equals(RANGE.WEEK))
            {
                foreach (String change in m_PresenceChanges)
                {
                    String[] data = change.Split(' ');
                    int[] changedCells = Array.ConvertAll<string, int>(data, int.Parse);

                    /*
                     * If a row in the table corresponds to the person and the date then the person was present.
                     * If the person wasn't present on the given date, then no row corresponds to it.
                     * Therefore we only insert and delete rows, no updates.
                    **/

                    if ((bool)dgvPresenceWeekFormat.Rows[changedCells[0]].Cells[changedCells[1]].Value)
                    {
                        /*
                         * The checkbox is checked, which means that the person was present on that day.
                         * We now have to check if the row already exists in the table. (in the case that the user unchecked and rechecked the checkbox)
                         * If the row doesn't exist we insert it into the table.
                         * */

                        using (SqlConnection conn = new SqlConnection())
                        {
                            DatabaseConnection.OpenConnection(conn);

                            SqlCommand command = new SqlCommand("SELECT * FROM PRESENCE WHERE DIA = convert(varchar(30),cast(@day as datetime),102) AND ID_PERSON = @id " +
                                                                "IF @@ROWCOUNT = 0" +
                                                                "    INSERT INTO PRESENCE(DIA, ID_PERSON) VALUES(convert(varchar(30),cast(@day as datetime),102), @id)", conn);
                            command.Parameters.Add(new SqlParameter("day", m_DatesWeekFormat[changedCells[1] - 2]));
                            command.Parameters.Add(new SqlParameter("id", (int)(dgvPresenceWeekFormat.Rows[changedCells[0]].Cells["colPresPersonID"]).Value));

                            // We add the cell to the checked cells
                            m_PresenceCheckedCells.Add(changedCells[0] * changedCells[1] + changedCells[1]);

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
                            DatabaseConnection.OpenConnection(conn);

                            SqlCommand command = new SqlCommand("DELETE FROM PRESENCE WHERE DIA = CONVERT(VARCHAR(30), CAST(@day AS DATETIME), 102) AND ID_PERSON = @id", conn);
                            command.Parameters.Add(new SqlParameter("day", m_DatesWeekFormat[changedCells[1] - 2]));
                            command.Parameters.Add(new SqlParameter("id", (int)(dgvPresenceWeekFormat.Rows[changedCells[0]].Cells["colPresPersonID"]).Value));

                            // We remove the cell from the checked cells
                            m_PresenceCheckedCells.Remove(changedCells[0] * changedCells[1] + changedCells[1]);

                            Console.WriteLine("Deletion affected " + command.ExecuteNonQuery() + " rows.");
                        }
                    }
                }
            }
            else if (m_ViewRange.Equals(RANGE.MONTH))
            {
                foreach (String change in m_PresenceChanges)
                {
                    String[] data = change.Split(' ');
                    int[] changedCells = Array.ConvertAll<string, int>(data, int.Parse);

                    /*
                     * If a row in the table corresponds to the person and the date then the person was present.
                     * If the person wasn't present on the given date, then no row corresponds to it.
                     * Therefore we only insert and delete rows, no updates.
                    **/

                    if ((bool)dgvPresenceMonthFormat.Rows[changedCells[0]].Cells[changedCells[1]].Value)
                    {
                        /*
                         * The checkbox is checked, which means that the person was present on that day.
                         * We now have to check if the row already exists in the table. (in the case that the user unchecked and rechecked the checkbox)
                         * If the row doesn't exist we insert it into the table.
                         * */

                        using (SqlConnection conn = new SqlConnection())
                        {
                            DatabaseConnection.OpenConnection(conn);

                            SqlCommand command = new SqlCommand("SELECT * FROM PRESENCE WHERE DIA = convert(varchar(30),cast(@day as datetime),102) AND ID_PERSON = @id " +
                                                                "IF @@ROWCOUNT = 0" +
                                                                "    INSERT INTO PRESENCE(DIA, ID_PERSON) VALUES(convert(varchar(30),cast(@day as datetime),102), @id)", conn);
                            command.Parameters.Add(new SqlParameter("day", m_DatesMonthFormat[changedCells[1] - 2]));
                            command.Parameters.Add(new SqlParameter("id", (int)(dgvPresenceMonthFormat.Rows[changedCells[0]].Cells["colPresPersonID"]).Value));

                            // We add the cell to the checked cells
                            m_PresenceCheckedCells.Add(changedCells[0] * changedCells[1] + changedCells[1]);

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
                            DatabaseConnection.OpenConnection(conn);

                            SqlCommand command = new SqlCommand("DELETE FROM PRESENCE WHERE DIA = CONVERT(VARCHAR(30), CAST(@day AS DATETIME), 102) AND ID_PERSON = @id", conn);
                            command.Parameters.Add(new SqlParameter("day", m_DatesMonthFormat[changedCells[1] - 2]));
                            command.Parameters.Add(new SqlParameter("id", (int)(dgvPresenceMonthFormat.Rows[changedCells[0]].Cells["colPresPersonID"]).Value));

                            // We remove the cell from the checked cells
                            m_PresenceCheckedCells.Remove(changedCells[0] * changedCells[1] + changedCells[1]);

                            Console.WriteLine("Deletion affected " + command.ExecuteNonQuery() + " rows.");
                        }
                    }
                }
            }

            MessageBox.Show(m_PresenceChanges.Count + " changes were registered into the database.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            m_PresenceChanges.Clear();

            // Don't know if we keep that, makes an additional request to the database, but at least we see the state of the database right away
            if (m_ViewRange.Equals(RANGE.WEEK))
                UpdatePresenceGridInformationWeekFormat();
            else
                UpdatePresenceGridInformationMonthFormat();

        }

        private int RealModulo(int a, int b)
        {
            return (Math.Abs(a * b) + a) % b;
        }

        private void btnNextRange_Click(object sender, EventArgs e)
        {
            if (!AllChangesSaved())
                return;

            if (m_ViewRange.Equals(RANGE.WEEK))
            {
                m_WeekDifference++;
                UpdatePresenceGridInformationWeekFormat();
            }
            else if (m_ViewRange.Equals(RANGE.MONTH))
            {
                m_CurrentMonth = RealModulo(m_CurrentMonth + 1, 12);
                if (m_CurrentMonth == 0)
                    m_CurrentMonth = 12;
                else if (m_CurrentMonth == 1)
                    m_YearDifference++;
                UpdatePresenceGridInformationMonthFormat();
            }
        }

        private void btnPreviousRange_Click(object sender, EventArgs e)
        {
            if (!AllChangesSaved())
                return;

            if (m_ViewRange.Equals(RANGE.WEEK))
            {
                m_WeekDifference--;
                UpdatePresenceGridInformationWeekFormat();
            }
            else if (m_ViewRange.Equals(RANGE.MONTH))
            {
                m_CurrentMonth = RealModulo(m_CurrentMonth - 1, 12);
                if (m_CurrentMonth == 0)
                {
                    m_YearDifference--;
                    m_CurrentMonth = 12;
                }
                UpdatePresenceGridInformationMonthFormat();
            }
        }

        // If the user made changes without saving them we need to tell him so he can decide what to do
        private bool AllChangesSaved()
        {
            if (m_PresenceChanges.Count != 0)
            {
                DialogResult res = MessageBox.Show("You have made "+m_PresenceChanges.Count+" changes that won't be saved.\nDo you want to continue?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);

                if (res == DialogResult.No)
                    return false;
            }
            return true;
        }

        private void dgvOverview_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            PersonForm form = PersonForm.GetInstance(PersonForm.FormType.MODIFICATION, int.Parse(dgvOverview.Rows[e.RowIndex].Cells["colOverPersonID"].Value.ToString()));
            form.FormClosing += OnFormClosing;

            if (!form.Visible)
                form.Show();
            else
                form.BringToFront();
        }

        private void btnAddPerson_Click(object sender, EventArgs e)
        {
            PersonForm form = PersonForm.GetInstance(PersonForm.FormType.ADDITION);
            form.FormClosing += OnFormClosing;

            if (!form.Visible)
                form.Show();
            else
                form.BringToFront();
        }
        
        public void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            GetPersonData();
        }

        private void btnAddProgram_Click(object sender, EventArgs e)
        {
            ProgramAdditionForm form = ProgramAdditionForm.GetInstance();
            if (!form.Visible)
                form.Show();
            else
                form.BringToFront();
        }

        private void GetPrograms()
        {
            using (SqlConnection conn = new SqlConnection())
            {
                DatabaseConnection.OpenConnection(conn);

                SqlCommand command = new SqlCommand("SELECT PROGRAM_ID AS ID, NAME FROM PROGRAM", conn);

                cbbPrograms.DisplayMember = "Text";
                cbbPrograms.ValueMember = "Value";

                Dictionary<Object, Object> comboSource = new Dictionary<Object, Object>();

                bool empty = true;
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        comboSource.Add(reader["ID"], reader["NAME"]);
                        empty = false;
                    }
                }

                if (!empty)
                {
                    cbbPrograms.DataSource = new BindingSource(comboSource, null);
                    cbbPrograms.DisplayMember = "Value";
                    cbbPrograms.ValueMember = "Key";
                    cbbPrograms.SelectedItem = null;
                }
            }
        }

        private void cbbPrograms_SelectedValueChanged(object sender, EventArgs e)
        {
            if (m_Initialisation)
                return;

            m_ProgramID = (int)((KeyValuePair<Object, Object>)cbbPrograms.SelectedItem).Key;
            UpdatePresenceGridInformationWeekFormat();
            gbPresence.Text = (String)((KeyValuePair<Object, Object>)cbbPrograms.SelectedItem).Value;
        }

        private void btnAddGroup_Click(object sender, EventArgs e)
        {
            GroupForm form = GroupForm.GetInstance();
            if (!form.Visible)
                form.Show();
            else
                form.BringToFront();
        }

        private void btnAddService_Click(object sender, EventArgs e)
        {
            ServiceForm form = ServiceForm.GetInstance();
            if (!form.Visible)
                form.Show();
            else
                form.BringToFront();
        }

        private void btnViewRange_Click(object sender, EventArgs e)
        {
            if (m_ViewRange.Equals(RANGE.WEEK))
            {
                btnViewRange.Text = "Weekly view";
                m_ViewRange = RANGE.MONTH;
                btnPreviousRange.Text = "Previous month";
                btnNextRange.Text = "Next month";
                UpdatePresenceGridInformationMonthFormat();
            }
            else if (m_ViewRange.Equals(RANGE.MONTH))
            {
                btnViewRange.Text = "Monthly view";
                m_ViewRange = RANGE.WEEK;
                btnPreviousRange.Text = "Previous week";
                btnNextRange.Text = "Next week";
                UpdatePresenceGridInformationWeekFormat();
            }
        }

        private void btnPublicHolidays_Click(object sender, EventArgs e)
        {
            HolidayManagerForm form = HolidayManagerForm.GetInstance();
            if (!form.Visible)
                form.Show();
            else
                form.BringToFront();
        }

        // Search by first name and last name
        private void txtSearchBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter) && txtSearchBox.Text != "")
            {
                cbbGroupIDs.SelectedIndex = -1;
                dgvOverview.Rows.Clear();

                using (SqlConnection conn = new SqlConnection())
                {
                    DatabaseConnection.OpenConnection(conn);

                    SqlCommand command = new SqlCommand("SELECT PERSON_ID, FIRSTNAME, LASTNAME, (SELECT CONVERT(varchar(10), BIRTHDAY, 103) AS [DD/MM/YYYY]) AS BIRTHDAY " +
                                                        "FROM PERSON " +
                                                        "WHERE FIRSTNAME LIKE @search OR LASTNAME LIKE @search", conn);
                    command.Parameters.AddWithValue("search", "%"+txtSearchBox.Text+"%");

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            dgvOverview.Rows.Add(reader["PERSON_ID"], reader["FIRSTNAME"], reader["LASTNAME"], reader["BIRTHDAY"]);
                        }
                    }
                }
            }
            // Reset
            else if(e.KeyCode.Equals(Keys.Enter) && txtSearchBox.Text == "")
            {
                GetPersonData();
            }
        }

        private void cbbGroupIDs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_Initialisation || cbbGroupIDs.SelectedIndex == -1)
                return;

            txtSearchBox.Text = "";
            dgvOverview.Rows.Clear();
            int groupID = (int)((KeyValuePair<Object, Object>)cbbGroupIDs.SelectedItem).Key;

            using (SqlConnection conn = new SqlConnection())
            {
                DatabaseConnection.OpenConnection(conn);

                SqlCommand command = new SqlCommand("SELECT p.PERSON_ID, p.FIRSTNAME, p.LASTNAME, (SELECT CONVERT(varchar(10), p.BIRTHDAY, 103) AS [DD/MM/YYYY]) AS BIRTHDAY " +
                                                    "FROM PERSON p, PERSON_GRUPO pg " +
                                                    "WHERE p.PERSON_ID = pg.ID_PERSON AND pg.ID_GRUPO = @groupID", conn);
                command.Parameters.AddWithValue("groupID", groupID);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        dgvOverview.Rows.Add(reader["PERSON_ID"], reader["FIRSTNAME"], reader["LASTNAME"], reader["BIRTHDAY"]);
                    }
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtSearchBox.Text = "";
            cbbGroupIDs.SelectedIndex = -1;
            GetPersonData();
        }

        private void btnCreateAttendanceSheets_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("You are going to create a pdf document with everyone in it!\nAre you sure you want to proceed?", "Attention", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res.Equals(DialogResult.Yes))
            {
                // Get the list of person IDs
                List<int> personIDs = new List<int>();
                foreach (DataRow row in dgvOverview.Rows)
                {
                    personIDs.Add((int)row["colOverPersonID"]);
                }
            }
        }
    }
}
