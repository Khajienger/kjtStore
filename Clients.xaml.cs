using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace kjtStore
{
    /// <summary>
    /// Логика взаимодействия для Clients.xaml
    /// </summary>
    public partial class Clients
    {
        private Connections connections = new Connections();
        private DataRowView dataRowView;

        public Clients()
        {
            InitializeComponent();
            connections.StartPreparing();
            connections.SelectClientsTable();
            RefreshClientsGrid();
        }

        public void RefreshClientsGrid()
        {
            ClientsGrid.DataContext = connections.GetClientsTable().DefaultView;
        }

        private void CurrentCellChanging(object sender, EventArgs e)
        {
            dataRowView = (DataRowView)ClientsGrid.SelectedItem;
            dataRowView.BeginEdit();
        }

        private void EndCurrentCellChanging(object sender, DataGridCellEditEndingEventArgs e)
        {
            void OnEndChangingCycle()
            {
                try
                {
                    dataRowView.EndEdit();
                    connections.UpdateClientsTable();
                }
                catch (NullReferenceException)
                {
                    connections.StartPreparing();
                    OnEndChangingCycle();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"{ex.Message}");
                }
            }
        }

        private void DeleteClient(object sender, RoutedEventArgs e)
        {
            SoundManager.PlaySound(@"pd\Resources\ClickContextMenuSound.wav");
            connections.DeleteClient((DataRowView)ClientsGrid.SelectedItem);
        }
    }
}