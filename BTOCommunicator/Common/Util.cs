using System;
using System.Data;
using System.Globalization;
using System.Text;

namespace Common
{
    public static class Util
    {
        public static int ToInt(string month)
        {
            return DateTime.ParseExact(month, "MMMM", CultureInfo.InvariantCulture).Month;
        }

        public static string GetMonth(int number)
        {
            return CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(number);
        }

        public static string ConcatString(params object[] parameters)
        {
            var concatedString = new StringBuilder();
            foreach (var value in parameters)
            {
                concatedString.Append(value);
            }
            return concatedString.ToString();
        }

        public static int NumberOfWeeks(DateTime dateFrom, DateTime dateTo)
        {
            TimeSpan span = dateTo.Subtract(dateFrom);

            if (span.Days <= 7)
            {
                if (dateFrom.DayOfWeek > dateTo.DayOfWeek)
                {
                    return 2;
                }

                return 1;
            }

            int days = span.Days - 7 + (int)dateFrom.DayOfWeek;
            int weekCount;
            int dayCount = 0;

            for (weekCount = 1; dayCount < days; weekCount++)
            {
                dayCount += 7;
            }

            return weekCount;
        }

        public static int GetIso8601WeekOfYear(DateTime time)
        {
            // Seriously cheat.  If its Monday, Tuesday or Wednesday, then it'll 
            // be the same week# as whatever Thursday, Friday or Saturday are,
            // and we always get those right
            DayOfWeek day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(time);
            if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
            {
                time = time.AddDays(3);
            }

            // Return the week of our adjusted day
            return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(time, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }

     

        public static DateTime FirstDateOfWeek(int year, int weekOfYear, CultureInfo ci)
        {
            var jan1 = new DateTime(year, 1, 1);
            int daysOffset = (int)ci.DateTimeFormat.FirstDayOfWeek - (int)jan1.DayOfWeek;
            DateTime firstWeekDay = jan1.AddDays(daysOffset);
            int firstWeek = ci.Calendar.GetWeekOfYear(jan1, ci.DateTimeFormat.CalendarWeekRule, ci.DateTimeFormat.FirstDayOfWeek);
            if (firstWeek <= 1 || firstWeek > 50)
            {
                weekOfYear -= 1;
            }
            return firstWeekDay.AddDays(weekOfYear * 7);
        }


         public static bool ChangeColumnDataType(DataTable table, string columnname, Type newtype)
         {
             if (table.Columns.Contains(columnname) == false)
                 return false;

             DataColumn column = table.Columns[columnname];
             if (column.DataType == newtype)
                 return true;

             try
             {
                 DataColumn newcolumn = new DataColumn("temporary", newtype);
                 table.Columns.Add(newcolumn);
                 foreach (DataRow row in table.Rows)
                 {
                     try
                     {
                         row["temporary"] = Convert.ChangeType(row[columnname], newtype);
                     }
                     catch
                     {
                     }
                 }
                 table.Columns.Remove(columnname);
                 newcolumn.ColumnName = columnname;
                 return true;
             }
             catch (Exception)
             {
                 return false;
             }

         }
     }
    
}
