namespace Lista_de_Presencia {
    partial class ServiceForm {
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
            this.txtServiceName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbbPrograms = new System.Windows.Forms.ComboBox();
            this.btnAddService = new System.Windows.Forms.Button();
            this.gbExistingServices = new System.Windows.Forms.GroupBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(231, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name";
            // 
            // txtServiceName
            // 
            this.txtServiceName.Location = new System.Drawing.Point(272, 22);
            this.txtServiceName.Name = "txtServiceName";
            this.txtServiceName.Size = new System.Drawing.Size(100, 20);
            this.txtServiceName.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Program";
            // 
            // cbbPrograms
            // 
            this.cbbPrograms.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbPrograms.FormattingEnabled = true;
            this.cbbPrograms.Location = new System.Drawing.Point(74, 22);
            this.cbbPrograms.Name = "cbbPrograms";
            this.cbbPrograms.Size = new System.Drawing.Size(121, 21);
            this.cbbPrograms.TabIndex = 3;
            // 
            // btnAddService
            // 
            this.btnAddService.Location = new System.Drawing.Point(297, 54);
            this.btnAddService.Name = "btnAddService";
            this.btnAddService.Size = new System.Drawing.Size(75, 23);
            this.btnAddService.TabIndex = 4;
            this.btnAddService.Text = "Add Service";
            this.btnAddService.UseVisualStyleBackColor = true;
            this.btnAddService.Click += new System.EventHandler(this.btnAddService_Click);
            // 
            // gbExistingServices
            // 
            this.gbExistingServices.Location = new System.Drawing.Point(25, 87);
            this.gbExistingServices.Name = "gbExistingServices";
            this.gbExistingServices.Size = new System.Drawing.Size(347, 151);
            this.gbExistingServices.TabIndex = 5;
            this.gbExistingServices.TabStop = false;
            this.gbExistingServices.Text = "Existing services";
            // 
            // ServiceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.gbExistingServices);
            this.Controls.Add(this.btnAddService);
            this.Controls.Add(this.cbbPrograms);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtServiceName);
            this.Controls.Add(this.label1);
            this.Name = "ServiceForm";
            this.Text = "ServiceForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ServiceForm_FormClosing);
            this.Load += new System.EventHandler(this.ServiceForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtServiceName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbbPrograms;
        private System.Windows.Forms.Button btnAddService;
        private System.Windows.Forms.GroupBox gbExistingServices;
    }
}