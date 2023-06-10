using System;
using System.Data.OleDb;
using System.Data;
using System.Windows;
using System.Media;
using System.Data.SqlClient;

namespace kjtStore
{
    internal class Connections
    {
        public Connections() { }

        private SqlConnectionStringBuilder sqConSB = new SqlConnectionStringBuilder();
        private OleDbConnectionStringBuilder odbConSB = new OleDbConnectionStringBuilder();

        private SqlConnection sqConnection;
        private SqlCommand sqCommand;
        private SqlDataAdapter sqDataUpdater = new SqlDataAdapter();

        private OleDbConnection odbConnection;
        private OleDbCommand odbCommand;
        private OleDbDataAdapter odbDataUpdater = new OleDbDataAdapter();

        private DataTable clientsTable = new DataTable();
        private DataTable ordersTable = new DataTable();

        private string sq;
        private string odb;

        public DataTable GetClientsTable()
        {
            return clientsTable;
        }

        public DataTable GetOrdersTable()
        {
            return ordersTable;
        }

        public void Preparing()
        {
            sqConSB = new SqlConnectionStringBuilder
            {
                DataSource = @"(localdb)\MSSQLLocalDB",
                InitialCatalog = "kjtStoreDB",
                IntegratedSecurity = true,
                Pooling = true,
            };

            odbConSB = new OleDbConnectionStringBuilder
            {
                Provider = "Microsoft.ACE.OLEDB.12.0",
                DataSource = @"C:\Users\Khajienger\C#Projects\homeworks\hmw16-kjtStore\kjtStore\OrdersDataBaseAccess\kjtStoreOrders.accdb",
            };
        }

        public void SelectClientsTable()
        {
            clientsTable = new DataTable();

            using (sqConnection = new SqlConnection(sqConSB.ConnectionString))
            {
                sqConnection.Open();

                sq = @"select * from Clients order by Clients.id";
                
                sqDataUpdater.SelectCommand = new SqlCommand(sq, sqConnection);
                sqDataUpdater.Fill(clientsTable);

                sqConnection.Dispose();
            }
        }

        public void SelectOrdersTable()
        {
            ordersTable = new DataTable();

            using (odbConnection = new OleDbConnection(odbConSB.ConnectionString))
            {
                odbConnection.Open();

                odb = @"select * from Orders order by Orders.id";
                
                odbDataUpdater.SelectCommand = new OleDbCommand(odb, odbConnection);
                odbDataUpdater.Fill(ordersTable);

                odbConnection.Dispose();
            }
        }

        public void AddNewOrderAndClientInfo(Client client, Order order)
        {
            AddClient(client);
            AddOrder(order);

            SelectClientsTable();
            SelectOrdersTable();
        }

        public void UpdateClient(int ID, string[] ClientData)
        {
            try
            {
                using (sqConnection = new SqlConnection(sqConSB.ConnectionString))
                {
                    sqConnection.Open();

                    sq = $@"update Clients set
                        firstName = N'{ClientData[0]}',
                        secondName = N'{ClientData[1]}',
                        patronymicName = N'{ClientData[2]}',
                        phone = N'{ClientData[3]}',
                        email = N'{ClientData[4]}'
                        where
                        id = {ID}";

                    sqCommand = new SqlCommand(sq, sqConnection);

                    sqCommand.Parameters.AddWithValue("@id", "id").Direction = ParameterDirection.Output;
                    sqCommand.Parameters.AddWithValue("@firstName", "firstName");
                    sqCommand.Parameters.AddWithValue("@secondName", "secondName");
                    sqCommand.Parameters.AddWithValue("@patronymicName", "patronymicName");
                    sqCommand.Parameters.AddWithValue("@phone", "phone");
                    sqCommand.Parameters.AddWithValue("@email", "email");

                    int number = sqCommand.ExecuteNonQuery();

                    sqConnection.Dispose();
                }
                SelectClientsTable();
            }
            catch (Exception e)
            {
                MessageBox.Show($"{e.Message}");
            }
        }

        public void UpdateOrder(int ID, string[] OrderData)
        {
            try
            {
                using (odbConnection = new OleDbConnection(odbConSB.ConnectionString))
                {
                    odbConnection.Open();

                    odb = $@"update Orders set
                        email = '{OrderData[0]}',
                        code = '{OrderData[1]}',
                        title = '{OrderData[2]}'
                        where
                        id = {ID}";

                    odbCommand = new OleDbCommand(odb, odbConnection);

                    int number = odbCommand.ExecuteNonQuery();

                    odbConnection.Dispose();
                }
                SelectOrdersTable();
            }
            catch (Exception e)
            {
                MessageBox.Show($"{e.Message}");
            }
        }

        public void DeleteClient(int ClientID, DataRowView drv)
        {
            using (sqConnection = new SqlConnection(sqConSB.ConnectionString))
            {
                try
                {
                    sqConnection.Open();

                    sq = $"delete from Clients where id = {ClientID}";
                    
                    sqCommand = new SqlCommand(sq, sqConnection);
                    int number = sqCommand.ExecuteNonQuery();

                    if (drv != null)
                    {
                        drv.Row.Delete();

                        SystemSounds.Asterisk.Play();
                        MessageBox.Show($"Клиент {ClientID} удалён!");
                    }

                    sqConnection.Dispose();

                    SelectClientsTable();
                }
                catch (Exception e)
                {
                    MessageBox.Show($"{e.Message}");
                }
            }
        }

        public void DeleteOrder(int OrderID, DataRowView drv)
        {
            using (odbConnection = new OleDbConnection(odbConSB.ConnectionString))
            {
                try
                {
                    odbConnection.Open();

                    odb = $"delete from Orders where id = {OrderID}";
                    
                    odbCommand = new OleDbCommand(odb, odbConnection);
                    int number = odbCommand.ExecuteNonQuery();

                    if (drv != null)
                    {
                        drv.Row.Delete();

                        SystemSounds.Asterisk.Play();
                        MessageBox.Show($"Заказ {OrderID} удалён!");
                    }

                    odbCommand.Dispose();

                    SelectOrdersTable();
                }
                catch (Exception e)
                {
                    MessageBox.Show($"{e.Message}");
                }
            }
        }

        private void AddClient(Client client)
        {
            try
            {
                using (sqConnection = new SqlConnection(sqConSB.ConnectionString))
                {
                    sqConnection.Open();

                    sq = $@"insert into Clients (                   

                    firstName,
                    secondName,
                    patronymicName,
                    phone,
                    email )

                    values ( 

                    N'{client.GetFirstName()}',
                    N'{client.GetSecondName()}',
                    N'{client.GetPatronymicName()}',
                    N'{client.GetPhone()}',
                    N'{client.GetEMail()}' )";

                    sqCommand = new SqlCommand(sq, sqConnection);
                    sqCommand.ExecuteNonQuery();

                    sqConnection.Dispose();
                }
            }
            catch(Exception e)
            {
                MessageBox.Show(
                    $"Возникла ошибка:\n\n{e.Message}",
                    "Добавление клиента",
                    MessageBoxButton.OK);
            }
        }

        private void AddOrder(Order order)
        {
            try
            {
                using (odbConnection = new OleDbConnection(odbConSB.ConnectionString))
                {
                    odbConnection.Open();

                    odb = $@"insert into Orders (                   

                    email,
                    code,
                    title )

                    values ( 

                    '{order.GetEMail()}',
                    '{order.GetCode()}',
                    '{order.GetTitle()}' )";

                    odbCommand = new OleDbCommand(odb, odbConnection);
                    odbCommand.ExecuteNonQuery();

                    odbConnection.Dispose();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(
                    $"Возникла ошибка:\n\n{e.Message}",
                    "Добавление заказа",
                    MessageBoxButton.OK);
            }
        }
    }
}