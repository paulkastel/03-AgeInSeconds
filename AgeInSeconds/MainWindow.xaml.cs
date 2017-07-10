using AgeInSecondsWPF.DB;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace AgeInSecondsWPF
{
   /// <summary>
   /// Interaction logic for MainWindow.xaml
   /// </summary>
   public partial class MainWindow : Window
   {
      public readonly int _currentMonth = DateTime.Now.Month;
      public readonly int _currentDay = DateTime.Now.Day;
      public readonly int _currentYear = DateTime.Now.Year;

      private int _selIdxMonth = DateTime.Now.Month - 1;
      private int _selIdxDay = DateTime.Now.Day - 1;
      private int _selIdxYear = 0;

      private DatabaseConnection DB = new DatabaseConnection();
      private CalendarDate calDate;

      /// <summary>
      /// App window constructor
      /// </summary>
      public MainWindow()
      {
         InitializeComponent();

         FillYearComboBox(_currentYear);
         FillMonthComboBox(_currentMonth);
         FillDayComboBox(_currentDay);

         SetComboboxesForToday();
      }

      /// <summary>
      /// Set combobox.selectedindex for today date
      /// </summary>
      private void SetComboboxesForToday()
      {
         cbYear.SelectedIndex = 0;
         cbMonth.SelectedIndex = DateTime.Now.Month - 1;
         cbDay.SelectedIndex = DateTime.Now.Day - 1;
         SaveSelectedIndexes(cbYear.SelectedIndex, cbMonth.SelectedIndex, cbDay.SelectedIndex);
      }

      /// <summary>
      /// Save present indexes to variables
      /// </summary>
      /// <param name="year">index of comboBox year </param>
      /// <param name="month">index of comboMonth month</param>
      /// <param name="day">index of comboMonth day</param>
      private void SaveSelectedIndexes(int year, int month, int day)
      {
         _selIdxDay = day;
         _selIdxMonth = month;
         _selIdxYear = year;
      }

      private void FillYearComboBox(int currentYr)
      {
         cbYear.Items.Clear();
         int tYear = 10000;

         for (int nextYrs = 0; tYear > 1582; nextYrs++)
         {
            tYear = currentYr - nextYrs;
            ComboBoxItem item = new ComboBoxItem();
            if (DateTime.IsLeapYear(tYear))
            {
               item.Foreground = Brushes.Aqua;
            }
            item.Content = tYear;

            cbYear.Items.Add(item);
         }
         cbYear.SelectedIndex = _selIdxYear;
      }

      private void FillMonthComboBox(int currentMth)
      {
         cbMonth.Items.Clear();
         for (int nextMths = 1; nextMths < currentMth + 1; nextMths++)
         {
            cbMonth.Items.Add(DateTimeFormatInfo.CurrentInfo.GetMonthName(nextMths));
         }
         if (_selIdxMonth >= currentMth)
            cbMonth.SelectedIndex = currentMth - 1;
         else
            cbMonth.SelectedIndex = _selIdxMonth;
      }

      private void FillDayComboBox(int daysCount)
      {
         cbDay.Items.Clear();
         for (int i = 1; i < daysCount + 1; i++)
         {
            cbDay.Items.Add(i);
         }
         if (_selIdxDay >= daysCount)
         {
            cbDay.SelectedIndex = daysCount - 1;
         }
         else
            cbDay.SelectedIndex = _selIdxDay;
      }

      /// <summary>
      /// On month open fill proper months
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      private void cbMonth_DropDownOpened(object sender, EventArgs e)
      {
         if (_selIdxYear == 0)
            FillMonthComboBox(_currentMonth);   //only up current month
         else
            FillMonthComboBox(12);  //all months
      }

      /// <summary>
      /// On day open, fill list with proper dates
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      private void cbDay_DropDownOpened(object sender, EventArgs e)
      {
         if (_selIdxYear == 0 && _selIdxMonth == _currentMonth - 1)
         {
            FillDayComboBox(_currentDay); //current month set it only up today
         }
         else
         {
            int year = int.Parse(cbYear.Text.ToString());
            int month = cbMonth.SelectedIndex + 1;
            FillDayComboBox(DateTime.DaysInMonth(year, month));

         }
      }

      /// <summary>
      /// On year close check if current month is not later than should
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      private void cbYear_DropDownClosed(object sender, EventArgs e)
      {
         SaveSelectedIndexes(cbYear.SelectedIndex, cbMonth.SelectedIndex, cbDay.SelectedIndex);
         //if specified month doesnt come around set index for current month
         if (_selIdxYear == 0 && _selIdxMonth > _currentMonth - 1)
         {
            cbMonth.SelectedIndex = _currentMonth - 1;
            SaveSelectedIndexes(cbYear.SelectedIndex, cbMonth.SelectedIndex, cbDay.SelectedIndex);
         }

         cbMonth_DropDownClosed(sender, e);

      }

      /// <summary>
      /// On month close, check if current day is not later than shlould
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      private void cbMonth_DropDownClosed(object sender, EventArgs e)
      {
         SaveSelectedIndexes(cbYear.SelectedIndex, cbMonth.SelectedIndex, cbDay.SelectedIndex);
         int dayInMonth = DateTime.DaysInMonth(int.Parse(cbYear.Text), _selIdxMonth + 1);
         //if current day is not last day of month this year, set selected index for today
         if (_selIdxYear == 0 & _selIdxMonth + 1 == _currentMonth)
         {
            cbDay.SelectedIndex = _currentDay - 1;
            SaveSelectedIndexes(cbYear.SelectedIndex, cbMonth.SelectedIndex, cbDay.SelectedIndex);
         }
         //else set index for last day of month
         else if (cbDay.SelectedIndex + 1 > dayInMonth)
         {
            cbDay.SelectedIndex = dayInMonth - 1;
            SaveSelectedIndexes(cbYear.SelectedIndex, cbMonth.SelectedIndex, cbDay.SelectedIndex);
         }
      }

      /// <summary>
      /// On button click set all comboboxes value to today date
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      private void btnSetToday_Click(object sender, RoutedEventArgs e)
      {
         SetComboboxesForToday();
      }

      //TODO updating everysceond 1962
      private void btncountIt_Click(object sender, RoutedEventArgs e)
      {
         calDate = new CalendarDate(int.Parse(cbYear.Text), int.Parse((_selIdxMonth+1).ToString()), int.Parse((_selIdxDay + 1).ToString()));
         List<Object> dHistoric = DB.getImportantDates(calDate);
         List<Object> dBirthday = DB.getFamousBirthdayDates(calDate);

         string strHistory = string.Empty;

         foreach (var evt in dHistoric)
         {
            strHistory += evt.ToString()+"\n";
         }
         strHistory.Replace("\n", Environment.NewLine);
         lblOutputEvents.Text = strHistory;

         lblOutputTime.Text = calDate.CalculateTime(calDate);
      }
   }
}
