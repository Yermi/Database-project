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
    /// Interaction logic for StaffingsWindow.xaml
    /// </summary>
    public partial class StaffingsWindow : Window
    {
        DbProviderFactory factory;
        DbConnection connection;

        string strQuery = null;
        string dbConnectionStr = "Data Source=localhost;Persist Security Info=True;User ID=system;Password=0326;Unicode=True";
        string userschoice = null;

        int workerID;
        int flightID;

        public StaffingsWindow(string str)
        {
            InitializeComponent();
            userschoice = str;
        }

        private void load_page(object sender, RoutedEventArgs e)
        {
            Title = userschoice + " staffing";
            buttonDoneClick.Content = userschoice;

            // fil the combo box 
            OracleConnection oraclecon = Singleton.getInstance();
            try
            {
                oraclecon.Open();
                string query = "select * from air_crew";
                OracleCommand createCommand = new OracleCommand(query, oraclecon);
                OracleDataReader dr = createCommand.ExecuteReader();
                while (dr.Read())
                {
                    airCrewManComboBox.Items.Add(dr["pilotid"].ToString());
                    
                }
                query = "select * from flight where TRUNC(take_off_date) > (select CURRENT_DATE from DUAL)";
                createCommand = new OracleCommand(query, oraclecon);
                dr = createCommand.ExecuteReader();
                while (dr.Read())
                {
                    flightIDComboBox.Items.Add(dr["flightid"].ToString());
                }
                oraclecon.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("error: " + ex.ToString());
            }
        
        }

        private void button_Done_Click(object sender, RoutedEventArgs e)
        {
            factory = DbProviderFactories.GetFactory("System.Data.OracleClient");
            connection = factory.CreateConnection();

            connection.ConnectionString = "Data Source=localhost;Persist Security Info=True;User ID=system;Password=0326;Unicode=True";
            connection.Open();

            DbCommand command = factory.CreateCommand();
            command.CommandText = strQuery; //make the query 
            command.Connection = connection; //connect the command 

            switch (userschoice)
            {
                case "add":
                    workerID = Int32.Parse(airCrewManComboBox.SelectedItem.ToString());
                    flightID = Int32.Parse(flightIDComboBox.SelectedItem.ToString());

                    // create insert functions to tacbels credit_card and passanger
                    strQuery = "insert into SYSTEM.STAFFING(FLIGHTID, PILOTID) values (" + flightID + "," + workerID + ")";

                    command.CommandText = strQuery;

                    break;

                case "update":
                    workerID = Int32.Parse(airCrewManComboBox.SelectedItem.ToString());
                    flightID = Int32.Parse(flightIDComboBox.SelectedItem.ToString());

                    // create update functions to a row in ticket table
                    //strQuery = "update SYSTEM.STAFFING SET DEPARTMENT = '" + department_ + "', PASSANGERID = " + passangerID + ", FLIGHTID = " + flightID + " where TICKETID = " + id;

                    command.CommandText = strQuery;

                    break;

                case "delete":
                    workerID = Int32.Parse(airCrewManComboBox.SelectedItem.ToString());
                    flightID = Int32.Parse(flightIDComboBox.SelectedItem.ToString());

                    // create delete query from tickets
                    strQuery = "delete from SYSTEM.STAFFING where FLIGHTID = " + flightID + " and PILOTID = " + workerID;

                    command.CommandText = strQuery;

                    break;
                default:
                    break;
            }

            try
            {
                int check = command.ExecuteNonQuery();
                if (check == 1)
                    MessageBox.Show("staffing " + userschoice + "ed successfully! \n ");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + ex.Message);
            }

            connection.Close();

        }

        private void airCrewManComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            workerID = Int32.Parse(airCrewManComboBox.SelectedItem.ToString());

            OracleConnection oraclecon = Singleton.getInstance();
            try
            {
                oraclecon.Open();
                string query = "select * from air_crew where pilotid = " + workerID;
                OracleCommand createCommand = new OracleCommand(query, oraclecon);
                OracleDataReader dr = createCommand.ExecuteReader();

                while (dr.Read())
                {
                    string fn = dr["first_name"].ToString();
                    string ln = dr["last_name"].ToString();

                    nameLabel.Content = "name: " + fn + " " + ln;                  
                }
                oraclecon.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("error: " + ex.ToString());
            }
        }

        private void flightIDComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            flightID = Int32.Parse(flightIDComboBox.SelectedItem.ToString());

            OracleConnection oraclecon = Singleton.getInstance();
            try
            {
                oraclecon.Open();
                string query = "select * from flight natural join flight_line where FLIGHTID = " + flightID;
                OracleCommand createCommand = new OracleCommand(query, oraclecon);
                OracleDataReader dr = createCommand.ExecuteReader();

                while (dr.Read())
                {
                    string s = dr["origin"].ToString();
                    string d = dr["destination"].ToString();
                    DateTime dt = DateTime.Parse(dr["take_off_date"].ToString());

                    sourceLabel.Content = "source: " + s;
                    destinationLabel.Content = "destination: " + d;
                    takeOffLabel.Content = "take off: " + dt;
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
