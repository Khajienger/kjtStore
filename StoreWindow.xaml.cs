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
        private Clients clients;
        private Orders orders;
        private Settings settings;

        public StoreWindow()
        {
            InitializeComponent();
            Login.Text = KjtWriterReaderCipher.ReadText(@"pd\lg.kjt", true);
        }

        public void RefreshGrids()
        {
            clients.RefreshClientsGrid();
            orders.RefreshOrdersGrid();
        }

        public void ReOpenClientsAndOrdersWindows()
        {
            if(clients.IsLoaded)
            {
                clients.Close();
                OpenClients();
            }
            if(orders.IsLoaded)
            {
                orders.Close();
                OpenOrders();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SoundManager.PlayClick();
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
                addOrder = new AddOrderWindow();
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
                clients = new Clients();
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
                orders = new Orders();
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