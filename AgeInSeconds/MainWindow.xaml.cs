using AgeInSecondsWPF.DB;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

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

      /// <summary>
      /// Date entered by user
      /// </summary>
      private CalendarDate calDate;

      /// <summary>
      /// Tool to refresh every second
      /// </summary>
      private DispatcherTimer timer;

      /// <summary>
      /// App window constructor - initialize comboboxes, and timer.
      /// </summary>
      public MainWindow()
      {
         InitializeComponent();

         timer = new DispatcherTimer();
         timer.Interval = TimeSpan.FromSeconds(1);
         timer.Tick += timer_Tick;

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

      /// <summary>
      /// Fill Year combobox with years since 1582
      /// </summary>
      /// <param name="currentYr">Present year</param>
      private void FillYearComboBox(int currentYr)
      {
         cbYear.Items.Clear();
         int tYear = 10000;

         for (int nextYrs = 0; tYear > CalendarDate._minYear; nextYrs++) //that year gregorian calendar was invented
         {
            tYear = currentYr - nextYrs;
            ComboBoxItem item = new ComboBoxItem();
            if (DateTime.IsLeapYear(tYear))
            {
               item.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF898989"));
            }
            item.Content = tYear;

            cbYear.Items.Add(item);
         }
         cbYear.SelectedIndex = _selIdxYear;
      }

      /// <summary>
      /// Fill Month combobox with months until present month present year
      /// </summary>
      /// <param name="currentMth">present month this year</param>
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

      /// <summary>
      /// Fill Day combobox with proper days eg: feb - 28/29, july 31 etc.
      /// </summary>
      /// <param name="daysCount">number of days</param>
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
            FillMonthComboBox(12);
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

      /// <summary>
      /// Update label with time difference every second
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      void timer_Tick(object sender, EventArgs e)
      {
         lblOutputTime.Text = calDate.CalculateTime(calDate);
      }

      /// <summary>
      /// On btnclick show difference in time, and fun fact from past 
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      private void btncountIt_Click(object sender, RoutedEventArgs e)
      {
         lblOutputEvents.Text = string.Empty;
         SaveSelectedIndexes(cbYear.SelectedIndex, cbMonth.SelectedIndex, cbDay.SelectedIndex);
         calDate = new CalendarDate(int.Parse(cbYear.Text), int.Parse((_selIdxMonth + 1).ToString()), int.Parse((_selIdxDay + 1).ToString()));
         lblOutputTime.Text = calDate.CalculateTime(calDate);
         timer.Start();

         List<Object> dList = new List<Object>();
         dList.AddRange(DB.getImportantDates(calDate));
         dList.AddRange(DB.getFamousBirthdayDates(calDate));

         if (dList.Count != 0)
         {
            Random random = new Random();
            int randomNumber = random.Next(0, dList.Count);
            lblOutputEvents.Text = "- " + dList[randomNumber];
         }
      }
   }
}
