using System;
using System.Resources;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.OracleClient;
using System.Data;
using System.Windows.Forms;
using System.Collections;
using System.Data.Common;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using System.Windows.Controls.Primitives;
using System.IO;
using System.Diagnostics;
using System.Windows.Threading;
using System.Threading;
using System.ComponentModel;

namespace WpfApplication2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DbProviderFactory factory;
        DbConnection connection;
        string strTableName = null;
        string oraclePassword = null;
        Stopwatch stopWatch = new Stopwatch();
        BackgroundWorker worker = new BackgroundWorker();

        public MainWindow()
        {
            InitializeComponent();
            SetTimer(); // initilaze the clock
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en");

        }

        public void SetTimer()
        {
            DispatcherTimer timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {
                this.clock.Content = DateTime.Now.ToString("HH:mm:ss") + "  " + DateTime.Today.DayOfWeek + "  " + DateTime.Today.ToString("dd/MM/yyyy");
            }, this.Dispatcher);
        }

        internal void setOraclePassword(string oracle_pass)
        {
            oraclePassword = oracle_pass;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void showClients(object sender, RoutedEventArgs e)
        {
            factory = DbProviderFactories.GetFactory("System.Data.OracleClient");
            connection = factory.CreateConnection();

            try
            {
                connection.ConnectionString = "Data Source=localhost;Persist Security Info=True;User ID=system;Password=" + oraclePassword + ";Unicode=True";
                connection.Open();

                strTableName = "PASSANGER";
                showTable(strTableName);
            }
            catch (Exception e2)
            {
                System.Windows.MessageBox.Show("ERROR: " + e2.Message, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            connection.Close();
        }

        private void showflights(object sender, RoutedEventArgs e)
        {
            factory = DbProviderFactories.GetFactory("System.Data.OracleClient");
            connection = factory.CreateConnection();

            try
            {
                connection.ConnectionString = "Data Source=localhost;Persist Security Info=True;User ID=system;Password=" + oraclePassword + ";Unicode=True";
                connection.Open();

                strTableName = "FLIGHT";
                showTable(strTableName);
            }
            catch (Exception e2)
            {
                System.Windows.MessageBox.Show("ERROR: " + e2.Message, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            connection.Close();
        }

        private void showAirplanes(object sender, RoutedEventArgs e)
        {
            factory = DbProviderFactories.GetFactory("System.Data.OracleClient");
            connection = factory.CreateConnection();

            connection.ConnectionString = "Data Source=localhost;Persist Security Info=True;User ID=system;Password=" + oraclePassword + ";Unicode=True";
            connection.Open();

            strTableName = "AIRPLANE";
            showTable(strTableName);

            connection.Close();
        }

        private void showWorkers(object sender, RoutedEventArgs e)
        {
            factory = DbProviderFactories.GetFactory("System.Data.OracleClient");
            connection = factory.CreateConnection();

            connection.ConnectionString = "Data Source=localhost;Persist Security Info=True;User ID=system;Password=" + oraclePassword + ";Unicode=True";
            connection.Open();

            strTableName = "AIR_CREW";
            showTable(strTableName);

            connection.Close();
        }

        private void showTickets(object sender, RoutedEventArgs e)
        {
            factory = DbProviderFactories.GetFactory("System.Data.OracleClient");
            connection = factory.CreateConnection();

            connection.ConnectionString = "Data Source=localhost;Persist Security Info=True;User ID=system;Password=" + oraclePassword + ";Unicode=True";
            connection.Open();

            strTableName = "TICKET";
            showTable(strTableName);

            connection.Close();
        }

        private void showFlightLines(object sender, RoutedEventArgs e)
        {
            factory = DbProviderFactories.GetFactory("System.Data.OracleClient");
            connection = factory.CreateConnection();

            connection.ConnectionString = "Data Source=localhost;Persist Security Info=True;User ID=system;Password=" + oraclePassword + ";Unicode=True";
            connection.Open();

            strTableName = "FLIGHT_LINE";
            showTable(strTableName);

            connection.Close();
        }

        private void showstaffings(object sender, RoutedEventArgs e)
        {
            factory = DbProviderFactories.GetFactory("System.Data.OracleClient");
            connection = factory.CreateConnection();

            connection.ConnectionString = "Data Source=localhost;Persist Security Info=True;User ID=system;Password=" + oraclePassword + ";Unicode=True";
            connection.Open();

            strTableName = "STAFFING";
            showTable(strTableName);

            connection.Close();
        }

        private void showTable(string strTableName)
        {
            string strQuery = null;
            switch (strTableName)
            {
                case "PASSANGER":
                    strQuery = "Select * from PASSANGER";
                    break;
                case "FLIGHT":
                    strQuery = "Select * from flight_line natural join flight";
                    break;
                case "AIRPLANE":
                    strQuery = "Select * from AIRPLANE";
                    break;
                case "AIR_CREW":
                    strQuery = "Select * from AIR_CREW";
                    break;
                case "TICKET":
                    strQuery = "Select * from TICKET";
                    break;
                case "FLIGHT_LINE":
                    strQuery = "Select * from FLIGHT_LINE";
                    break;
                case "STAFFING":
                    strQuery = "Select * from air_crew natural join staffing natural join flight";
                    break;
                default:
                    break;
            }

            worker.WorkerReportsProgress = true;
            worker.DoWork += worker_DoWork;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;

            if (worker.CancellationPending)
            {
                worker.WorkerSupportsCancellation = true;
                worker.CancelAsync();
            }
            worker.RunWorkerAsync(strQuery); // execute the DoWork methods                    

            printButton.IsEnabled = true; // enable the 'print' button
            createPdfFile.IsEnabled = true; // enable to create a pdf file from the dataGrid
            createCsvFile.IsEnabled = true; // enable to create a csv file from the dataGrid
        }

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            // visible the spinner
            this.Dispatcher.Invoke(() =>
            {
                circle.Spin = true;
                circle.Visibility = Visibility.Visible;
                data_grid.IsEnabled = false;
                numOfRecords.Content = "Preparing tabele...";
            });

            stopWatch.Start(); // start run the stoper

            DbCommand command = factory.CreateCommand();
            command.CommandText = (string)e.Argument; //make the query 
            command.Connection = connection; //connect the command 

            DbDataAdapter adapter = factory.CreateDataAdapter();
            adapter.SelectCommand = command;
            DataTable dataTable = new DataTable();
            DataSet ds = new DataSet();
            adapter.Fill(ds, strTableName);
            dataTable = ds.Tables[0];

            // fill the data to the dataGrid, stop the spinner and enable the dataGrid
            this.Dispatcher.Invoke(() =>
            {
                data_grid.ItemsSource = dataTable.DefaultView;
                data_grid.IsEnabled = true;
                circle.Spin = false;
                circle.Visibility = Visibility.Hidden;
            });
            stopWatch.Stop(); // stop run the stoper 
            this.Dispatcher.Invoke(() => {
                numOfRecords.Content = data_grid.Items.Count + " records selected in " + (stopWatch.Elapsed.TotalMilliseconds / 1000).ToString("0.00") + " seconds"; // show the number of records
            });
            
            stopWatch.Reset();
        }
        
        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            AboutBox1 about_box = new AboutBox1();
            about_box.Show();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = System.Windows.MessageBox.Show("Do you want to close TWM Airline System?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                System.Windows.Application.Current.Shutdown();
            }
        }

        // print the table that is displyed in the dataGrid
        private void printData(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.PrintDialog Printdlg = new System.Windows.Controls.PrintDialog();
            if ((bool)Printdlg.ShowDialog().GetValueOrDefault())
            {
                Size pageSize = new Size(Printdlg.PrintableAreaWidth, Printdlg.PrintableAreaHeight);
                // sizing of the element.
                data_grid.Measure(pageSize);
                data_grid.Arrange(new Rect(5, 5, pageSize.Width, pageSize.Height));
                Printdlg.PrintVisual(data_grid, Title);
            }
        }

        // creat a new PDF file with the table that is displyed in the dataGrid
        private void exportToPdf(object sender, RoutedEventArgs e)
        {
            
            System.Windows.Controls.DataGrid grid = data_grid;
            PdfPTable table = new PdfPTable(grid.Columns.Count);
            Document doc = new Document(iTextSharp.text.PageSize.LETTER, 10, 10, 42, 35);
            string pdfFileName = strTableName + ".pdf";
            PdfWriter writer = PdfWriter.GetInstance(doc, new System.IO.FileStream(pdfFileName, System.IO.FileMode.Create));
            doc.Open();

            for (int j = 0; j < grid.Columns.Count; j++)
            {
                table.AddCell(new Phrase(grid.Columns[j].Header.ToString()));
            }
            table.HeaderRows = 1;
            IEnumerable itemsSource = grid.ItemsSource as IEnumerable;
            if (itemsSource != null)
            {
                foreach (var item in itemsSource)
                {
                    DataGridRow row = grid.ItemContainerGenerator.ContainerFromItem(item) as DataGridRow;
                    if (row != null)
                    {
                        DataGridCellsPresenter presenter = FindVisualChild<DataGridCellsPresenter>(row);
                        for (int i = 0; i < grid.Columns.Count; ++i)
                        {
                            System.Windows.Controls.DataGridCell cell = (System.Windows.Controls.DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(i);
                            TextBlock txt = cell.Content as TextBlock;
                            if (txt != null)
                            {
                                table.AddCell(new Phrase(txt.Text));
                            }
                        }
                    }
                }

                doc.Add(table);
                doc.Close();
            }
            Process.Start(pdfFileName);
            /*
            new pdfCreator("Contract");
            System.Diagnostics.Process.Start(@"Contract");
            e.Handled = true;
            */
        }
        private T FindVisualChild<T>(DependencyObject obj) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (child != null && child is T)
                    return (T)child;
                else
                {
                    T childOfChild = FindVisualChild<T>(child);
                    if (childOfChild != null)
                        return childOfChild;
                }
            }
            return null;
        }

        // creat a new Excell file with the table that is displyed in the dataGrid
        private void exportToExcell(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.DataGrid dg = data_grid;
            dg.SelectAllCells();
            dg.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;
            ApplicationCommands.Copy.Execute(null, dg);
            dg.UnselectAllCells();
            String Clipboardresult = (string)System.Windows.Clipboard.GetData(System.Windows.DataFormats.CommaSeparatedValue);

            string excellFileName = strTableName + ".csv";
            StreamWriter swObj = new StreamWriter(excellFileName);
            swObj.WriteLine(Clipboardresult);
            swObj.Close();
            Process.Start(excellFileName);
        }

        private void addToTable(object sender, RoutedEventArgs e)
        {
            switch (strTableName)
            {
                case "PASSANGER":
                    ClientsWindow clientWindow = new ClientsWindow("add");
                    clientWindow.Show();
                    break;
                case "AIRPLANE":
                    airplaneWindow airplaneWindow = new airplaneWindow("add");
                    airplaneWindow.Show();
                    break;
                case "TICKET":
                    TicketWindow ticketWindow = new TicketWindow("add");
                    ticketWindow.Show();
                    break;
                case "AIR_CREW":
                    WorekersWindow workerWindow = new WorekersWindow("add");
                    workerWindow.Show();
                    break;
                case "FLIGHT":
                    FlightWindow flightEindow = new FlightWindow("add");
                    flightEindow.Show();
                    break;
                case "FLIGHT_LINE":
                    FlightLineWindow flightLineWindow = new FlightLineWindow("add");
                    flightLineWindow.Show();
                    break;
                case "STAFFING":
                    StaffingsWindow staffingWindow = new StaffingsWindow("add");
                    staffingWindow.Show();
                    break;
                default:
                    break;
            }

        }

        private void updateOnTable(object sender, RoutedEventArgs e)
        {
            switch (strTableName)
            {
                case "PASSANGER":
                    ClientsWindow clientWindow = new ClientsWindow("update");
                    clientWindow.Show();
                    break;
                case "AIRPLANE":
                    airplaneWindow airplaneWindow = new airplaneWindow("update");
                    airplaneWindow.Show();
                    break;
                case "TICKET":
                    TicketWindow ticketWindow = new TicketWindow("update");
                    ticketWindow.Show();
                    break;
                case "AIR_CREW":
                    WorekersWindow workerWindow = new WorekersWindow("update");
                    workerWindow.Show();
                    break;
                case "FLIGHT":
                    FlightWindow flightEindow = new FlightWindow("update");
                    flightEindow.Show();
                    break;
                case "FLIGHT_LINE":
                    FlightLineWindow flightLineWindow = new FlightLineWindow("update");
                    flightLineWindow.Show();
                    break;
                case "STAFFING":
                    StaffingsWindow staffingWindow = new StaffingsWindow("update");
                    staffingWindow.Show();
                    break;
                default:
                    break;
            }
        }

        private void deleteFromTable(object sender, RoutedEventArgs e)
        {
            switch (strTableName)
            {
                case "PASSANGER":
                    ClientsWindow clientWindow = new ClientsWindow("delete");
                    clientWindow.Show();
                    break;
                case "AIRPLANE":
                    airplaneWindow airplaneWindow = new airplaneWindow("delete");
                    airplaneWindow.Show();
                    break;
                case "TICKET":
                    TicketWindow ticketWindow = new TicketWindow("delete");
                    ticketWindow.Show();
                    break;
                case "AIR_CREW":
                    WorekersWindow workerWindow = new WorekersWindow("delete");
                    workerWindow.Show();
                    break;
                case "FLIGHT":
                    FlightWindow flightEindow = new FlightWindow("delete");
                    flightEindow.Show();
                    break;
                case "FLIGHT_LINE":
                    FlightLineWindow flightLineWindow = new FlightLineWindow("delete");
                    flightLineWindow.Show();
                    break;
                case "STAFFING":
                    StaffingsWindow staffingWindow = new StaffingsWindow("delete");
                    staffingWindow.Show();
                    break;
                default:
                    break;
            }
        }

        private void flighst_by_year_click(object sender, RoutedEventArgs e)
        {
            Window1 functionsWindow = new Window1("flightByYear");
            functionsWindow.Show();            
        }

        private void change_dollar_click(object sender, RoutedEventArgs e)
        {
            Window1 functionsWindow = new Window1("ChangeDollar");
            functionsWindow.Show();          
        }

        private void ChangeLanguage(object sender, SelectionChangedEventArgs e)
        {
            // get the selected language
            ComboBoxItem typeItem = (ComboBoxItem)language.SelectedItem;
            string selectedLanguage = typeItem.Content.ToString();

            switch (selectedLanguage)
            {
                case "English":
                    Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en");
                    flight_lines.FontSize = 20;
                    break;
                case "עברית":
                    Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("he");
                    flight_lines.FontSize = 20;
                    break;
                case "Français":
                    Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("fr");
                    flight_lines.FontSize = 16;
                    break;
                default:
                    break;
            }

            // translate all strings to the selected language
            clients.Content = WpfApplication2.Properties.Resources.Clients;
            flights.Content = WpfApplication2.Properties.Resources.Flights;
            airplanes.Content = WpfApplication2.Properties.Resources.Airplanse;
            workers.Content = WpfApplication2.Properties.Resources.Workers;
            tickets.Content = WpfApplication2.Properties.Resources.Tickets;
            flight_lines.Content = WpfApplication2.Properties.Resources.Flight_lines;
            staffings.Content = WpfApplication2.Properties.Resources.Staffings;
            Title = WpfApplication2.Properties.Resources.Title;
        }

        private void Connect_Click(object sender, RoutedEventArgs e)
        {
            Window2 w = new Window2();
            w.Show();
        }
    } 
}