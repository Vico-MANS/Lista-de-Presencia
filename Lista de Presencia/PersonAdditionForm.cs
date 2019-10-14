﻿using System;
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
        
        public static void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            // When the form is closed again we set the reference of the singleton to null
            s_Instance = null;
        }

        public PersonAdditionForm()
        {
            InitializeComponent();
            this.CenterToScreen();
        }
        
        private void Form2_Load(object sender, EventArgs e)
        {
            LoadPrograms();
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
                    while (reader.Read())
                    {
                        CheckBox box = new CheckBox();
                        box.Tag = reader["PROGRAM_ID"].ToString();
                        box.Text = reader["NAME"].ToString();
                        box.AutoSize = true;
                        box.Location = new Point(10, 20 + programCounter * 25);
                        gbPrograms.Controls.Add(box);
                        programCounter++;
                    }
                }
            }
        }

        private void RetrieveAllPrograms()
        {
            using (SqlConnection conn = new SqlConnection())
            {
                DatabaseConnection.OpenConnection(conn);

                SqlCommand command = new SqlCommand("INSERT INTO PERSON (FIRSTNAME, LASTNAME, BIRTHDAY) VALUES (@firstname, @lastname, @birthday)", conn);
                command.Parameters.Add(new SqlParameter("firstname", txtFirstname.Text));
                command.Parameters.Add(new SqlParameter("lastname", txtLastname.Text));
                command.Parameters.Add(new SqlParameter("birthday", dtpBirthday.Value));

                Console.WriteLine("Insert affected " + command.ExecuteNonQuery() + " rows.");

                ClearPersonAdditionFields();
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
                        SqlCommand commandInsertPerson = new SqlCommand("INSERT INTO PERSON (FIRSTNAME, LASTNAME, BIRTHDAY, WORKER) VALUES (@firstname, @lastname, @birthday, @worker)", conn, transaction);
                        commandInsertPerson.Parameters.AddWithValue("firstname", txtFirstname.Text);
                        commandInsertPerson.Parameters.AddWithValue("lastname", txtLastname.Text);
                        commandInsertPerson.Parameters.AddWithValue("birthday", dtpBirthday.Value);
                        commandInsertPerson.Parameters.AddWithValue("worker", cbWorker.Checked ? 1 : 0);

                        Console.WriteLine("Insert affected " + commandInsertPerson.ExecuteNonQuery() + " rows.");
                        
                        SqlCommand commandGetPersonID = new SqlCommand("SELECT MAX(PERSON_ID) AS LAST_ID FROM PERSON WHERE FIRSTNAME = @firstname AND LASTNAME = @lastname", conn, transaction);
                        commandGetPersonID.Parameters.Add(new SqlParameter("firstname", txtFirstname.Text));
                        commandGetPersonID.Parameters.Add(new SqlParameter("lastname", txtLastname.Text));

                        int personID;
                        using (SqlDataReader reader = commandGetPersonID.ExecuteReader())
                        {
                            reader.Read();
                            personID = (int)reader["LAST_ID"];
                            Console.WriteLine("Last inserted ID is: " + personID);
                        }

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
                        
                        transaction.Commit();

                        MessageBox.Show("The person has been succesfully added to the database", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        ClearPersonAdditionFields();
                    }
                    catch(Exception excep)
                    {
                        transaction.Rollback();

                        Console.WriteLine(excep.Message);
                        MessageBox.Show("An error has occured!\nThe person couldn't be added to the database...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please fill out all the mandatory fields!");
            }
        }        
    }
}
