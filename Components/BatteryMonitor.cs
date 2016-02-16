using System;
using Windows.UI.Xaml;

namespace eLight
{
    public sealed class BatteryMonitor
    {
        private readonly MainPage _mainPage;

        public BatteryMonitor(MainPage mainPage)
        {
            _mainPage = mainPage;

            DisplayBatteryLevel();
        }

        private void dispatcherTimer_Tick(object sender, object e)
        {
            string level = Windows.Phone.Devices.Power.Battery.GetDefault().RemainingChargePercent.ToString();
            _mainPage.BatteryLevel = "Battery Level: " + level + "%";
        }

        public void DisplayBatteryLevel()
        {
            _mainPage.BatteryLevelBlock.DataContext = _mainPage;
            string level = Windows.Phone.Devices.Power.Battery.GetDefault().RemainingChargePercent.ToString();
            _mainPage.BatteryLevel = "Battery Level: " + level + "%";
            var dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
        }
    }
}