using System.Data;
using System.Windows;

namespace kjtStore
{
    /// <summary>
    /// Логика взаимодействия для Orders.xaml
    /// </summary>
    public partial class Orders
    {
        private AddOrderWindow addOrderWindow;
        private EditOrderWindow editOrderWindow;
        private ConfirmDeleteOrderWindow confirmDeleteOrderWindow;
        private Clients clients;

        private Connections connections = new Connections();
        private DataRowView dataRowView;

        public Orders(Clients Clients)
        {
            InitializeComponent();

            connections.Preparing();
            connections.SelectOrdersTable();

            this.clients = Clients;
            RefreshOrdersGrid();
        }

        private void RefreshOrdersGrid()
        {
            OrdersGrid.DataContext = connections.GetOrdersTable().DefaultView;
        }

        private void AddOrder(object sender, RoutedEventArgs e)
        {
            SoundManager.PlayClickContextMenu();
            OpenAddOrderWindow();
        }

        private void EditOrder(object sender, RoutedEventArgs e)
        {
            SoundManager.PlayClickContextMenu();
            OpenEditOrderWindow();
        }

        private void DeleteOrder(object sender, RoutedEventArgs e)
        {
            SoundManager.PlayClickContextMenu();
            OpenConfirmDeleteOrderWindow();
        }

        #region Управление окнами.

        private void OpenAddOrderWindow()
        {
            if (addOrderWindow == null || !addOrderWindow.IsLoaded)
            {
                addOrderWindow = new AddOrderWindow(clients, this);
                addOrderWindow.Show();
            }
            else if (addOrderWindow.IsLoaded && !addOrderWindow.IsActive)
            {
                addOrderWindow.Show();
            }
        }

        private void OpenEditOrderWindow()
        {
            dataRowView = (DataRowView)OrdersGrid.SelectedItem;

            if (dataRowView != null)
            {
                string[] orderData = new string[3]
                {
                    $"{dataRowView.Row.ItemArray[1]}",
                    $"{dataRowView.Row.ItemArray[2]}",
                    $"{dataRowView.Row.ItemArray[3]}"
                };

                editOrderWindow = new EditOrderWindow(int.Parse(dataRowView.Row.ItemArray[0].ToString()), orderData, this);
                editOrderWindow.Show();
            }
        }

        private void OpenConfirmDeleteOrderWindow()
        {
            dataRowView = (DataRowView)OrdersGrid.SelectedItem;

            if (dataRowView != null)
            {
                confirmDeleteOrderWindow = new ConfirmDeleteOrderWindow(int.Parse(dataRowView.Row.ItemArray[0].ToString()), this, dataRowView);
                confirmDeleteOrderWindow.Show();
            }
        }

        #endregion
    }
}