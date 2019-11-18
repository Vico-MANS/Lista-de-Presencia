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
    public partial class PersonForm : Form
    {
        public enum FormType { ADDITION, MODIFICATION };

        private static PersonForm s_Instance;

        public static PersonForm GetInstance(FormType type, int personID = -1)
        {
            if (type.Equals(FormType.ADDITION))
            {
                if (s_Instance == null)
                {
                    s_Instance = new PersonForm(type, personID);
                }
            }
            else if (type.Equals(FormType.MODIFICATION))
            {
                //if (s_Instance != null)
                //    Console.WriteLine("Current ID: " + s_Instance.m_PersonID + " & wanted ID: " + personID);
                if (s_Instance == null || (s_Instance != null && s_Instance.m_PersonID != personID))
                {
                    // We close the last form
                    if (s_Instance != null)
                        s_Instance.Close();
                    s_Instance = new PersonForm(type, personID);
                }
            }
            return s_Instance;
        }

        private bool m_Initialisation;

        private FormType m_FormType;
        private int m_PersonID;
        private bool m_IsWorker;
        // The programs that the person is assigned to in the database
        private List<int> m_InitPersonPrograms;

        // (ADDITION) Contains the cells that are checked for a person's addition, these cell's information need to be registered into the database
        private List<int> m_WeeklyPresence;
        // (MODIFICATION) Contains the cells that were initially checked, before the user could perform any modification
        private List<int> m_WeeklyPresenceCheckedCells = new List<int>();
        // (MODIFICATION) Contains the cells that got changed in comparision to the WeeklyPresenceCheckedCells, therefore it holds the information that has to be updated in the database
        private List<int> m_WeeklyPresenceChanges = new List<int>();

        // Keeps track of the group's that the person was added to
        private List<int> m_GroupIDs;

        /**
         * TO KNOW IF THE USER MADE ANY MODIFICATION (ON FORM CLOSING)
         * */
        private string m_InitFirstname;
        private string m_InitLastname;
        private string m_InitBirthday;        
        
        public PersonForm(FormType type, int personID)
        {            
            InitializeComponent();
            this.CenterToScreen();
            m_FormType = type;

            dgvWeeklyDetail.Rows.Clear();
            dgvWeeklyDetail.Rows.Add();

            if (type.Equals(FormType.ADDITION))
            {
                btnValidateForm.Text = "Add Person";
            }
            else if (type.Equals(FormType.MODIFICATION))
            {
                btnValidateForm.Text = "Update Person";
                m_PersonID = personID;
                LoadPersonInformation();
                RetrievePersonProgramInformation();
            }
            LoadPrograms();
            GetPrograms();
            m_WeeklyPresence = new List<int>();
            m_GroupIDs = new List<int>();
        }
        
        private void Form2_Load(object sender, EventArgs e)
        {
            //ClearPersonAdditionFields();
        }

        public void LoadPersonInformation()
        {
            RetrievePersonInformation();

            if (!m_IsWorker)
            {
                gbWeeklyPresence.Visible = true;
                RetrieveWeeklyPresenceInformation();
            }
            else
            {
                gbWeeklyPresence.Visible = false;
            }
        }

        private void RetrievePersonInformation()
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
                    m_IsWorker = (bool)reader["WORKER"];

                    m_InitFirstname = txtFirstname.Text;
                    m_InitLastname = txtLastname.Text;
                    m_InitBirthday = dtpBirthday.Value.ToString();

                    cbWorker.Checked = m_IsWorker;
                    cbWorker.Enabled = false;
                }
            }
        }

        private void RetrievePersonProgramInformation()
        {
            using (SqlConnection conn = new SqlConnection())
            {
                DatabaseConnection.OpenConnection(conn);

                SqlCommand command = new SqlCommand("SELECT * FROM PERSON_PROGRAM WHERE ID_PERSON = @id", conn);
                command.Parameters.AddWithValue("id", m_PersonID);

                m_InitPersonPrograms = new List<int>();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        m_InitPersonPrograms.Add((int)reader["ID_PROGRAM"]);
                    }
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
                command.Parameters.AddWithValue("id", m_PersonID);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        dgvWeeklyDetail.Rows[0].Cells[(int)reader["WEEK_DAY"] - 1].Value = true;
                        m_WeeklyPresenceCheckedCells.Add((int)reader["WEEK_DAY"]);
                    }
                }

                m_Initialisation = false;
            }
        }

        private void LoadPrograms()
        {
            using (SqlConnection conn = new SqlConnection())
            {
                DatabaseConnection.OpenConnection(conn);

                SqlCommand command = new SqlCommand("SELECT PROGRAM_ID, NAME FROM PROGRAM", conn);

                int programCounter = 0;
                
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    int x = 30;
                    int y = 15;
                    while (reader.Read())
                    {
                        CheckBox box = new CheckBox
                        {
                            Tag = reader["PROGRAM_ID"].ToString(),
                            Text = reader["NAME"].ToString(),
                            AutoSize = true
                        };

                        if (m_FormType.Equals(FormType.MODIFICATION) && m_InitPersonPrograms.Contains((int)reader["PROGRAM_ID"]))
                                box.Checked = true;
                        
                        if (programCounter > 1 && programCounter % 5 == 0)
                        {
                            x += 200;
                            y = 15;
                        }
                        else if(programCounter > 0)
                        {
                            y += 25;
                        }

                        box.Location = new Point(x, y);
                        gbPrograms.Controls.Add(box);
                        programCounter++;
                    }
                }

                // TODO: What to do if there's not a single program in the database? 'cause we can't add "non workers" then...
            }
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

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        comboSource.Add(reader["ID"], reader["NAME"]);
                    }
                }

                cbbPrograms.DataSource = new BindingSource(comboSource, null);
                cbbPrograms.DisplayMember = "Value";
                cbbPrograms.ValueMember = "Key";
                cbbPrograms.SelectedItem = null;
            }
        }

        private void GetProgramGroups(int programID)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                DatabaseConnection.OpenConnection(conn);

                SqlCommand command = new SqlCommand("SELECT GRUPO_ID AS ID, GRUPO.NAME+' ('+CONVERT(NVARCHAR,GRUPO_ID)+')' AS NAME FROM GRUPO WHERE ID_SERVICIO IN (SELECT SERVICIO_ID FROM SERVICIO WHERE ID_PROGRAM = @programID);", conn);
                command.Parameters.AddWithValue("programID", programID);

                cbbProgramGroups.DisplayMember = "Text";
                cbbProgramGroups.ValueMember = "Value";

                Dictionary<Object, Object> comboSource = new Dictionary<Object, Object>();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        comboSource.Add(reader["ID"], reader["NAME"]);
                    }
                }

                cbbProgramGroups.DataSource = new BindingSource(comboSource, null);
                cbbProgramGroups.DisplayMember = "Value";
                cbbProgramGroups.ValueMember = "Key";
                cbbProgramGroups.SelectedItem = null;
            }
        }

        private void ClearPersonAdditionFields()
        {
            txtFirstname.Clear();
            txtLastname.Clear();
            dtpBirthday.Value = DateTime.Now;

            //foreach(CheckBox cb in gbPrograms.Controls.OfType<CheckBox>())
            //    cb.Checked = false;

            cbWorker.Checked = false;

            dgvWeeklyDetail.Rows.Clear();
            dgvWeeklyDetail.Rows.Add();
            dgvWeeklyDetail.ClearSelection();

            ClearGroupInfoBox();

            m_WeeklyPresence.Clear();
        }

        // Checks if all the necessary fields have been filled out
        private bool IsPersonAdditionValid()
        {
            // The person has to have a first and last name
            if (txtFirstname.Text == "" || txtLastname.Text == "")
                return false;

            // TODO: What about parents? (there aren't part of any program)
            // If the person isn't working here, it has to be part of a program
            if(!cbWorker.Checked)
            {
                // If this is true it means that the person is part of at least one group
                return gbGroupInfo.Controls.Count > 3;
                //bool found = false;
                //foreach(CheckBox cb in gbPrograms.Controls.OfType<CheckBox>())
                //{
                //    if (cb.Checked)
                //    {
                //        found = true;
                //        break;
                //    }
                //}
                //if (!found)
                //    return false;
            }
            return true;
        }

        private void dgvWeeklyDetail_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (m_Initialisation)
                return;

            if (e.RowIndex != -1)
            {
                if (m_FormType.Equals(FormType.ADDITION))
                {
                    if ((bool)dgvWeeklyDetail.Rows[e.RowIndex].Cells[e.ColumnIndex].Value)
                        m_WeeklyPresence.Add(e.ColumnIndex + 1);
                    else
                        m_WeeklyPresence.Remove(e.ColumnIndex + 1);
                }
                else if (m_FormType.Equals(FormType.MODIFICATION))
                {
                    if (((bool)dgvWeeklyDetail.Rows[e.RowIndex].Cells[e.ColumnIndex].Value && !m_WeeklyPresenceCheckedCells.Contains(e.ColumnIndex + 1))
                       || (!(bool)dgvWeeklyDetail.Rows[e.RowIndex].Cells[e.ColumnIndex].Value && m_WeeklyPresenceCheckedCells.Contains(e.ColumnIndex + 1)))
                        m_WeeklyPresenceChanges.Add(e.ColumnIndex + 1);
                    else
                        m_WeeklyPresenceChanges.Remove(e.ColumnIndex + 1);
                }
            }
        }

        private void dgvWeeklyDetail_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex != -1) 
            {
                dgvWeeklyDetail.EndEdit();
            }
        }

        private void btnAddPerson_Click(object sender, EventArgs e)
        {
            if(IsPersonAdditionValid())
            {
                if (m_FormType.Equals(FormType.ADDITION))
                    AddPerson();
                else if (m_FormType.Equals(FormType.MODIFICATION))
                    UpdatePerson();
            }
            else
            {
                MessageBox.Show("Please fill out all the mandatory fields!");
            }
        }

        // Maybe change this later so that we don't insert all the stuff all the time when there's like no or only a minor change
        // We can't change the worker statement, it doesn't really make sense to do that anyway.
        private void UpdatePerson()
        {
            using (SqlConnection conn = new SqlConnection())
            {
                DatabaseConnection.OpenConnection(conn);
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    /*
                     * UPDATE PERSON TABLE
                     * */
                    SqlCommand commandUpdatePerson = new SqlCommand("UPDATE PERSON SET FIRSTNAME = @firstname, LASTNAME = @lastname, BIRTHDAY = @birthday WHERE PERSON_ID = @id", conn, transaction);
                    commandUpdatePerson.Parameters.AddWithValue("firstname", txtFirstname.Text);
                    commandUpdatePerson.Parameters.AddWithValue("lastname", txtLastname.Text);
                    commandUpdatePerson.Parameters.AddWithValue("birthday", dtpBirthday.Value);
                    commandUpdatePerson.Parameters.AddWithValue("id", m_PersonID);
                    commandUpdatePerson.ExecuteNonQuery();

                    m_InitFirstname = txtFirstname.Text;
                    m_InitLastname = txtLastname.Text;
                    m_InitBirthday = dtpBirthday.Value.ToString();

                    Console.WriteLine("Person table UPDATED");

                    /*
                     * UPDATE PERSON_PROGRAM TABLE
                     * */
                    // First we have to get all the programs that the person has now
                    List<int> currentPersonPrograms = new List<int>();
                    foreach (CheckBox cb in gbPrograms.Controls.OfType<CheckBox>())
                        if (cb.Checked)
                            currentPersonPrograms.Add(Convert.ToInt32(cb.Tag));
                    // Now that we got that information we can compare it to the initial values
                    foreach(int programID in currentPersonPrograms)
                    {
                        // If the value isn't present in the initial list it means that the person got added to the program, we have to register it in the database
                        if(!m_InitPersonPrograms.Contains(programID))
                        {
                            // Add program to person
                            SqlCommand commandAddPersonProgram = new SqlCommand("INSERT INTO PERSON_PROGRAM (ID_PERSON, ID_PROGRAM) VALUES (@idPerson, @idProgram)", conn, transaction);
                            commandAddPersonProgram.Parameters.AddWithValue("idPerson", m_PersonID);
                            commandAddPersonProgram.Parameters.AddWithValue("idProgram", programID);
                            commandAddPersonProgram.ExecuteNonQuery();
                        }
                        // The person is already assigned to that program
                        else
                        {
                            // We remove it from the list so we can keep track of which programs we have to delete from the database later
                            m_InitPersonPrograms.Remove(programID);
                        }
                    }
                    // Now that we are here all the program IDs left in the m_InitPersonPrograms list should be those that we have to remove from the database for that person
                    foreach(int programID in m_InitPersonPrograms)
                    {
                        SqlCommand commandDeletePersonProgram = new SqlCommand("DELETE FROM PERSON_PROGRAM WHERE ID_PERSON = @idPerson AND ID_PROGRAM = @idProgram", conn, transaction);
                        commandDeletePersonProgram.Parameters.AddWithValue("idPerson", m_PersonID);
                        commandDeletePersonProgram.Parameters.AddWithValue("idProgram", programID);
                        commandDeletePersonProgram.ExecuteNonQuery();
                    }
                    // Now we update the list with the right values
                    m_InitPersonPrograms = currentPersonPrograms;
                    Console.WriteLine("Person_Program table UPDATED");
                    
                    /*
                     * UPDATE WEEKLY_PRESENCE TABLE
                     * */
                    if (!m_IsWorker)
                    {
                        // The list contains ONLY the changes to the weekly presence
                        foreach(int weekday in m_WeeklyPresenceChanges)
                        {
                            // If this particular weekday is checked we need to add that information to the database
                            if ((bool)dgvWeeklyDetail.Rows[0].Cells[weekday - 1].Value)
                            {
                                SqlCommand commandInsertWeeklyPresence = new SqlCommand("INSERT INTO WEEKLY_PRESENCE (ID_PERSON, WEEK_DAY) VALUES (@idPerson, @weekday)", conn, transaction);
                                commandInsertWeeklyPresence.Parameters.AddWithValue("idPerson", m_PersonID);
                                commandInsertWeeklyPresence.Parameters.AddWithValue("weekday", weekday);
                                commandInsertWeeklyPresence.ExecuteNonQuery();
                            }
                            // If it's not checked we need to remove this weekday from the database
                            else
                            {
                                SqlCommand commandDeleteWeeklyPresence = new SqlCommand("DELETE FROM WEEKLY_PRESENCE WHERE ID_PERSON = @idPerson AND WEEK_DAY = @weekday", conn, transaction);
                                commandDeleteWeeklyPresence.Parameters.AddWithValue("idPerson", m_PersonID);
                                commandDeleteWeeklyPresence.Parameters.AddWithValue("weekday", weekday);
                                commandDeleteWeeklyPresence.ExecuteNonQuery();
                            }
                        }
                        Console.WriteLine("Weekly_Presence table UPDATED");
                    }

                    transaction.Commit();

                    MessageBox.Show("The person's information has been successfully updated!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch(Exception e)
                {
                    transaction.Rollback();

                    Console.WriteLine(e);
                    MessageBox.Show("An error has occured!\nThe person couldn't be updated in the database...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void AddPerson()
        {
            using (SqlConnection conn = new SqlConnection())
            {
                DatabaseConnection.OpenConnection(conn);

                SqlTransaction transaction = conn.BeginTransaction();
                try
                {
                    /**
                     * INSERT PERSON
                     * */

                    SqlCommand commandInsertPerson = new SqlCommand("INSERT INTO PERSON (FIRSTNAME, LASTNAME, BIRTHDAY, WORKER) VALUES (@firstname, @lastname, @birthday, @worker)", conn, transaction);
                    commandInsertPerson.Parameters.AddWithValue("firstname", txtFirstname.Text);
                    commandInsertPerson.Parameters.AddWithValue("lastname", txtLastname.Text);
                    commandInsertPerson.Parameters.AddWithValue("birthday", dtpBirthday.Value);
                    commandInsertPerson.Parameters.AddWithValue("worker", cbWorker.Checked ? 1 : 0);

                    commandInsertPerson.ExecuteNonQuery();
                    Console.WriteLine("Person inserted");

                    /**
                     * RETRIEVE PERSON ID
                     * */

                    SqlCommand commandGetPersonID = new SqlCommand("SELECT MAX(PERSON_ID) AS LAST_ID FROM PERSON WHERE FIRSTNAME = @firstname AND LASTNAME = @lastname", conn, transaction);
                    commandGetPersonID.Parameters.Add(new SqlParameter("firstname", txtFirstname.Text));
                    commandGetPersonID.Parameters.Add(new SqlParameter("lastname", txtLastname.Text));

                    int personID;
                    using (SqlDataReader reader = commandGetPersonID.ExecuteReader())
                    {
                        reader.Read();
                        personID = (int)reader["LAST_ID"];
                        Console.WriteLine("ID retrieved: " + personID);
                    }

                    /**
                     * INSERT PROGRAMS (useless since we do it with groups now)
                     * */

                    foreach (CheckBox cb in gbPrograms.Controls.OfType<CheckBox>())
                    {
                        if (cb.Checked)
                        {
                            //MessageBox.Show("Checkbox " + cb.Text + " (" + cb.Tag + "): " + cb.Checked);
                            SqlCommand commandAddProgramToPerson = new SqlCommand("INSERT INTO PERSON_PROGRAM VALUES (@personID, @programID)", conn, transaction);
                            commandAddProgramToPerson.Parameters.AddWithValue("personID", personID);
                            commandAddProgramToPerson.Parameters.AddWithValue("programID", cb.Tag);

                            commandAddProgramToPerson.ExecuteNonQuery();
                        }
                    }

                    /**
                     * INSERT GROUPS
                     * */

                    foreach(int groupID in m_GroupIDs)
                    {
                        SqlCommand commandAddGroupToPerson = new SqlCommand("INSERT INTO PERSON_GRUPO VALUES (@personID, @groupID)", conn, transaction);
                        commandAddGroupToPerson.Parameters.AddWithValue("personID", personID);
                        commandAddGroupToPerson.Parameters.AddWithValue("groupID", groupID);

                        commandAddGroupToPerson.ExecuteNonQuery();
                    }

                    Console.WriteLine("Group assignement inserted");

                    /**
                     * INSERT WEEKLY PRESENCE
                     * */

                    if (!cbWorker.Checked)
                    {
                        Console.Write("Week days: ");
                        foreach (int weekday in m_WeeklyPresence)
                        {
                            Console.Write(weekday + " ");
                        }
                        Console.WriteLine();

                        foreach (int weekday in m_WeeklyPresence)
                        {
                            SqlCommand command = new SqlCommand("INSERT INTO WEEKLY_PRESENCE VALUES (@id, @weekday)", conn, transaction);
                            command.Parameters.AddWithValue("id", personID);
                            command.Parameters.AddWithValue("weekday", weekday);
                            command.ExecuteNonQuery();
                        }
                    }

                    Console.WriteLine("Weekly presence inserted");

                    transaction.Commit();

                    MessageBox.Show("The person has been succesfully added to the database", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    ClearPersonAdditionFields();
                }
                catch (Exception excep)
                {
                    transaction.Rollback();

                    Console.WriteLine(excep);
                    MessageBox.Show("An error has occured!\nThe person couldn't be added to the database...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void cbWorker_CheckedChanged(object sender, EventArgs e)
        {
            gbWeeklyPresence.Enabled = !cbWorker.Checked;
        }

        private void dgvWeeklyDetail_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1 || e.ColumnIndex == -1)
                return;

            // This works, but the problem is that the cell click event is not always called, if we click too fast.
            if (dgvWeeklyDetail.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == null || (bool)dgvWeeklyDetail.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == false)
                dgvWeeklyDetail.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = true;
            else
                dgvWeeklyDetail.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = false;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            s_Instance.Close();
        }

        private void PersonForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Only for modification!?
            if (m_FormType.Equals(FormType.MODIFICATION))
            {
                bool modificationInPrograms = false;
                List<int> currentPersonPrograms = new List<int>();
                foreach (CheckBox cb in gbPrograms.Controls.OfType<CheckBox>())
                    if (cb.Checked)
                        currentPersonPrograms.Add(Convert.ToInt32(cb.Tag));
                // Now that we got that information we can compare it to the initial values
                foreach (int programID in currentPersonPrograms)
                {
                    // If the value isn't present in the initial list it means that the user just checked the box
                    if (!m_InitPersonPrograms.Contains(programID))
                        modificationInPrograms = true;
                }
                // A bit redundant, but necessary
                foreach (int programID in m_InitPersonPrograms)
                {
                    // If the value isn't present in the current list it means that the user just unchecked the box
                    if (!currentPersonPrograms.Contains(programID))
                        modificationInPrograms = true;
                }

                if (m_InitFirstname != txtFirstname.Text || m_InitLastname != txtLastname.Text || m_InitBirthday != dtpBirthday.Value.ToString() || modificationInPrograms || m_WeeklyPresenceChanges.Count > 0)
                {
                    DialogResult res = MessageBox.Show("If you have made changes, these won't be saved!", "Information", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
                    e.Cancel = res.Equals(DialogResult.Cancel);
                }
            }
        }

        private void PersonForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            // When the form is closed again we set the reference of the singleton to null
            s_Instance = null;
        }

        private void cbbPrograms_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbbPrograms.SelectedIndex != -1)
                GetProgramGroups((int)((KeyValuePair<Object, Object>)cbbPrograms.SelectedItem).Key);
        }

        private void btnAddGroup_Click(object sender, EventArgs e)
        {
            if (cbbProgramGroups.SelectedItem == null)
                return;

            int groupID = (int)((KeyValuePair<Object, Object>)cbbProgramGroups.SelectedItem).Key;
            if (m_GroupIDs.Contains(groupID))
            {
                ResetGroupAddition();
                return;
            }
            // We keep track of the group's ID to insert it later in the database
            m_GroupIDs.Add(groupID);

            // We also add a line in the form to show the user what he just added
            UpdateGroupInfoBox();
            
            ResetGroupAddition();
        }

        private void ClearGroupInfoBox()
        {
            // We read it in reverse because we want to delete elements while reading
            for (int i = gbGroupInfo.Controls.Count - 1; i >= 0; i--)
            {
                Control control = gbGroupInfo.Controls[i];
                // The legend labels are tagged 'Freeze' and shouldn't be deleted
                if (control.Tag == null || !control.Tag.Equals("Freeze"))
                    gbGroupInfo.Controls.Remove(control);
            }
        }

        // Updates the GroupBox that holds the information about the groups to which the person belongs
        private void UpdateGroupInfoBox()
        {
            ClearGroupInfoBox();

            // Not the cleanest way to do it since we retrieved the information of the form load...
            using (SqlConnection conn = new SqlConnection())
            {
                DatabaseConnection.OpenConnection(conn);

                int counter = 0;
                foreach(int groupID in m_GroupIDs)
                {
                    SqlCommand command = new SqlCommand("SELECT GRUPO_ID, NAME, CONVERT(VARCHAR, START_DATE, 103) AS START_DATE, CONVERT(VARCHAR, END_DATE, 103) AS END_DATE FROM GRUPO WHERE GRUPO_ID = @groupID", conn);
                    command.Parameters.AddWithValue("groupID", groupID);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        reader.Read();

                        int y = lblGroupNameLegend.Location.Y + 20 + counter * 20;
                        Label lblGroupName = new Label
                        {
                            Text = reader["NAME"].ToString(),
                            Location = new Point(lblGroupNameLegend.Location.X, y),
                            AutoSize = true
                        };
                        gbGroupInfo.Controls.Add(lblGroupName);

                        Label lblGroupID = new Label
                        {
                            Text = reader["GRUPO_ID"].ToString(),
                            Location = new Point(lblGroupIDLegend.Location.X, y),
                            AutoSize = true
                        };
                        gbGroupInfo.Controls.Add(lblGroupID);

                        Label lblGroupDates = new Label
                        {
                            Text = reader["START_DATE"].ToString() + " - " + reader["END_DATE"].ToString(),
                            Location = new Point(lblGroupDatesLegend.Location.X, y),
                            AutoSize = true
                        };
                        gbGroupInfo.Controls.Add(lblGroupDates);

                        Button btnDeleteGroupRelation = new Button
                        {
                            Text = "-",
                            Location = new Point(lblGroupDates.Right + 10, y - 4),
                            Tag = reader["GRUPO_ID"],
                            TextAlign = ContentAlignment.MiddleCenter,
                            Size = new Size(25, 20)
                        };
                        btnDeleteGroupRelation.Click += btnDeleteGroupRelation_Click;
                        gbGroupInfo.Controls.Add(btnDeleteGroupRelation);

                        counter++;
                    }
                }                
            }
        }

        private void btnDeleteGroupRelation_Click(object sender, EventArgs e)
        {
            m_GroupIDs.Remove((int)((Button)sender).Tag);            
            UpdateGroupInfoBox();
        }

        private void ResetGroupAddition()
        {
            cbbPrograms.SelectedItem = null;
            cbbProgramGroups.SelectedItem = null;
        }
    }
}
