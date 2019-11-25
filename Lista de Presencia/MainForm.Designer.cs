namespace Lista_de_Presencia
{
    partial class MainForm
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
            this.btnAddService = new System.Windows.Forms.Button();
            this.btnAddGroup = new System.Windows.Forms.Button();
            this.btnAddProgram = new System.Windows.Forms.Button();
            this.btnDeleteSelected = new System.Windows.Forms.Button();
            this.tabPresence = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.cbbPrograms = new System.Windows.Forms.ComboBox();
            this.gbPresence = new System.Windows.Forms.GroupBox();
            this.lblRangeInfo = new System.Windows.Forms.Label();
            this.btnViewRange = new System.Windows.Forms.Button();
            this.btnPreviousRange = new System.Windows.Forms.Button();
            this.btnSavePresenceChanges = new System.Windows.Forms.Button();
            this.btnNextRange = new System.Windows.Forms.Button();
            this.dgvPresenceMonthFormat = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewCheckBoxColumn1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewCheckBoxColumn2 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewCheckBoxColumn3 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewCheckBoxColumn4 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewCheckBoxColumn5 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewCheckBoxColumn6 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewCheckBoxColumn7 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dgvPresenceWeekFormat = new System.Windows.Forms.DataGridView();
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
            this.tabPresence.SuspendLayout();
            this.gbPresence.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPresenceMonthFormat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPresenceWeekFormat)).BeginInit();
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
            this.dgvOverview.Size = new System.Drawing.Size(722, 301);
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
            this.colBirthday.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colBirthday.Width = 225;
            // 
            // btnAddPerson
            // 
            this.btnAddPerson.Location = new System.Drawing.Point(656, 375);
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
            this.tabControl.Size = new System.Drawing.Size(940, 436);
            this.tabControl.TabIndex = 4;
            // 
            // tabOverview
            // 
            this.tabOverview.Controls.Add(this.btnAddService);
            this.tabOverview.Controls.Add(this.btnAddGroup);
            this.tabOverview.Controls.Add(this.btnAddProgram);
            this.tabOverview.Controls.Add(this.btnDeleteSelected);
            this.tabOverview.Controls.Add(this.dgvOverview);
            this.tabOverview.Controls.Add(this.btnAddPerson);
            this.tabOverview.Location = new System.Drawing.Point(4, 22);
            this.tabOverview.Name = "tabOverview";
            this.tabOverview.Padding = new System.Windows.Forms.Padding(3);
            this.tabOverview.Size = new System.Drawing.Size(932, 410);
            this.tabOverview.TabIndex = 0;
            this.tabOverview.Text = "Overview";
            this.tabOverview.UseVisualStyleBackColor = true;
            // 
            // btnAddService
            // 
            this.btnAddService.Location = new System.Drawing.Point(112, 375);
            this.btnAddService.Name = "btnAddService";
            this.btnAddService.Size = new System.Drawing.Size(86, 29);
            this.btnAddService.TabIndex = 15;
            this.btnAddService.Text = "Add Service";
            this.btnAddService.UseVisualStyleBackColor = true;
            this.btnAddService.Click += new System.EventHandler(this.btnAddService_Click);
            // 
            // btnAddGroup
            // 
            this.btnAddGroup.Location = new System.Drawing.Point(204, 375);
            this.btnAddGroup.Name = "btnAddGroup";
            this.btnAddGroup.Size = new System.Drawing.Size(86, 29);
            this.btnAddGroup.TabIndex = 14;
            this.btnAddGroup.Text = "Add Group";
            this.btnAddGroup.UseVisualStyleBackColor = true;
            this.btnAddGroup.Click += new System.EventHandler(this.btnAddGroup_Click);
            // 
            // btnAddProgram
            // 
            this.btnAddProgram.Location = new System.Drawing.Point(20, 375);
            this.btnAddProgram.Name = "btnAddProgram";
            this.btnAddProgram.Size = new System.Drawing.Size(86, 29);
            this.btnAddProgram.TabIndex = 13;
            this.btnAddProgram.Text = "Add Program";
            this.btnAddProgram.UseVisualStyleBackColor = true;
            this.btnAddProgram.Click += new System.EventHandler(this.btnAddProgram_Click);
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
            // tabPresence
            // 
            this.tabPresence.Controls.Add(this.label1);
            this.tabPresence.Controls.Add(this.cbbPrograms);
            this.tabPresence.Controls.Add(this.gbPresence);
            this.tabPresence.Location = new System.Drawing.Point(4, 22);
            this.tabPresence.Name = "tabPresence";
            this.tabPresence.Padding = new System.Windows.Forms.Padding(3);
            this.tabPresence.Size = new System.Drawing.Size(932, 410);
            this.tabPresence.TabIndex = 1;
            this.tabPresence.Text = "Presence";
            this.tabPresence.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Choose a program";
            // 
            // cbbPrograms
            // 
            this.cbbPrograms.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbPrograms.FormattingEnabled = true;
            this.cbbPrograms.Location = new System.Drawing.Point(106, 16);
            this.cbbPrograms.Name = "cbbPrograms";
            this.cbbPrograms.Size = new System.Drawing.Size(121, 21);
            this.cbbPrograms.TabIndex = 5;
            this.cbbPrograms.SelectedValueChanged += new System.EventHandler(this.cbbPrograms_SelectedValueChanged);
            // 
            // gbPresence
            // 
            this.gbPresence.Controls.Add(this.lblRangeInfo);
            this.gbPresence.Controls.Add(this.btnViewRange);
            this.gbPresence.Controls.Add(this.btnPreviousRange);
            this.gbPresence.Controls.Add(this.btnSavePresenceChanges);
            this.gbPresence.Controls.Add(this.btnNextRange);
            this.gbPresence.Controls.Add(this.dgvPresenceMonthFormat);
            this.gbPresence.Controls.Add(this.dgvPresenceWeekFormat);
            this.gbPresence.Location = new System.Drawing.Point(7, 43);
            this.gbPresence.Name = "gbPresence";
            this.gbPresence.Size = new System.Drawing.Size(919, 361);
            this.gbPresence.TabIndex = 4;
            this.gbPresence.TabStop = false;
            this.gbPresence.Text = "TBD";
            // 
            // lblRangeInfo
            // 
            this.lblRangeInfo.AutoSize = true;
            this.lblRangeInfo.Location = new System.Drawing.Point(33, 17);
            this.lblRangeInfo.Name = "lblRangeInfo";
            this.lblRangeInfo.Size = new System.Drawing.Size(60, 13);
            this.lblRangeInfo.TabIndex = 6;
            this.lblRangeInfo.Text = "Range Info";
            // 
            // btnViewRange
            // 
            this.btnViewRange.Location = new System.Drawing.Point(417, 12);
            this.btnViewRange.Name = "btnViewRange";
            this.btnViewRange.Size = new System.Drawing.Size(86, 23);
            this.btnViewRange.TabIndex = 4;
            this.btnViewRange.Text = "Monthly View";
            this.btnViewRange.UseVisualStyleBackColor = true;
            this.btnViewRange.Click += new System.EventHandler(this.btnViewRange_Click);
            // 
            // btnPreviousRange
            // 
            this.btnPreviousRange.Location = new System.Drawing.Point(721, 12);
            this.btnPreviousRange.Name = "btnPreviousRange";
            this.btnPreviousRange.Size = new System.Drawing.Size(93, 23);
            this.btnPreviousRange.TabIndex = 3;
            this.btnPreviousRange.Text = "Previous week";
            this.btnPreviousRange.UseVisualStyleBackColor = true;
            this.btnPreviousRange.Click += new System.EventHandler(this.btnPreviousRange_Click);
            // 
            // btnSavePresenceChanges
            // 
            this.btnSavePresenceChanges.Location = new System.Drawing.Point(814, 332);
            this.btnSavePresenceChanges.Name = "btnSavePresenceChanges";
            this.btnSavePresenceChanges.Size = new System.Drawing.Size(99, 23);
            this.btnSavePresenceChanges.TabIndex = 1;
            this.btnSavePresenceChanges.Text = "Save changes";
            this.btnSavePresenceChanges.UseVisualStyleBackColor = true;
            this.btnSavePresenceChanges.Click += new System.EventHandler(this.btnSavePresenceChanges_Click);
            // 
            // btnNextRange
            // 
            this.btnNextRange.Location = new System.Drawing.Point(820, 12);
            this.btnNextRange.Name = "btnNextRange";
            this.btnNextRange.Size = new System.Drawing.Size(93, 23);
            this.btnNextRange.TabIndex = 2;
            this.btnNextRange.Text = "Next week";
            this.btnNextRange.UseVisualStyleBackColor = true;
            this.btnNextRange.Click += new System.EventHandler(this.btnNextRange_Click);
            // 
            // dgvPresenceMonthFormat
            // 
            this.dgvPresenceMonthFormat.AllowUserToAddRows = false;
            this.dgvPresenceMonthFormat.AllowUserToDeleteRows = false;
            this.dgvPresenceMonthFormat.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPresenceMonthFormat.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewCheckBoxColumn1,
            this.dataGridViewCheckBoxColumn2,
            this.dataGridViewCheckBoxColumn3,
            this.dataGridViewCheckBoxColumn4,
            this.dataGridViewCheckBoxColumn5,
            this.dataGridViewCheckBoxColumn6,
            this.dataGridViewCheckBoxColumn7});
            this.dgvPresenceMonthFormat.Location = new System.Drawing.Point(3, 41);
            this.dgvPresenceMonthFormat.Name = "dgvPresenceMonthFormat";
            this.dgvPresenceMonthFormat.Size = new System.Drawing.Size(910, 287);
            this.dgvPresenceMonthFormat.TabIndex = 5;
            this.dgvPresenceMonthFormat.Visible = false;
            this.dgvPresenceMonthFormat.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPresenceMonthFormat_CellClick);
            this.dgvPresenceMonthFormat.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvPresenceMonthFormat_CellMouseUp);
            this.dgvPresenceMonthFormat.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPresenceMonthFormat_CellValueChanged);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "PersonID";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Visible = false;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Person";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Width = 200;
            // 
            // dataGridViewCheckBoxColumn1
            // 
            this.dataGridViewCheckBoxColumn1.HeaderText = "Monday";
            this.dataGridViewCheckBoxColumn1.Name = "dataGridViewCheckBoxColumn1";
            this.dataGridViewCheckBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewCheckBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dataGridViewCheckBoxColumn1.Width = 75;
            // 
            // dataGridViewCheckBoxColumn2
            // 
            this.dataGridViewCheckBoxColumn2.HeaderText = "Tuesday";
            this.dataGridViewCheckBoxColumn2.Name = "dataGridViewCheckBoxColumn2";
            this.dataGridViewCheckBoxColumn2.Width = 75;
            // 
            // dataGridViewCheckBoxColumn3
            // 
            this.dataGridViewCheckBoxColumn3.HeaderText = "Wednesday";
            this.dataGridViewCheckBoxColumn3.Name = "dataGridViewCheckBoxColumn3";
            this.dataGridViewCheckBoxColumn3.Width = 75;
            // 
            // dataGridViewCheckBoxColumn4
            // 
            this.dataGridViewCheckBoxColumn4.HeaderText = "Thursday";
            this.dataGridViewCheckBoxColumn4.Name = "dataGridViewCheckBoxColumn4";
            this.dataGridViewCheckBoxColumn4.Width = 75;
            // 
            // dataGridViewCheckBoxColumn5
            // 
            this.dataGridViewCheckBoxColumn5.HeaderText = "Friday";
            this.dataGridViewCheckBoxColumn5.Name = "dataGridViewCheckBoxColumn5";
            this.dataGridViewCheckBoxColumn5.Width = 75;
            // 
            // dataGridViewCheckBoxColumn6
            // 
            this.dataGridViewCheckBoxColumn6.HeaderText = "Saturday";
            this.dataGridViewCheckBoxColumn6.Name = "dataGridViewCheckBoxColumn6";
            this.dataGridViewCheckBoxColumn6.Width = 75;
            // 
            // dataGridViewCheckBoxColumn7
            // 
            this.dataGridViewCheckBoxColumn7.HeaderText = "Sunday";
            this.dataGridViewCheckBoxColumn7.Name = "dataGridViewCheckBoxColumn7";
            this.dataGridViewCheckBoxColumn7.Width = 75;
            // 
            // dgvPresenceWeekFormat
            // 
            this.dgvPresenceWeekFormat.AllowUserToAddRows = false;
            this.dgvPresenceWeekFormat.AllowUserToDeleteRows = false;
            this.dgvPresenceWeekFormat.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPresenceWeekFormat.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colPresPersonID,
            this.colPerson,
            this.colMonday,
            this.colTuesday,
            this.colWednesday,
            this.colThursday,
            this.colFriday,
            this.colSaturday,
            this.colSunday});
            this.dgvPresenceWeekFormat.Location = new System.Drawing.Point(3, 41);
            this.dgvPresenceWeekFormat.Name = "dgvPresenceWeekFormat";
            this.dgvPresenceWeekFormat.Size = new System.Drawing.Size(910, 287);
            this.dgvPresenceWeekFormat.TabIndex = 0;
            this.dgvPresenceWeekFormat.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPresenceWeekFormat_CellClick);
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
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(953, 440);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.splitContainer1);
            this.Name = "MainForm";
            this.Text = "Main";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOverview)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.tabOverview.ResumeLayout(false);
            this.tabPresence.ResumeLayout(false);
            this.tabPresence.PerformLayout();
            this.gbPresence.ResumeLayout(false);
            this.gbPresence.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPresenceMonthFormat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPresenceWeekFormat)).EndInit();
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
        private System.Windows.Forms.DataGridView dgvPresenceWeekFormat;
        private System.Windows.Forms.Button btnSavePresenceChanges;
        private System.Windows.Forms.Button btnPreviousRange;
        private System.Windows.Forms.Button btnNextRange;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPresPersonID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPerson;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colMonday;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colTuesday;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colWednesday;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colThursday;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colFriday;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colSaturday;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colSunday;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.Button btnAddProgram;
        private System.Windows.Forms.GroupBox gbPresence;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbbPrograms;
        private System.Windows.Forms.DataGridViewTextBoxColumn colOverPersonID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFirstName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLastName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBirthday;
        private System.Windows.Forms.Button btnAddGroup;
        private System.Windows.Forms.Button btnAddService;
        private System.Windows.Forms.Button btnViewRange;
        private System.Windows.Forms.DataGridView dgvPresenceMonthFormat;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn2;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn3;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn4;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn5;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn6;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn7;
        private System.Windows.Forms.Label lblRangeInfo;
    }
}

