namespace BudgetData.Service
{
    public interface IBudgetService
    {
        decimal QueryDate(DateTime start, DateTime end);
    }
}
