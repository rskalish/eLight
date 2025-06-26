using System;
using System.Threading.Tasks;
using Windows.Media.Capture;
using Windows.Media.MediaProperties;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace eLight.Components
{
    public class FlashControl : IDisposable
    {
        private readonly MainPage _mainPage;

        private readonly MediaCapture _mediaCapture = new MediaCapture();

        private readonly MediaEncodingProfile _videoEncodingProperties = MediaEncodingProfile.CreateMp4(VideoEncodingQuality.Vga);

        private StorageFile _videoStorageFile;

        private bool _isFlashOn;

        public FlashControl(MainPage mainPage)
        {
            // Capture the reference to the hosting page
            _mainPage = mainPage;
        }

        public async Task InitializeAsync()
        {
            await _mediaCapture.InitializeAsync();
        }

        public async Task CleanTmpVideosAsync()
        {
            // Clean up temporary recording files if they were created.
            if (_videoStorageFile != null)
            {
                await _mediaCapture.StopRecordAsync();
                await _videoStorageFile.DeleteAsync();
                _videoStorageFile = null;
            }
        }

        public async Task ToggleFlashAsync()
        {
            if (!_isFlashOn)
            {
                _mainPage.FlashToggleButton.Content = ResourceKeeper.Instance.FlashOnImg;

                _videoStorageFile =
                    await KnownFolders.VideosLibrary.CreateFileAsync("tempVideo.mp4", CreationCollisionOption.GenerateUniqueName);

                await _mediaCapture.StartRecordToStorageFileAsync(_videoEncodingProperties, _videoStorageFile);
                await Task.Delay(TimeSpan.FromMilliseconds(50));
                
                _mediaCapture.VideoDeviceController.TorchControl.Enabled = true;
                _mediaCapture.VideoDeviceController.FlashControl.Enabled = true;
                _isFlashOn = true;
            }
            else
            {
                await _mediaCapture.StopRecordAsync();
                await _videoStorageFile.DeleteAsync();
                _videoStorageFile = null;

                _mediaCapture.VideoDeviceController.TorchControl.Enabled = false;
                _mediaCapture.VideoDeviceController.FlashControl.Enabled = false;
                _mainPage.FlashToggleButton.Content = ResourceKeeper.Instance.FlashOffImg;
                _isFlashOn = false;
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _mediaCapture.Dispose();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}