namespace Nultien.TheShop.Application
{
    class Program
    {
        static void Main(string[] args)
        {
            // Application setup and start
            var host = Startup.Start();

            // Get instance
            //var shopService = ActivatorUtilities.CreateInstance<IShopService>(host.Services);
        }
    }
}
