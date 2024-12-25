using Budget.Repository;
using Budget.Service;

using Microsoft.VisualStudio.TestPlatform.ObjectModel.Client.Payloads;
using NSubstitute;
using NSubstitute.Core;
using Budget = BudgetData.DataModels.Budget;

namespace BudgetTest
{
    public class Tests
    {
        BudgetService _budgetService;
        private IBudgetRepo? _budgetRepo;


        [SetUp]
        public void Setup()
        {
            _budgetRepo = Substitute.For<IBudgetRepo>();
            _budgetService = new BudgetService(_budgetRepo);
        }

        [TestCaseSource(nameof(DateCases))]
        public void GetInvailedDate(DateTime start, DateTime end)
        {
            var expect =_budgetService.Query(start,end);
            Assert.AreEqual(0, expect);
        }

        [Test]
        public void GetMonthlyBudget_ShouldReturnAllMonth()
        {
            _budgetRepo.GetAll().Returns(new List<BudgetData.DataModels.Budget>
            {
                new BudgetData.DataModels.Budget("202412", 3100)
            });

            DateTime start= new DateTime(2024,12,1);
            DateTime end= new DateTime(2024,12,31);

            var result = _budgetService.Query(start, end);
            Assert.AreEqual(3100, result);
        }        
        
        [Test]
        public void GetMonthlyBudget_ShouldReturnDaysInSameMonth()
        {
            _budgetRepo.GetAll().Returns(new List<BudgetData.DataModels.Budget>
            {
                new BudgetData.DataModels.Budget("202412", 3100)
            });

            DateTime start= new DateTime(2024,12,1);
            DateTime end= new DateTime(2024,12,5);

            var result = _budgetService.Query(start, end);
            Assert.AreEqual(500, result);
        }


        public static IEnumerable<TestCaseData> DateCases
        {
            get
            {
                yield return new TestCaseData(DateTime.Now, DateTime.Now.AddDays(-1));
                yield return new TestCaseData(DateTime.Now.AddDays(1), DateTime.Now);
            }
        }

    }
}