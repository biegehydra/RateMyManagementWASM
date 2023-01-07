using System.Threading.Tasks;
using RateMyManagementWASM.Client.IServices;
using RateMyManagementWASM.Client.Services;

namespace RateMyManagement.bUnitTesting
{
    /// <summary>
    /// These tests are written entirely in C#.
    /// Learn more at https://bunit.dev/docs/getting-started/writing-tests.html#creating-basic-tests-in-cs-files
    /// </summary>
    public class CounterCSharpTests : TestContext
    {
        [Fact]
        public async Task Test()
        {
            using var ctx = new TestContext();
            ctx.Services.AddScoped<TestDb>();
            var serviceProvider = ctx.Services.BuildServiceProvider();
            var testDb = serviceProvider.GetService<TestDb>();
            var result =  await testDb.GetAll(null);
            // Arrange
            var cut = RenderComponent<Counter>();

            // Assert that content of the paragraph shows counter at zero
            cut.Find("p").MarkupMatches("<p>Current count: 0</p>");
        }

        [Fact]
        public void ClickingButtonIncrementsCounter()
        {
            // Arrange
            var cut = RenderComponent<Counter>();

            // Act - click button to increment counter
            cut.Find("button").Click();

            // Assert that the counter was incremented
            cut.Find("p").MarkupMatches("<p>Current count: 1</p>");
        }
    }
}