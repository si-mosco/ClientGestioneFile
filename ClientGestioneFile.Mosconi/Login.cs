using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;

namespace ClientGestioneFile.Mosconi {
    public partial class LoginForm : Form {
        private const string ServerIp = "127.0.0.1";
        private int ClientPort = 6969;
         
        public string name { private set; get; }
        public LoginForm(int Port) {
            InitializeComponent();
            ClientPort = Port;
        }

        private void LoginButton_Click_1(object sender, EventArgs e) {
            try {
                using (TcpClient client = new TcpClient(ServerIp, ClientPort))
                using (NetworkStream stream = client.GetStream())
                using (BinaryWriter writer = new BinaryWriter(stream, Encoding.UTF8))
                using (BinaryReader reader = new BinaryReader(stream, Encoding.UTF8)) {
                    writer.Write("LOGIN");
                    if (!String.IsNullOrEmpty(usernameTextBox.Text) && !String.IsNullOrEmpty(passwordTextBox.Text))
                    {
                        writer.Write(usernameTextBox.Text);
                        writer.Write(passwordTextBox.Text);

                        string response = reader.ReadString();
                        if (response == "OK")
                        {
                            MessageBox.Show("Accesso effettuato con successo.");
                            name = usernameTextBox.Text;
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Credenziali non valide.");
                        }
                    }
                }
            } catch (Exception ex) {
                MessageBox.Show($"Errore durante il login: {ex.Message}");
            }
        }

        private void RegisterButton_Click(object sender, EventArgs e) {
            try {
                if (!String.IsNullOrEmpty(usernameTextBox.Text) && !String.IsNullOrEmpty(passwordTextBox.Text))
                {
                    using (TcpClient client = new TcpClient(ServerIp, ClientPort))
                    using (NetworkStream stream = client.GetStream())
                    using (BinaryWriter writer = new BinaryWriter(stream, Encoding.UTF8))
                    using (BinaryReader reader = new BinaryReader(stream, Encoding.UTF8))
                    {
                        writer.Write("REGISTER");
                        writer.Write(usernameTextBox.Text);
                        writer.Write(passwordTextBox.Text);

                        string response = reader.ReadString();
                        if (response == "OK")
                        {
                            MessageBox.Show("Registrazione effettuata con successo.");
                            name = usernameTextBox.Text;
                            this.DialogResult = DialogResult.OK;
                        }
                        else
                        {
                            MessageBox.Show("Credenziali non valide.");
                        }
                    }
                }
                else MessageBox.Show($"Errore durante la registrazione: Inserire Valori validi");
            } catch (Exception ex) {
                MessageBox.Show($"Errore durante la registrazione: {ex.Message}");
            }
        }
    }
}