
namespace API.Utilities.Handlers
{
    public class WeekDayDateHandler
    {
        public static int Calculate(DateTime startDate, DateTime endDate, int totalDay = 0)
        {   // melopping sampat endate dan juga date + 1 day
            for (var date = startDate; date < endDate; date = date.AddDays(1))
            {   // jika tidak ada sabtu dan miggu ditambah
                if (date.DayOfWeek != DayOfWeek.Saturday&& date.DayOfWeek != DayOfWeek.Sunday)
                    totalDay++;
            }
            return totalDay;
        }
    }
}
