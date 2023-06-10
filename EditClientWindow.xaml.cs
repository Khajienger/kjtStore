using Microsoft.IdentityModel.Tokens;
using System.Media;
using System.Windows;

namespace kjtStore
{
    /// <summary>
    /// Логика взаимодействия для EditClientWindow.xaml
    /// </summary>
    public partial class EditClientWindow : Window
    {
        private Clients clients;
        private Connections connections = new Connections();

        private int clientID;
        private string clientFirstName;
        private string clientSecondName;
        private string clientPatronymicdName;
        private string clientPhone;
        private string clientEmail;

        public EditClientWindow(int ClientID, string[] ClientData, Clients Clients)
        {
            InitializeComponent();

            clientID = ClientID;
            clientFirstName = ClientData[0];
            clientSecondName = ClientData[1];
            clientPatronymicdName = ClientData[2];
            clientPhone = ClientData[3];
            clientEmail = ClientData[4];

            this.ClientID.Text = clientID.ToString();
            this.ClientFirstName.Text = clientFirstName;
            this.ClientSecondName.Text = clientSecondName;
            this.ClientPatronymicName.Text = clientPatronymicdName;
            this.ClientPhone.Text = clientPhone;
            this.ClientEmail.Text = clientEmail;
            this.clients = Clients;

            connections.Preparing();
        }

        private bool CheckCorrectClientData()
        {
            bool success = true;
            bool _success = true;

            if (ClientFirstName.Text.IsNullOrEmpty())
            {
                SystemSounds.Asterisk.Play();
                MessageBox.Show("Поле 'Фамилия' не может быть пустым.");
                _success = false;
            }

            if (ClientSecondName.Text.IsNullOrEmpty())
            {
                SystemSounds.Asterisk.Play();
                MessageBox.Show("Поле 'Имя' не может быть пустым.");
                _success = false;
            }

            if (!ClientPhone.Text.IsNullOrEmpty())
            {
                try
                {
                    string _phone = ClientPhone.Text;
                    long.Parse(_phone);
                }
                catch
                {
                    SystemSounds.Asterisk.Play();
                    _success = false;
                    MessageBox.Show("Поле 'Номер клиента' должно состоять только из цифр.");
                }
            }

            if (ClientEmail.Text.IsNullOrEmpty())
            {
                SystemSounds.Asterisk.Play();
                MessageBox.Show("Поле 'Почта клиента' не может быть пустым.");
                _success = false;
            }

            success = _success;
            return success;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SoundManager.PlayClick();

            if(CheckCorrectClientData())
            {
                string[] clientData = new string[5]
                {
                    $"{this.ClientFirstName.Text}",
                    $"{this.ClientSecondName.Text}",
                    $"{this.ClientPatronymicName.Text}",
                    $"{this.ClientPhone.Text}",
                    $"{this.ClientEmail.Text}"
                };

                connections.UpdateClient(clientID, clientData);
                clients.ClientsGrid.DataContext = connections.GetClientsTable();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            SoundManager.PlayClick();
            this.Hide();
        }
    }
}