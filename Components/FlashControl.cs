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

        private bool _flashOn;

        public FlashControl(MainPage mainPage)
        {
            _mediaCapture.InitializeAsync();
            _mainPage = mainPage;
        }

        public void CleanTmpVideos()
        {
            _mediaCapture.StopRecordAsync();
            _videoStorageFile.DeleteAsync();
        }

        public async Task FlashOnOf()
        {
            if (!_flashOn)
            {
                _mainPage.LightOnOfBtn.Content = ResourceKeeper.Instance.FlashOnImg;

                _videoStorageFile =
                    await KnownFolders.VideosLibrary.CreateFileAsync("tempVideo.mp4", CreationCollisionOption.GenerateUniqueName);

                await _mediaCapture.StartRecordToStorageFileAsync(_videoEncodingProperties, _videoStorageFile);
                await Task.Delay(TimeSpan.FromMilliseconds(50));
                
                _mediaCapture.VideoDeviceController.TorchControl.Enabled = true;
                _mediaCapture.VideoDeviceController.FlashControl.Enabled = true;
                _flashOn = true;
            }
            else
            {
                _mediaCapture.StopRecordAsync();
                _videoStorageFile.DeleteAsync();

                _mediaCapture.VideoDeviceController.TorchControl.Enabled = false;
                _mediaCapture.VideoDeviceController.FlashControl.Enabled = false;
                _mainPage.LightOnOfBtn.Content = ResourceKeeper.Instance.FlashOffImg;
                _flashOn = false;
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