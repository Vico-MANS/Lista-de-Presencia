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
    public partial class PersonAdditionForm : Form
    {
        private static PersonAdditionForm s_Instance;

        public static PersonAdditionForm GetInstance()
        {
            if (s_Instance == null)
            {
                s_Instance = new PersonAdditionForm();
                s_Instance.FormClosing += OnFormClosing;
            }
            return s_Instance;
        }

        private List<int> m_WeeklyPresence;
        
        public static void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            // When the form is closed again we set the reference of the singleton to null
            s_Instance = null;
        }

        public PersonAdditionForm()
        {
            InitializeComponent();
            this.CenterToScreen();
            m_WeeklyPresence = new List<int>();
        }
        
        private void Form2_Load(object sender, EventArgs e)
        {
            LoadPrograms();
            ClearPersonAdditionFields();
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
                        CheckBox box = new CheckBox();
                        box.Tag = reader["PROGRAM_ID"].ToString();
                        box.Text = reader["NAME"].ToString();
                        box.AutoSize = true;
                        
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
            }
        }

        private void ClearPersonAdditionFields()
        {
            txtFirstname.Clear();
            txtLastname.Clear();
            dtpBirthday.Value = DateTime.Now;

            foreach(CheckBox cb in gbPrograms.Controls.OfType<CheckBox>())
            {
                cb.Checked = false;
            }

            cbWorker.Checked = false;

            dgvWeeklyDetail.Rows.Clear();
            dgvWeeklyDetail.Rows.Add();
            dgvWeeklyDetail.ClearSelection();
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
                bool found = false;
                foreach(CheckBox cb in gbPrograms.Controls.OfType<CheckBox>())
                {
                    if (cb.Checked)
                    {
                        found = true;
                        break;
                    }
                }
                if (!found)
                    return false;
            }
            return true;
        }

        private void dgvWeeklyDetail_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                if ((bool)dgvWeeklyDetail.Rows[e.RowIndex].Cells[e.ColumnIndex].Value)
                    m_WeeklyPresence.Add(e.ColumnIndex + 1);
                else
                    m_WeeklyPresence.Remove(e.ColumnIndex + 1);
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
                         * INSERT PROGRAMS
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

                        Console.WriteLine("Program participation inserted");

                        /**
                         * INSERT WEEKLY PRESENCE
                         * */

                        if (!cbWorker.Checked)
                        {
                            foreach(int weekday in m_WeeklyPresence)
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
                    catch(Exception excep)
                    {
                        transaction.Rollback();

                        Console.WriteLine(excep);
                        MessageBox.Show("An error has occured!\nThe person couldn't be added to the database...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please fill out all the mandatory fields!");
            }
        }

        private void cbWorker_CheckedChanged(object sender, EventArgs e)
        {
            gbWeeklyPresence.Enabled = !cbWorker.Checked;
        }
    }
}
