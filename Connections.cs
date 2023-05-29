using Microsoft.Data.SqlClient;
using System;
using System.Data.OleDb;
using System.Data;
using System.Windows;
using System.Media;

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

        public void StartPreparing()
        {
            Preparing();
        }

        public void SelectClientsTable()
        {
            StartSelectClientsTable();
        }

        public void SelectOrdersTable()
        {
            StartSelectOrdersTable();
        }

        public void AddNewOrderAndClientInfo(Client client, Order order)
        {
            AddClient(client);
            AddOrder(order);
        }

        public void UpdateClientsTable()
        {
            StartUpdateClientsTable();
        }

        public void UpdateOrdersTable()
        {
            StartUpdateOrdersTable();
        }

        public void DeleteClient(DataRowView drv)
        {
            StartDeleteClient(drv);
        }

        public void DeleteOrder(DataRowView drv)
        {
            StartDeleteOrder(drv);
        }

        private void Preparing()
        {
            #region Строки подключения.

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

            #endregion

            #region Параметризация SQL Data Udapter и OLE DB Data Udapter.

            #region Insert.

            sq = "delete from Clients where id = @id";
            sqConnection = new SqlConnection(sqConSB.ConnectionString);
            sqCommand = new SqlCommand(sq, sqConnection);
            sqDataUpdater.InsertCommand = sqCommand;

            sqDataUpdater.InsertCommand = sqCommand;
            sqDataUpdater.InsertCommand.Parameters.Add("@id", SqlDbType.Int, 5, "id").Direction = ParameterDirection.Output;
            sqDataUpdater.InsertCommand.Parameters.Add("@firstName", SqlDbType.NVarChar, 25, "firstName");
            sqDataUpdater.InsertCommand.Parameters.Add("@secondName", SqlDbType.NVarChar, 25, "secondName");
            sqDataUpdater.InsertCommand.Parameters.Add("@patronymicName", SqlDbType.NVarChar, 25, "patronymicName");
            sqDataUpdater.InsertCommand.Parameters.Add("@phone", SqlDbType.NVarChar, 15, "phone");
            sqDataUpdater.InsertCommand.Parameters.Add("@email", SqlDbType.NVarChar, 25, "email");

            odb = "delete from Orders where id = @id";
            odbConnection = new OleDbConnection(odbConSB.ConnectionString);
            odbCommand = new OleDbCommand(odb, odbConnection);
            odbDataUpdater.InsertCommand = odbCommand;

            odbDataUpdater.InsertCommand = odbCommand;
            odbDataUpdater.InsertCommand.Parameters.Add("@id", OleDbType.Integer, 5, "id").Direction = ParameterDirection.Output;
            odbDataUpdater.InsertCommand.Parameters.Add("@email", OleDbType.VarWChar, 25, "email");
            odbDataUpdater.InsertCommand.Parameters.Add("@code", OleDbType.Integer, 25, "code");
            odbDataUpdater.InsertCommand.Parameters.Add("@title", OleDbType.VarWChar, 25, "title");

            #endregion

            #region Update.

            sq = "delete from Clients where id = @id";
            sqConnection = new SqlConnection(sqConSB.ConnectionString);
            sqCommand = new SqlCommand(sq, sqConnection);
            sqDataUpdater.UpdateCommand = sqCommand;

            sqDataUpdater.UpdateCommand = sqCommand;
            sqDataUpdater.UpdateCommand.Parameters.Add("@id", SqlDbType.Int, 5, "id").Direction = ParameterDirection.Output;
            sqDataUpdater.UpdateCommand.Parameters.Add("@firstName", SqlDbType.NVarChar, 25, "firstName");
            sqDataUpdater.UpdateCommand.Parameters.Add("@secondName", SqlDbType.NVarChar, 25, "secondName");
            sqDataUpdater.UpdateCommand.Parameters.Add("@patronymicName", SqlDbType.NVarChar, 25, "patronymicName");
            sqDataUpdater.UpdateCommand.Parameters.Add("@phone", SqlDbType.NVarChar, 15, "phone");
            sqDataUpdater.UpdateCommand.Parameters.Add("@email", SqlDbType.NVarChar, 25, "email");

            odb = "delete from Orders where id = @id";
            odbConnection = new OleDbConnection(odbConSB.ConnectionString);
            odbCommand = new OleDbCommand(odb, odbConnection);
            odbDataUpdater.UpdateCommand = odbCommand;

            odbDataUpdater.UpdateCommand = odbCommand;
            odbDataUpdater.UpdateCommand.Parameters.Add("@id", OleDbType.Integer, 5, "id").Direction = ParameterDirection.Output;
            odbDataUpdater.UpdateCommand.Parameters.Add("@email", OleDbType.VarWChar, 25, "email");
            odbDataUpdater.UpdateCommand.Parameters.Add("@code", OleDbType.Integer, 10, "code");
            odbDataUpdater.UpdateCommand.Parameters.Add("@title", OleDbType.VarWChar, 20, "@title");

            #endregion

            #region Delete.

            sq = "delete from Clients where id = @id";
            sqConnection = new SqlConnection(sqConSB.ConnectionString);
            sqCommand = new SqlCommand(sq, sqConnection);

            sqDataUpdater.DeleteCommand = sqCommand;
            sqDataUpdater.DeleteCommand.Parameters.Add("@id", SqlDbType.Int, 5, "id");
            sqDataUpdater.DeleteCommand.Parameters.Add("@firstName", SqlDbType.NVarChar, 25, "firstName");
            sqDataUpdater.DeleteCommand.Parameters.Add("@secondName", SqlDbType.NVarChar, 25, "secondName");
            sqDataUpdater.DeleteCommand.Parameters.Add("@patronymicName", SqlDbType.NVarChar, 25, "patronymicName");
            sqDataUpdater.DeleteCommand.Parameters.Add("@phone", SqlDbType.NVarChar, 15, "phone");
            sqDataUpdater.DeleteCommand.Parameters.Add("@email", SqlDbType.NVarChar, 25, "email");

            odb = "delete from Orders where id = @id";
            odbConnection = new OleDbConnection(odbConSB.ConnectionString);
            odbCommand = new OleDbCommand(odb, odbConnection);

            odbDataUpdater.DeleteCommand = odbCommand;
            odbDataUpdater.DeleteCommand.Parameters.Add("@id", OleDbType.Integer, 5, "id");
            odbDataUpdater.DeleteCommand.Parameters.Add("@email", OleDbType.VarWChar, 25, "email");
            odbDataUpdater.DeleteCommand.Parameters.Add("@code", OleDbType.Integer, 10, "code");
            odbDataUpdater.DeleteCommand.Parameters.Add("@title", OleDbType.VarWChar, 20, "title");

            #endregion

            #endregion
        }

        private void StartSelectClientsTable()
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


        private void StartSelectOrdersTable()
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
                    $"Возникла ошибка:\n{e.Message}",
                    "Добавление заказа",
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

                SystemSounds.Exclamation.Play();

                MessageBox.Show(
                    "Заказ успешно добавлен!",
                    "Добавление заказа",
                    MessageBoxButton.OK);
            }

            catch (Exception e)
            {
                MessageBox.Show(
                    $"Возникла ошибка:\n{e.Message}",
                    "Добавление заказа",
                    MessageBoxButton.OK);
            }
        }

        private void StartUpdateClientsTable()
        {
            try
            {
                using (sqConnection = new SqlConnection(sqConSB.ConnectionString))
                {
                    sqConnection.Open();

                    sq = @"update Clients set
                        firstName = @firstName,
                        secondName = @secondName,
                        patronymicName = @patronymicName,
                        phone = @phone,
                        email = @email
                        where
                        id = @id";

                    sqCommand = new SqlCommand(sq, sqConnection);
                    sqDataUpdater.Update(clientsTable);

                    //sqDataUpdater.Fill(clientsTable);
                    //sqCommand.ExecuteNonQuery();

                    sqConnection.Dispose();
                }
            }
            catch(Exception e)
            {
                MessageBox.Show(
                    $"{e.Message}");
            }
        }

        private void StartUpdateOrdersTable()
        {
            try
            {
                using (odbConnection = new OleDbConnection(odbConSB.ConnectionString))
                {
                    odbConnection.Open();

                    odb = @"update Orders set
                    email = @email,
                    code = @code,
                    title = @title
                    where id = @id";

                    odbCommand = new OleDbCommand(odb, odbConnection);
                    odbDataUpdater.UpdateCommand = odbCommand;

                    //odbDataUpdater.Fill(ordersTable);
                    //odbCommand.ExecuteNonQuery();

                    odbDataUpdater.Update(ordersTable);

                    odbConnection.Dispose();
                }
            }
            catch(Exception e)
            {
                MessageBox.Show(
                    $"{e.Message}");
            }
        }

        private void StartDeleteClient(DataRowView drv)
        {
            using(sqConnection = new SqlConnection(sqConSB.ConnectionString))
            {
                try
                {
                    sqConnection.Open();

                    sq = "delete from Clients where id = @id";
                    sqCommand = new SqlCommand(sq, sqConnection);
                    sqDataUpdater.DeleteCommand = sqCommand;

                    if (drv != null)
                    {
                        drv.Row.Delete();
                    }

                    //sqCommand.ExecuteNonQuery();
                    //sqDataUpdater.Update(clientsTable);

                    sqConnection.Dispose();
                }

                catch(Exception e)
                {
                    MessageBox.Show($"{e.Message}");
                }
            }
        }

        private void StartDeleteOrder(DataRowView drv)
        {
            using(odbConnection = new OleDbConnection(odbConSB.ConnectionString))
            {
                try
                {
                    odbConnection.Open();

                    odb = "delete from Orders where id = @id";
                    odbCommand = new OleDbCommand(odb, odbConnection);
                    odbDataUpdater.DeleteCommand = odbCommand;

                    if (drv != null)
                    {
                        drv.Row.Delete();
                    }

                    //odbCommand.ExecuteNonQuery();
                    //odbDataUpdater.Update(ordersTable);

                    odbConnection.Dispose();
                }

                catch (Exception e)
                {
                    MessageBox.Show($"{e.Message}");
                }
            }
        }
    }
}