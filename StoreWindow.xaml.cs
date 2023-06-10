using System;
using System.Windows;

namespace kjtStore
{
    /// <summary>
    /// Логика взаимодействия для StoreWindow.xaml
    /// </summary>
    ///
    public partial class StoreWindow : Window
    {
        private AddOrderWindow addOrder;
        private Settings settings;
        private Clients clients;
        private Orders orders;

        public StoreWindow()
        {
            InitializeComponent();
            Login.Text = KjtWriterReaderCipher.ReadText(@"pd\lg.kjt", true);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SoundManager.PlayClick();

            if (clients == null || !clients.IsLoaded)
            {
                clients = new Clients(orders);
            }
            if (orders == null || !orders.IsLoaded)
            {
                orders = new Orders(clients);
            }

            OpenAddOrder();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            SoundManager.PlayClick();
            OpenClients();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            SoundManager.PlayClick();
            OpenOrders();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            SoundManager.PlayClick();
            Environment.Exit(0);
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            SoundManager.PlayClick();
            OpenSettings();
        }

        #region Управление окнами.

        private void OpenAddOrder()
        {
            if (addOrder == null || !addOrder.IsLoaded)
            {
                addOrder = new AddOrderWindow(clients, orders);
                addOrder.Show();
            }
            else if (addOrder.IsLoaded && !addOrder.IsActive)
            {
                addOrder.Show();
            }
        }

        private void OpenClients()
        {
            if (clients == null || !clients.IsLoaded)
            {
                clients = new Clients(orders);
                clients.Show();
            }
            else if (clients.IsLoaded && !clients.IsActive)
            {
                clients.Show();
            }
        }

        private void OpenOrders()
        {
            if (orders == null || !orders.IsLoaded)
            {
                orders = new Orders(clients);
                orders.Show();
            }
            else if (orders.IsLoaded && !orders.IsActive)
            {
                orders.Show();
            }
        }

        private void OpenSettings()
        {
            if (settings == null || !settings.IsLoaded)
            {
                settings = new Settings();
                settings.Show();
            }
            else if (settings.IsLoaded && !settings.IsActive)
            {
                settings.Show();
            }
        }

        #endregion
    }
}