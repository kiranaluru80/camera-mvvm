using System;
using System.IO;
using Xamarians.Media;
using Xamarin.Forms;
namespace CamaraSampleNew
{
    public class CamaraSampleNewPageViewModel:ViewModelBase
    {


		string imagesource;
		public string ImageSource
		{
			get { return imagesource; }
			set
			{
				if (imagesource != value)
				{
					imagesource = value;
					OnPropertyChanged("ImageSource");
				}
			}
		}
		Command _takephotocommand;
		Command _chooseimagecommand;
		Command _choosevideocommand;
		Command _chooseaudiocommand;

		public Command TakePhotoCommand => _takephotocommand ?? (_takephotocommand = new Command(ExecuteTakePhotoCommand));
		public Command ChooseImageCommand => _chooseimagecommand ?? (_chooseimagecommand = new Command(ExecuteChooseImageCommand));
		public Command ChooseVideoCommand => _choosevideocommand ?? (_choosevideocommand = new Command(ExecuteChooseVideoCommand));
		public Command ChooseAudioCommand => _chooseaudiocommand ?? (_chooseaudiocommand = new Command(ExecuteChooseAudioCommand));


		private string GenerateFilePath()
		{
			return Path.Combine(MediaService.Instance.GetPublicDirectoryPath(), MediaService.Instance.GenerateUniqueFileName("jpg"));
		}

		private async void ExecuteTakePhotoCommand()
		{
            string filePath = GenerateFilePath();
			var result = await MediaService.Instance.TakePhotoAsync(new CameraOption()
			{
				FilePath = filePath,
				MaxWidth = 50,
				MaxHeight = 50
			});
            if (result.IsSuccess)
            {
                ImageSource = result.FilePath;

                FileImageSource obj = new FileImageSource();
            }
            else
            {
				//await DisplayAlert("Error", result.Message, "OK");
                MessagingCenter.Send<CamaraSampleNewPageViewModel>(this, "Error1");

			}
          }
		private async void ExecuteChooseImageCommand()
		{
			MediaResult result = await MediaService.Instance.OpenMediaPickerAsync(MediaType.Image);
			if (result.IsSuccess)
			{
				ImageSource = result.FilePath;
				String[] obj = result.FilePath.Split('/');
				string ImageName = obj[obj.Length - 1];

				//Assembly assembly = typeof(NameOfClass).GetTypeInfo().Assembly;

				//byte[] buffer;
				//using (Stream stream = assembly.GetManifestResourceStream(imagePath))
				//{
				//  long length = stream.Length;
				//  buffer = new byte[length];
				//  stream.Read(buffer, 0, (int)length);

				//  var storeragePath = await iStorageService.SaveBinaryObjectToStorageAsync(string.Format(FileNames.ApplicationIcon, app.ApplicationId), buffer);
				//  app.IconURLLocal = storeragePath;
				//}

			}
			else
				MessagingCenter.Send<CamaraSampleNewPageViewModel>(this, "Error1");

		}
		private async void ExecuteChooseVideoCommand()
		{
			var result = await MediaService.Instance.OpenMediaPickerAsync(MediaType.Video);
			if (result.IsSuccess)
				MessagingCenter.Send<CamaraSampleNewPageViewModel>(this, "Sucess");
			else
				MessagingCenter.Send<CamaraSampleNewPageViewModel>(this, "Error1");
		}
        private async void ExecuteChooseAudioCommand()
        {
            var result = await MediaService.Instance.OpenMediaPickerAsync(MediaType.Audio);
            if (result.IsSuccess)
                MessagingCenter.Send<CamaraSampleNewPageViewModel>(this, "Sucess");
            else
                MessagingCenter.Send<CamaraSampleNewPageViewModel>(this, "Error1");
        }
			public CamaraSampleNewPageViewModel()
        {
        }
    }
}
