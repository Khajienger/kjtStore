using System.Media;
using System.Windows;

namespace kjtStore
{
    /// <summary>
    /// Логика взаимодействия для Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        private Autorizator autorizator;

        public Settings()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SoundManager.PlayClick();
            this.Hide();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            SoundManager.PlayClick();

            if(autorizator == null)
            {
                autorizator = new Autorizator();
                autorizator.Preparing();
            }

            if (autorizator.ChangePassword(OldPassword.Password, NewPassword.Password, NewPasswordRepeat.Password))
            {
                SystemSounds.Asterisk.Play();

                MessageBox.Show(
                    "Пароль успешно заменён!",
                    "Настройки",
                    MessageBoxButton.OK);
            }
        }
    }
}