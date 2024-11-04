using System.Diagnostics;

namespace MauiAppBSException
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
            this.Loaded += MainPage_Loaded;

        }

        private void MainPage_Loaded(object sender, EventArgs e)
        {

            if (!MauiAppBSException.Platforms.Android.AndroidServiceManager.IsRunning)
            {
                MauiAppBSException.Platforms.Android.AndroidServiceManager.StartMyService();
                Debug.WriteLine("Service has started");
            }
            else
            {
                Debug.WriteLine("Service is running");
            }

        }


        private void OnCounterClicked(object sender, EventArgs e)
        {
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);
        }
    }

}
