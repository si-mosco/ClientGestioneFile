using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace ClientGestioneFile.Mosconi {
    public partial class Form1 : Form {
        public string ServerIp = "127.0.0.1";
        public int ServerPort = 6969;

        public int ClientPort = -1;

        private string name = "";
        public Form1() {
            InitializeComponent();
            UploadButton.Enabled = false;
            DownloadButton.Enabled = false;
            LoginButton.Enabled = true;
            button1.Enabled = false;
        }

        private void UploadButton_MouseClick(object sender, MouseEventArgs e) {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK) {
                try {
                    using (TcpClient client = new TcpClient(ServerIp, ClientPort))
                    using (NetworkStream stream = client.GetStream())
                    using (BinaryWriter writer = new BinaryWriter(stream, Encoding.UTF8))
                    using (BinaryReader reader = new BinaryReader(stream, Encoding.UTF8)) {
                        // Invia il comando per il caricamento di un file
                        writer.Write("UPLOAD");

                        string fileName = Path.GetFileName(openFileDialog.FileName);
                        MessageBox.Show(fileName);
                        byte[] fileData = File.ReadAllBytes(openFileDialog.FileName);

                        writer.Write(fileName);
                        writer.Write(fileData.Length);
                        writer.Write(fileData);
                        writer.Write(name);

                        string response = reader.ReadString();
                        MessageBox.Show(response);
                    }
                } catch (Exception ex) {
                    MessageBox.Show($"Errore durante il caricamento del file: {ex.Message}");
                }
            }
        }

        private void DownloadButton_Click_1(object sender, EventArgs e) {
            // Richiedi l'elenco dei file disponibili e permetti all'utente di selezionarne uno per il download
            RequestFileList();
        }

        private void LoginButton_Click_1(object sender, EventArgs e) {
            using (LoginForm loginForm = new LoginForm(ClientPort)) {
                if (loginForm.ShowDialog() == DialogResult.OK) {
                    UploadButton.Enabled = true;
                    DownloadButton.Enabled = true;
                    LoginButton.Enabled = false;
                    button1.Enabled = true;
                }
                name = loginForm.name;
            }
        }

        private void RequestFileList() {
            try {
                using (TcpClient client = new TcpClient(ServerIp, ClientPort))
                using (NetworkStream stream = client.GetStream())
                using (BinaryWriter writer = new BinaryWriter(stream, Encoding.UTF8))
                using (BinaryReader reader = new BinaryReader(stream, Encoding.UTF8)) {
                    // Invia il comando al server per richiedere l'elenco dei file
                    writer.Write("LIST_FILES");
                    writer.Write(name);

                    // Leggi il numero di file dal server
                    int fileCount = reader.ReadInt32();
                    List<string> fileList = new List<string>();
                    for (int i = 0; i < fileCount; i++) {
                        // Leggi ogni nome file e aggiungilo all'elenco
                        string fileName = reader.ReadString();
                        fileList.Add(fileName);
                    }

                    // Visualizza l'elenco dei file e consente all'utente di selezionare uno per il download
                    SelectFileForDownload(fileList);
                }
            } catch (Exception ex) {
                MessageBox.Show($"Errore durante il recupero dell'elenco dei file: {ex.Message}");
            }
        }

        private void SelectFileForDownload(List<string> fileList) {
            // Mostra una finestra di dialogo personalizzata per selezionare un file dalla lista
            using (FileSelectionForm fileSelectionForm = new FileSelectionForm(fileList)) {
                if (fileSelectionForm.ShowDialog() == DialogResult.OK) {
                    DownloadSelectedFile(fileSelectionForm.SelectedFile);
                }
            }
        }

        private void DownloadSelectedFile(string fileName) {
            // Crea un dialogo per selezionare il percorso di salvataggio del file
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = fileName;
            MessageBox.Show(fileName);
            if (saveFileDialog.ShowDialog() == DialogResult.OK) {
                try {
                    using (TcpClient client = new TcpClient(ServerIp, ClientPort))
                    using (NetworkStream stream = client.GetStream())
                    using (BinaryWriter writer = new BinaryWriter(stream, Encoding.UTF8))
                    using (BinaryReader reader = new BinaryReader(stream, Encoding.UTF8)) {
                        // Invia al server il comando per il download del file selezionato
                        writer.Write("DOWNLOAD");
                        writer.Write(fileName);

                        // Ricevi la risposta dal server
                        string response = reader.ReadString();
                        if (response == "OK") {
                            // Se la risposta è OK, leggi la dimensione del file e i dati del file
                            int fileSize = reader.ReadInt32();
                            byte[] fileData = reader.ReadBytes(fileSize);

                            // Salva il file sul disco locale
                            File.WriteAllBytes(saveFileDialog.FileName, fileData);

                            // Mostra un messaggio di conferma
                            MessageBox.Show($"File scaricato con successo: {saveFileDialog.FileName}");
                        } else {
                            // Se il file richiesto non esiste sul server, mostra un messaggio di errore
                            MessageBox.Show("Il file richiesto non esiste sul server.");
                        }
                    }
                } catch (Exception ex) {
                    // Gestisci eventuali errori durante il download
                    MessageBox.Show($"Errore durante il download del file: {ex.Message}");
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try {
                using (TcpClient client = new TcpClient(ServerIp, ServerPort))
                using (NetworkStream stream = client.GetStream())
                using (BinaryReader reader = new BinaryReader(stream, Encoding.UTF8))
                using (BinaryWriter writer = new BinaryWriter(stream, Encoding.UTF8)) {
                    ClientPort = int.Parse(reader.ReadString());
                }
            } catch (Exception ex) {
                MessageBox.Show($"Errore durante il recupero della porta client: {ex.Message}");
            }
        }
        private void button1_Click(object sender, EventArgs e) {
            name = "";

            UploadButton.Enabled = false;
            DownloadButton.Enabled = false;
            LoginButton.Enabled = true;
            button1.Enabled = false;
        }
    }
}