using Microsoft.IdentityModel.Tokens;
using System.Media;
using System.Windows;

namespace kjtStore
{
    /// <summary>
    /// Логика взаимодействия для AddOrderWindow.xaml
    /// </summary>
    public partial class AddOrderWindow : Window
    {
        private Connections connections;
        private Client client;
        private Order order;

        public AddOrderWindow()
        {
            InitializeComponent();
            connections = new Connections();
            connections.StartPreparing();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            bool success;
            bool _success = true;

            SoundManager.PlayClick();

            if(CheckCorrectClientData())
            {
                client = new Client(
                    firstName.Text,
                    secondName.Text,
                    patronymicName.Text,
                    phone.Text,
                    email.Text);
            }
            else
            {
                _success = false;
            }

            if (CheckCorrectOrderData())
            {
                order = new Order(
                    email.Text,
                    int.Parse(code.Text),
                    title.Text);
            }
            else
            {
                success = false;
            }

            success = _success;

            if(success)
            {
                connections.AddNewOrderAndClientInfo(client, order);
            }
        }

        private bool CheckCorrectClientData()
        {
            bool success;
            bool _success = true;

            if(firstName.Text.IsNullOrEmpty())
            {
                SystemSounds.Asterisk.Play();
                _success = false;
                MessageBox.Show("Поле 'Фамилия клиента' не может быть пустым.");
            }

            if (secondName.Text.IsNullOrEmpty())
            {
                SystemSounds.Asterisk.Play();
                _success = false;
                MessageBox.Show("Поле 'Имя клиента' не может быть пустым.");
            }

            if (!phone.Text.IsNullOrEmpty())
            {
                try
                {
                    string _phone = phone.Text;
                    long.Parse(_phone);
                }
                catch
                {
                    SystemSounds.Asterisk.Play();
                    _success = false;
                    MessageBox.Show("Поле 'Номер клиента' должно состоять только из цифр.");
                }
            }

            if (email.Text.IsNullOrEmpty())
            {
                SystemSounds.Asterisk.Play();
                _success = false;
                MessageBox.Show("Поле 'Почта клиента' не может быть пустым.");
            }

            success = _success;

            return success;
        }

        private bool CheckCorrectOrderData()
        {
            bool success;
            bool _success = true;

            if (code.Text.IsNullOrEmpty())
            {
                SystemSounds.Asterisk.Play();
                _success = false;
                MessageBox.Show("Поле 'Код товара' не может быть пустым.");
            }

            if (!code.Text.IsNullOrEmpty())
            {
                try
                {
                    long.Parse(code.Text);
                }
                catch
                {
                    SystemSounds.Asterisk.Play();
                    _success = false;
                    MessageBox.Show("Поле 'Код товара' должно состоять только из цифр.");
                }
            }

            if (title.Text.IsNullOrEmpty())
            {
                SystemSounds.Asterisk.Play();
                _success = false;
                MessageBox.Show("Поле 'Название товара' не может быть пустым.");
            }

            success = _success;
            return success;
        }
    }
}