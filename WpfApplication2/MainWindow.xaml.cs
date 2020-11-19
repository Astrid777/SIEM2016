using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

//using Finisar.SQLite;

namespace WpfApplication2
{
    /// <summary>
    ///     Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SQLiteDataAdapter DB;
        private DataSet DS = new DataSet();
        private DataTable DT = new DataTable();

        public IList<Log> logs = new List<Log>();

        private SQLiteCommand sqlite_cmd;
        //private SQLiteConnection sqlite_conn;
        private SQLiteDataReader sqlite_datareader;

        public MainWindow()
        {
            InitializeComponent();
        }


        private void SetConnection() //Listener
        {
            //создали БД
            var databaseName = @"Eve.db";
            SQLiteConnection.CreateFile(databaseName);

            var connection =
                new SQLiteConnection(string.Format("Data Source={0};", databaseName));
            connection.Open();


            var command =
                new SQLiteCommand(
                    "CREATE TABLE Events (System TEXT, User TEXT, GUID INTEGER, EventID integer, domain TEXT, date TEXT, other TEXT) ;",
                    connection);

            command.ExecuteNonQuery();


            var caa = new CAA("127.0.0.1", 6161);
            while (true)
            {
                var db = new Database();

                while (caa.MessageQueue.Any())
                {
                    var data = caa.MessageQueue.Dequeue();
                    var parr = new Parser();
                    var log = parr.Log_Parse(data);
                    logs.Add(log);

                    var command1 = new SQLiteCommand(
                        "INSERT INTO 'Events' ('System', 'User', 'GUID', 'EventID', 'domain', 'date', 'other') VALUES ('" +
                        log.System + "', '" + log.User + "','" + log.GuId + "','" + log.EventID + "','" + log.Domain +
                        "','" + log.date + "','" + log.other + "');", connection);
                    command1.ExecuteNonQuery();


                    var command2 = new SQLiteCommand("SELECT * FROM 'Events';", connection);

                    var ds = new DataSet();

                    using (var da = new SQLiteDataAdapter(command2))
                    {
                        da.Fill(ds);
                        Dispatcher.Invoke(() => DataGrid1.ItemsSource = ds.Tables[0].DefaultView);
                    }

SQLiteCommand command3 = new SQLiteCommand("SELECT * FROM 'Events' WHERE EventID=4689;", connection);
                    SQLiteDataReader reader = command3.ExecuteReader();

                    if (reader.Read())
                    {

                    string System = reader["System"].ToString();
                    string User = reader["User"].ToString();
                    string GUID = reader["GUID"].ToString();
                    string EventID = reader["EventID"].ToString();
                    string domain = reader["domain"].ToString();
                    string date = reader["date"].ToString();
                    string other = reader["other"].ToString();
Dispatcher.Invoke(() => TextBlock.Text +=System + " "+ User + " " + GUID + " " + EventID + " " + domain + " " + date + " " + other + "\n" );
                      
                    }




                }
                    
                Thread.Sleep(100);
                // connection.Close();
            }
        }

 



        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var th = new Thread(SetConnection);
            th.Start();
 //var DataGrid1 = sender as DataGrid;
 //           DataGrid1.ItemsSource = logs;

            
        }
    }
}