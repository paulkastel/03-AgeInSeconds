using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgeInSecondsConsole
{
   /// <summary>
   /// Struct responsible for storing info about Dates
   /// </summary>
   public struct CalendarDate
   {
      /// <summary>
      /// Date of introducing Gregorian calendar
      /// </summary>
      public static readonly int _minYear = 1582;

      /// <summary>
      /// A year drifted away 1000 years from now
      /// </summary>
      public static readonly int _maxYear = DateTime.Now.Year + 1000;

      /// <summary>
      /// stored Year
      /// </summary>
      public int _year { get; private set; }

      /// <summary>
      /// stored Month
      /// </summary>
      public int _month { get; private set; }

      /// <summary>
      /// stored Day
      /// </summary>
      public int _day { get; private set; }

      /// <summary>
      /// Check if stored variables are valid together
      /// </summary>
      /// <returns>true if everythig is ok</returns>
      public bool IsDateIsValid()
      {
         if (_year <= _maxYear && _year >= _minYear)
         {
            //year ok
            if (_month >= 1 && _month <= 12)
            {
               //month ok
               if (_day > 0 && _day <= DateTime.DaysInMonth(_year, _month))
               {
                  //day ok
                  return true;
               }
            }
         }
         return false;
      }

      /// <summary>
      /// Sets variables for current date
      /// </summary>
      public void SetCalDateToday()
      {
         _year = DateTime.Now.Year;
         _month = DateTime.Now.Month;
         _day = DateTime.Now.Day;
      }

      /// <summary>
      /// Phase needed to calculate time
      /// </summary>
      private enum Phase { Years, Months, Days, Done };

      /// <summary>
      /// Count difference in time between today and date.
      /// </summary>
      /// <param name="A_calDate">Date from which difference will be counted</param>
      /// <returns>Whole raport describing difference</returns>
      public string CalculateTime(CalendarDate A_calDate)
      {
         DateTime date1 = new DateTime(A_calDate._year, A_calDate._month, A_calDate._day, 0, 0, 0);
         DateTime date2 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);

         TimeSpan dateDiff = date2.Subtract(date1);
         bool bFuture = false;

         if (date2 < date1)
         {
            bFuture = true;
            var tmpDate = date1;
            date1 = date2;
            date2 = tmpDate;
         }

         int years = 0, months = 0, days = 0;
         int tMonths = 0;
         DateTime current = date1;

         int officalDay = current.Day;
         Phase p = Phase.Years;

         while (p != Phase.Done)
         {
            switch (p)
            {
               case Phase.Years:
                  if (current.AddYears(years + 1) > date2)
                  {
                     p = Phase.Months;
                     current = current.AddYears(years);
                  }
                  else
                  {
                     years++;
                     tMonths += 12;
                  }
                  break;
               case Phase.Months:
                  if (current.AddMonths(months + 1) > date2)
                  {
                     p = Phase.Days;
                     current = current.AddMonths(months);
                     if (current.Day < officalDay && officalDay <= DateTime.DaysInMonth(current.Year, current.Month))
                        current = current.AddDays(officalDay - current.Day);
                  }
                  else
                     months++;
                  break;
               case Phase.Days:
                  if (current.AddDays(days + 1) > date2)
                  {
                     current = current.AddDays(days);
                     p = Phase.Done;

                  }
                  else
                     days++;
                  break;
            }
         }

         string outputTxt = string.Empty;
         long tmpTime = 0;

         if (bFuture)
         {
            outputTxt = "It will be: " + date1.DayOfWeek.ToString();
            outputTxt += string.Format("\nThis will be in " + Properties.Resources.strDate, years, months, days);
            outputTxt += string.Format(Properties.Resources.strMonths, tMonths + months);
            outputTxt += string.Format(Properties.Resources.strWeeks, dateDiff.Days / 7);
            outputTxt += string.Format(Properties.Resources.strDays, dateDiff.Days);

            tmpTime = dateDiff.Days * 24 + dateDiff.Hours;
            outputTxt += string.Format(Properties.Resources.strHours, Math.Abs(tmpTime));

            tmpTime = tmpTime * 60 + dateDiff.Minutes;
            outputTxt += string.Format(Properties.Resources.strMinutes, Math.Abs(tmpTime));

            tmpTime = tmpTime * 60 + dateDiff.Seconds;
            outputTxt += string.Format(Properties.Resources.strSeconds, Math.Abs(tmpTime));
         }
         else
         {
            outputTxt = "That day was: " + date1.DayOfWeek.ToString();
            outputTxt += string.Format("\nThat was "+Properties.Resources.strDate, years, months, days);
            outputTxt += string.Format(Properties.Resources.strMonths, tMonths + months);
            outputTxt += string.Format(Properties.Resources.strWeeks, dateDiff.Days / 7);
            outputTxt += string.Format(Properties.Resources.strDays, dateDiff.Days);

            tmpTime = dateDiff.Days * 24 + dateDiff.Hours;
            outputTxt += string.Format(Properties.Resources.strHours, tmpTime);

            tmpTime = tmpTime * 60 + dateDiff.Minutes;
            outputTxt += string.Format(Properties.Resources.strMinutes, tmpTime);

            tmpTime = tmpTime * 60 + dateDiff.Seconds;
            outputTxt += string.Format(Properties.Resources.strSeconds, tmpTime);
         }

         return outputTxt;
      }

      /// <summary>
      /// Constructor
      /// </summary>
      /// <param name="yr">year</param>
      /// <param name="mth">month</param>
      /// <param name="dy">day</param>
      public CalendarDate(int yr, int mth, int dy)
      {
         _year = yr;
         _month = mth;
         _day = dy;
      }

      /// <summary>
      /// Constructor
      /// </summary>
      /// <param name="A_arrDate">Specially formatted array yyyy-mm-dd</param>
      public CalendarDate(int[] A_arrDate)
      {
         if (A_arrDate.Length < 3)
            throw new IndexOutOfRangeException(Properties.Resources.errOnCreateCalDate);
         else
         {
            _year = A_arrDate[0];
            _month = A_arrDate[1];
            _day = A_arrDate[2];
         }
      }
   }
}
