using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApplication2
{
    /// <summary>
    /// Interaction logic for Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        DbProviderFactory factory;
        DbConnection connection;
        Random r = new Random();
        int rondom_n;
        public Window2()
        {
            InitializeComponent();
            oracle_password.Focus();
            confirm_password.Focus();
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            rondom_n = r.Next(35, 42);
            
            for (int i = 0; i < rondom_n; i++)
            {
                (sender as BackgroundWorker).ReportProgress(i);
                Thread.Sleep(30);
            }
            Thread.Sleep(500);

            for (int i = rondom_n+1; i <= 100; i++)
            {
                (sender as BackgroundWorker).ReportProgress(i);
                Thread.Sleep(12);
            }
        }

        void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progress.Value = e.ProgressPercentage;
            if (e.ProgressPercentage < rondom_n)
            {
                load_data_label.Content = "Cheking password... (" + e.ProgressPercentage + " %)";
            }
            else
            {
                load_data_label.Content = "Loading data...(" + e.ProgressPercentage + " %)";
            }
        }

        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Close();
            //enable the buttuns to get access to the date
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(MainWindow))
                {
                    (window as MainWindow).clients.IsEnabled = true;
                    (window as MainWindow).flights.IsEnabled = true;
                    (window as MainWindow).airplanes.IsEnabled = true;
                    (window as MainWindow).workers.IsEnabled = true;
                    (window as MainWindow).tickets.IsEnabled = true;
                    (window as MainWindow).flight_lines.IsEnabled = true;
                    (window as MainWindow).staffings.IsEnabled = true;
                    (window as MainWindow).setOraclePassword(oracle_password.Password.ToString());
                }
            }
        }

        private void confirm_password_Click(object sender, RoutedEventArgs e)
        {
            string or_Password = oracle_password.Password.ToString();
            factory = DbProviderFactories.GetFactory("System.Data.OracleClient");
            connection = factory.CreateConnection();

            
            //try to connect oracleDb with the password given by the user
            try
            {
                connection.ConnectionString = "Data Source=localhost;Persist Security Info=True;User ID=system;Password=" + or_Password + ";Unicode=True";
                connection.Open();
                connection.Close();
            }
            catch (Exception e2)
            {
                System.Windows.MessageBox.Show("ERROR: " + e2.Message + "\nhint: 0326", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                this.Close();               
            }

            BackgroundWorker worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.DoWork += worker_DoWork;
            worker.ProgressChanged += worker_ProgressChanged;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;

            worker.RunWorkerAsync();

            load_data_label.Visibility = System.Windows.Visibility.Visible;
            progress.Visibility = System.Windows.Visibility.Visible;
        }
    }
}