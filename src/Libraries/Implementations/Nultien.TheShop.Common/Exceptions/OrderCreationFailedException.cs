using Nultien.TheShop.Common.Metrics;
using System;

namespace Nultien.TheShop.Common.Exceptions
{
    public class OrderCreationFailedException : Exception
    {
        public OrderCreationFailedException(string errorMessage, OrderMetrics metrics)
        {
            metrics.IncreaseFailed();
            ErrorMessage = errorMessage;
        }

        public string ErrorMessage { get; set; }
    }
}
