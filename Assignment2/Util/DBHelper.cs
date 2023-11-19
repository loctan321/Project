using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2
{
    class DBHelper
    {
        public static SqlConnection GetConnection()
        {
            SqlConnection cn = new SqlConnection(@"server=localhost; User ID=sa; Pwd=12345; database=QLSVien");
            return cn;
        }
    }
}
