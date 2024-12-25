namespace Budget.Repository
{
    public interface IBudgetRepo
    {
        List<BudgetData.DataModels.Budget> GetAll();
    }

    public class BudgetRepo : IBudgetRepo
    {
        public List<BudgetData.DataModels.Budget> GetAll()
        {
            var a = new List<BudgetData.DataModels.Budget>();

            return a;
        }
    }
}