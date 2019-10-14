namespace Lista_de_Presencia
{
    partial class PersonInformationForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.txtFirstname = new System.Windows.Forms.TextBox();
            this.txtLastname = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpBirthday = new System.Windows.Forms.DateTimePicker();
            this.gbWeeklyPresence = new System.Windows.Forms.GroupBox();
            this.dgvWeeklyDetail = new System.Windows.Forms.DataGridView();
            this.btnSaveWeeklyChanges = new System.Windows.Forms.Button();
            this.colWeekPresPersonID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colWeekPersonName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colWeekMonday = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colWeekTuesday = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colWeekWednesday = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colWeekThursday = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colWeekFriday = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colWeekSaturday = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colWeekSunday = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.gbWeeklyPresence.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWeeklyDetail)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "First name";
            // 
            // txtFirstname
            // 
            this.txtFirstname.Location = new System.Drawing.Point(95, 33);
            this.txtFirstname.Name = "txtFirstname";
            this.txtFirstname.Size = new System.Drawing.Size(100, 20);
            this.txtFirstname.TabIndex = 1;
            // 
            // txtLastname
            // 
            this.txtLastname.Location = new System.Drawing.Point(292, 33);
            this.txtLastname.Name = "txtLastname";
            this.txtLastname.Size = new System.Drawing.Size(100, 20);
            this.txtLastname.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(230, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Last name";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(435, 36);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Birthday";
            // 
            // dtpBirthday
            // 
            this.dtpBirthday.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpBirthday.Location = new System.Drawing.Point(486, 33);
            this.dtpBirthday.Name = "dtpBirthday";
            this.dtpBirthday.Size = new System.Drawing.Size(97, 20);
            this.dtpBirthday.TabIndex = 5;
            // 
            // gbWeeklyPresence
            // 
            this.gbWeeklyPresence.Controls.Add(this.dgvWeeklyDetail);
            this.gbWeeklyPresence.Controls.Add(this.btnSaveWeeklyChanges);
            this.gbWeeklyPresence.Location = new System.Drawing.Point(37, 149);
            this.gbWeeklyPresence.Name = "gbWeeklyPresence";
            this.gbWeeklyPresence.Size = new System.Drawing.Size(722, 115);
            this.gbWeeklyPresence.TabIndex = 13;
            this.gbWeeklyPresence.TabStop = false;
            this.gbWeeklyPresence.Text = "Weekly Presence";
            // 
            // dgvWeeklyDetail
            // 
            this.dgvWeeklyDetail.AllowUserToAddRows = false;
            this.dgvWeeklyDetail.AllowUserToDeleteRows = false;
            this.dgvWeeklyDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvWeeklyDetail.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colWeekPresPersonID,
            this.colWeekPersonName,
            this.colWeekMonday,
            this.colWeekTuesday,
            this.colWeekWednesday,
            this.colWeekThursday,
            this.colWeekFriday,
            this.colWeekSaturday,
            this.colWeekSunday});
            this.dgvWeeklyDetail.Location = new System.Drawing.Point(3, 35);
            this.dgvWeeklyDetail.Name = "dgvWeeklyDetail";
            this.dgvWeeklyDetail.Size = new System.Drawing.Size(716, 45);
            this.dgvWeeklyDetail.TabIndex = 10;
            this.dgvWeeklyDetail.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvWeeklyDetail_CellMouseUp);
            this.dgvWeeklyDetail.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvWeeklyDetail_CellValueChanged);
            // 
            // btnSaveWeeklyChanges
            // 
            this.btnSaveWeeklyChanges.Enabled = false;
            this.btnSaveWeeklyChanges.Location = new System.Drawing.Point(627, 86);
            this.btnSaveWeeklyChanges.Name = "btnSaveWeeklyChanges";
            this.btnSaveWeeklyChanges.Size = new System.Drawing.Size(89, 23);
            this.btnSaveWeeklyChanges.TabIndex = 11;
            this.btnSaveWeeklyChanges.Text = "Save changes";
            this.btnSaveWeeklyChanges.UseVisualStyleBackColor = true;
            this.btnSaveWeeklyChanges.Click += new System.EventHandler(this.btnSaveWeeklyChanges_Click);
            // 
            // colWeekPresPersonID
            // 
            this.colWeekPresPersonID.HeaderText = "ID";
            this.colWeekPresPersonID.Name = "colWeekPresPersonID";
            this.colWeekPresPersonID.Visible = false;
            this.colWeekPresPersonID.Width = 5;
            // 
            // colWeekPersonName
            // 
            this.colWeekPersonName.HeaderText = "Name";
            this.colWeekPersonName.Name = "colWeekPersonName";
            this.colWeekPersonName.ReadOnly = true;
            this.colWeekPersonName.Visible = false;
            // 
            // colWeekMonday
            // 
            this.colWeekMonday.HeaderText = "Monday";
            this.colWeekMonday.Name = "colWeekMonday";
            this.colWeekMonday.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colWeekMonday.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.colWeekMonday.Width = 75;
            // 
            // colWeekTuesday
            // 
            this.colWeekTuesday.HeaderText = "Tuesday";
            this.colWeekTuesday.Name = "colWeekTuesday";
            this.colWeekTuesday.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colWeekTuesday.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.colWeekTuesday.Width = 75;
            // 
            // colWeekWednesday
            // 
            this.colWeekWednesday.HeaderText = "Wednesday";
            this.colWeekWednesday.Name = "colWeekWednesday";
            this.colWeekWednesday.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colWeekWednesday.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.colWeekWednesday.Width = 75;
            // 
            // colWeekThursday
            // 
            this.colWeekThursday.HeaderText = "Thursday";
            this.colWeekThursday.Name = "colWeekThursday";
            this.colWeekThursday.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colWeekThursday.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.colWeekThursday.Width = 75;
            // 
            // colWeekFriday
            // 
            this.colWeekFriday.HeaderText = "Friday";
            this.colWeekFriday.Name = "colWeekFriday";
            this.colWeekFriday.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colWeekFriday.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.colWeekFriday.Width = 75;
            // 
            // colWeekSaturday
            // 
            this.colWeekSaturday.HeaderText = "Saturday";
            this.colWeekSaturday.Name = "colWeekSaturday";
            this.colWeekSaturday.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colWeekSaturday.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.colWeekSaturday.Width = 75;
            // 
            // colWeekSunday
            // 
            this.colWeekSunday.HeaderText = "Sunday";
            this.colWeekSunday.Name = "colWeekSunday";
            this.colWeekSunday.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colWeekSunday.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.colWeekSunday.Width = 75;
            // 
            // PersonInformationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.gbWeeklyPresence);
            this.Controls.Add(this.dtpBirthday);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtLastname);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtFirstname);
            this.Controls.Add(this.label1);
            this.Name = "PersonInformationForm";
            this.Text = "PersonInformationForm";
            this.Load += new System.EventHandler(this.PersonInformationForm_Load);
            this.gbWeeklyPresence.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvWeeklyDetail)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtFirstname;
        private System.Windows.Forms.TextBox txtLastname;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpBirthday;
        private System.Windows.Forms.GroupBox gbWeeklyPresence;
        private System.Windows.Forms.DataGridView dgvWeeklyDetail;
        private System.Windows.Forms.Button btnSaveWeeklyChanges;
        private System.Windows.Forms.DataGridViewTextBoxColumn colWeekPresPersonID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colWeekPersonName;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colWeekMonday;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colWeekTuesday;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colWeekWednesday;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colWeekThursday;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colWeekFriday;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colWeekSaturday;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colWeekSunday;
    }
}