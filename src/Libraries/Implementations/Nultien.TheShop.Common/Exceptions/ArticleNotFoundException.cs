using Nultien.TheShop.Common.Metrics;
using System;

namespace Nultien.TheShop.Common.Exceptions
{
    public class ArticleNotFoundException : Exception
    {
        public ArticleNotFoundException(string errorMessage, ArticleMetrics metrics)
        {
            metrics.IncreaseNotFound();
            ErrorMessage = errorMessage;
        }

        public string ErrorMessage { get; set; }
    }
}
