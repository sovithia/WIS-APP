using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibVLCSharp.Shared;
using MediaManager;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WIS.Views
{
    public enum PlayState
    {
        STOPPED,
        PLAYING,
        PAUSED
    }
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IncomingAudioTemplate : ViewCell
    {
        PlayState state;
        public IncomingAudioTemplate()
        {
            InitializeComponent();
            state = PlayState.STOPPED;
            actionBtn.ImageSource = "Play";
            //VideoView1.MediaPlayer = new MediaPlayer(_libvlc);
            //using (var media = new Media(_libvlc, new Uri(VIDEO_URL)))
            //    VideoView1.MediaPlayer.Play(media);

            //mediaPlayerElement.MediaPlayer = mediaPlayer;
            //MediaPlayer.Play();


        }

        MediaPlayer mediaPlayer;

        void ActionClickedAsync(System.Object sender, System.EventArgs e)
        {
            Button b = (Button)sender;
            string path = (string)b.CommandParameter;
            /*
            var media = new Media(new LibVLC(),
               new Uri("http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/ElephantsDream.mp4"));
            MediaPlayer mediaPlayer = new MediaPlayer(media) { EnableHardwareDecoding = true };
            media.Dispose();
            */
            
            if (state == PlayState.STOPPED)
            {
                //FileInfo file = new FileInfo(path);
                if (path != "")
                {
                    var media = new Media(new LibVLC(),new Uri(path));
                    mediaPlayer = new MediaPlayer(media) { EnableHardwareDecoding = true };
                    media.Dispose();
                    mediaPlayer.Play();
                    
                    /*
                    string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                    string localFilename = "voice/6064363332255613843.oga";
                    string localPath = Path.Combine(documentsPath, localFilename);
                    var pathToFileURL = new System.Uri(localPath).AbsolutePath;
                    */
                    //await CrossMediaManager.Current.Play("file://" + pathToFileURL);

                    //var pathToFileURL = new System.Uri(path).AbsolutePath;
                    //System.Environment.SpecialFolder.MyDocuments()
                    //await CrossMediaManager.Current.Play("file://" + "voice.mp3");
                    actionBtn.ImageSource = "Pause";
                    state = PlayState.PLAYING;
                }                                                        
                
            }              
            else if (state == PlayState.PLAYING)
            {
                mediaPlayer.Pause();                
                actionBtn.ImageSource = "Play";
                state = PlayState.PAUSED;
            }
            else if (state == PlayState.PAUSED)
            {
                mediaPlayer.Play();                
                actionBtn.ImageSource = "Pause";
                state = PlayState.PLAYING;
            }                        
        }

       

     
    }
}
