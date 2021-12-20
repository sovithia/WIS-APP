using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AppCenter.Analytics;
using Plugin.Media;
using Syncfusion.XForms.SignaturePad;
using WIS.Interfaces;
using WIS.Models;
using WIS.Services;
using WIS.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace WIS.ViewModels
{
    public class InvoiceDetailsPageViewModel : BaseViewModel
    {

#region properties

        public bool IsSubmitable { get; set; }

        private ObservableCollection<InvoiceElement> invoiceLines;
        public ObservableCollection<InvoiceElement> InvoiceLines
        {
            get {
                return invoiceLines;
            }
            set
            {
                this.SetProperty(ref this.invoiceLines, value);
            }
        }

        private string total;
        public string Total
        {
            get
            {
                return total;
            }
            set
            {
                this.SetProperty(ref this.total, value);
            }
        }

        public string invoiceID;
        public string InvoiceID
        {
            get
            {
                return "Invoice No:" + invoiceID;
            }
            set
            {
                this.SetProperty(ref this.invoiceID, value);
            }
        }

        public bool IsParent { get; set; }
        public bool IsRegistrar
        {
            get
            {
                return !IsParent;
            }
        }
        #endregion


        public string signatureB64 {
            get {
                signaturePad.Save();
                StreamImageSource streamImageSource = (StreamImageSource)signaturePad.ImageSource;
                System.Threading.CancellationToken cancellationToken =
                    System.Threading.CancellationToken.None;
                Task<Stream> task = streamImageSource.Stream(cancellationToken);
                Stream stream = task.Result;
                // store bytes
                byte[] bytes = new byte[stream.Length];
                stream.Read(bytes, 0, bytes.Length);
                return Convert.ToBase64String(bytes, 0, bytes.Length);
            }
        }

        public string proofB64
        {
            get {
                return Convert.ToBase64String(byteProof, 0, byteProof.Length);
            }
        }

        
        

        SfSignaturePad signaturePad;


        private Invoice currentInvoice;


        private ImageSource proofImageIn;
        public ImageSource ProofImageIn
        {
            get{
                return proofImageIn;  
            }
            set{
                this.SetProperty(ref this.proofImageIn, value);
            }
            
        }

        private ImageSource parentsigImageIn;
        public ImageSource ParentSigImageIn{
            get{
                return parentsigImageIn;
            }
            set{
                this.SetProperty(ref this.parentsigImageIn, value);
            }
        }

        private ImageSource registrarsigImageIn;
        public ImageSource RegistrarSigImageIn
        {
            get{
                return registrarsigImageIn;
            }
            set{
                this.SetProperty(ref this.registrarsigImageIn, value);
            }
        }

      

        public string AcknowledgmentText { get; set; }
        public InvoiceDetailsPageViewModel(string _invoiceID,SfSignaturePad pad)
        {
            InvoiceID = _invoiceID;
            signaturePad = pad;
            submitCommand = new Command(submitClick);
            showGalleryCommand = new Command<System.EventArgs>(showGallery);
            showCameraCommand = new Command<System.EventArgs>(showCamera);
            submitCommand = new Command(submitClick);
            showPictureZoomCommand = new Command(showPictureZoom);
           
            string type = Preferences.Get("TYPE", "");
            if (type == "PARENT")
            {
                IsParent = true;
                IsSubmitable = true;
                AcknowledgmentText = "I acknowledge, that I have paid and attach the payment proof.";
            }
            else
            {
                IsParent = false;
                AcknowledgmentText = "I verified the payment and validate it.";                
            }
                
            float amt = 0;
            DataService.Instance.GetInvoiceDetails(_invoiceID, (invoice) =>
             {
                

                 ObservableCollection<InvoiceElement> tmp = new ObservableCollection<InvoiceElement>();
                 foreach (InvoiceElement line in invoice.invoiceelementList)
                 {
                     amt += float.Parse(line.amount);
                     tmp.Add(line);
                 }
                 Total = "$ " + amt.ToString();    
                 InvoiceLines = tmp;
                 
             });
                     
        }
   
        public Command submitCommand { get; set; }
        public Command showGalleryCommand { get; set; }
        public Command showCameraCommand { get; set; }
        public Command showPictureZoomCommand { get; set; }

        private void submitClick(object obj)
        {

            Analytics.TrackEvent(this.GetType().ToString() + " submitClick");
            if (IsParent == true)
            {
                DataService.Instance.SubmitInvoice(invoiceID, signatureB64, proofB64, (success) =>
                {
                    if (success)
                    {
                        Device.BeginInvokeOnMainThread(async () => {
                            await AppShell.Current.DisplayAlert("Success", "Invoice submitted", "OK");
                            await AppShell.Current.Navigation.PopAsync();
                        });
                    }
                    else
                    {
                        Device.BeginInvokeOnMainThread(async () => {
                            await AppShell.Current.DisplayAlert("Error", "Error submitting attendance", "OK");
                        });
                    }
                });

            }
            else
            {

                DataService.Instance.ValidateInvoice(invoiceID, signatureB64, (success) =>
                {
                    if (success)
                    {
                        Device.BeginInvokeOnMainThread(async () => {
                            await AppShell.Current.DisplayAlert("Success", "Invoice validated", "OK");
                            await AppShell.Current.Navigation.PopAsync();
                        });
                    }
                    else
                    {
                        Device.BeginInvokeOnMainThread(async () => {
                            await AppShell.Current.DisplayAlert("Error", "Error validating attendance", "OK");
                        });
                    }
                });
            }
            
        }

        public byte[] byteProof;
        private ImageSource proofImageOut; 
        public ImageSource ProofImageOut
        {
            get{
                return this.proofImageOut;
            }
            set{
                this.SetProperty(ref this.proofImageOut, value);
            }
        }

        protected void showPictureZoom(object obj)
        {
            ImageSource source = (ImageSource)obj;
            if (source != null)
            {
                PictureZoomPage page = new PictureZoomPage(source);
                AppShell.Current.Navigation.PushModalAsync(page);
            }            
        }

        



        protected async void showGallery(System.EventArgs e)
        {
            Stream stream = await DependencyService.Get<IPhotoPickerService>().GetImageStreamAsync();
            if (stream != null)
            {
                var memoryStream = new MemoryStream();
                stream.CopyTo(memoryStream);
                byteProof = memoryStream.ToArray();
                ProofImageOut = ImageSource.FromStream(() => new MemoryStream(byteProof));
            }
        }

        protected async void showCamera(System.EventArgs e)
        {
         
            var photo = await Plugin.Media.CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions()
            {
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Full,
                SaveMetaData = true,

            });
            if (photo != null)
            {
                Stream s = photo.GetStreamWithImageRotatedForExternalStorage();
                var memoryStream = new MemoryStream();
                s.CopyTo(memoryStream);
                byteProof = memoryStream.ToArray();
                ProofImageOut = ImageSource.FromStream(() => new MemoryStream(byteProof));
            }

        }

    }
}
