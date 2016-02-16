using System;
using System.Threading.Tasks;
using Windows.Media.Capture;
using Windows.Media.MediaProperties;
using Windows.Storage;

namespace eLight
{
    public sealed class FlashControl
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
                _mainPage.LightOnOfBtn.Content = "Flash Off";

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
                _mainPage.LightOnOfBtn.Content = "Flash On";
                _flashOn = false;
            }
        }
    }
}