using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Coursework56
{
  class ConectDB
  {
    SqlConnection sqlConnection = new SqlConnection(@"Data Source=LAPTOP-GP9Q6VJG\SQLEXPRESS;Initial Catalog=UNIVERSITY;Integrated Security=True");
    public void openConnection()
    {
      if (sqlConnection.State == System.Data.ConnectionState.Closed)
      {
        sqlConnection.Open();
      }
    }

    public void closeConnection()
    {
      if (sqlConnection.State == System.Data.ConnectionState.Open)
      {
        sqlConnection.Close();
      }
    }

    public SqlConnection getConnection()
    {
      return sqlConnection;
    }
  }
}
