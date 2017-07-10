using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgeInSecondsWPF.DB
{
   public class DatabaseConnection
   {
      public SqlConnection _connection { get; private set; }
      private SqlDataReader _dataReader;
      private SqlCommand _command;

      public DatabaseConnection()
      {
         bool canConnect = createConnection();
         if (!canConnect)
            throw new Exception();
      }

      public bool createConnection()
      {
         string connetionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\pawel.kastelik\Desktop\xd\03-AgeInSeconds\AgeInSeconds\DB\HistoricDB.mdf;Integrated Security=True";
         
         try
         {
            _connection = new SqlConnection(connetionString);
            _connection.Open();
            _connection.Close();
            return true;
         }
         catch (Exception)
         {
            return false;
         }
      }

      public List<Object> getFamousBirthdayDates(CalendarDate date)
      {
         string sql = string.Format("SELECT WhoBorn FROM Birthday WHERE BirthdayDate = '{0}-{1}-{2}'", date._year, date._month, date._day);
         try
         {
            _connection.Open();

            _command = new SqlCommand(sql, _connection);
            _dataReader = _command.ExecuteReader();

            List<Object> result = new List<object>();
            while (_dataReader.Read())
            {
               var x = _dataReader.GetValue(0);
               result.Add(x);
            }

            _dataReader.Close();
            _command.Dispose();

            _connection.Close();
            return result;
         }
         catch (Exception ex)
         {
            throw ex;
         }
      }

      public List<Object> getImportantDates(CalendarDate date)
      {
         string sql = string.Format("SELECT Event FROM TodayInHistory WHERE HistoricalDate = '{0}-{1}-{2}'", date._year.ToString().PadLeft(4, '0'), date._month, date._day);
         try
         {
            _connection.Open();

            _command = new SqlCommand(sql, _connection);
            _dataReader = _command.ExecuteReader();
            List<Object> result = new List<object>();
            while (_dataReader.Read())
            {
               var x = _dataReader.GetValue(0);
               result.Add(x);
            }
            _dataReader.Close();
            _command.Dispose();

            _connection.Close();
            return result;
         }
         catch (Exception ex)
         {
            throw ex;
         }
      }
   }
}
