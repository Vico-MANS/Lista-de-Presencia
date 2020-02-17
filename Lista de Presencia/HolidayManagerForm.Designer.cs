namespace Lista_de_Presencia {
    partial class HolidayManagerForm {
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
            this.gbPublicHolidays = new System.Windows.Forms.GroupBox();
            this.lblSchoolYear = new System.Windows.Forms.Label();
            this.btnPreviousSchoolYear = new System.Windows.Forms.Button();
            this.btnNextSchoolYear = new System.Windows.Forms.Button();
            this.dtpHolidayDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtHolidayName = new System.Windows.Forms.TextBox();
            this.btnAddHoliday = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // gbPublicHolidays
            // 
            this.gbPublicHolidays.Location = new System.Drawing.Point(15, 50);
            this.gbPublicHolidays.Name = "gbPublicHolidays";
            this.gbPublicHolidays.Size = new System.Drawing.Size(419, 360);
            this.gbPublicHolidays.TabIndex = 0;
            this.gbPublicHolidays.TabStop = false;
            this.gbPublicHolidays.Text = "Public Holidays";
            // 
            // lblSchoolYear
            // 
            this.lblSchoolYear.AutoSize = true;
            this.lblSchoolYear.Location = new System.Drawing.Point(12, 20);
            this.lblSchoolYear.Name = "lblSchoolYear";
            this.lblSchoolYear.Size = new System.Drawing.Size(165, 13);
            this.lblSchoolYear.TabIndex = 1;
            this.lblSchoolYear.Text = "Current School Year: 2019 - 2020";
            // 
            // btnPreviousSchoolYear
            // 
            this.btnPreviousSchoolYear.Location = new System.Drawing.Point(190, 14);
            this.btnPreviousSchoolYear.Name = "btnPreviousSchoolYear";
            this.btnPreviousSchoolYear.Size = new System.Drawing.Size(25, 25);
            this.btnPreviousSchoolYear.TabIndex = 2;
            this.btnPreviousSchoolYear.Text = "—";
            this.btnPreviousSchoolYear.UseVisualStyleBackColor = true;
            this.btnPreviousSchoolYear.Click += new System.EventHandler(this.btnPreviousSchoolYear_Click);
            // 
            // btnNextSchoolYear
            // 
            this.btnNextSchoolYear.Location = new System.Drawing.Point(221, 14);
            this.btnNextSchoolYear.Name = "btnNextSchoolYear";
            this.btnNextSchoolYear.Size = new System.Drawing.Size(25, 25);
            this.btnNextSchoolYear.TabIndex = 3;
            this.btnNextSchoolYear.Text = "+";
            this.btnNextSchoolYear.UseVisualStyleBackColor = true;
            this.btnNextSchoolYear.Click += new System.EventHandler(this.btnNextSchoolYear_Click);
            // 
            // dtpHolidayDate
            // 
            this.dtpHolidayDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpHolidayDate.Location = new System.Drawing.Point(48, 418);
            this.dtpHolidayDate.Name = "dtpHolidayDate";
            this.dtpHolidayDate.Size = new System.Drawing.Size(88, 20);
            this.dtpHolidayDate.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 421);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Date";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(151, 421);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Name";
            // 
            // txtHolidayName
            // 
            this.txtHolidayName.Location = new System.Drawing.Point(192, 418);
            this.txtHolidayName.Name = "txtHolidayName";
            this.txtHolidayName.Size = new System.Drawing.Size(161, 20);
            this.txtHolidayName.TabIndex = 7;
            // 
            // btnAddHoliday
            // 
            this.btnAddHoliday.Location = new System.Drawing.Point(359, 416);
            this.btnAddHoliday.Name = "btnAddHoliday";
            this.btnAddHoliday.Size = new System.Drawing.Size(75, 23);
            this.btnAddHoliday.TabIndex = 8;
            this.btnAddHoliday.Text = "Add";
            this.btnAddHoliday.UseVisualStyleBackColor = true;
            this.btnAddHoliday.Click += new System.EventHandler(this.btnAddHoliday_Click);
            // 
            // HolidayManagerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(447, 450);
            this.Controls.Add(this.btnAddHoliday);
            this.Controls.Add(this.txtHolidayName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtpHolidayDate);
            this.Controls.Add(this.btnNextSchoolYear);
            this.Controls.Add(this.btnPreviousSchoolYear);
            this.Controls.Add(this.lblSchoolYear);
            this.Controls.Add(this.gbPublicHolidays);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "HolidayManagerForm";
            this.Text = "Public Holidays";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.HolidayManagerForm_FormClosing);
            this.Load += new System.EventHandler(this.HolidayManagerForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbPublicHolidays;
        private System.Windows.Forms.Label lblSchoolYear;
        private System.Windows.Forms.Button btnPreviousSchoolYear;
        private System.Windows.Forms.Button btnNextSchoolYear;
        private System.Windows.Forms.DateTimePicker dtpHolidayDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtHolidayName;
        private System.Windows.Forms.Button btnAddHoliday;
    }
}