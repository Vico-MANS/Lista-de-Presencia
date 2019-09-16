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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnAddPerson = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabOverview = new System.Windows.Forms.TabPage();
            this.btnDeleteSelected = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpBirthday = new System.Windows.Forms.DateTimePicker();
            this.txtLastname = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtFirstname = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPresence = new System.Windows.Forms.TabPage();
            this.colFirstName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLastName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colBirthday = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabOverview.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colFirstName,
            this.colLastName,
            this.colBirthday});
            this.dataGridView1.Location = new System.Drawing.Point(20, 9);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(722, 301);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
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
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabOverview);
            this.tabControl1.Controls.Add(this.tabPresence);
            this.tabControl1.Location = new System.Drawing.Point(1, 2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(787, 436);
            this.tabControl1.TabIndex = 4;
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
            this.tabOverview.Controls.Add(this.dataGridView1);
            this.tabOverview.Controls.Add(this.btnAddPerson);
            this.tabOverview.Location = new System.Drawing.Point(4, 22);
            this.tabOverview.Name = "tabOverview";
            this.tabOverview.Padding = new System.Windows.Forms.Padding(3);
            this.tabOverview.Size = new System.Drawing.Size(779, 410);
            this.tabOverview.TabIndex = 0;
            this.tabOverview.Text = "Overview";
            this.tabOverview.UseVisualStyleBackColor = true;
            this.tabOverview.Click += new System.EventHandler(this.tabPage1_Click);
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
            this.label3.Click += new System.EventHandler(this.label3_Click);
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
            this.txtLastname.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
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
            this.txtFirstname.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(45, 381);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "First name";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // tabPresence
            // 
            this.tabPresence.Location = new System.Drawing.Point(4, 22);
            this.tabPresence.Name = "tabPresence";
            this.tabPresence.Padding = new System.Windows.Forms.Padding(3);
            this.tabPresence.Size = new System.Drawing.Size(779, 410);
            this.tabPresence.TabIndex = 1;
            this.tabPresence.Text = "Presence";
            this.tabPresence.UseVisualStyleBackColor = true;
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
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabOverview.ResumeLayout(false);
            this.tabOverview.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnAddPerson;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TabControl tabControl1;
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
    }
}

