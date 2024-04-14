using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Coursework56
{
  public static class DataHandler
  {
    public static string ExecuteProc(string querry)
    {
      string result;
      using (SqlConnection connection = (new ConectDB()).getConnection())
      {
        connection.Open();
        SqlCommand command1 = new SqlCommand(querry, connection);

        try
        {
          command1.ExecuteNonQuery();
          result = "Все прошло успешно!";
        }
        catch (Exception exc)
        {
          result = exc.Message;
        }
        return result;
      }
    }
  }
}
