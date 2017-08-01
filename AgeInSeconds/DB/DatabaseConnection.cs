using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;

namespace AgeInSecondsWPF.DB
{
   /// <summary>
   /// Class responsible for operation on Database
   /// </summary>
   public class DatabaseConnection
   {
      public SqlConnection _connection { get; private set; }
      private SqlDataReader _dataReader;
      private SqlCommand _command;

      /// <summary>
      /// Constructor - on create do connection
      /// </summary>
      public DatabaseConnection()
      {
         bool canConnect = createConnection();
         if (!canConnect)
            throw new Exception();
      }

      /// <summary>
      /// Create connection with databse
      /// </summary>
      /// <returns>true if succceded</returns>
      public bool createConnection()
      {
         string dbPath = Path.Combine(Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())), "DB\\HistoricDB.mdf");
         string connetionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + dbPath + ";Integrated Security=True";

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

      /// <summary>
      /// Connect with table "Birthday" and gets all birthdays from specifed date
      /// </summary>
      /// <param name="A_date">Date when famous people born</param>
      /// <returns>list with famous people born</returns>
      public List<Object> getFamousBirthdayDates(CalendarDate A_date)
      {
         string sql = string.Format("SELECT WhoBorn FROM Birthday WHERE BirthdayDate = '{0}-{1}-{2}'", A_date._year, A_date._month, A_date._day);
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

      /// <summary>
      /// Connect with table "todayinhistory and gets all important events from post
      /// </summary>
      /// <param name="A_date">date when smth important happened</param>
      /// <returns>list with important history dates</returns>
      public List<Object> getImportantDates(CalendarDate A_date)
      {
         string sql = string.Format("SELECT WhatHappened FROM TodayInHistory WHERE HistoricalDate = '{0}-{1}-{2}'", A_date._year.ToString().PadLeft(4, '0'), A_date._month, A_date._day);
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
