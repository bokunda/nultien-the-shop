using Nultien.TheShop.Tests.Unit.Common;
using System.Linq;
using Xunit;

namespace Nultien.TheShop.Tests.Integration
{
    [Collection("Sequential")]
    public class IntegrationTests : BaseTestsSetup
    {

        [Theory]
        [InlineData(new bool[] { true }, new string[] { "123-456-789" }, new long[] { 10 }, new double[] { 1000 }, 1, 0)] // one article, happy flow
        [InlineData(new bool[] { true, true }, new string[] { "123-456-789", "111-222-333" }, new long[] { 10, 4 }, new double[] { 1000, 20000 }, 2, 0)] // two articles, happy flow
        [InlineData(new bool[] { true, false }, new string[] { "123-456-789", "111-222-333" }, new long[] { 10, 4000000 }, new double[] { 1000, 20000 }, 1, 1)] // one article twice, second order with invalid quantity
        [InlineData(new bool[] { false }, new string[] { "INVALID" }, new long[] { 10 }, new double[] { 1000 }, 0, 1)] // article code doesn't exist
        [InlineData(new bool[] { false }, new string[] { "123-456-789" }, new long[] { 100000 }, new double[] { 1000 }, 0, 1)] // quantity is bigger than expected
        [InlineData(new bool[] { false }, new string[] { "123-456-789" }, new long[] { 10 }, new double[] { 1 }, 0, 1)] // maxExpectedPrice is lower than expected
        [InlineData(new bool[] { false }, new string[] { "" }, new long[] { 10 }, new double[] { 1 }, 0, 1)] // string.Empty as article code 
        [InlineData(new bool[] { false }, new string[] { null }, new long[] { 10 }, new double[] { 1 }, 0, 1)] // string.Empty as article code 
        public void TestFullArticleSellingFlow(bool[] shouldCompleteOrder, string[] articleCode, long[] quantity, double[] maxExpectedPrice, long metricsCompleted, long metricsFailed)
        {
            // Arrange

            // Act
            for (var i = 0; i < articleCode.Length; i++)
            {
                var orderItems = ShopService.SellArticle(articleCode[i], quantity[i], maxExpectedPrice[i]);
                ShopService.CompleteOrder(orderItems, Context.Customers.First().Id);

                // Assert
                if (shouldCompleteOrder[i])
                {
                    Assert.NotNull(orderItems);
                    Assert.NotNull(Context.Orders);
                }
                else
                {
                    Assert.Equal(Context.Orders.Count, metricsFailed);
                }
            }

            Assert.Equal(OrderMetrics.Completed, metricsCompleted);
            Assert.Equal(OrderMetrics.Failed, metricsFailed);
        }

        [Theory]
        [InlineData(true, "123-456-789", 1, 0)]
        [InlineData(false, "INVALID", 0, 1)]
        [InlineData(false, "", 0, 1)]
        [InlineData(false, null, 0, 1)]
        public void TestFullArticleSearchFlow(bool shouldPass, string articleCode, long metricsFound, long metricsNotFound)
        {
            // Arrange

            // Act
            var article = ShopService.GetArticleInformation(articleCode);

            // Assert
            if (shouldPass)
            {
                Assert.NotNull(articleCode);
                Assert.Equal(article.Code, articleCode);
            }
            else
            {
                Assert.Null(article);
            }

            Assert.Equal(ArticleMetrics.Found, metricsFound);
            Assert.Equal(ArticleMetrics.NotFound, metricsNotFound);
        }
    }
}
