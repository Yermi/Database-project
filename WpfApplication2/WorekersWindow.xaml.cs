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
    /// Interaction logic for WorekersWindow.xaml
    /// </summary>
    public partial class WorekersWindow : Window
    {
        DbProviderFactory factory;
        DbConnection connection;

        string usersChoice = null;
        string strQuery = null;

        int ID;
        string firstName;
        string lastName;
        int age;
        string position;

        public WorekersWindow(string str)
        {
            InitializeComponent();
            usersChoice = str;
        }

        private void worker_Window_Loaded(object sender, RoutedEventArgs e)
        {
            Title = usersChoice + " worker";
            workersDoneButton.Content = usersChoice;

            if (usersChoice == "delete")
            {
                workerFirstName.IsEnabled = false;
                workerLastName.IsEnabled = false;
                workerAge.IsEnabled = false;
                workerPosition.IsEnabled = false;
            }
        }
      
        private void workerDoneButton_Click(object sender, RoutedEventArgs e)
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
                    ID = Int32.Parse(workerid.Text);
                    firstName = workerFirstName.Text;
                    lastName = workerLastName.Text;
                    age = Int32.Parse(workerAge.Text);
                    position = workerPosition.Text.ToString();

                    // create insert functions to tacbels credit_card and passanger
                    strQuery = "insert into SYSTEM.AIR_CREW(PILOTID, ROLE1, AGE,FIRST_NAME,LAST_NAME) values (" + ID + ",'" + position + "'," + age + ",'" + firstName + "','" + lastName + "')";
                    command.CommandText = strQuery;
                    break;

                case "update":
                    ID = Int32.Parse(workerid.Text);
                    firstName = workerFirstName.Text;
                    lastName = workerLastName.Text;
                    age = Int32.Parse(workerAge.Text);
                    position = workerPosition.Text.ToString();

                    // create update functions to tacbels credit_card and passanger
                    strQuery = "update SYSTEM.AIR_CREW SET ROLE1 = '" + position + "', AGE = " + age + ", FIRST_NAME = '" + firstName + "', LAST_NAME = '" + lastName + "' where PILOTID = " + ID;
                    command.CommandText = strQuery;
                    break;

                case "delete":
                    ID = Int32.Parse(workerid.Text);

                    // create delete functions to tacbels credit_card and passanger
                    strQuery = "delete from SYSTEM.AIR_CREW where PILOTID = " + ID;
                    command.CommandText = strQuery;
                    break;

                default:
                    break;
            }

            try
            {
                int check = command.ExecuteNonQuery();
                if (check == 1)
                    MessageBox.Show("worker " + usersChoice + "ed successfully! \n ");
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