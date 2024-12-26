using Budget.Repository;

namespace Budget.Service
{
    public class BudgetService
    {
        private readonly IBudgetRepo _budgetRepo;

        public BudgetService(IBudgetRepo budgetRepo)
        {
            _budgetRepo = budgetRepo;
        }

        public static void Main(string[] args)
        {
            Console.WriteLine("Test");
        }

        public decimal Query(DateTime start, DateTime end)
        {
            if (start > end)
            {
                return 0;
            }

            var MonthDiff = CalculateMonthDifference(start, end);
            var startMonthDate = DateTime.DaysInMonth(start.Year, start.Month);
            var endMonthDate = DateTime.DaysInMonth(end.Year, end.Month);

            var startMonth = start.ToString("yyyyMM");
            var endMonth = end.ToString("yyyyMM");

            var budgets = _budgetRepo.GetAll();
            var sum = 0;
            if (MonthDiff == 0)
            {
                var currentStartMonthAmount = budgets.FirstOrDefault(x => x.YearMonth == startMonth)?.Amount ?? 0;
                sum = (currentStartMonthAmount / startMonthDate) * (Math.Abs((end - start).Days + 1));
            }
            else
            {
                for (int i = 0; i <= MonthDiff; i++)
                {
                    if (i == 0) // 起始月份
                    {
                        var currentStartMonthAmount = budgets.FirstOrDefault(x => x.YearMonth == startMonth)?.Amount ?? 0;
                        sum += (currentStartMonthAmount / startMonthDate) * (startMonthDate - start.Day + 1);
                    }
                    else if (i == MonthDiff) // 結束月份
                    {
                        var currentEndMonthAmount = budgets.FirstOrDefault(x => x.YearMonth == endMonth)?.Amount ?? 0;
                        sum += (currentEndMonthAmount / endMonthDate) * end.Day;
                    }
                    else // 中間月份
                    {
                        string currentMonth = start.AddMonths(i).ToString("yyyyMM");
                        var currentMonthAmount = budgets.FirstOrDefault(x => x.YearMonth == currentMonth)?.Amount ?? 0;
                        sum += currentMonthAmount;
                    }
                }
            }

            return sum;

        }
        public int CalculateMonthDifference(DateTime startDate, DateTime endDate)
        {
            // 確保起始日期早於結束日期
            if (startDate > endDate)
            {
                var temp = startDate;
                startDate = endDate;
                endDate = temp;
            }

            // 計算年份與月份的差異
            int yearDifference = endDate.Year - startDate.Year;
            int monthDifference = endDate.Month - startDate.Month;

            return yearDifference * 12 + monthDifference;
        }
    }
}