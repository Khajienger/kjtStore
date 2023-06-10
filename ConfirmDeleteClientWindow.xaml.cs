using System.Data;
using System.Windows;

namespace kjtStore
{
    /// <summary>
    /// Логика взаимодействия для ConfirmDeleteClientWindow.xaml
    /// </summary>
    public partial class ConfirmDeleteClientWindow : Window
    {
        private Connections connections = new Connections();
        private Clients clients;
        private DataRowView drv;
        private int clientID;

        public ConfirmDeleteClientWindow(int ClientID, Clients Clients, DataRowView DRV)
        {
            InitializeComponent();

            this.clientID = ClientID;
            this.clients = Clients;
            this.drv = DRV;
            this.ClientID.Text = ClientID.ToString();

            connections.Preparing();
            connections.SelectClientsTable();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SoundManager.PlayClick();
            this.Hide();

            connections.DeleteClient(clientID, drv);
            clients.ClientsGrid.DataContext = connections.GetClientsTable();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            SoundManager.PlayClick();
            this.Hide();
        }
    }
}