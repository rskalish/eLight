using System;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace eLight.Components
{
    public class ScreenLight
    {
        public async void HighlightScreenAsync()
        {
            Grid contentGrid = new Grid
            {
                Height = Window.Current.Bounds.Height,
                Width = Window.Current.Bounds.Width
            };

            ContentDialog dlg = new ContentDialog { Content = contentGrid };
            SolidColorBrush color = new SolidColorBrush(Colors.White) { Opacity = 1 };
            dlg.Background = color;
            await dlg.ShowAsync();
        }
    }
}
