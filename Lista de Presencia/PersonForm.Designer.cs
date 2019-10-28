namespace Lista_de_Presencia
{
    partial class PersonForm
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
            this.label3 = new System.Windows.Forms.Label();
            this.dtpBirthday = new System.Windows.Forms.DateTimePicker();
            this.txtLastname = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtFirstname = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnValidateForm = new System.Windows.Forms.Button();
            this.gbPrograms = new System.Windows.Forms.GroupBox();
            this.cbWorker = new System.Windows.Forms.CheckBox();
            this.gbWeeklyPresence = new System.Windows.Forms.GroupBox();
            this.dgvWeeklyDetail = new System.Windows.Forms.DataGridView();
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
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(375, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "Birthday";
            // 
            // dtpBirthday
            // 
            this.dtpBirthday.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpBirthday.Location = new System.Drawing.Point(426, 24);
            this.dtpBirthday.Name = "dtpBirthday";
            this.dtpBirthday.Size = new System.Drawing.Size(99, 20);
            this.dtpBirthday.TabIndex = 3;
            // 
            // txtLastname
            // 
            this.txtLastname.Location = new System.Drawing.Point(253, 24);
            this.txtLastname.Name = "txtLastname";
            this.txtLastname.Size = new System.Drawing.Size(99, 20);
            this.txtLastname.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(191, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Last name";
            // 
            // txtFirstname
            // 
            this.txtFirstname.Location = new System.Drawing.Point(90, 24);
            this.txtFirstname.Name = "txtFirstname";
            this.txtFirstname.Size = new System.Drawing.Size(77, 20);
            this.txtFirstname.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "First name";
            // 
            // btnValidateForm
            // 
            this.btnValidateForm.Location = new System.Drawing.Point(626, 355);
            this.btnValidateForm.Name = "btnValidateForm";
            this.btnValidateForm.Size = new System.Drawing.Size(86, 29);
            this.btnValidateForm.TabIndex = 9;
            this.btnValidateForm.Text = "Validate";
            this.btnValidateForm.UseVisualStyleBackColor = true;
            this.btnValidateForm.Click += new System.EventHandler(this.btnAddPerson_Click);
            // 
            // gbPrograms
            // 
            this.gbPrograms.Location = new System.Drawing.Point(32, 50);
            this.gbPrograms.Name = "gbPrograms";
            this.gbPrograms.Size = new System.Drawing.Size(680, 138);
            this.gbPrograms.TabIndex = 16;
            this.gbPrograms.TabStop = false;
            this.gbPrograms.Text = "Programs";
            // 
            // cbWorker
            // 
            this.cbWorker.AutoSize = true;
            this.cbWorker.Location = new System.Drawing.Point(558, 27);
            this.cbWorker.Name = "cbWorker";
            this.cbWorker.Size = new System.Drawing.Size(61, 17);
            this.cbWorker.TabIndex = 4;
            this.cbWorker.Text = "Worker";
            this.cbWorker.UseVisualStyleBackColor = true;
            this.cbWorker.CheckedChanged += new System.EventHandler(this.cbWorker_CheckedChanged);
            // 
            // gbWeeklyPresence
            // 
            this.gbWeeklyPresence.Controls.Add(this.dgvWeeklyDetail);
            this.gbWeeklyPresence.Location = new System.Drawing.Point(32, 194);
            this.gbWeeklyPresence.Name = "gbWeeklyPresence";
            this.gbWeeklyPresence.Size = new System.Drawing.Size(680, 86);
            this.gbWeeklyPresence.TabIndex = 17;
            this.gbWeeklyPresence.TabStop = false;
            this.gbWeeklyPresence.Text = "Weekly Presence";
            // 
            // dgvWeeklyDetail
            // 
            this.dgvWeeklyDetail.AllowUserToAddRows = false;
            this.dgvWeeklyDetail.AllowUserToDeleteRows = false;
            this.dgvWeeklyDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvWeeklyDetail.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colWeekMonday,
            this.colWeekTuesday,
            this.colWeekWednesday,
            this.colWeekThursday,
            this.colWeekFriday,
            this.colWeekSaturday,
            this.colWeekSunday});
            this.dgvWeeklyDetail.Location = new System.Drawing.Point(3, 35);
            this.dgvWeeklyDetail.Name = "dgvWeeklyDetail";
            this.dgvWeeklyDetail.Size = new System.Drawing.Size(671, 45);
            this.dgvWeeklyDetail.TabIndex = 10;
            this.dgvWeeklyDetail.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvWeeklyDetail_CellClick);
            this.dgvWeeklyDetail.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvWeeklyDetail_CellMouseUp);
            this.dgvWeeklyDetail.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvWeeklyDetail_CellValueChanged);
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
            // PersonAdditionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.gbWeeklyPresence);
            this.Controls.Add(this.cbWorker);
            this.Controls.Add(this.gbPrograms);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dtpBirthday);
            this.Controls.Add(this.txtLastname);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtFirstname);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnValidateForm);
            this.Name = "PersonAdditionForm";
            this.Text = "Person";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.gbWeeklyPresence.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvWeeklyDetail)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpBirthday;
        private System.Windows.Forms.TextBox txtLastname;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtFirstname;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnValidateForm;
        private System.Windows.Forms.GroupBox gbPrograms;
        private System.Windows.Forms.CheckBox cbWorker;
        private System.Windows.Forms.GroupBox gbWeeklyPresence;
        private System.Windows.Forms.DataGridView dgvWeeklyDetail;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colWeekMonday;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colWeekTuesday;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colWeekWednesday;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colWeekThursday;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colWeekFriday;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colWeekSaturday;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colWeekSunday;
    }
}