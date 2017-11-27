using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for ClientsWindow.xaml
    /// </summary>
    public partial class ClientsWindow : Window
    {
        DbProviderFactory factory;
        DbConnection connection;

        string usersChoice = null;
        string strQuery = null;
        
        int ID;
        string firstName;
        string lastName;
        int age;
        long cardNumber;
        string vadility;
        int securityNumber;

        public ClientsWindow(string str)
        {
            InitializeComponent();
            usersChoice = str;
        }

        private void Client_Window_Loaded(object sender, RoutedEventArgs e)
        {
            Title = usersChoice + " client";
            clientsDoneButton.Content = usersChoice;

            if (usersChoice == "update")
            {
                clientCardNumber.IsEnabled = false;
                clientVadilityCardNumber.IsEnabled = false;
                clientSecurityNumberCardNumber.IsEnabled = false;
            }

            else if (usersChoice == "delete")
            {
                clientFirstName.IsEnabled = false;
                clientlastName.IsEnabled = false;
                clientAge.IsEnabled = false;
                clientCardNumber.IsEnabled = false;
                clientVadilityCardNumber.IsEnabled = false;
                clientSecurityNumberCardNumber.IsEnabled = false;
            }
        }

        private void clientsDoneButton_Click(object sender, RoutedEventArgs e)
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
                    ID = Int32.Parse(clientid.Text);
                    firstName = clientFirstName.Text;
                    lastName = clientlastName.Text;
                    age = Int32.Parse(clientAge.Text);
                    cardNumber = Int32.Parse(clientCardNumber.Text);
                    vadility = clientVadilityCardNumber.Text.ToString();
                    securityNumber = Int32.Parse(clientSecurityNumberCardNumber.Text);

                    // create insert functions to tacbels credit_card and passanger
                    strQuery = "insert into SYSTEM.CREDIT_CARD(CARD_NUMBER, VALIDITY, SECURITY_NUMBER) values (" + cardNumber + ",'" + vadility + "'," + securityNumber + ")";
                    command.CommandText = strQuery;

                    strQuery = "insert into SYSTEM.PASSANGER(PASSANGERID, FIRST_NAME, LAST_NAME, AGE, CARD_NUMBER) values ("
                         + ID + ",'" + firstName + "','" + lastName + "'," + age + "," + cardNumber + ")";
                    command.CommandText = strQuery;
                    break;

                case "update":
                    ID = Int32.Parse(clientid.Text);
                    firstName = clientFirstName.Text;
                    lastName = clientlastName.Text;
                    age = Int32.Parse(clientAge.Text);

                    // create update functions to tacbels credit_card and passanger
                    strQuery = "update SYSTEM.PASSANGER set FIRST_NAME = '" + firstName + "', LAST_NAME = '" + lastName + "',AGE = " + age + " where PASSANGERID = " + ID;
                    command.CommandText = strQuery;
                    break;

                case "delete":
                    ID = Int32.Parse(clientid.Text);

                    // create delete functions to tacbels credit_card and passanger
                    strQuery = "delete from SYSTEM.PASSANGER where PASSANGERID = " + ID;
                    command.CommandText = strQuery;
                    break;

                default:
                    break;
            }


            try
            {
                int check = command.ExecuteNonQuery();
                if (check == 1)
                    MessageBox.Show("client " + usersChoice + "ed successfully! \n ");
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