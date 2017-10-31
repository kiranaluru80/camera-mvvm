using System;
using System.IO;
using Xamarians.Media;
using Xamarin.Forms;

namespace CamaraSampleNew
{
    public partial class CamaraSampleNewPage : ContentPage
    {
        public CamaraSampleNewPage()
        {
            InitializeComponent();
			this.BindingContext = new CamaraSampleNewPageViewModel();
			MessagingCenter.Subscribe<CamaraSampleNewPageViewModel>(this, "Error1", (sender) =>
			{
                DisplayAlert("Error","Error Message 1","OK");
			});

			MessagingCenter.Subscribe<CamaraSampleNewPageViewModel>(this, "Sucess", (sender) =>
			{
				DisplayAlert("Error", "you are success", "OK");
			});
        }

		
       
		private string GenerateFilePath()
		{
			return Path.Combine(MediaService.Instance.GetPublicDirectoryPath(), MediaService.Instance.GenerateUniqueFileName("jpg"));
		}

		private async void TakePhoto_Clicked(object sender, EventArgs e)
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
                image.Source = result.FilePath;

                FileImageSource obj = new FileImageSource();
            }
			else
				await DisplayAlert("Error", result.Message, "OK");
		}

		private async void ChooseImage_Clicked(object sender, EventArgs e)
		{
            MediaResult result = await MediaService.Instance.OpenMediaPickerAsync(MediaType.Image);
			if (result.IsSuccess)
            {
               image.Source = result.FilePath;
                String[] obj = result.FilePath.Split('/');
                string ImageName = obj[obj.Length - 1];

				//Assembly assembly = typeof(NameOfClass).GetTypeInfo().Assembly;

				//byte[] buffer;
				//using (Stream stream = assembly.GetManifestResourceStream(imagePath))
				//{
				//	long length = stream.Length;
				//	buffer = new byte[length];
				//	stream.Read(buffer, 0, (int)length);

				//	var storeragePath = await iStorageService.SaveBinaryObjectToStorageAsync(string.Format(FileNames.ApplicationIcon, app.ApplicationId), buffer);
				//	app.IconURLLocal = storeragePath;
				//}
  
            }
			else
				await DisplayAlert("Error", result.Message, "OK");
		}
      


		private async void ChooseVideo_Clicked(object sender, EventArgs e)
		{
			var result = await MediaService.Instance.OpenMediaPickerAsync(MediaType.Video);
			if (result.IsSuccess)
				await DisplayAlert("Success", result.FilePath, "OK");
			else
				await DisplayAlert("Error", result.Message, "OK");
		}


		private async void ChooseAudio_Clicked(object sender, EventArgs e)
		{
			var result = await MediaService.Instance.OpenMediaPickerAsync(MediaType.Audio);
			if (result.IsSuccess)
				await DisplayAlert("Success", result.FilePath, "OK");
			else
				await DisplayAlert("Error", result.Message, "OK");
		}

		private async void ResizeImage_Clicked(object sender, EventArgs e)
		{
			var result = await MediaService.Instance.OpenMediaPickerAsync(MediaType.Image);
			string resizeFilePath = GenerateFilePath();
			var success = await MediaService.Instance.ResizeImageAsync(result.FilePath, resizeFilePath, 250, 250);
			if (success)
			{
				image.Source = resizeFilePath;
			}
		}
	}
}


