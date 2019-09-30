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
            this.colOverPersonID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFirstName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLastName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colBirthday = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnAddPerson = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabOverview = new System.Windows.Forms.TabPage();
            this.gbWeeklyPresence = new System.Windows.Forms.GroupBox();
            this.dgvWeeklyDetail = new System.Windows.Forms.DataGridView();
            this.colWeekPresPersonID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPersonName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colWeekMonday = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colWeekTuesday = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colWeekWednesday = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colWeekThursday = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colWeekFriday = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colWeekSaturday = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colWeekSunday = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.btnSaveWeeklyChanges = new System.Windows.Forms.Button();
            this.btnDeleteSelected = new System.Windows.Forms.Button();
            this.tabPresence = new System.Windows.Forms.TabPage();
            this.btnPreviousWeek = new System.Windows.Forms.Button();
            this.btnNextWeek = new System.Windows.Forms.Button();
            this.btnSavePresenceChanges = new System.Windows.Forms.Button();
            this.dgvPresence = new System.Windows.Forms.DataGridView();
            this.colPresPersonID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPerson = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMonday = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colTuesday = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colWednesday = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colThursday = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colFriday = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colSaturday = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colSunday = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOverview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabOverview.SuspendLayout();
            this.gbWeeklyPresence.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWeeklyDetail)).BeginInit();
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
            this.colOverPersonID,
            this.colFirstName,
            this.colLastName,
            this.colBirthday});
            this.dgvOverview.Location = new System.Drawing.Point(20, 9);
            this.dgvOverview.Name = "dgvOverview";
            this.dgvOverview.ReadOnly = true;
            this.dgvOverview.Size = new System.Drawing.Size(722, 155);
            this.dgvOverview.TabIndex = 1;
            this.dgvOverview.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvOverview_CellDoubleClick);
            // 
            // colOverPersonID
            // 
            this.colOverPersonID.HeaderText = "ID";
            this.colOverPersonID.Name = "colOverPersonID";
            this.colOverPersonID.ReadOnly = true;
            this.colOverPersonID.Visible = false;
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
            this.btnAddPerson.Location = new System.Drawing.Point(7, 375);
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
            this.tabOverview.Controls.Add(this.gbWeeklyPresence);
            this.tabOverview.Controls.Add(this.btnDeleteSelected);
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
            // gbWeeklyPresence
            // 
            this.gbWeeklyPresence.Controls.Add(this.dgvWeeklyDetail);
            this.gbWeeklyPresence.Controls.Add(this.btnSaveWeeklyChanges);
            this.gbWeeklyPresence.Location = new System.Drawing.Point(20, 204);
            this.gbWeeklyPresence.Name = "gbWeeklyPresence";
            this.gbWeeklyPresence.Size = new System.Drawing.Size(722, 115);
            this.gbWeeklyPresence.TabIndex = 12;
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
            this.colPersonName,
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
            // colWeekPresPersonID
            // 
            this.colWeekPresPersonID.HeaderText = "ID";
            this.colWeekPresPersonID.Name = "colWeekPresPersonID";
            this.colWeekPresPersonID.Visible = false;
            this.colWeekPresPersonID.Width = 5;
            // 
            // colPersonName
            // 
            this.colPersonName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colPersonName.HeaderText = "Name";
            this.colPersonName.Name = "colPersonName";
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
            // btnSaveWeeklyChanges
            // 
            this.btnSaveWeeklyChanges.Location = new System.Drawing.Point(627, 86);
            this.btnSaveWeeklyChanges.Name = "btnSaveWeeklyChanges";
            this.btnSaveWeeklyChanges.Size = new System.Drawing.Size(89, 23);
            this.btnSaveWeeklyChanges.TabIndex = 11;
            this.btnSaveWeeklyChanges.Text = "Save changes";
            this.btnSaveWeeklyChanges.UseVisualStyleBackColor = true;
            this.btnSaveWeeklyChanges.Click += new System.EventHandler(this.btnSaveWeeklyChanges_Click);
            // 
            // btnDeleteSelected
            // 
            this.btnDeleteSelected.Location = new System.Drawing.Point(608, 170);
            this.btnDeleteSelected.Name = "btnDeleteSelected";
            this.btnDeleteSelected.Size = new System.Drawing.Size(134, 23);
            this.btnDeleteSelected.TabIndex = 9;
            this.btnDeleteSelected.Text = "Delete selected rows";
            this.btnDeleteSelected.UseVisualStyleBackColor = true;
            this.btnDeleteSelected.Click += new System.EventHandler(this.btn_DeleteSelected);
            // 
            // tabPresence
            // 
            this.tabPresence.Controls.Add(this.btnPreviousWeek);
            this.tabPresence.Controls.Add(this.btnNextWeek);
            this.tabPresence.Controls.Add(this.btnSavePresenceChanges);
            this.tabPresence.Controls.Add(this.dgvPresence);
            this.tabPresence.Location = new System.Drawing.Point(4, 22);
            this.tabPresence.Name = "tabPresence";
            this.tabPresence.Padding = new System.Windows.Forms.Padding(3);
            this.tabPresence.Size = new System.Drawing.Size(779, 410);
            this.tabPresence.TabIndex = 1;
            this.tabPresence.Text = "Presence";
            this.tabPresence.UseVisualStyleBackColor = true;
            // 
            // btnPreviousWeek
            // 
            this.btnPreviousWeek.Location = new System.Drawing.Point(575, 8);
            this.btnPreviousWeek.Name = "btnPreviousWeek";
            this.btnPreviousWeek.Size = new System.Drawing.Size(93, 23);
            this.btnPreviousWeek.TabIndex = 3;
            this.btnPreviousWeek.Text = "Previous week";
            this.btnPreviousWeek.UseVisualStyleBackColor = true;
            this.btnPreviousWeek.Click += new System.EventHandler(this.btnPreviousWeek_Click);
            // 
            // btnNextWeek
            // 
            this.btnNextWeek.Location = new System.Drawing.Point(674, 8);
            this.btnNextWeek.Name = "btnNextWeek";
            this.btnNextWeek.Size = new System.Drawing.Size(93, 23);
            this.btnNextWeek.TabIndex = 2;
            this.btnNextWeek.Text = "Next week";
            this.btnNextWeek.UseVisualStyleBackColor = true;
            this.btnNextWeek.Click += new System.EventHandler(this.btnNextWeek_Click);
            // 
            // btnSavePresenceChanges
            // 
            this.btnSavePresenceChanges.Location = new System.Drawing.Point(674, 347);
            this.btnSavePresenceChanges.Name = "btnSavePresenceChanges";
            this.btnSavePresenceChanges.Size = new System.Drawing.Size(99, 23);
            this.btnSavePresenceChanges.TabIndex = 1;
            this.btnSavePresenceChanges.Text = "Save changes";
            this.btnSavePresenceChanges.UseVisualStyleBackColor = true;
            this.btnSavePresenceChanges.Click += new System.EventHandler(this.btnSavePresenceChanges_Click);
            // 
            // dgvPresence
            // 
            this.dgvPresence.AllowUserToAddRows = false;
            this.dgvPresence.AllowUserToDeleteRows = false;
            this.dgvPresence.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPresence.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colPresPersonID,
            this.colPerson,
            this.colMonday,
            this.colTuesday,
            this.colWednesday,
            this.colThursday,
            this.colFriday,
            this.colSaturday,
            this.colSunday});
            this.dgvPresence.Location = new System.Drawing.Point(7, 37);
            this.dgvPresence.Name = "dgvPresence";
            this.dgvPresence.Size = new System.Drawing.Size(766, 304);
            this.dgvPresence.TabIndex = 0;
            // 
            // colPresPersonID
            // 
            this.colPresPersonID.HeaderText = "PersonID";
            this.colPresPersonID.Name = "colPresPersonID";
            this.colPresPersonID.Visible = false;
            // 
            // colPerson
            // 
            this.colPerson.HeaderText = "Person";
            this.colPerson.Name = "colPerson";
            this.colPerson.Width = 200;
            // 
            // colMonday
            // 
            this.colMonday.HeaderText = "Monday";
            this.colMonday.Name = "colMonday";
            this.colMonday.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colMonday.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.colMonday.Width = 75;
            // 
            // colTuesday
            // 
            this.colTuesday.HeaderText = "Tuesday";
            this.colTuesday.Name = "colTuesday";
            this.colTuesday.Width = 75;
            // 
            // colWednesday
            // 
            this.colWednesday.HeaderText = "Wednesday";
            this.colWednesday.Name = "colWednesday";
            this.colWednesday.Width = 75;
            // 
            // colThursday
            // 
            this.colThursday.HeaderText = "Thursday";
            this.colThursday.Name = "colThursday";
            this.colThursday.Width = 75;
            // 
            // colFriday
            // 
            this.colFriday.HeaderText = "Friday";
            this.colFriday.Name = "colFriday";
            this.colFriday.Width = 75;
            // 
            // colSaturday
            // 
            this.colSaturday.HeaderText = "Saturday";
            this.colSaturday.Name = "colSaturday";
            this.colSaturday.Width = 75;
            // 
            // colSunday
            // 
            this.colSunday.HeaderText = "Sunday";
            this.colSunday.Name = "colSunday";
            this.colSunday.Width = 75;
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
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
            this.gbWeeklyPresence.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvWeeklyDetail)).EndInit();
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
        private System.Windows.Forms.Button btnDeleteSelected;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.DataGridView dgvPresence;
        private System.Windows.Forms.Button btnSavePresenceChanges;
        private System.Windows.Forms.Button btnPreviousWeek;
        private System.Windows.Forms.Button btnNextWeek;
        private System.Windows.Forms.DataGridViewTextBoxColumn colOverPersonID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFirstName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLastName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBirthday;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPresPersonID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPerson;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colMonday;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colTuesday;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colWednesday;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colThursday;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colFriday;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colSaturday;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colSunday;
        private System.Windows.Forms.DataGridView dgvWeeklyDetail;
        private System.Windows.Forms.Button btnSaveWeeklyChanges;
        private System.Windows.Forms.GroupBox gbWeeklyPresence;
        private System.Windows.Forms.DataGridViewTextBoxColumn colWeekPresPersonID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPersonName;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colWeekMonday;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colWeekTuesday;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colWeekWednesday;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colWeekThursday;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colWeekFriday;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colWeekSaturday;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colWeekSunday;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
    }
}

