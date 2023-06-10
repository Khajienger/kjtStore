using System.Data;
using System.Windows;

namespace kjtStore
{
    /// <summary>
    /// Логика взаимодействия для ConfirmDeleteOrderWindow.xaml
    /// </summary>
    public partial class ConfirmDeleteOrderWindow : Window
    {
        private Connections connections = new Connections();
        private Orders orders;
        private DataRowView drv;
        private int orderID;

        public ConfirmDeleteOrderWindow(int OrderID, Orders Orders, DataRowView DRV)
        {
            InitializeComponent();

            this.orderID = OrderID;
            this.orders = Orders;
            this.drv = DRV;
            this.OrderID.Text = OrderID.ToString();

            connections.Preparing();
            connections.SelectOrdersTable();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SoundManager.PlayClick();
            this.Hide();

            connections.DeleteOrder(orderID, drv);
            orders.OrdersGrid.DataContext = connections.GetOrdersTable();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            SoundManager.PlayClick();
            this.Hide();
        }
    }
}