namespace ClientGestioneFile.Mosconi {
    partial class FileSelectionForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.listBoxFiles = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // listBoxFiles
            // 
            this.listBoxFiles.BackColor = System.Drawing.Color.DarkGray;
            this.listBoxFiles.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.97015F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBoxFiles.ForeColor = System.Drawing.SystemColors.Control;
            this.listBoxFiles.FormattingEnabled = true;
            this.listBoxFiles.ItemHeight = 30;
            this.listBoxFiles.Location = new System.Drawing.Point(12, 77);
            this.listBoxFiles.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.listBoxFiles.Name = "listBoxFiles";
            this.listBoxFiles.Size = new System.Drawing.Size(403, 304);
            this.listBoxFiles.TabIndex = 0;
            this.listBoxFiles.Click += new System.EventHandler(this.listBoxFiles_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 24.1791F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.Control;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(396, 52);
            this.label1.TabIndex = 3;
            this.label1.Text = "SELEZIONA FILE";
            // 
            // FileSelectionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DimGray;
            this.ClientSize = new System.Drawing.Size(427, 412);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listBoxFiles);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "FileSelectionForm";
            this.Text = "FileSelectionForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxFiles;
        private System.Windows.Forms.Label label1;
    }
}