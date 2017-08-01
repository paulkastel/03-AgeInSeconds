using Microsoft.VisualStudio.TestTools.UnitTesting;
using AgeInSecondsConsole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgeInSecondsConsole.Tests
{
   /// <summary>
   /// Test of ConsoleHelper
   /// </summary>
   [TestClass()]
   public class ConsoleHelperShould
   {
      [TestMethod()]
      public void SetTo1IfStringisBad1()
      {
         DateParseAsBad("20-12,2016");
      }
      [TestMethod()]
      public void SetTo1IfStringisBad2()
      {
         DateParseAsBad("20.12.2016");
      }
      [TestMethod()]
      public void SetTo1IfStringisBad3()
      {
         DateParseAsBad("aaaa-bc-ds");
      }
      [TestMethod()]
      public void SetTo1IfStringisBad4()
      {
         DateParseAsBad("32");
      }
      [TestMethod()]
      public void SetTo1IfStringisBad5()
      {
         DateParseAsBad("32-bc");
      }

      private void DateParseAsBad(string badstring)
      {
         //if string is bad always would return object like this
         CalendarDate testObj = ConsoleHelper.ParseDate(badstring);
         CalendarDate expectedcalDate = new CalendarDate(-1, -1, -1);
         Assert.AreEqual(expectedcalDate, testObj);
      }

      [TestMethod()]
      public void DateParseNormal()
      {
         //objects should be the same
         CalendarDate testObj = ConsoleHelper.ParseDate("2016-09-21");
         CalendarDate expectedcalDate = new CalendarDate(2016, 9, 21);
         Assert.AreEqual(expectedcalDate, testObj);
      }

      [TestMethod()]
      public void DateParseIfNullConstructor11()
      {
         //construtor one
         CalendarDate testObj = ConsoleHelper.ParseDate("");
         CalendarDate expectedcalDate = new CalendarDate(-1, -1, -1);
         Assert.AreEqual(expectedcalDate, testObj);
      }

      [TestMethod()]
      public void DateParseIfNullConstructor21()
      {
         //construtor two
         CalendarDate testObj = ConsoleHelper.ParseDate("");
         int[] wrongArrayDate = new int[3] { -1, -1, -1 };
         CalendarDate expectedcalDate = new CalendarDate(wrongArrayDate);
         Assert.AreEqual(expectedcalDate, testObj);
      }
   }
}
