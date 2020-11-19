using System;
using System.Collections.Generic;
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
using System.Data;
using System.Data.SQLite;
using System.Linq;

namespace WpfApplication2
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>




    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }


        private SQLiteDataAdapter DB;
        private DataSet ds = new DataSet();
        private DataTable DT = new DataTable();

        private SQLiteCommand sqlite_cmd;
        //private SQLiteConnection sqlite_conn;
        private SQLiteDataReader sqlite_datareader;



        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
 var DataGrid2 = sender as DataGrid;
           

const string databaseName = @"EventsDB.db";
            SQLiteConnection connection =
                new SQLiteConnection(string.Format("Data Source={0};", databaseName));
            connection.Open();

            SQLiteCommand command2 = new SQLiteCommand("SELECT * FROM 'Events';", connection);

            var ds = new DataSet();

            using (var da = new SQLiteDataAdapter(command2))
            {
                da.Fill(ds);
                DataGrid2.ItemsSource = ds.Tables[0].DefaultView;
            }


           // TextBlock.Text += "yes bitches";
            //Assign ItemsSource of DataGrid.
           
            // DataGrid2.ItemsSource = resultDetails;
        }

        private void DataGrid2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
