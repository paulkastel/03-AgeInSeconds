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
      public static readonly int _minYear = DateTime.Now.Year - 150;
      public static readonly int _maxYear = DateTime.Now.Year + 150;
      public int _year { get; private set; }
      public int _month { get; private set; }
      public int _day { get; private set; }

      public bool IsDateIsValid()
      {
         if (_year < _maxYear && _year > _minYear)
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

      public void SetCalDateToday()
      {
         _year = DateTime.Now.Year;
         _month = DateTime.Now.Month;
         _day = DateTime.Now.Day;
      }

      public string CalculateTime(CalendarDate inputDate)
      {
         CalendarDate todayDate = new CalendarDate();
         todayDate.SetCalDateToday();

         return string.Empty;
      }

      public CalendarDate(int y, int m, int d)
      {
         _year = y;
         _month = m;
         _day = d;
      }

      public CalendarDate(int[] date)
      {
         if (date.Length < 3)
            throw new IndexOutOfRangeException("Size of array is insignificant!");
         else
         {
            _year = date[0];
            _month = date[1];
            _day = date[2];
         }
      }
   }
}
