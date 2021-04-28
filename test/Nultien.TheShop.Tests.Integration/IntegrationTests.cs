using Nultien.TheShop.Tests.Unit.Common;
using System.Linq;
using Xunit;

namespace Nultien.TheShop.Tests.Integration
{
    [Collection("Sequential")]
    public class IntegrationTests : BaseTestsSetup
    {

        [Theory]
        [InlineData(true, "123-456-789", 10, 1000, 1, 0)]
        [InlineData(false, "INVALID", 10, 1000, 0, 1)] // article code doesn't exist
        [InlineData(false, "123-456-789", 100000, 1000, 0, 1)] // quantity is bigger than expected
        [InlineData(false, "123-456-789", 10, 1, 0, 1)] // maxExpectedPrice is lower than expected
        [InlineData(false, "", 10, 1, 0, 1)] // string.Empty as article code 
        [InlineData(false, null, 10, 1, 0, 1)] // string.Empty as article code 
        public void TestFullArticleSellingFlow(bool shouldCompleteOrder, string articleCode, long quantity, double maxExpectedPrice, long metricsCompleted, long metricsFailed)
        {
            // Arrange

            // Act
            var orderItems = ShopService.SellArticle(articleCode, quantity, maxExpectedPrice);
            ShopService.CompleteOrder(orderItems, Context.Customers.First().Id);

            // Assert
            if (shouldCompleteOrder)
            {
                Assert.NotNull(orderItems);
                Assert.NotNull(Context.Orders);
            }
            else
            {
                Assert.False(Context.Orders.Any());
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
