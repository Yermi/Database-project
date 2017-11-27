using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication2
{
    class Singleton
    {
        private static OracleConnection connection = null;
        public static OracleConnection getInstance()
        {
            if (connection == null)
            {
                connection = new OracleConnection("Data Source=localhost;Persist Security Info=True;User ID=system;Password=0326;Unicode=True");
            }
            return connection;
        }
    }
}
