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
    public partial class ServiceForm : Form 
    {
        private static ServiceForm s_Instance;

        public static ServiceForm GetInstance()
        {
            if (s_Instance == null)
            {
                s_Instance = new ServiceForm();
                s_Instance.FormClosing += OnFormClosing;
            }
            return s_Instance;
        }

        public ServiceForm()
        {
            InitializeComponent();
            this.CenterToScreen();
        }
        
        public static void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            // When the form is closed again we set the reference of the singleton to null
            s_Instance = null;
        }

        private void ServiceForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Ask if sure?
        }

        private void ServiceForm_Load(object sender, EventArgs e)
        {
            GetPrograms();
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

        private void ResetForm()
        {
            txtServiceName.Text = "";
            cbbPrograms.SelectedItem = null;
        }

        private void btnAddService_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                DatabaseConnection.OpenConnection(conn);

                SqlCommand command = new SqlCommand("INSERT INTO SERVICIO (ID_PROGRAM, NAME) VALUES (@programID, @name)", conn);
                command.Parameters.AddWithValue("programID", ((KeyValuePair<Object, Object>)cbbPrograms.SelectedItem).Key);
                command.Parameters.AddWithValue("name", txtServiceName.Text);
                
                command.ExecuteNonQuery();

                MessageBox.Show("The service has been successfully added to the database!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                ResetForm();
            }
        }
    }
}
