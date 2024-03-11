using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ClientGestioneFile.Mosconi {
    public partial class FileSelectionForm : Form {
        public string SelectedFile { get; private set; }

        public FileSelectionForm(List<string> fileList) {
            InitializeComponent();
            PopulateFileList(fileList);
        }

        private void PopulateFileList(List<string> fileList) {
            foreach (var file in fileList) {
                listBoxFiles.Items.Add(file);
            }
        }

        private void listBoxFiles_Click(object sender, EventArgs e) {
            if (listBoxFiles.SelectedItem != null) {
                SelectedFile = listBoxFiles.SelectedItem.ToString();
                DialogResult = DialogResult.OK;
                Close();
            } else {
                MessageBox.Show("Seleziona un file dalla lista.", "Attenzione", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
