using System;
using System.Windows;
using System.Media;

namespace kjtStore
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private StoreWindow storeWindow;
        private Autorizator autorizator;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SoundManager.PlayClick();

            if (autorizator == null)
            {
                autorizator = new Autorizator();
                autorizator.Preparing();
            }

            if (autorizator.LoginAndPassword(Login.Text, Password.Password))
            {
                OpenStoreWindow();
                this.Hide();
            }
            else
            {
                SystemSounds.Asterisk.Play();
                System.Windows.Forms.MessageBox.Show("Неверный логин или пароль.");
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            SoundManager.PlayClick();
            Environment.Exit(0);
        }

        #region Управление окнами.

        private void OpenStoreWindow()
        {
            if (storeWindow == null || !storeWindow.IsLoaded)
            {
                storeWindow = new StoreWindow();
                storeWindow.Show();
            }
            else if (storeWindow.IsLoaded && !storeWindow.IsActive)
            {
                storeWindow.Show();
            }
        }

        #endregion
    }
}