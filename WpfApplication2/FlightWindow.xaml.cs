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
    /// Interaction logic for FlightWindow.xaml
    /// </summary>
    public partial class FlightWindow : Window
    {
        DbProviderFactory factory;
        DbConnection connection;

        string usersChoice = null;
        string strQuery = null;
        string dbConnectionStr = "Data Source=localhost;Persist Security Info=True;User ID=system;Password=0326;Unicode=True";
        Random rand = new Random();

        int id;
        string take_off;
        int airplaneId;
        int flightLineId;

        public FlightWindow(string str)
        {
            InitializeComponent();
            usersChoice = str;
        }

        private void Flight_Window_Loaded(object sender, RoutedEventArgs e)
        {
            Title = usersChoice + " flight";
            flightDoneButton.Content = usersChoice;

            OracleConnection oraclecon = Singleton.getInstance();
            try
            {
                oraclecon.Open();
                string query = "select * from flight_line";
                OracleCommand createCommand = new OracleCommand(query, oraclecon);
                OracleDataReader dr = createCommand.ExecuteReader();
                while (dr.Read())
                {
                    flight_line_id.Items.Add(dr["flight_line_id"].ToString());
                }
                query = "select * from airplane";
                createCommand = new OracleCommand(query, oraclecon);
                dr = createCommand.ExecuteReader();
                while (dr.Read())
                {
                    airplane_id.Items.Add(dr["airplaneid"].ToString());
                }
                oraclecon.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("error: " + ex.ToString());
            }

            flightDoneButton.Content = usersChoice;
            if (usersChoice == "add")
            {
                createIdNum();
            }

            else if (usersChoice == "delete")
            {
                flight_line_id.IsEnabled = false;
                takeOffDatePicker.IsEnabled = false;
                airplane_id.IsEnabled = false;
            }
        }

        void createIdNum() 
        {
            OracleConnection oraclecon = new OracleConnection(dbConnectionStr);
            try
            {
                oraclecon.Open();
                string query = "select MAX(FLIGHTID) as maxid from FLIGHT ";
                OracleCommand createCommand = new OracleCommand(query, oraclecon);
                OracleDataReader dr = createCommand.ExecuteReader();

                while (dr.Read())
                {
                    int maxid = Int32.Parse(dr["maxid"].ToString()) + 1;
                    flight_id.Text = maxid.ToString();
                }
                oraclecon.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("error: " + ex.ToString());
            }        
        }

        private void flightDoneButton_Click(object sender, RoutedEventArgs e)
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
                    id = Int32.Parse(flight_id.Text);
                    take_off = takeOffDatePicker.Text;
                    airplaneId = Int32.Parse(airplane_id.Text);
                    flightLineId = Int32.Parse(flight_line_id.Text);

                    // create insert functions to tacbels credit_card and passanger
                    strQuery = "insert into SYSTEM.FLIGHT(FLIGHTID, TAKE_OFF_DATE, AIRPLANEID, FLIGHT_LINE_ID) values (" + id + ",to_date('" + take_off + " " + rand.Next(10,23) + ":" + rand.Next(10,59) + ":" + rand.Next(10,59) + "', 'dd-mm-yyyy hh24:mi:ss')," + airplaneId + "," + flightLineId + ")";

                    command.CommandText = strQuery;

                    break;

                case "update":
                    id = Int32.Parse(flight_id.Text);
                    take_off = takeOffDatePicker.Text;
                    airplaneId = Int32.Parse(airplane_id.Text);
                    flightLineId = Int32.Parse(flight_line_id.Text);

                    // create update functions to a row in ticket table
                    strQuery = "update SYSTEM.FLIGHT SET TAKE_OFF_DATE = to_date('" + take_off + " " + rand.Next(10, 23) + ":" + rand.Next(10, 59) + ":" + rand.Next(10, 59) + "', 'dd-mm-yyyy hh24:mi:ss'), AIRPLANEID = " + airplaneId + ", FLIGHT_LINE_ID = " + flightLineId + " where FLIGHTID = " + id;

                    command.CommandText = strQuery;

                    break;

                case "delete":
                    id = Int32.Parse(flight_id.Text);

                    // create delete query from tickets
                    strQuery = "delete from SYSTEM.FLIGHT where FLIGHTID = " + id;

                    command.CommandText = strQuery;

                    break;
                default:
                    break;
            }

            try
            {
                int check = command.ExecuteNonQuery();
                if (check == 1)
                    MessageBox.Show("FLIGHT " + usersChoice + "ed successfully! \n ");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + ex.Message);
            }

            connection.Close();
        }

        private void flight_line_id_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            flightLineId = Int32.Parse(flight_line_id.SelectedItem.ToString());

            OracleConnection oraclecon = Singleton.getInstance();
            try
            {
                oraclecon.Open();
                string query = "select * from flight_line where flight_line_id = " + flightLineId;
                OracleCommand createCommand = new OracleCommand(query, oraclecon);
                OracleDataReader dr = createCommand.ExecuteReader();

                while (dr.Read())
                {
                    string s = dr["origin"].ToString();
                    string d = dr["destination"].ToString();

                    sourceLabel.Content = "source:           " + s;
                    destinationLabel.Content = "destination    " + d;
                    
                }
                oraclecon.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("error: " + ex.ToString());
            }
        }

        private void airplane_id_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            airplaneId = Int32.Parse(airplane_id.SelectedItem.ToString());

            OracleConnection oraclecon = Singleton.getInstance();
            try
            {
                oraclecon.Open();
                string query = "select * from airplane where airplaneid = " + airplaneId;
                OracleCommand createCommand = new OracleCommand(query, oraclecon);
                OracleDataReader dr = createCommand.ExecuteReader();

                while (dr.Read())
                {
                    string s = dr["produser"].ToString();
                    //string d = dr["destination"].ToString();
                    
                    produserLabel.Content = "source:           " + s;
                    //destinationLabel.Content = "destination:           " + d;

                }
                oraclecon.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("error: " + ex.ToString());
            }
        }
    }
}
