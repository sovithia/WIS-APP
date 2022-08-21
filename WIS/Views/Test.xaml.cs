using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Syncfusion.XForms.PopupLayout;
using WIS.Interfaces;
using WIS.Models;
using Xamarin.Forms;

namespace WIS.Views
{
    public class User
    {
        public string Name { get; set; }
        public string Initials => new string(Name.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(item => item.FirstOrDefault()).ToArray());
    }


    public partial class Test : ContentPage
    {
        //AudioRecorderService recorder;
        //AudioPlayer player;
        public Test()
        {
            InitializeComponent();

            /*
            image.Source = new AvatarImageSource("GC", Color.DarkSlateBlue, Color.White, 48);                       
            UsersListView.ItemsSource = new List<User>()
            {
                new User() {Name = "Ahmed Fouad"},
                new User() {Name = "Elif Vo"},
                new User() {Name = "Nur Byers"}
            };
            */
            //image.Source = new AvatarImageSource("AA",Color.Red,Color.Yellow);
            /*
            recorder =  new AudioRecorderService
            {
                StopRecordingOnSilence = true, //will stop recording after 2 seconds (default)
                StopRecordingAfterTimeout = true,  //stop recording after a max timeout (defined below)
                TotalAudioTimeout = TimeSpan.FromSeconds(15) //audio will stop recording after 15 seconds
            };
            player = new AudioPlayer();
            */
        }
        private void Button_Clicked(object sender, EventArgs e)
        {
            popUpLayout.Show();
        }
        /*

        void pictureFromCameraBtn(System.Object sender, System.EventArgs e)
        {
        }

        void pictureFromGalleryBtn(System.Object sender, System.EventArgs e)
        {
        }

        protected async void showCameraAsync(object sender, System.EventArgs e)
        {
            
            var photo = await Plugin.Media.CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions()
            {
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Full,
                SaveMetaData = true,

            });
            if (photo != null)
            {
                Stream s = photo.GetStreamWithImageRotatedForExternalStorage();
                byte[] bytes;
                var memoryStream = new MemoryStream();
                s.CopyTo(memoryStream);
                bytes = memoryStream.ToArray();

                //image.Source = ImageSource.FromStream(() => new MemoryStream(bytes));
                //this.base64Image = Convert.ToBase64String(bytes);
            }

        }

        protected async void showLibraryAsync(object sender, System.EventArgs e)
        {
            

            Stream stream = await DependencyService.Get<IPhotoPickerService>().GetImageStreamAsync();
            if (stream != null)
            {
                byte[] bytes;
                var memoryStream = new MemoryStream();
                stream.CopyTo(memoryStream);
                bytes = memoryStream.ToArray();
                //image.Source = ImageSource.FromStream(() => new MemoryStream(bytes));
                //this.base64Image = Convert.ToBase64String(bytes);
            }
        }


        // VOICE

        async Task RecordAudio()
        {
            try
            {
                if (!recorder.IsRecording)
                {
                    recorder.AudioInputReceived += Recorder_AudioInputReceived;
                    await recorder.StartRecording();
                }
                else
                {
                    await recorder.StopRecording();
                }
            }
            catch (Exception ex)
            {
            
	        }
        }

        void Play_Clicked(object sender, EventArgs e)
        {
            PlayAudio();
        }

        void PlayAudio()
        {
            try
            {
                var filePath = recorder.GetAudioFilePath();

                if (filePath != null)
                {
                   

                    player.Play(filePath);
                }
            }
            catch (Exception ex)
            {
                //blow up the app!
                throw ex;
            }
        }
        private void Recorder_AudioInputReceived(object sender, string audioFile)
        {
            audiopath.Text = audioFile;
            //do something with the file
        }

        async void recordVoiceBtn(System.Object sender, System.EventArgs e)
        {
            await RecordAudio();
        }

        async void stopRecordBtn(System.Object sender, System.EventArgs e)
        {
            if (recorder.IsRecording)
                await recorder.StopRecording();
        }

        */
    }
}
