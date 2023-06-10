using Microsoft.IdentityModel.Tokens;
using System.Media;
using System.Windows;

namespace kjtStore
{
    /// <summary>
    /// Логика взаимодействия для EditOrderWindow.xaml
    /// </summary>
    public partial class EditOrderWindow : Window
    {
        private Orders orders;
        private Connections connections = new Connections();

        private int orderID;
        private string email;
        private int code;
        private string title;

        public EditOrderWindow(int OrderID, string[] OrderData, Orders Orders)
        {
            InitializeComponent();
            orderID = OrderID;
            email = OrderData[0];
            code = int.Parse(OrderData[1]);
            title = OrderData[2];

            this.OrderID.Text = orderID.ToString();
            this.ClientEmail.Text = email;
            this.CodeProduct.Text = code.ToString();
            this.TitleProduct.Text = title;

            this.orders = Orders;

            connections.Preparing();
        }

        private bool CheckCorrectOrderData()
        {
            bool success = true;
            bool _success = true;

            if (ClientEmail.Text.IsNullOrEmpty())
            {
                SystemSounds.Asterisk.Play();
                MessageBox.Show("Поле 'Почта клиента' не может быть пустым.");
                _success = false;
            }

            if (CodeProduct.Text.IsNullOrEmpty())
            {
                SystemSounds.Asterisk.Play();
                MessageBox.Show("Поле 'Код товара' не может быть пустым.");
                _success = false;
            }

            if (!CodeProduct.Text.IsNullOrEmpty())
            {
                try
                {
                    long.Parse(CodeProduct.Text);
                }
                catch
                {
                    SystemSounds.Asterisk.Play();
                    _success = false;
                    MessageBox.Show("Поле 'Код товара' должно состоять только из цифр.");
                }
            }

            if (TitleProduct.Text.IsNullOrEmpty())
            {
                SystemSounds.Asterisk.Play();
                MessageBox.Show("Поле 'Название' не может быть пустым.");
                _success = false;
            }

            success = _success;
            return success;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SoundManager.PlayClick();

            if(CheckCorrectOrderData())
            {
                string[] orderData = new string[3]
                {
                    $"{this.ClientEmail.Text}",
                    $"{this.CodeProduct.Text}",
                    $"{this.TitleProduct.Text}"
                };

                connections.UpdateOrder(orderID, orderData);
                orders.OrdersGrid.DataContext = connections.GetOrdersTable();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            SoundManager.PlayClick();
            this.Hide();
        }
    }
}