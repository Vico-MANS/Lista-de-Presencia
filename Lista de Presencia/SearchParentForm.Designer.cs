namespace Lista_de_Presencia {
    partial class SearchParentForm {
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
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtSearchText = new System.Windows.Forms.TextBox();
            this.dgvResults = new System.Windows.Forms.DataGridView();
            this.colPersonID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPersonFirstName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPersonLastName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnValidate = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name";
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(235, 8);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtSearchText
            // 
            this.txtSearchText.Location = new System.Drawing.Point(54, 10);
            this.txtSearchText.Name = "txtSearchText";
            this.txtSearchText.Size = new System.Drawing.Size(163, 20);
            this.txtSearchText.TabIndex = 2;
            this.txtSearchText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearchText_KeyDown);
            // 
            // dgvResults
            // 
            this.dgvResults.AllowUserToAddRows = false;
            this.dgvResults.AllowUserToDeleteRows = false;
            this.dgvResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResults.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colPersonID,
            this.colPersonFirstName,
            this.colPersonLastName});
            this.dgvResults.Location = new System.Drawing.Point(16, 50);
            this.dgvResults.MultiSelect = false;
            this.dgvResults.Name = "dgvResults";
            this.dgvResults.ReadOnly = true;
            this.dgvResults.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvResults.Size = new System.Drawing.Size(444, 174);
            this.dgvResults.TabIndex = 3;
            this.dgvResults.SelectionChanged += new System.EventHandler(this.dgvResults_SelectionChanged);
            // 
            // colPersonID
            // 
            this.colPersonID.HeaderText = "ID";
            this.colPersonID.Name = "colPersonID";
            this.colPersonID.ReadOnly = true;
            this.colPersonID.Visible = false;
            // 
            // colPersonFirstName
            // 
            this.colPersonFirstName.HeaderText = "First Name";
            this.colPersonFirstName.Name = "colPersonFirstName";
            this.colPersonFirstName.ReadOnly = true;
            this.colPersonFirstName.Width = 150;
            // 
            // colPersonLastName
            // 
            this.colPersonLastName.HeaderText = "Last Name";
            this.colPersonLastName.Name = "colPersonLastName";
            this.colPersonLastName.ReadOnly = true;
            this.colPersonLastName.Width = 250;
            // 
            // btnValidate
            // 
            this.btnValidate.Location = new System.Drawing.Point(385, 239);
            this.btnValidate.Name = "btnValidate";
            this.btnValidate.Size = new System.Drawing.Size(75, 23);
            this.btnValidate.TabIndex = 4;
            this.btnValidate.Text = "Validate";
            this.btnValidate.UseVisualStyleBackColor = true;
            this.btnValidate.Click += new System.EventHandler(this.btnValidate_Click);
            // 
            // SearchParentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(475, 269);
            this.Controls.Add(this.btnValidate);
            this.Controls.Add(this.dgvResults);
            this.Controls.Add(this.txtSearchText);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SearchParentForm";
            this.Text = "SearchParentForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SearchParentForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SearchParentForm_FormClosed);
            this.Load += new System.EventHandler(this.SearchParentForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtSearchText;
        private System.Windows.Forms.DataGridView dgvResults;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPersonID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPersonFirstName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPersonLastName;
        private System.Windows.Forms.Button btnValidate;
    }
}