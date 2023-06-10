using System.Data;
using System.Windows;

namespace kjtStore
{
    /// <summary>
    /// Логика взаимодействия для Clients.xaml
    /// </summary>
    public partial class Clients
    {
        private AddOrderWindow addOrderWindow;
        private EditClientWindow editClientWindow;
        private ConfirmDeleteClientWindow confirmDeleteClientWindow;
        private Orders orders;
        
        private Connections connections = new Connections();
        private DataRowView dataRowView;

        public Clients(Orders Orders)
        {
            InitializeComponent();

            connections.Preparing();
            connections.SelectClientsTable();

            this.orders = Orders;
            RefreshClientsGrid();
        }

        private void RefreshClientsGrid()
        {
            ClientsGrid.DataContext = connections.GetClientsTable().DefaultView;
        }

        private void AddOrder(object sender, RoutedEventArgs e)
        {
            SoundManager.PlayClickContextMenu();
            OpenAddOrderWindow();
        }

        private void EditClient(object sender, RoutedEventArgs e)
        {
            OpenEditClientWindow();
            SoundManager.PlayClickContextMenu();
        }

        private void DeleteClient(object sender, RoutedEventArgs e)
        {
            SoundManager.PlayClickContextMenu();
            OpenConfirmDeleteClientWindow();
        }

        #region Управление окнами.

        private void OpenAddOrderWindow()
        {
            if (addOrderWindow == null || !addOrderWindow.IsLoaded)
            {
                addOrderWindow = new AddOrderWindow(this, orders);
                addOrderWindow.Show();
            }
            else if (addOrderWindow.IsLoaded && !addOrderWindow.IsActive)
            {
                addOrderWindow.Show();
            }
        }

        private void OpenEditClientWindow()
        {
            dataRowView = (DataRowView)ClientsGrid.SelectedItem;

            if (dataRowView != null)
            {
                string[] clientData = new string[5]
                {
                $"{dataRowView.Row.ItemArray[1]}",
                $"{dataRowView.Row.ItemArray[2]}",
                $"{dataRowView.Row.ItemArray[3]}",
                $"{dataRowView.Row.ItemArray[4]}",
                $"{dataRowView.Row.ItemArray[5]}"
                };

                editClientWindow = new EditClientWindow(int.Parse(dataRowView.Row.ItemArray[0].ToString()), clientData, this);
                editClientWindow.Show();
            }
        }

        private void OpenConfirmDeleteClientWindow()
        {
            dataRowView = (DataRowView)ClientsGrid.SelectedItem;

            if (dataRowView != null)
            {
                confirmDeleteClientWindow = new ConfirmDeleteClientWindow(int.Parse(dataRowView.Row.ItemArray[0].ToString()), this, dataRowView);
                confirmDeleteClientWindow.Show();
            }
        }

        #endregion
    }
}