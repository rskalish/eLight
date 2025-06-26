using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace eLight.Components
{
    public sealed class ResourceKeeper
    {
        public Image FlashOnImg = new Image
        {
            Source = new BitmapImage(new Uri("ms-appx:///Assets/light_bulb_on.png", UriKind.Absolute))
        };

        public Image FlashOffImg = new Image
        {
            Source = new BitmapImage(new Uri("ms-appx:///Assets/light_bulb_off.png", UriKind.Absolute))
        };

        private static readonly Lazy<ResourceKeeper> _instance =
            new Lazy<ResourceKeeper>(() => new ResourceKeeper());

        private ResourceKeeper() { }

        public static ResourceKeeper Instance => _instance.Value;
    }
}
