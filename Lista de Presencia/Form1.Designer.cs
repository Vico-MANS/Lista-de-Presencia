namespace Lista_de_Presencia
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.dgvOverview = new System.Windows.Forms.DataGridView();
            this.colFirstName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLastName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colBirthday = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnAddPerson = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabOverview = new System.Windows.Forms.TabPage();
            this.btnDeleteSelected = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpBirthday = new System.Windows.Forms.DateTimePicker();
            this.txtLastname = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtFirstname = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPresence = new System.Windows.Forms.TabPage();
            this.dgvPresence = new System.Windows.Forms.DataGridView();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.colPerson = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMonday = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colTuesday = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colWednesday = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colThursday = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colFriday = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colSaturday = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colSunday = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOverview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabOverview.SuspendLayout();
            this.tabPresence.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPresence)).BeginInit();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // dgvOverview
            // 
            this.dgvOverview.AllowUserToAddRows = false;
            this.dgvOverview.AllowUserToDeleteRows = false;
            this.dgvOverview.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOverview.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colFirstName,
            this.colLastName,
            this.colBirthday});
            this.dgvOverview.Location = new System.Drawing.Point(20, 9);
            this.dgvOverview.Name = "dgvOverview";
            this.dgvOverview.ReadOnly = true;
            this.dgvOverview.Size = new System.Drawing.Size(722, 301);
            this.dgvOverview.TabIndex = 1;
            // 
            // colFirstName
            // 
            this.colFirstName.HeaderText = "FIRSTNAME";
            this.colFirstName.Name = "colFirstName";
            this.colFirstName.ReadOnly = true;
            this.colFirstName.Width = 225;
            // 
            // colLastName
            // 
            this.colLastName.HeaderText = "LASTNAME";
            this.colLastName.Name = "colLastName";
            this.colLastName.ReadOnly = true;
            this.colLastName.Width = 225;
            // 
            // colBirthday
            // 
            this.colBirthday.HeaderText = "BIRTHDAY";
            this.colBirthday.Name = "colBirthday";
            this.colBirthday.ReadOnly = true;
            this.colBirthday.Width = 225;
            // 
            // btnAddPerson
            // 
            this.btnAddPerson.Location = new System.Drawing.Point(567, 373);
            this.btnAddPerson.Name = "btnAddPerson";
            this.btnAddPerson.Size = new System.Drawing.Size(86, 29);
            this.btnAddPerson.TabIndex = 2;
            this.btnAddPerson.Text = "Add Person";
            this.btnAddPerson.UseVisualStyleBackColor = true;
            this.btnAddPerson.Click += new System.EventHandler(this.btnAddPerson_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Location = new System.Drawing.Point(218, 340);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Size = new System.Drawing.Size(54, 19);
            this.splitContainer1.SplitterDistance = 25;
            this.splitContainer1.TabIndex = 3;
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabOverview);
            this.tabControl.Controls.Add(this.tabPresence);
            this.tabControl.Location = new System.Drawing.Point(1, 2);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(787, 436);
            this.tabControl.TabIndex = 4;
            // 
            // tabOverview
            // 
            this.tabOverview.Controls.Add(this.btnDeleteSelected);
            this.tabOverview.Controls.Add(this.label3);
            this.tabOverview.Controls.Add(this.dtpBirthday);
            this.tabOverview.Controls.Add(this.txtLastname);
            this.tabOverview.Controls.Add(this.label2);
            this.tabOverview.Controls.Add(this.txtFirstname);
            this.tabOverview.Controls.Add(this.label1);
            this.tabOverview.Controls.Add(this.dgvOverview);
            this.tabOverview.Controls.Add(this.btnAddPerson);
            this.tabOverview.Location = new System.Drawing.Point(4, 22);
            this.tabOverview.Name = "tabOverview";
            this.tabOverview.Padding = new System.Windows.Forms.Padding(3);
            this.tabOverview.Size = new System.Drawing.Size(779, 410);
            this.tabOverview.TabIndex = 0;
            this.tabOverview.Text = "Overview";
            this.tabOverview.UseVisualStyleBackColor = true;
            // 
            // btnDeleteSelected
            // 
            this.btnDeleteSelected.Location = new System.Drawing.Point(608, 316);
            this.btnDeleteSelected.Name = "btnDeleteSelected";
            this.btnDeleteSelected.Size = new System.Drawing.Size(134, 23);
            this.btnDeleteSelected.TabIndex = 9;
            this.btnDeleteSelected.Text = "Delete selected rows";
            this.btnDeleteSelected.UseVisualStyleBackColor = true;
            this.btnDeleteSelected.Click += new System.EventHandler(this.btn_DeleteSelected);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(391, 381);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Birthday";
            // 
            // dtpBirthday
            // 
            this.dtpBirthday.Location = new System.Drawing.Point(442, 378);
            this.dtpBirthday.Name = "dtpBirthday";
            this.dtpBirthday.Size = new System.Drawing.Size(99, 20);
            this.dtpBirthday.TabIndex = 7;
            // 
            // txtLastname
            // 
            this.txtLastname.Location = new System.Drawing.Point(269, 378);
            this.txtLastname.Name = "txtLastname";
            this.txtLastname.Size = new System.Drawing.Size(99, 20);
            this.txtLastname.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(207, 381);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Last name";
            // 
            // txtFirstname
            // 
            this.txtFirstname.Location = new System.Drawing.Point(106, 378);
            this.txtFirstname.Name = "txtFirstname";
            this.txtFirstname.Size = new System.Drawing.Size(77, 20);
            this.txtFirstname.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(45, 381);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "First name";
            // 
            // tabPresence
            // 
            this.tabPresence.Controls.Add(this.dgvPresence);
            this.tabPresence.Location = new System.Drawing.Point(4, 22);
            this.tabPresence.Name = "tabPresence";
            this.tabPresence.Padding = new System.Windows.Forms.Padding(3);
            this.tabPresence.Size = new System.Drawing.Size(779, 410);
            this.tabPresence.TabIndex = 1;
            this.tabPresence.Text = "Presence";
            this.tabPresence.UseVisualStyleBackColor = true;
            // 
            // dgvPresence
            // 
            this.dgvPresence.AllowUserToAddRows = false;
            this.dgvPresence.AllowUserToDeleteRows = false;
            this.dgvPresence.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPresence.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colPerson,
            this.colMonday,
            this.colTuesday,
            this.colWednesday,
            this.colThursday,
            this.colFriday,
            this.colSaturday,
            this.colSunday});
            this.dgvPresence.Location = new System.Drawing.Point(7, 6);
            this.dgvPresence.Name = "dgvPresence";
            this.dgvPresence.ReadOnly = true;
            this.dgvPresence.Size = new System.Drawing.Size(766, 304);
            this.dgvPresence.TabIndex = 0;
            this.dgvPresence.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPresence_CellContentClick);
            // 
            // colPerson
            // 
            this.colPerson.HeaderText = "Person";
            this.colPerson.Name = "colPerson";
            this.colPerson.ReadOnly = true;
            this.colPerson.Width = 200;
            // 
            // colMonday
            // 
            this.colMonday.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colMonday.HeaderText = "Monday";
            this.colMonday.Name = "colMonday";
            this.colMonday.ReadOnly = true;
            this.colMonday.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colMonday.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // colTuesday
            // 
            this.colTuesday.HeaderText = "Tuesday";
            this.colTuesday.Name = "colTuesday";
            this.colTuesday.ReadOnly = true;
            this.colTuesday.Width = 75;
            // 
            // colWednesday
            // 
            this.colWednesday.HeaderText = "Wednesday";
            this.colWednesday.Name = "colWednesday";
            this.colWednesday.ReadOnly = true;
            this.colWednesday.Width = 75;
            // 
            // colThursday
            // 
            this.colThursday.HeaderText = "Thursday";
            this.colThursday.Name = "colThursday";
            this.colThursday.ReadOnly = true;
            this.colThursday.Width = 75;
            // 
            // colFriday
            // 
            this.colFriday.HeaderText = "Friday";
            this.colFriday.Name = "colFriday";
            this.colFriday.ReadOnly = true;
            this.colFriday.Width = 75;
            // 
            // colSaturday
            // 
            this.colSaturday.HeaderText = "Saturday";
            this.colSaturday.Name = "colSaturday";
            this.colSaturday.ReadOnly = true;
            this.colSaturday.Width = 75;
            // 
            // colSunday
            // 
            this.colSunday.HeaderText = "Sunday";
            this.colSunday.Name = "colSunday";
            this.colSunday.ReadOnly = true;
            this.colSunday.Width = 75;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOverview)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.tabOverview.ResumeLayout(false);
            this.tabOverview.PerformLayout();
            this.tabPresence.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPresence)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.DataGridView dgvOverview;
        private System.Windows.Forms.Button btnAddPerson;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabOverview;
        private System.Windows.Forms.TabPage tabPresence;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtFirstname;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtLastname;
        private System.Windows.Forms.DateTimePicker dtpBirthday;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnDeleteSelected;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFirstName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLastName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBirthday;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.DataGridView dgvPresence;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPerson;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colMonday;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colTuesday;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colWednesday;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colThursday;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colFriday;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colSaturday;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colSunday;
    }
}

