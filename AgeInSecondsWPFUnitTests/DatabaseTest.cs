using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AgeInSecondsWPF.DB;
using AgeInSecondsWPF;
using System.Data.SqlClient;

namespace AgeInSecondsWPFUnitTests
{
   /// <summary>
   /// Summary description for Database
   /// </summary>
   [TestClass]
   public class DatabaseShould
   {
      [TestMethod()]
      public void TestConnection()
      {
         DatabaseConnection testConn = new DatabaseConnection();

         Assert.IsTrue(testConn.createConnection());
      }

      [TestMethod()]
      public void CreateConnection()
      {
         try
         {
            DatabaseConnection testConn = new DatabaseConnection();
            testConn.createConnection();
         }
         catch (Exception)
         {
            Assert.Fail();
         }
      }

      [TestMethod()]
      public void DatabaseGetFameousBirthday()
      {
         DatabaseConnection testConn = new DatabaseConnection();
         CalendarDate cal = new CalendarDate(1867, 01, 01);
         string outputResult = string.Empty;
         List<Object> testRes;
         try
         {
            testRes = testConn.getFamousBirthdayDates(cal);
            foreach (var el in testRes)
            {
               Console.WriteLine(el.ToString());
               outputResult = el.ToString();
            }
         }
         catch (Exception ex)
         {
            Console.WriteLine(ex);
            Assert.Fail();
         }
         Assert.AreEqual("Lew Fields, comedian (of Weber & Fields)", outputResult);
      }

      [TestMethod()]
      public void DatabaseGetHistoryEvent()
      {
         DatabaseConnection testConn = new DatabaseConnection();
         CalendarDate cal = new CalendarDate(1892, 01, 01);
         string outputResult = string.Empty;
         List<Object> testRes;
         try
         {
            testRes = testConn.getImportantDates(cal);
            foreach (var el in testRes)
            {
               Console.WriteLine(el.ToString());
               outputResult = el.ToString();
            }
         }
         catch (Exception ex)
         {
            Console.WriteLine(ex);
            Assert.Fail();
         }
         Assert.AreEqual("Brooklyn merges with NY to form present City of NY - Ellis Island became reception center for new immigrants", outputResult);
      }

      [TestMethod()]
      public void DatabaseGetNothing()
      {
         DatabaseConnection testConn = new DatabaseConnection();
         CalendarDate cal = new CalendarDate(2100, 1, 1);
         string outputResult = string.Empty;
         List<Object> testRes;
         try
         {
            testRes = testConn.getImportantDates(cal);
            foreach (var el in testRes)
            {
               Console.WriteLine(el.ToString());
               outputResult = el.ToString();
            }
         }
         catch (Exception ex)
         {
            Console.WriteLine(ex);
            Assert.Fail();
         }
         Assert.AreEqual("", outputResult);
      }
   }
}
