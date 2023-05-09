using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Полёты
{
    public class DataBase
    {
        SqlConnection sc = new SqlConnection(@"Data Source=WIN-6QNUPO7UA3T\MSSQLSERVER01;Initial Catalog=aero;Integrated Security=True");

        public void OpenConnection()
        {
            if (sc.State == System.Data.ConnectionState.Closed)
            {
                sc.Open();
            }
        }
        public void CloseConnection()
        {
            if (sc.State == System.Data.ConnectionState.Open)
            {
                sc.Close();
            }
        }
        public SqlConnection GetConnection()
        {
            return sc;
        }
    }
}
