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
        private Connections connections;
        private DataRowView dataRowView;

        public Clients()
        {
            InitializeComponent();
            connections = new Connections();
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
            dataRowView.EndEdit();
            connections.UpdateClientsTable();
        }

        private void DeleteClient(object sender, RoutedEventArgs e)
        {
            SoundManager.PlaySound(@"pd\Resources\ClickContextMenuSound.wav");
            connections.DeleteClient((DataRowView)ClientsGrid.SelectedItem);
        }
    }
}