using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.OracleClient;
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
using System.Windows.Shapes;

namespace WpfApplication2
{
    /// <summary>
    /// Interaction logic for FlightLineWindow.xaml
    /// </summary>
    public partial class FlightLineWindow : Window
    {
        DbProviderFactory factory;
        DbConnection connection;

        string usersChoice = null;
        string strQuery = null;
        string dbConnectionStr = "Data Source=localhost;Persist Security Info=True;User ID=system;Password=0326;Unicode=True";

        int id;
        string source;
        string destinatin;
        int totalKM;

        public FlightLineWindow(string str)
        {
            InitializeComponent();
            usersChoice = str;
        }

        private void flight_line_Window_Loaded(object sender, RoutedEventArgs e)
        {
            Title = usersChoice + " flight line";
            flightLinesDoneButton.Content = usersChoice;

            OracleConnection oraclecon = Singleton.getInstance();
            try
            {
                oraclecon.Open();
                string query = "select distinct origin from flight_line union select distinct destination from flight_line";
                OracleCommand createCommand = new OracleCommand(query, oraclecon);
                OracleDataReader dr = createCommand.ExecuteReader();
                while (dr.Read())
                {
                    sourceCMB.Items.Add(dr["origin"].ToString());
                    destinationCMB.Items.Add(dr["origin"].ToString());

                }
                oraclecon.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("error: " + ex.ToString());
            }

            flightLinesDoneButton.Content = usersChoice;

            if (usersChoice == "delete")
            {
                sourceCMB.IsEnabled = false;
                destinationCMB.IsEnabled = false;
                totalKM_TXB.IsEnabled = false;
            }
        }

        private void source_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void destination_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void flightLinesDoneButton_Click(object sender, RoutedEventArgs e)
        {
            factory = DbProviderFactories.GetFactory("System.Data.OracleClient");
            connection = factory.CreateConnection();

            connection.ConnectionString = "Data Source=localhost;Persist Security Info=True;User ID=system;Password=0326;Unicode=True";
            connection.Open();

            DbCommand command = factory.CreateCommand();
            command.CommandText = strQuery; //make the query 
            command.Connection = connection; //connect the command 

            switch (usersChoice)
            {
                case "add":
                    id = Int32.Parse(flightLineId.Text);
                    source = sourceCMB.Text;
                    destinatin = destinationCMB.Text;
                    totalKM = Int32.Parse(totalKM_TXB.Text);
                    
                    // create insert functions to tacbels credit_card and passanger
                    strQuery = "insert into SYSTEM.FLIGHT_LINE(FLIGHT_LINE_ID, ORIGIN, DESTINATION,TOTAL_KM) values (" + id + ",'" + source + "','" + destinatin + "'," + totalKM + ")";
                    command.CommandText = strQuery;
                    break;

                case "update":
                    id = Int32.Parse(flightLineId.Text);
                    source = sourceCMB.Text;
                    destinatin = destinationCMB.Text;
                    totalKM = Int32.Parse(totalKM_TXB.Text);

                    // create update functions to tacbels credit_card and passanger
                    strQuery = "update SYSTEM.FLIGHT_LINE SET ORIGIN = '" + source + "', DESTINATION = '" + destinatin + "', TOTAL_KM = " + totalKM + " where FLIGHT_LINE_ID = " + id;
                    command.CommandText = strQuery;
                    break;

                case "delete":
                    id = Int32.Parse(flightLineId.Text);

                    // create delete functions to tacbels credit_card and passanger
                    strQuery = "delete from SYSTEM.FLIGHT_LINE where FLIGHT_LINE_ID = " + id;
                    command.CommandText = strQuery;
                    break;
                default:
                    break;
            }

            try
            {
                int check = command.ExecuteNonQuery();
                if (check == 1)
                    MessageBox.Show("flight line " + usersChoice + "ed successfully! \n ");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + ex.Message);
            }
            connection.Close();
        }
    }
}