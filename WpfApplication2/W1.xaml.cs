using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Finisar.SQLite;

namespace WpfApplication2
{
    /// <summary>
    /// Логика взаимодействия для W1.xaml
    /// </summary>
    public partial class W1 : Window
    {
        public W1()
        {
            InitializeComponent();
        }

        private SQLiteDataAdapter DB;
        private DataSet DS = new DataSet();
        private DataTable DT = new DataTable();
        public IList<Log> logs = new List<Log>();

        private SQLiteCommand sqlite_cmd;
        private SQLiteConnection sqlite_conn;
        private SQLiteDataReader sqlite_datareader;

        private void SetConnection1() //Listener
        {
            var caa = new CAA("127.0.0.1", 6161);

          //  sqlite_conn = new SQLiteConnection("Data Source=EventDB.db; Version=3; New=True; Compress=True;");

           
         //   sqlite_conn.Open();
            sqlite_cmd = sqlite_conn.CreateCommand();

            sqlite_cmd.CommandText = "SELECT * FROM Events";
            var ds = new DataSet();

            using (var da = new SQLiteDataAdapter(sqlite_cmd.CommandText, sqlite_conn))
            {
                da.Fill(ds);
                Dispatcher.Invoke(() => DataGrid2.ItemsSource = ds.Tables[0].DefaultView);
            }

        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var th = new Thread(SetConnection1);
            th.Start();

            var DataGrid2 = sender as DataGrid;
        }
    }
}
