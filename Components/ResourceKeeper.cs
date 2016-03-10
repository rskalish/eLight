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

        private static volatile ResourceKeeper _instance;
        private static readonly object SyncRoot = new Object();

        private ResourceKeeper() { }

        public static ResourceKeeper Instance
        {
           get 
           {
              if (_instance == null) 
              {
                 lock (SyncRoot) 
                 {
                    if (_instance == null)
                        _instance = new ResourceKeeper();
                 }
              }

              return _instance;
           }
        }
    }
}
