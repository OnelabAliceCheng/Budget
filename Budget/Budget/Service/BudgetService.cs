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
            var startMonth = start.ToString("yyyy-MM-dd");
            var endMonth = end.ToString("yyyy-MM-dd");
            var budgets = _budgetRepo.GetAll();

            if (startMonth == endMonth)
            {
                var currentMonthAmount = budgets.Where(x => x.YearMonth == startMonth).FirstOrDefault().Amount;

                var MonthDate = DateTime.DaysInMonth(start.Year, start.Month);
                return (currentMonthAmount / MonthDate) * (Math.Abs((end - start).Days + 1));
            }
            else
            {
                var currentStartMonthAmount = budgets.Where(x => x.YearMonth == startMonth).FirstOrDefault().Amount;
                var currentEndMonthAmount = budgets.Where(x => x.YearMonth == endMonth).FirstOrDefault().Amount;

                var startMonthDate = DateTime.DaysInMonth(start.Year, start.Month);
                var endMonthDate = DateTime.DaysInMonth(end.Year, end.Month);

                return (currentStartMonthAmount/startMonthDate)*(startMonthDate - start.Day + 1) + (currentEndMonthAmount/endMonthDate)*(endMonthDate - end.Day + 1);
            }

            if (start > end)
            {
                return 0;
            }

            return budgets[0].Amount;
        }
    }
}