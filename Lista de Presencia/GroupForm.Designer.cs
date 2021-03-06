﻿namespace Lista_de_Presencia {
    partial class GroupForm {
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
            this.txtGroupID = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbbSupervisors = new System.Windows.Forms.ComboBox();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.cbbServices = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnAddGroup = new System.Windows.Forms.Button();
            this.txtGroupName = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.gbExistingGroups = new System.Windows.Forms.GroupBox();
            this.lblGroupSupervisorLegend = new System.Windows.Forms.Label();
            this.lblGroupDatesLegend = new System.Windows.Forms.Label();
            this.lblGroupServiceLegend = new System.Windows.Forms.Label();
            this.lblGroupNameLegend = new System.Windows.Forms.Label();
            this.lblGroupIDLegend = new System.Windows.Forms.Label();
            this.pnlExistingGroups = new System.Windows.Forms.Panel();
            this.gbExistingGroups.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(42, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Group ID";
            // 
            // txtGroupID
            // 
            this.txtGroupID.Location = new System.Drawing.Point(98, 57);
            this.txtGroupID.Name = "txtGroupID";
            this.txtGroupID.Size = new System.Drawing.Size(100, 20);
            this.txtGroupID.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(395, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Supervisor";
            // 
            // cbbSupervisors
            // 
            this.cbbSupervisors.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbSupervisors.FormattingEnabled = true;
            this.cbbSupervisors.Location = new System.Drawing.Point(458, 26);
            this.cbbSupervisors.Name = "cbbSupervisors";
            this.cbbSupervisors.Size = new System.Drawing.Size(121, 21);
            this.cbbSupervisors.TabIndex = 3;
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpStartDate.Location = new System.Drawing.Point(257, 60);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(95, 20);
            this.dtpStartDate.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(222, 60);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Start";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(426, 60);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(26, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "End";
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEndDate.Location = new System.Drawing.Point(458, 60);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(95, 20);
            this.dtpEndDate.TabIndex = 6;
            // 
            // cbbServices
            // 
            this.cbbServices.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbServices.FormattingEnabled = true;
            this.cbbServices.Location = new System.Drawing.Point(257, 25);
            this.cbbServices.Name = "cbbServices";
            this.cbbServices.Size = new System.Drawing.Size(121, 21);
            this.cbbServices.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(208, 28);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(43, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Service";
            // 
            // btnAddGroup
            // 
            this.btnAddGroup.Location = new System.Drawing.Point(504, 98);
            this.btnAddGroup.Name = "btnAddGroup";
            this.btnAddGroup.Size = new System.Drawing.Size(75, 23);
            this.btnAddGroup.TabIndex = 7;
            this.btnAddGroup.Text = "Add Group";
            this.btnAddGroup.UseVisualStyleBackColor = true;
            this.btnAddGroup.Click += new System.EventHandler(this.btnAddGroup_Click);
            // 
            // txtGroupName
            // 
            this.txtGroupName.Location = new System.Drawing.Point(98, 22);
            this.txtGroupName.Name = "txtGroupName";
            this.txtGroupName.Size = new System.Drawing.Size(100, 20);
            this.txtGroupName.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(25, 25);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Group Name";
            // 
            // gbExistingGroups
            // 
            this.gbExistingGroups.Controls.Add(this.lblGroupSupervisorLegend);
            this.gbExistingGroups.Controls.Add(this.lblGroupDatesLegend);
            this.gbExistingGroups.Controls.Add(this.lblGroupServiceLegend);
            this.gbExistingGroups.Controls.Add(this.lblGroupNameLegend);
            this.gbExistingGroups.Controls.Add(this.lblGroupIDLegend);
            this.gbExistingGroups.Controls.Add(this.pnlExistingGroups);
            this.gbExistingGroups.Location = new System.Drawing.Point(28, 142);
            this.gbExistingGroups.Name = "gbExistingGroups";
            this.gbExistingGroups.Size = new System.Drawing.Size(653, 161);
            this.gbExistingGroups.TabIndex = 12;
            this.gbExistingGroups.TabStop = false;
            this.gbExistingGroups.Text = "Existing groups";
            // 
            // lblGroupSupervisorLegend
            // 
            this.lblGroupSupervisorLegend.AutoSize = true;
            this.lblGroupSupervisorLegend.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGroupSupervisorLegend.Location = new System.Drawing.Point(450, 20);
            this.lblGroupSupervisorLegend.Name = "lblGroupSupervisorLegend";
            this.lblGroupSupervisorLegend.Size = new System.Drawing.Size(57, 13);
            this.lblGroupSupervisorLegend.TabIndex = 5;
            this.lblGroupSupervisorLegend.Tag = "Freeze";
            this.lblGroupSupervisorLegend.Text = "Supervisor";
            // 
            // lblGroupDatesLegend
            // 
            this.lblGroupDatesLegend.AutoSize = true;
            this.lblGroupDatesLegend.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGroupDatesLegend.Location = new System.Drawing.Point(300, 20);
            this.lblGroupDatesLegend.Name = "lblGroupDatesLegend";
            this.lblGroupDatesLegend.Size = new System.Drawing.Size(35, 13);
            this.lblGroupDatesLegend.TabIndex = 4;
            this.lblGroupDatesLegend.Tag = "Freeze";
            this.lblGroupDatesLegend.Text = "Dates";
            // 
            // lblGroupServiceLegend
            // 
            this.lblGroupServiceLegend.AutoSize = true;
            this.lblGroupServiceLegend.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGroupServiceLegend.Location = new System.Drawing.Point(175, 20);
            this.lblGroupServiceLegend.Name = "lblGroupServiceLegend";
            this.lblGroupServiceLegend.Size = new System.Drawing.Size(43, 13);
            this.lblGroupServiceLegend.TabIndex = 3;
            this.lblGroupServiceLegend.Tag = "Freeze";
            this.lblGroupServiceLegend.Text = "Service";
            // 
            // lblGroupNameLegend
            // 
            this.lblGroupNameLegend.AutoSize = true;
            this.lblGroupNameLegend.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGroupNameLegend.Location = new System.Drawing.Point(75, 20);
            this.lblGroupNameLegend.Name = "lblGroupNameLegend";
            this.lblGroupNameLegend.Size = new System.Drawing.Size(35, 13);
            this.lblGroupNameLegend.TabIndex = 2;
            this.lblGroupNameLegend.Tag = "Freeze";
            this.lblGroupNameLegend.Text = "Name";
            // 
            // lblGroupIDLegend
            // 
            this.lblGroupIDLegend.AutoSize = true;
            this.lblGroupIDLegend.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGroupIDLegend.Location = new System.Drawing.Point(15, 20);
            this.lblGroupIDLegend.Name = "lblGroupIDLegend";
            this.lblGroupIDLegend.Size = new System.Drawing.Size(18, 13);
            this.lblGroupIDLegend.TabIndex = 1;
            this.lblGroupIDLegend.Tag = "Freeze";
            this.lblGroupIDLegend.Text = "ID";
            // 
            // pnlExistingGroups
            // 
            this.pnlExistingGroups.AutoScroll = true;
            this.pnlExistingGroups.Location = new System.Drawing.Point(6, 40);
            this.pnlExistingGroups.Name = "pnlExistingGroups";
            this.pnlExistingGroups.Size = new System.Drawing.Size(641, 115);
            this.pnlExistingGroups.TabIndex = 13;
            this.pnlExistingGroups.MouseEnter += new System.EventHandler(this.pnlExistingGroups_MouseEnter);
            // 
            // GroupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.gbExistingGroups);
            this.Controls.Add(this.txtGroupName);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnAddGroup);
            this.Controls.Add(this.cbbServices);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dtpEndDate);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dtpStartDate);
            this.Controls.Add(this.cbbSupervisors);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtGroupID);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GroupForm";
            this.Text = "GroupForm";
            this.Load += new System.EventHandler(this.GroupForm_Load);
            this.gbExistingGroups.ResumeLayout(false);
            this.gbExistingGroups.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtGroupID;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbbSupervisors;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.ComboBox cbbServices;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnAddGroup;
        private System.Windows.Forms.TextBox txtGroupName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox gbExistingGroups;
        private System.Windows.Forms.Label lblGroupSupervisorLegend;
        private System.Windows.Forms.Label lblGroupDatesLegend;
        private System.Windows.Forms.Label lblGroupServiceLegend;
        private System.Windows.Forms.Label lblGroupNameLegend;
        private System.Windows.Forms.Label lblGroupIDLegend;
        private System.Windows.Forms.Panel pnlExistingGroups;
    }
}