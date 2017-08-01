using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgeInSecondsConsole
{
   /// <summary>
   /// Class to validate entered data in console
   /// </summary>
   public class ConsoleHelper
   {
      /// <summary>
      /// Parse date entered by user
      /// </summary>
      /// <param name="AtextToParse">eneterd date (should be: YYYY-MM-DD)</param>
      /// <returns></returns>
      public static CalendarDate ParseDate(string AtextToParse)
      {
         int[] date = new int[3];
         CalendarDate cD_err = new CalendarDate(-1, -1, -1);

         var splited = AtextToParse.Split('-');
         if (splited.Length == 3)
         {
            if (splited[0].Length == 4 && (splited[1].Length <= 2 && splited[1].Length > 0) && (splited[2].Length <= 2 && splited[2].Length > 0))
            {
               for (int i = 0; i < splited.Length; i++)
               {
                  if (!Int32.TryParse(splited[i], out date[i]))
                  {
                     Console.WriteLine(Properties.Resources.errNotNumber, splited[i]);
                     return cD_err;
                  }
               }
            }
            else
            {
               Console.WriteLine(Properties.Resources.errNotFormat);
               return cD_err;
            }
         }
         else
         {
            Console.WriteLine(Properties.Resources.errNotGood);
            return cD_err;
         }

         //if enetered stuff seems valid - check if is proper
         CalendarDate cD = new CalendarDate(date);
         if (cD.IsDateIsValid())
         {
            return cD; //is fine
         }
         else
         {
            Console.WriteLine(Properties.Resources.errNotExist); //no such date
            return cD_err;
         }
      }
   }

   class Program
   {
      /// <summary>
      /// Main Function
      /// </summary>
      /// <param name="args"></param>
      static void Main(string[] args)
      {
         ConsoleHelper consoleHelper = new ConsoleHelper();
         Console.WriteLine(Properties.Resources.msgHello);

         CalendarDate inputDate = new CalendarDate(1, 1, 1);

         bool enteredDataisOK = false;
         //user enter stuff until is OK
         do
         {
            string input = Console.ReadLine();

            inputDate = ConsoleHelper.ParseDate(input);

            if (inputDate._year == -1 || inputDate._month == -1 || inputDate._day == -1)
            {
               enteredDataisOK = false;
               Console.Write(Properties.Resources.msgCorrectDate);
            }
            else
            {
               enteredDataisOK = true;
            }
         }
         while (!enteredDataisOK);

         //show difference
         Console.WriteLine(inputDate.CalculateTime(inputDate));
         Console.ReadKey();
      }
   }
}
