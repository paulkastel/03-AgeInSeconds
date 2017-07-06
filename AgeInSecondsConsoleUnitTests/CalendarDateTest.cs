using AgeInSecondsConsole;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgeInSecondsConsoleUnitTests
{
   [TestClass()]
   public class CalendarDateTestShould
   {

      [TestMethod()]
      public void IsDateCorrectIfStringOK1()
      {
         DateValidateIfIsCorrect("1800-12-5", false);
      }
      [TestMethod()]
      public void IsDateCorrectIfStringOK2()
      {
         DateValidateIfIsCorrect("3002-10-11", false);
      }
      [TestMethod()]
      public void IsDateCorrectIfStringOK3()
      {
         DateValidateIfIsCorrect("2086-26-01", false);
      }
      [TestMethod()]
      public void IsDateCorrectIfStringOK4()
      {
         DateValidateIfIsCorrect("2017-2-28", true);
      }
      [TestMethod()]
      public void IsDateCorrectIfStringOK5()
      {
         DateValidateIfIsCorrect("1990-4-29", true);
      }

      private void DateValidateIfIsCorrect(string strOKBadDate, bool isOK)
      {
         //test whether checks if is date is correct
         CalendarDate testObj = ConsoleHelper.ParseDate(strOKBadDate);
         Assert.AreEqual(isOK, testObj.IsDateIsValid());
      }

      [TestMethod()]
      public void CatchAnExceptionIfArrayTooSmall()
      {
         try
         {
            CalendarDate dc = new CalendarDate(new int[2] { -1, -1 });
         }
         catch (IndexOutOfRangeException ex)
         {
            Assert.AreEqual(ex.Message, "Size of array is insignificant!");
         }
      }
      [TestMethod()]
      public void ReturnDateInString()
      {
         CalendarDate cd = new CalendarDate(1, 1, 1);
         string txt = cd.CalculateTime(cd);
         Assert.AreEqual("", txt);

      }
   }
}
