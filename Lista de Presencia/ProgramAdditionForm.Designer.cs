namespace Lista_de_Presencia
{
    partial class ProgramAdditionForm
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
            this.txtProgramName = new System.Windows.Forms.TextBox();
            this.btnCreateProgram = new System.Windows.Forms.Button();
            this.gbExistingPrograms = new System.Windows.Forms.GroupBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name";
            // 
            // txtProgramName
            // 
            this.txtProgramName.Location = new System.Drawing.Point(68, 24);
            this.txtProgramName.Name = "txtProgramName";
            this.txtProgramName.Size = new System.Drawing.Size(100, 20);
            this.txtProgramName.TabIndex = 1;
            // 
            // btnCreateProgram
            // 
            this.btnCreateProgram.Location = new System.Drawing.Point(204, 20);
            this.btnCreateProgram.Name = "btnCreateProgram";
            this.btnCreateProgram.Size = new System.Drawing.Size(95, 27);
            this.btnCreateProgram.TabIndex = 4;
            this.btnCreateProgram.Text = "Create Program";
            this.btnCreateProgram.UseVisualStyleBackColor = true;
            this.btnCreateProgram.Click += new System.EventHandler(this.btnCreateProgram_Click);
            // 
            // gbExistingPrograms
            // 
            this.gbExistingPrograms.Location = new System.Drawing.Point(30, 68);
            this.gbExistingPrograms.Name = "gbExistingPrograms";
            this.gbExistingPrograms.Size = new System.Drawing.Size(269, 149);
            this.gbExistingPrograms.TabIndex = 5;
            this.gbExistingPrograms.TabStop = false;
            this.gbExistingPrograms.Text = "Existing programs";
            // 
            // ProgramAdditionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.gbExistingPrograms);
            this.Controls.Add(this.btnCreateProgram);
            this.Controls.Add(this.txtProgramName);
            this.Controls.Add(this.label1);
            this.Name = "ProgramAdditionForm";
            this.Text = "Program Addition";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ProgramAdditionForm_FormClosing);
            this.Load += new System.EventHandler(this.Form3_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtProgramName;
        private System.Windows.Forms.Button btnCreateProgram;
        private System.Windows.Forms.GroupBox gbExistingPrograms;
    }
}