using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace kjtStore
{
    /// <summary>
    /// Логика взаимодействия для Orders.xaml
    /// </summary>
    public partial class Orders
    {
        private AddOrderWindow addOrderWindow;

        private Connections connections;

        private DataRowView dataRowView;

        public Orders()
        {
            InitializeComponent();
            connections = new Connections();
            connections.StartPreparing();
            connections.SelectOrdersTable();
            RefreshOrdersGrid();
        }

        public void RefreshOrdersGrid()
        {
            OrdersGrid.DataContext = connections.GetOrdersTable().DefaultView;
        }

        private void CurrentCellChanging(object sender, EventArgs e)
        {
            dataRowView = (DataRowView)OrdersGrid.SelectedItem;
            dataRowView.BeginEdit();
        }

        private void EndCurrentCellChanging(object sender, DataGridCellEditEndingEventArgs e)
        {
            dataRowView.EndEdit();
            connections.UpdateOrdersTable();
        }

        private void OpenAddOrderWindow(object sender, RoutedEventArgs e)
        {
            SoundManager.PlaySound(@"pd\Resources\ClickContextMenuSound.wav");
            OpenAddOrderWindow();
        }

        private void DeleteOrder(object sender, RoutedEventArgs e)
        {
            SoundManager.PlaySound(@"pd\Resources\ClickContextMenuSound.wav");
            connections.DeleteOrder((DataRowView)OrdersGrid.SelectedItem);
        }

        #region Управление окнами.

        private void OpenAddOrderWindow()
        {
            if (addOrderWindow == null || !addOrderWindow.IsLoaded)
            {
                addOrderWindow = new AddOrderWindow();
                addOrderWindow.Show();
            }
            else if (addOrderWindow.IsLoaded && !addOrderWindow.IsActive)
            {
                addOrderWindow.Show();
            }
        }

        #endregion
    }
}