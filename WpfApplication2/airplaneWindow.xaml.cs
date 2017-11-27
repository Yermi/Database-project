using System;
using System.Collections.Generic;
using System.Data.Common;
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
    /// Interaction logic for airplaneWindow.xaml
    /// </summary>
    public partial class airplaneWindow : Window
    {
        DbProviderFactory factory;
        DbConnection connection;

        string usersChoice = null;
        string strQuery = null;

        int ID;
        string produser;
        int yearOfProduction;
        int numOfSeats;

        public airplaneWindow(string str)
        {
            InitializeComponent();
            usersChoice = str;
        }

        private void airplane_Window_Loaded(object sender, RoutedEventArgs e)
        {
            Title = usersChoice + " airplane";
            airplansDoneButton.Content = usersChoice;

            if (usersChoice == "delete")
            {
                airplaneProduser.IsEnabled = false;
                airplaneYearOfProduction.IsEnabled = false;
                airplaneNumOfSeats.IsEnabled = false;
            }
        }

        private void airplaneDoneButton_Click(object sender, RoutedEventArgs e)
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
                    ID = Int32.Parse(airplaneid.Text);
                    produser = airplaneProduser.Text;
                    yearOfProduction = Int32.Parse(airplaneYearOfProduction.Text);
                    numOfSeats = Int32.Parse(airplaneNumOfSeats.Text);

                    // create insert functions to tabels credit_card and passanger
                    strQuery = "insert into SYSTEM.AIRPLANE(AIRPLANEID, PRODUSER, YEAR_OF_PRODUCTION,NUM_OF_SEATS) values (" + ID + ",'" + produser + "'," + yearOfProduction + "," + numOfSeats + ")";
                    command.CommandText = strQuery;
                    break;

                case "update":
                    ID = Int32.Parse(airplaneid.Text);
                    produser = airplaneProduser.Text.ToString();
                    yearOfProduction = Int32.Parse(airplaneYearOfProduction.Text);
                    numOfSeats = Int32.Parse(airplaneNumOfSeats.Text);

                    // create update functions to tabels credit_card and passanger
                    strQuery = "update SYSTEM.AIRPLANE SET PRODUSER = '" + produser + "', YEAR_OF_PRODUCTION = " + yearOfProduction + ", NUM_OF_SEATS = " + numOfSeats + " where AIRPLANEID = " + ID;
                    command.CommandText = strQuery;
                    break;

                case "delete":
                    ID = Int32.Parse(airplaneid.Text);

                    // create delete functions to tabels credit_card and passanger
                    strQuery = "delete from SYSTEM.AIRPLANE where AIRPLANEID = " + ID;
                    command.CommandText = strQuery;
                    break;
                
                default:
                    break;
            }

            try
            {
                int check = command.ExecuteNonQuery();
                if (check == 1)
                    MessageBox.Show("airplane " + usersChoice + "ed successfully! \n ");
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