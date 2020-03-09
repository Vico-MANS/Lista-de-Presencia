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
        // The groups that the person is assigned to in the database
        private List<int> m_InitPersonGroups;
        // Keeps track of the group's that the person was added to
        private List<int> m_GroupIDs;

        // (ADDITION) Contains the cells that are checked for a person's addition, these cell's information need to be registered into the database
        private List<int> m_WeeklyPresence;
        // (MODIFICATION) Contains the cells that were initially checked, before the user could perform any modification
        private List<int> m_WeeklyPresenceCheckedCells = new List<int>();
        // (MODIFICATION) Contains the cells that got changed in comparision to the WeeklyPresenceCheckedCells, therefore it holds the information that has to be updated in the database
        private List<int> m_WeeklyPresenceChanges = new List<int>();
        
        /**
         * TO KNOW IF THE USER MADE ANY MODIFICATION (ON FORM CLOSING)
         * */
        // Do a dictionary with the control id as key and it's value as value. On control changed check if the current value is different from the dictionary one!
        private Dictionary<string, object> m_InitialValuesDictionary;
        // Holds the updated values
        private Dictionary<string, object> m_UpdatedValuesDictionary;
        
        public PersonForm(FormType type, int personID)
        {            
            InitializeComponent();
            this.CenterToScreen();
            m_FormType = type;

            dgvWeeklyDetail.Rows.Clear();
            dgvWeeklyDetail.Rows.Add();
            m_GroupIDs = new List<int>();

            if (type.Equals(FormType.ADDITION))
            {
                btnValidateForm.Text = "Add Person";
                btnPrintAttendanceSheet.Hide();
            }
            else if (type.Equals(FormType.MODIFICATION))
            {
                btnValidateForm.Text = "Update Person";
                m_PersonID = personID;
                LoadPersonInformation();
                RetrievePersonGroupInformation();
                //RetrievePersonProgramInformation();
            }
            //LoadPrograms();
            GetPrograms();
            m_WeeklyPresence = new List<int>();

            txtPersonType.Select();
        }
        
        private void Form2_Load(object sender, EventArgs e)
        {
            //ClearPersonAdditionFields();
        }

        public void LoadPersonInformation()
        {
            m_Initialisation = true;

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

            m_Initialisation = false;
        }

        private void RetrievePersonInformation()
        {
            using (SqlConnection conn = new SqlConnection())
            {
                DatabaseConnection.OpenConnection(conn);

                SqlCommand command = new SqlCommand("SELECT PERSON_TYPE, FIRSTNAME, LASTNAME, DOCUMENT_TYPE, DOCUMENT, " +
                                                        "BIRTHDAY, GENDER, NAME_DAY, IMAGE_RIGHTS, DATA_RIGHTS, WORKER, NATIONALITY," +
                                                        "YEARS_IN_COUNTRY, SCHOOL_NAME, SCHOOL_YEAR, STREET_TYPE, ADDRESS, HOME_NUMBER, " +
                                                        "HOME_STAIRS, HOME_STOREY, HOME_DOOR, HOME_ZIP_CODE, HOME_POPULATION, " +
                                                        "HOME_REGION, HOME_PROVINCE, HOME_COUNTRY, HOME_LANDLINE_NUMBER, CELLPHONE_NUMBER, " +
                                                        "EMAIL, ID_FATHER, ID_MOTHER, FAMILY_EMAIL, OTHER_CONTACT_RELATION," +
                                                        "OTHER_CONTACT_NAME, OTHER_CONTACT_PHONE, NUMBER_OF_BROTHERS, NUMBER_OF_SISTERS, " +
                                                        "SIBLING_RANK, PICK_OFF_PEOPLE, PROFESSION, BANK_IBAN, BANK_ACCOUNT_OWNER, OBSERVATIONS, " +
                                                        "FOOD_ALLERGY_CHRONIC_DISEASES, MEMBER_NUMBER, HEALTH_CARD_NUMBER " +
                                                    "FROM PERSON " +
                                                    "WHERE PERSON_ID = @id", conn);
                command.Parameters.AddWithValue("id", m_PersonID);

                // Dictionary used to check if the user modified some values in the form or not
                m_InitialValuesDictionary = new Dictionary<string, object>();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    // Should only be one retrieved row
                    reader.Read();

                    txtPersonType.Text = reader["PERSON_TYPE"].ToString();
                    txtFirstName.Text = reader["FIRSTNAME"].ToString();
                    txtLastName.Text = reader["LASTNAME"].ToString();
                    txtDocumentType.Text = reader["DOCUMENT_TYPE"].ToString();
                    txtDocument.Text = reader["DOCUMENT"].ToString();
                    dtpBirthday.Value = Convert.ToDateTime(reader["BIRTHDAY"]);
                    cbbGender.SelectedItem = reader["GENDER"].ToString();
                    txtNameDay.Text = reader["NAME_DAY"].ToString();
                    cbImageRights.Checked = (bool)reader["IMAGE_RIGHTS"];
                    cbDataRights.Checked = (bool)reader["DATA_RIGHTS"];
                    m_IsWorker = (bool)reader["WORKER"];
                    txtNationality.Text = reader["NATIONALITY"].ToString();
                    txtYearsInCountry.Text = reader["YEARS_IN_COUNTRY"].ToString();
                    txtSchoolName.Text = reader["SCHOOL_NAME"].ToString();
                    txtSchoolYear.Text = reader["SCHOOL_YEAR"].ToString();
                    txtStreetType.Text = reader["STREET_TYPE"].ToString();
                    txtAddress.Text = reader["ADDRESS"].ToString();
                    txtHomeNumber.Text = reader["HOME_NUMBER"].ToString();
                    txtHomeStairs.Text = reader["HOME_STAIRS"].ToString();
                    txtHomeStorey.Text = reader["HOME_STOREY"].ToString();
                    txtHomeDoor.Text = reader["HOME_DOOR"].ToString();
                    txtZIPCode.Text = reader["HOME_ZIP_CODE"].ToString();
                    txtPopulation.Text = reader["HOME_POPULATION"].ToString();
                    txtRegion.Text = reader["HOME_REGION"].ToString();
                    txtProvince.Text = reader["HOME_PROVINCE"].ToString();
                    txtCountry.Text = reader["HOME_COUNTRY"].ToString();
                    txtLandlineNumber.Text = reader["HOME_LANDLINE_NUMBER"].ToString();
                    txtCellphoneNumber.Text = reader["CELLPHONE_NUMBER"].ToString();
                    txtEmail.Text = reader["EMAIL"].ToString();
                    txtFatherID.Text = reader["ID_FATHER"].ToString();
                    if(txtFatherID.Text != "")
                        GetParentInformation(SearchParentForm.PARENT_TYPE.FATHER, int.Parse(txtFatherID.Text));
                    txtMotherID.Text = reader["ID_MOTHER"].ToString();
                    if(txtMotherID.Text != "")
                        GetParentInformation(SearchParentForm.PARENT_TYPE.MOTHER, int.Parse(txtMotherID.Text));
                    txtFatherEmail.Text = reader["FAMILY_EMAIL"].ToString();
                    txtOtherContactRelation.Text = reader["OTHER_CONTACT_RELATION"].ToString();
                    txtOtherContactName.Text = reader["OTHER_CONTACT_NAME"].ToString();
                    txtOtherContactPhone.Text = reader["OTHER_CONTACT_PHONE"].ToString();
                    txtNumberOfBrothers.Text = reader["NUMBER_OF_BROTHERS"].ToString();
                    txtNumberOfSisters.Text = reader["NUMBER_OF_SISTERS"].ToString();
                    txtSiblingRank.Text = reader["SIBLING_RANK"].ToString();
                    rtxtPeopleAllowedToPickOff.Text = reader["PICK_OFF_PEOPLE"].ToString();
                    txtProfession.Text = reader["PROFESSION"].ToString();
                    txtIBAN.Text = reader["BANK_IBAN"].ToString();
                    txtBankAccountOwner.Text = reader["BANK_ACCOUNT_OWNER"].ToString();
                    rtxtObservations.Text = reader["OBSERVATIONS"].ToString();
                    rtxtFoodAndDisease.Text = reader["FOOD_ALLERGY_CHRONIC_DISEASES"].ToString();
                    txtMemberNumber.Text = reader["MEMBER_NUMBER"].ToString();
                    txtHealthCardNumber.Text = reader["HEALTH_CARD_NUMBER"].ToString();

                    List<Control> controlList = new List<Control>();
                    GetAllControlsOfType(this, controlList, typeof(TextBox));
                    foreach(TextBox tb in controlList)
                    {
                        tb.TextChanged += new EventHandler(OnContentChanged);
                        m_InitialValuesDictionary.Add(tb.Name, tb.Text);
                    }
                    controlList = new List<Control>();
                    GetAllControlsOfType(this, controlList, typeof(CheckBox));
                    foreach(CheckBox cb in controlList)
                    {
                        cb.CheckedChanged += new EventHandler(OnContentChanged);
                        m_InitialValuesDictionary.Add(cb.Name, cb.Checked);
                    }
                    controlList = new List<Control>();
                    GetAllControlsOfType(this, controlList, typeof(RichTextBox));
                    foreach(RichTextBox rtb in controlList)
                    {
                        rtb.TextChanged += new EventHandler(OnContentChanged);
                        m_InitialValuesDictionary.Add(rtb.Name, rtb.Text);
                    }
                    controlList = new List<Control>();
                    GetAllControlsOfType(this, controlList, typeof(ComboBox));
                    foreach(ComboBox cbb in controlList)
                    {
                        cbb.SelectedIndexChanged += new EventHandler(OnContentChanged);
                        m_InitialValuesDictionary.Add(cbb.Name, (cbb.SelectedItem == null ? "" : cbb.SelectedItem.ToString()));
                    }
                    controlList = new List<Control>();
                    GetAllControlsOfType(this, controlList, typeof(DateTimePicker));
                    foreach(DateTimePicker dtp in controlList)
                    {
                        dtp.ValueChanged += new EventHandler(OnContentChanged);
                        m_InitialValuesDictionary.Add(dtp.Name, dtp.Value);
                    }

                    m_UpdatedValuesDictionary = new Dictionary<string, object>(m_InitialValuesDictionary);

                    cbWorker.Checked = m_IsWorker;
                    cbWorker.Enabled = false;

                    btnPrintAttendanceSheet.Visible = !m_IsWorker;
                }
            }
        }

        private void GetAllControlsOfType(Control container, List<Control> list, Type type)
        {
            foreach(Control c in container.Controls)
            {
                if (!c.GetType().Equals(typeof(TextBox)) || (c.GetType().Equals(typeof(TextBox)) && !((TextBox)c).ReadOnly))
                {
                    if (c.GetType().Equals(type))
                        list.Add(c);
                    if (c.HasChildren)
                        GetAllControlsOfType(c, list, type);
                }
            }
        }

        private void OnContentChanged(object sender, EventArgs e)
        {
            if (m_Initialisation)
                return;

            switch (sender)
            {
                case TextBox tb:
                    Console.WriteLine(((TextBox)sender).Name);
                    m_UpdatedValuesDictionary[((TextBox)sender).Name] = ((TextBox)sender).Text;
                    if (m_InitialValuesDictionary[((TextBox)sender).Name].ToString() != ((TextBox)sender).Text)
                        Console.WriteLine(((TextBox)sender).Name + " has changed value");
                    else
                        Console.WriteLine(((TextBox)sender).Name + " has the init value");
                    break;
                case RichTextBox rtb:
                    Console.WriteLine(((RichTextBox)sender).Name);
                    m_UpdatedValuesDictionary[((RichTextBox)sender).Name] = ((RichTextBox)sender).Text;
                    if (m_InitialValuesDictionary[((RichTextBox)sender).Name].ToString() != ((RichTextBox)sender).Text)
                        Console.WriteLine(((RichTextBox)sender).Name + " has changed value");
                    else
                        Console.WriteLine(((RichTextBox)sender).Name + " has the init value");
                    break;
                case ComboBox cbb:
                    // We don't want to keep track of these two ComboBoxes because they don't hold the actual information
                    if (cbb.Equals(cbbPrograms) || cbb.Equals(cbbProgramGroups))
                        break;
                    Console.WriteLine(((ComboBox)sender).Name);
                    m_UpdatedValuesDictionary[((ComboBox)sender).Name] = (cbb.SelectedItem == null ? "" : cbb.SelectedItem.ToString());
                    if (m_InitialValuesDictionary[((ComboBox)sender).Name].ToString() != (((ComboBox)sender).SelectedItem == null ? "" : ((ComboBox)sender).SelectedItem.ToString()))
                        Console.WriteLine(((ComboBox)sender).Name + " has changed value");
                    else
                        Console.WriteLine(((ComboBox)sender).Name + " has the init value");
                    break;
                case CheckBox cb:
                    Console.WriteLine(((CheckBox)sender).Name);
                    m_UpdatedValuesDictionary[((CheckBox)sender).Name] = cb.Checked;
                    if (m_InitialValuesDictionary[((CheckBox)sender).Name].ToString() != ((CheckBox)sender).Checked.ToString())
                        Console.WriteLine(((CheckBox)sender).Name + " has changed value");
                    else
                        Console.WriteLine(((CheckBox)sender).Name + " has the init value");
                    break;
                case DateTimePicker dtp:
                    Console.WriteLine(((DateTimePicker)sender).Name);
                    m_UpdatedValuesDictionary[((DateTimePicker)sender).Name] = dtp.Value;
                    if (m_InitialValuesDictionary[((DateTimePicker)sender).Name].ToString() != ((DateTimePicker)sender).Value.ToString())
                        Console.WriteLine(((DateTimePicker)sender).Name + " has changed value");
                    else
                        Console.WriteLine(((DateTimePicker)sender).Name + " has the init value");
                    break;
            }
            CheckForChanges();
        }

        private void RetrievePersonGroupInformation()
        {
            using (SqlConnection conn = new SqlConnection())
            {
                DatabaseConnection.OpenConnection(conn);

                // We get all the groups ID that the person is part from
                SqlCommand command = new SqlCommand("SELECT ID_GRUPO AS ID FROM PERSON_GRUPO WHERE ID_PERSON = @personID", conn);
                command.Parameters.AddWithValue("personID", m_PersonID);

                m_InitPersonGroups = new List<int>();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        m_GroupIDs.Add((int)reader["ID"]);
                        m_InitPersonGroups.Add((int)reader["ID"]);
                    }
                }
                // Then we show the information on the screen (a bit redundant since we retrieve the group's information again...)
                UpdateGroupInfoBox();
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

                SqlCommand command = new SqlCommand("SELECT GRUPO_ID AS ID, GRUPO.NAME+' ('+CONVERT(NVARCHAR,GRUPO_ID)+')' AS NAME FROM GRUPO WHERE ID_SERVICIO IN (SELECT SERVICIO_ID FROM SERVICIO WHERE ID_PROGRAM = @programID)", conn);
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

                if (comboSource.Count == 0)
                {
                    cbbProgramGroups.DataSource = new BindingSource(null, null);
                    return;
                }

                cbbProgramGroups.DataSource = new BindingSource(comboSource, null);
                cbbProgramGroups.DisplayMember = "Value";
                cbbProgramGroups.ValueMember = "Key";
                cbbProgramGroups.SelectedItem = null;
            }
        }

        private void ClearPersonAdditionFields()
        {
            txtFirstName.Clear();
            txtLastName.Clear();
            dtpBirthday.Value = DateTime.Now;

            cbWorker.Checked = false;

            m_GroupIDs.Clear();

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
            if (txtFirstName.Text == "" || txtLastName.Text == "")
                return false;
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
                    CheckForChanges();
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
            //m_GroupIDs.Sort();
            //m_InitPersonGroups.Sort();

            Console.Write("GroupIDs: ");
            foreach (int id in m_GroupIDs)
            {
                Console.Write(id + " ");
            }
            Console.Write("\n");

            Console.Write("InitGroups: ");
            foreach (int id in m_InitPersonGroups)
            {
                Console.Write(id + " ");
            }
            Console.Write("\n");

            if (m_InitPersonGroups.All(m_GroupIDs.Contains) && m_InitPersonGroups.Count == m_GroupIDs.Count)
                Console.WriteLine("Equal");
            else
                Console.WriteLine("Not equal");

            using (SqlConnection conn = new SqlConnection())
            {
                DatabaseConnection.OpenConnection(conn);
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    /*
                     * UPDATE PERSON TABLE
                     * */
                    SqlCommand commandUpdatePerson = new SqlCommand("UPDATE PERSON SET FIRSTNAME = @firstName, LASTNAME = @lastName, BIRTHDAY = @birthday WHERE PERSON_ID = @id", conn, transaction);
                    commandUpdatePerson.Parameters.AddWithValue("firstName", txtFirstName.Text);
                    commandUpdatePerson.Parameters.AddWithValue("lastName", txtLastName.Text);
                    commandUpdatePerson.Parameters.AddWithValue("birthday", dtpBirthday.Value);
                    commandUpdatePerson.Parameters.AddWithValue("id", m_PersonID);
                    commandUpdatePerson.ExecuteNonQuery();
                    
                    Console.WriteLine("Person table UPDATED");                    

                    /*
                     * UPDATE PERSON_GRUPO TABLE
                     * */
                    foreach(int groupID in m_InitPersonGroups)
                    {
                        // If this is true it means that the group got deleted from the person
                        if(!m_GroupIDs.Contains(groupID))
                        {
                            SqlCommand removePersonFromGroup = new SqlCommand("DELETE FROM PERSON_GRUPO WHERE ID_PERSON = @personID AND ID_GRUPO = @groupID", conn, transaction);
                            removePersonFromGroup.Parameters.AddWithValue("personID", m_PersonID);
                            removePersonFromGroup.Parameters.AddWithValue("groupID", groupID);
                            removePersonFromGroup.ExecuteNonQuery();
                        }
                    }
                    foreach(int groupID in m_GroupIDs)
                    {
                        // If this is true it means that the group got added to the person
                        if (!m_InitPersonGroups.Contains(groupID))
                        {
                            SqlCommand addPersonToGroup = new SqlCommand("INSERT INTO PERSON_GRUPO VALUES(@personID, @groupID)", conn, transaction);
                            addPersonToGroup.Parameters.AddWithValue("personID", m_PersonID);
                            addPersonToGroup.Parameters.AddWithValue("groupID", groupID);
                            addPersonToGroup.ExecuteNonQuery();
                        }
                    }
                    Console.WriteLine("Person_Grupo table UPDATED");
                                        
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

                    //m_GroupIDs.Clear();
                    m_GroupIDs = m_InitPersonGroups;
                    m_WeeklyPresenceChanges.Clear();

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

                    SqlCommand commandInsertPerson = new SqlCommand("INSERT INTO PERSON (PERSON_TYPE, FIRSTNAME, LASTNAME, DOCUMENT_TYPE, DOCUMENT, " +
                                                                        "BIRTHDAY, GENDER, NAME_DAY, IMAGE_RIGHTS, DATA_RIGHTS, WORKER, NATIONALITY," +
                                                                        "YEARS_IN_COUNTRY, SCHOOL_NAME, SCHOOL_YEAR, STREET_TYPE, ADDRESS, HOME_NUMBER, " +
                                                                        "HOME_STAIRS, HOME_STOREY, HOME_DOOR, HOME_ZIP_CODE, HOME_POPULATION, " +
                                                                        "HOME_REGION, HOME_PROVINCE, HOME_COUNTRY, HOME_LANDLINE_NUMBER, CELLPHONE_NUMBER, " +
                                                                        "EMAIL, ID_FATHER, ID_MOTHER, FAMILY_EMAIL, OTHER_CONTACT_RELATION," +
                                                                        "OTHER_CONTACT_NAME, OTHER_CONTACT_PHONE, NUMBER_OF_BROTHERS, NUMBER_OF_SISTERS, " +
                                                                        "SIBLING_RANK, PICK_OFF_PEOPLE, PROFESSION, BANK_IBAN, BANK_ACCOUNT_OWNER, OBSERVATIONS, " +
                                                                        "FOOD_ALLERGY_CHRONIC_DISEASES, MEMBER_NUMBER, HEALTH_CARD_NUMBER) " +
                                                                    "VALUES (@personType, @firstName, @lastName, @documentType, @document, " +
                                                                        "@birthday, @gender, @nameDay, @imageRights, @dataRights, @worker, @nationality," +
                                                                        "@yearsInCountry, @schoolName, @schoolYear, @streetType, @address, @homeNumber, " +
                                                                        "@homeStairs, @homeStorey, @homeDoor, @ZIPCode, @population, " +
                                                                        "@region, @province, @country, @landline, @cellphone, " +
                                                                        "@email, @fatherID, @motherID, @familyEmail, @otherContactRelation," +
                                                                        "@otherContactName, @otherContactPhone, @numberBrothers, @numberSisters, " +
                                                                        "@siblingRank, @pickOffPeople, @profession, @IBAN, @bankAccountOwner, @observations, " +
                                                                        "@foodAndDisease, @memberNumber, @healthCardNumber) ", conn, transaction);
                    commandInsertPerson.Parameters.AddWithValue("personType", txtPersonType.Text);
                    commandInsertPerson.Parameters.AddWithValue("firstName", txtFirstName.Text);
                    commandInsertPerson.Parameters.AddWithValue("lastName", txtLastName.Text);
                    commandInsertPerson.Parameters.AddWithValue("documentType", txtDocumentType.Text);
                    commandInsertPerson.Parameters.AddWithValue("document", txtDocument.Text);
                    commandInsertPerson.Parameters.AddWithValue("gender", cbbGender.SelectedItem.ToString());
                    commandInsertPerson.Parameters.AddWithValue("birthday", dtpBirthday.Value);
                    commandInsertPerson.Parameters.AddWithValue("worker", cbWorker.Checked ? 1 : 0);
                    commandInsertPerson.Parameters.AddWithValue("nameDay", txtNameDay.Text);
                    commandInsertPerson.Parameters.AddWithValue("imageRights", cbImageRights.Checked ? 1 : 0);
                    commandInsertPerson.Parameters.AddWithValue("dataRights", cbDataRights.Checked ? 1 : 0);
                    commandInsertPerson.Parameters.AddWithValue("nationality", txtNationality.Text);
                    commandInsertPerson.Parameters.AddWithValue("yearsInCountry", txtYearsInCountry.Text);
                    commandInsertPerson.Parameters.AddWithValue("schoolName", txtSchoolName.Text);
                    commandInsertPerson.Parameters.AddWithValue("schoolYear", txtSchoolYear.Text);
                    commandInsertPerson.Parameters.AddWithValue("streetType", txtStreetType.Text);
                    commandInsertPerson.Parameters.AddWithValue("address", txtAddress.Text);
                    commandInsertPerson.Parameters.AddWithValue("homeNumber", txtHomeNumber.Text);
                    commandInsertPerson.Parameters.AddWithValue("homeStairs", txtHomeStairs.Text);
                    commandInsertPerson.Parameters.AddWithValue("homeStorey", txtHomeStorey.Text);
                    commandInsertPerson.Parameters.AddWithValue("homeDoor", txtHomeDoor.Text);
                    commandInsertPerson.Parameters.AddWithValue("ZIPCode", txtZIPCode.Text);
                    commandInsertPerson.Parameters.AddWithValue("population", txtPopulation.Text);
                    commandInsertPerson.Parameters.AddWithValue("region", txtRegion.Text);
                    commandInsertPerson.Parameters.AddWithValue("province", txtProvince.Text);
                    commandInsertPerson.Parameters.AddWithValue("country", txtCountry.Text);
                    commandInsertPerson.Parameters.AddWithValue("landline", txtLandlineNumber.Text);
                    commandInsertPerson.Parameters.AddWithValue("cellphone", txtCellphoneNumber.Text);
                    commandInsertPerson.Parameters.AddWithValue("email", txtEmail.Text);
                    commandInsertPerson.Parameters.AddWithValue("profession", txtProfession.Text);
                    if (string.IsNullOrEmpty(txtFatherID.Text))
                        commandInsertPerson.Parameters.AddWithValue("fatherID", DBNull.Value);
                    else
                        commandInsertPerson.Parameters.AddWithValue("fatherID", txtFatherID.Text);
                    if (string.IsNullOrEmpty(txtMotherID.Text))
                        commandInsertPerson.Parameters.AddWithValue("motherID", DBNull.Value);
                    else
                        commandInsertPerson.Parameters.AddWithValue("motherID", txtMotherID.Text);
                    commandInsertPerson.Parameters.AddWithValue("familyEmail", txtFamilyEmail.Text);
                    commandInsertPerson.Parameters.AddWithValue("otherContactRelation", txtOtherContactRelation.Text);
                    commandInsertPerson.Parameters.AddWithValue("otherContactName", txtOtherContactName.Text);
                    commandInsertPerson.Parameters.AddWithValue("otherContactPhone", txtOtherContactPhone.Text);
                    commandInsertPerson.Parameters.AddWithValue("numberBrothers", txtNumberOfBrothers.Text);
                    commandInsertPerson.Parameters.AddWithValue("numberSisters", txtNumberOfSisters.Text);
                    commandInsertPerson.Parameters.AddWithValue("siblingRank", txtSiblingRank.Text);
                    commandInsertPerson.Parameters.AddWithValue("pickOffPeople", rtxtPeopleAllowedToPickOff.Text);
                    commandInsertPerson.Parameters.AddWithValue("IBAN", txtIBAN.Text);
                    commandInsertPerson.Parameters.AddWithValue("bankAccountOwner", txtBankAccountOwner.Text);
                    commandInsertPerson.Parameters.AddWithValue("observations", rtxtObservations.Text);
                    commandInsertPerson.Parameters.AddWithValue("foodAndDisease", rtxtFoodAndDisease.Text);
                    commandInsertPerson.Parameters.AddWithValue("memberNumber", txtMemberNumber.Text);
                    commandInsertPerson.Parameters.AddWithValue("healthCardNumber", txtHealthCardNumber.Text);

                    commandInsertPerson.ExecuteNonQuery();
                    Console.WriteLine("Person inserted");

                    /**
                     * RETRIEVE PERSON ID
                     * */

                    SqlCommand commandGetPersonID = new SqlCommand("SELECT MAX(PERSON_ID) AS LAST_ID FROM PERSON WHERE FIRSTNAME = @firstName AND LASTNAME = @lastName", conn, transaction);
                    commandGetPersonID.Parameters.Add(new SqlParameter("firstName", txtFirstName.Text));
                    commandGetPersonID.Parameters.Add(new SqlParameter("lastName", txtLastName.Text));

                    int personID;
                    using (SqlDataReader reader = commandGetPersonID.ExecuteReader())
                    {
                        reader.Read();
                        personID = (int)reader["LAST_ID"];
                        Console.WriteLine("ID retrieved: " + personID);
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

        // Checks for changes on the values of the form, if changes were made the validation button gets activated
        private void CheckForChanges()
        {
            // If the dictionaries are the same it means that the user didn't change any value concerning the person, therefore we disable the update button
            btnValidateForm.Enabled = !m_UpdatedValuesDictionary.Keys.All(k => m_InitialValuesDictionary[k].Equals(m_UpdatedValuesDictionary[k])) ||
                // This holds the changes on the weekdays attendance
                m_WeeklyPresenceChanges.Count > 0 ||
                // This checks if there were changes on the groups the person is part of (TODO, doesn't work correctly)
                !m_GroupIDs.All(m_InitPersonGroups.Contains) || !m_InitPersonGroups.All(m_GroupIDs.Contains);
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
                if (m_FormType.Equals(FormType.MODIFICATION) && btnValidateForm.Enabled)
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
                    Console.WriteLine("Group id: " + groupID);
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

        private void btnPrintAttendanceSheet_Click(object sender, EventArgs e)
        {
            PDFManager.CreateSingleAttendanceSheet(m_PersonID);
        }

        private void btnSearchFather_Click(object sender, EventArgs e)
        {
            OpenSearchParentForm(SearchParentForm.PARENT_TYPE.FATHER);
        }

        private void btnSearchMother_Click(object sender, EventArgs e)
        {
            OpenSearchParentForm(SearchParentForm.PARENT_TYPE.MOTHER);
        }

        private void OpenSearchParentForm(SearchParentForm.PARENT_TYPE parentType)
        {
            SearchParentForm form = SearchParentForm.GetInstance(parentType);
            form.FormClosing += OnFormClosing;

            if (!form.Visible)
                form.ShowDialog();
            else
                form.BringToFront();
        }
        
        // The only form we have is the search parent one, therefore this event is only called when that form gets closed
        public void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            SearchParentForm form = SearchParentForm.GetInstance();
            if (form.Valid)
                GetParentInformation(form.ParentType, form.ParentID);
        }

        private void GetParentInformation(SearchParentForm.PARENT_TYPE parentType, int personID)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                DatabaseConnection.OpenConnection(conn);

                SqlCommand command = new SqlCommand("SELECT FIRSTNAME+' '+LASTNAME AS NAME, CELLPHONE_NUMBER, EMAIL, DOCUMENT_TYPE, DOCUMENT, NATIONALITY, CONVERT(varchar(10), BIRTHDAY, 103) AS BIRTHDAY, PROFESSION " +
                                                    "FROM PERSON " +
                                                    "WHERE PERSON_ID = @personID", conn);
                command.Parameters.AddWithValue("personID", personID);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    reader.Read();

                    string parent = char.ToUpper(parentType.ToString()[0]) + parentType.ToString().ToLower().Substring(1);
                    Controls.Find("txt"+parent+"Name", true)[0].Text = reader["NAME"].ToString();
                    Controls.Find("txt" + parent + "Phone", true)[0].Text = reader["CELLPHONE_NUMBER"].ToString();
                    Controls.Find("txt" + parent + "Email", true)[0].Text = reader["EMAIL"].ToString();
                    Controls.Find("txt" + parent + "DocumentType", true)[0].Text = reader["DOCUMENT_TYPE"].ToString();
                    Controls.Find("txt" + parent + "Document", true)[0].Text = reader["DOCUMENT"].ToString();
                    Controls.Find("txt" + parent + "Nationality", true)[0].Text = reader["NATIONALITY"].ToString();
                    Controls.Find("txt" + parent + "Birthday", true)[0].Text = reader["BIRTHDAY"].ToString();
                    Controls.Find("txt" + parent + "Profession", true)[0].Text = reader["PROFESSION"].ToString();
                    Controls.Find("txt" + parent + "ID", true)[0].Text = personID.ToString();
                }
            }
        }
    }
}
