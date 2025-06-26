using System;
using System.ComponentModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;
using System.Runtime.CompilerServices;
using Windows.ApplicationModel;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using eLight.Components;

namespace eLight
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : INotifyPropertyChanged , IDisposable
    {
        private string _batteryLevel;
        private readonly FlashControl _flashControl;
        private readonly BatteryMonitor _batteryMonitor;
        private readonly ScreenLight _screenLight;

        public string BatteryLevel
        {
            get { return _batteryLevel; }
            set
            {
                if (value != _batteryLevel)
                {
                    _batteryLevel = value;
                    OnPropertyChanged();
                }
            }
        }

        public FlashControl FlashControl
        {
            get { return _flashControl; }
        }

        public BatteryMonitor BatteryMonitor
        {
            get { return _batteryMonitor; }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public MainPage()
        {
            InitializeComponent();

            Tools.PreventScreenLock();

            NavigationCacheMode = NavigationCacheMode.Required;
            Application.Current.Suspending += ApplicationSuspending;

            FlashToggleButton.Content = ResourceKeeper.Instance.FlashOffImg;

            _flashControl = new FlashControl(this);
            _flashControl.InitializeAsync().GetAwaiter().GetResult();
            _batteryMonitor = new BatteryMonitor(this);
            _screenLight = new ScreenLight();
        }

        private async void ApplicationSuspending(object sender, SuspendingEventArgs e)
        {
            await _flashControl.CleanTmpVideosAsync();
        }


        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: Prepare page for display here.

            // TODO: If your application contains multiple pages, ensure that you are
            // handling the hardware Back button by registering for the
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
            // If you are using the NavigationHelper provided by some templates,
            // this event is handled for you.
        }

        private async void ToggleFlash_Click(object sender, RoutedEventArgs e)
        {
            await FlashControl.ToggleFlashAsync();
        }

        private void ScreenOnOf_Click(object sender, RoutedEventArgs e)
        {
            _screenLight.HighlightScreenAsync();
        }

        public void Dispose()
        {
            _flashControl.Dispose();
        }
    }
}
