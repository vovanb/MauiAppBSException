namespace MauiAppBSException
{
    public partial class App : Application
    {
        public App()
        {
            try
            {
                InitializeComponent();

                // Use MainThread to ensure MainPage is set on the main thread
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    this.MainPage = new AppShell();
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        protected override MyWindow CreateWindow(IActivationState activationState)
        {
            return new MyWindow(MainPage);
        }

    }
    public class MyWindow : Window
    {
        public MyWindow() : base()
        {
        }

        public MyWindow(Page page) : base(page)
        {
        }

        protected override void OnDestroying()
        {

            //var logger = ((MainPage)MainPage.Instance).logger;
            //logger?.LogInformation("Destroying");

            //System.Diagnostics.Debug.WriteLine("Destroying");
            //MauiProgram.channel.Flush();
        }

        protected override void OnStopped()
        {

            //var logger = ((MainPage)MainPage.Instance).logger;

            //logger?.LogInformation("Stopping");
            //System.Diagnostics.Debug.WriteLine("Stopping");
            //MauiProgram.channel.Flush();
        }

        protected override void OnDeactivated()
        {
            System.Diagnostics.Debug.WriteLine("Deactivating");
        }
    }
}
