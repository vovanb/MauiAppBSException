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

        protected override Window CreateWindow(IActivationState activationState)
        {
            Window window = base.CreateWindow(activationState);

            // Remove existing event handlers to prevent multiple subscriptions
            window.Created -= OnWindowCreated;
            window.Destroying -= OnWindowDestroying;
            window.Stopped -= OnWindowStopped;

            // Add event handlers
            window.Created += OnWindowCreated;
            window.Destroying += OnWindowDestroying;
            window.Stopped += OnWindowStopped;


            return window;
        }

        // Define the event handlers as separate methods
        private void OnWindowCreated(object sender, EventArgs e)
        {
            // Custom logic for window.Created
        }

        private void OnWindowDestroying(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Destroying");
        }

        private void OnWindowStopped(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Stopped");
        }

    }
   
}
