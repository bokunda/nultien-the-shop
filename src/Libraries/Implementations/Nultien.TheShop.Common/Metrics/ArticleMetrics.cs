namespace Nultien.TheShop.Common.Metrics
{
    public class ArticleMetrics
    {
        public long Found { get; private set; }
        public long NotFound { get; private set; }

        public void IncreaseFound() => Found++;
        public void IncreaseNotFound() => NotFound++;
    }
}
