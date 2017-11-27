using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.OracleClient;
using System.Data.SqlClient;
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
    /// Interaction logic for TicketWindow.xaml
    /// </summary>
    public partial class TicketWindow : Window
    {

        DbProviderFactory factory;
        DbConnection connection;

        string usersChoice = null;
        string strQuery = null;
        string dbConnectionStr = "Data Source=localhost;Persist Security Info=True;User ID=system;Password=0326;Unicode=True";
        Random rand = new Random();

        int id;
        int price = 0;
        string department_ = null;
        int passangerID;
        int flightID;

        public TicketWindow(string str)
        {
            InitializeComponent();
            usersChoice = str;
        }

        private void ticket_Window_Loaded(object sender, RoutedEventArgs e)
        {
            Title = usersChoice + " ticket";
            OracleConnection oraclecon = Singleton.getInstance();
            try
            {
                oraclecon.Open();
                string query = "select * from flight where TRUNC(take_off_date) > (select CURRENT_DATE from DUAL)";
                OracleCommand createCommand = new OracleCommand(query, oraclecon);
                OracleDataReader dr = createCommand.ExecuteReader();
                while (dr.Read())
                {
                    flight_id.Items.Add(dr["flightid"].ToString());
                }
                oraclecon.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("error: " + ex.ToString());
            }

            ticketsDoneButton.Content = usersChoice;
            if (usersChoice == "add")
            {
                createIdNumber();
            }
            if (usersChoice == "delete")
            {
                department.IsEnabled = false;
                passanger_id.IsEnabled = false;
                flight_id.IsEnabled = false;
            }
        }

        private void ticketDoneButton_Click(object sender, RoutedEventArgs e)
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
                    id = Int32.Parse(ticket_id.Text);
                    department_ = department.Text;
                    passangerID = Int32.Parse(passanger_id.Text);
                    flightID = Int32.Parse(flight_id.Text);

                    // create insert functions to tacbels credit_card and passanger
                    strQuery = "insert into SYSTEM.TICKET(TICKETID, PRICE, DEPARTMENT,PASSANGERID, FLIGHTID) values (" + id + "," + price + ",'" + department_ + "'," + passangerID + "," + flightID +")";

                    command.CommandText = strQuery;

                    break;

                case "update":
                    id = Int32.Parse(ticket_id.Text);
                    department_ = department.Text;
                    passangerID = Int32.Parse(passanger_id.Text);
                    flightID = Int32.Parse(flight_id.Text);

                    // create update functions to a row in ticket table
                    strQuery = "update SYSTEM.TICKET SET DEPARTMENT = '" + department_ + "', PASSANGERID = " + passangerID + ", FLIGHTID = " + flightID + " where TICKETID = " + id;

                    command.CommandText = strQuery;

                    break;

                case "delete":
                    id = Int32.Parse(ticket_id.Text);

                    // create delete query from tickets
                    strQuery = "delete from SYSTEM.TICKET where TICKETID = " + id;

                    command.CommandText = strQuery;

                    break;
                default:
                    break;
            }

            try
            {
                int check = command.ExecuteNonQuery();
                if (check == 1)
                    MessageBox.Show("ticket " + usersChoice + "ed successfully! \n ");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + ex.Message);
            }

            connection.Close();
        }

        private void createIdNumber()
        {
            OracleConnection oraclecon = new OracleConnection(dbConnectionStr);
            try
            {
                oraclecon.Open();
                string query = "select MAX(TICKETID) as maxid from TICKET ";
                OracleCommand createCommand = new OracleCommand(query, oraclecon);
                OracleDataReader dr = createCommand.ExecuteReader();

                while (dr.Read())
                {
                    int maxid = Int32.Parse(dr["maxid"].ToString()) + 1;
                    ticket_id.Text = maxid.ToString();
                }
                oraclecon.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("error: " + ex.ToString());
            }
        }

        private void flight_id_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            flightID = Int32.Parse(flight_id.SelectedItem.ToString());

            OracleConnection oraclecon = Singleton.getInstance();
            try
            {
                oraclecon.Open();
                string query = "select * from flight natural join flight_line where flightid = " + flightID;
                OracleCommand createCommand = new OracleCommand(query, oraclecon);
                OracleDataReader dr = createCommand.ExecuteReader();
                
                while (dr.Read())
                {
                    string s = dr["origin"].ToString();
                    string d = dr["destination"].ToString();
                    DateTime dt = DateTime.Parse(dr["take_off_date"].ToString());

                    source.Content = "source:            " + s;
                    destination.Content = "destination:    " + d;
                    takeOff.Content = "take off:          " + dt.ToShortDateString();

                }
                oraclecon.Close();
                price = rand.Next(550, 1200);
                priceLabel.Content = "price: " + price + " $";
                department.IsEnabled = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show("error: " + ex.ToString());
            }

        }

        private void department_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int basePrice = price;

            department_ = ((sender as ComboBox).SelectedItem as ComboBoxItem).Content as string; //department.Text;
            if (department_ == "Premium economy")
                basePrice = Convert.ToInt32(basePrice * 1.05);
            else if (department_ == "Business")
                basePrice = Convert.ToInt32(basePrice * 1.1);
            else if (department_ == "First class")
                basePrice = Convert.ToInt32(basePrice * 1.3);

            priceLabel.Content = "price: " + basePrice + " $";
        }

        private void whowClientName(object sender, RoutedEventArgs e)
        {
            passangerID = Int32.Parse(passanger_id.Text);

            OracleConnection oraclecon = new OracleConnection(dbConnectionStr);
            try
            {
                oraclecon.Open();
                string query = "select * from passanger where passangerid = " + passangerID;
                OracleCommand createCommand = new OracleCommand(query, oraclecon);
                OracleDataReader dr = createCommand.ExecuteReader();

                while (dr.Read())
                {
                    string f_n = dr["first_name"].ToString();
                    string l_n = dr["last_name"].ToString();

                    passangerName.Content = "Name of passanger:        " + f_n + " " + l_n;
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