namespace Nultien.TheShop.Common.Metrics
{
    public class OrderMetrics
    {
        public long Completed { get; private set; }
        public long Failed { get; private set; }

        public void IncreaseFailed() => Failed++;
        public void IncreaseCompleted() => Completed++;
    }
}
