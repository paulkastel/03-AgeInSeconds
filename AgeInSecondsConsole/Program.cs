using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgeInSecondsConsole
{
   public class ConsoleHelper
   {
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

         CalendarDate cD = new CalendarDate(date);
         if (cD.IsDateIsValid())
         {
            return cD;
         }
         else
         {
            Console.WriteLine(Properties.Resources.errNotExist);
            return cD_err;
         }
      }
   }

   class Program
   {
      static void Main(string[] args)
      {
         ConsoleHelper consoleHelper = new ConsoleHelper();
         Console.WriteLine(Properties.Resources.msgHello);

         CalendarDate inputDate = new CalendarDate(1, 1, 1);

         bool enteredDataisOK = false;
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

         Console.WriteLine(inputDate.CalculateTime(inputDate));
         Console.ReadKey();
      }
   }
}
