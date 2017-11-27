using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        string function = null;
        public Window1(string str)
        {
            InitializeComponent();
            function = str;
        }

        private void load_page(object sender, RoutedEventArgs e)
        {
            if (function == "flightByYear")
            {
                Title = "get number of flights by years";
                enterLabel.Content = "enter a year (from 1980):";
            }

            else
            {
                Title = "Change Dollar to another coin";
                enterLabel.Content = "enter a value to \nchange the dollar:";
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            switch (function)
            {
                case "flightByYear":

                    string str = "TWM_airline_managment.FilghtsByYear";

                    OracleConnection oraclecon =  Singleton.getInstance();
                    OracleCommand cmd = new OracleCommand(str, oraclecon);
                    cmd.CommandType = CommandType.StoredProcedure;

                    int year = Int32.Parse(yearTextBox.Text);
                    string res = "No Data";

                    cmd.Parameters.Add("year_", year);
                    cmd.Parameters.Add("result", OracleType.Int32);
                    cmd.Parameters["result"].Direction = ParameterDirection.ReturnValue;

                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();

                    var result = Convert.ToString(cmd.Parameters["result"].Value);
                    result = result.Equals("") ? res : result;
                    cmd.Connection.Close();

                    answerLabel.Content = "number of flights in " + year + ": " + result;

                    break;
                case "ChangeDollar":

                    str = "TWM_airline_managment.ChangeDollar";

                    oraclecon = Singleton.getInstance();
                    cmd = new OracleCommand(str, oraclecon);
                    cmd.CommandType = CommandType.StoredProcedure;

                    double value = Convert.ToDouble(yearTextBox.Text);
                    res = "No Data";

                    cmd.Parameters.Add("valueOfCoin", value);
                    cmd.Parameters.Add("result", OracleType.Number);
                    cmd.Parameters["result"].Direction = ParameterDirection.ReturnValue;

                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();

                    result = Convert.ToString(cmd.Parameters["result"].Value);
                    result = result.Equals("") ? res : result;
                    cmd.Connection.Close();

                    answerLabel.Content = "the sum of tickets price calculate" + "\nwith value " + value + " is: " + result;

                    break;
                default:
                    break;
            }
        }
    }
}