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
    public partial class HolidayManagerForm : Form 
    {
        private static HolidayManagerForm s_Instance;

        public static HolidayManagerForm GetInstance()
        {
            if (s_Instance == null)
            {
                s_Instance = new HolidayManagerForm();
                s_Instance.FormClosing += OnFormClosing;
            }
            return s_Instance;
        }

        // 2019 = 2019-2020     2020 = 2020-2021    ...
        int m_currentSchoolYear;

        public HolidayManagerForm()
        {
            InitializeComponent();
            this.CenterToScreen();
        }

        public static void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            // When the form is closed again we set the reference of the singleton to null
            s_Instance = null;
        }

        private void HolidayManagerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Ask if sure?
        }

        private void HolidayManagerForm_Load(object sender, EventArgs e)
        {
            GetCurrentSchoolYear();
            RetrievePublicHolidays();
        }

        private void GetCurrentSchoolYear()
        {
            // A School year starts in september, therefore the year is the same
            if (DateTime.Now.Month >= 9)
                m_currentSchoolYear = DateTime.Now.Year;
            // If the current month is before september it means that the current school year started last year
            else
                m_currentSchoolYear = DateTime.Now.Year - 1;
            UpdateSchoolYearLabel();
        }

        private void UpdateSchoolYearLabel()
        {
            lblSchoolYear.Text = "Current school year: " + m_currentSchoolYear + " - " + (m_currentSchoolYear + 1);
        }

        private void RetrievePublicHolidays()
        {
            gbPublicHolidays.Controls.Clear();
            using (SqlConnection conn = new SqlConnection())
            {
                DatabaseConnection.OpenConnection(conn);
                SqlCommand command = new SqlCommand("SELECT DATE_DAY AS DAY, HOLIDAY_NAME AS NAME " +
                                                    "FROM PUBLIC_HOLIDAY " +
                                                    "WHERE DATE_DAY BETWEEN " +
                                                    "CONVERT(VARCHAR(30), CAST(@start AS DATETIME), 102) AND CONVERT(VARCHAR(30), CAST(@end AS DATETIME), 102)", conn);
                command.Parameters.AddWithValue("start", "09/01/"+m_currentSchoolYear.ToString());
                command.Parameters.AddWithValue("end", "08/31/"+(m_currentSchoolYear+1).ToString());

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    int counter = 0;
                    while (reader.Read())
                    {
                        Button delete = new Button
                        {
                            Text = "—",
                            Location = new Point(10, 22 + 22 * counter),
                            Size = new Size(20, 20),
                            Tag = reader.GetDateTime(reader.GetOrdinal("DAY")).ToShortDateString()
                        };
                        delete.Click += new EventHandler(btnDeleteHoliday_Click);
                        Label holiday = new Label
                        {
                            Text = reader.GetDateTime(reader.GetOrdinal("DAY")).ToShortDateString() + " - " + reader["NAME"].ToString(),
                            Location = new Point(30, 25 + 22 * counter),
                            AutoSize = true
                        };
                        gbPublicHolidays.Controls.Add(delete);
                        gbPublicHolidays.Controls.Add(holiday);
                        counter++;
                    }
                }
            }
        }

        private void btnDeleteHoliday_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            DialogResult res = MessageBox.Show("Are you sure that you want to delete the "+btn.Tag.ToString()+" holiday?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if(res == DialogResult.Yes)
            {
                using (SqlConnection conn = new SqlConnection())
                {
                    DatabaseConnection.OpenConnection(conn);
                    SqlCommand command = new SqlCommand("DELETE FROM PUBLIC_HOLIDAY WHERE CONVERT(VARCHAR(30), DATE_DAY, 103) = @holiday", conn);
                    command.Parameters.AddWithValue("holiday", btn.Tag.ToString());
                    
                    if (command.ExecuteNonQuery() > 0)
                    {
                        RetrievePublicHolidays();
                        MessageBox.Show("The selected holiday has been deleted from the database!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("The select holiday couldn't be deleted!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnPreviousSchoolYear_Click(object sender, EventArgs e)
        {
            m_currentSchoolYear--;
            UpdateSchoolYearLabel();
            RetrievePublicHolidays();
        }

        private void btnNextSchoolYear_Click(object sender, EventArgs e)
        {
            m_currentSchoolYear++;
            UpdateSchoolYearLabel();
            RetrievePublicHolidays();
        }

        private void btnAddHoliday_Click(object sender, EventArgs e)
        {
            if(txtHolidayName.Text.Length > 3)
            {
                using (SqlConnection conn = new SqlConnection())
                {
                    DatabaseConnection.OpenConnection(conn);

                    // Checks if the holiday is already in the database
                    SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM PUBLIC_HOLIDAY WHERE CONVERT(VARCHAR(30), DATE_DAY, 103)  = @holiday", conn);
                    command.Parameters.AddWithValue("holiday", dtpHolidayDate.Value.ToShortDateString());
                    
                    if ((int)command.ExecuteScalar() > 0)
                    {
                        MessageBox.Show("This holiday already exists in the database!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    command = new SqlCommand("INSERT INTO PUBLIC_HOLIDAY VALUES(@date, @name)", conn);
                    command.Parameters.AddWithValue("date", dtpHolidayDate.Value);
                    command.Parameters.AddWithValue("name", txtHolidayName.Text);

                    command.ExecuteNonQuery();
                    dtpHolidayDate.Value = DateTime.Now;
                    txtHolidayName.Text = "";
                    RetrievePublicHolidays();
                    MessageBox.Show("The holiday has been added to the database!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}
