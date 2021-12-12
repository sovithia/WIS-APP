using WIS.Models;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace WIS.Views
{
    /// <summary>
    /// Implements the message data template selector class.
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public class MessageDataTemplateSelector : DataTemplateSelector
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageDataTemplateSelector" /> class.
        /// </summary>
        public MessageDataTemplateSelector()
        {
            this.IncomingTextTemplate = new DataTemplate(typeof(IncomingTextTemplate));
            this.OutgoingTextTemplate = new DataTemplate(typeof(OutgoingTextTemplate));

            this.IncomingImageTemplate = new DataTemplate(typeof(IncomingImageTemplate));
            this.OutgoingImageTemplate = new DataTemplate(typeof(OutgoingImageTemplate));

            this.IncomingAudioTemplate = new DataTemplate(typeof(IncomingAudioTemplate));
            this.OutgoingAudioTemplate = new DataTemplate(typeof(OutgoingAudioTemplate));

            this.IncomingVideoTemplate = new DataTemplate(typeof(IncomingVideoTemplate));
            this.OutgoingVideoTemplate = new DataTemplate(typeof(OutgoingVideoTemplate));

            this.IncomingStickerTemplate = new DataTemplate(typeof(IncomingStickerTemplate));
            this.OutgoingStickerTemplate = new DataTemplate(typeof(OutgoingStickerTemplate));

            this.IncomingUnsupportedTemplate = new DataTemplate(typeof(IncomingUnsupportedTemplate));
            this.OutgoingUnsupportedTemplate = new DataTemplate(typeof(OutgoingUnsupportedTemplate));
        }
        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the incoming text template.
        /// </summary>
        public DataTemplate IncomingTextTemplate { get; set; }

        /// <summary>
        /// Gets or sets the outgoing text template.
        /// </summary>
        public DataTemplate OutgoingTextTemplate { get; set; }

        /// <summary>
        /// Gets or sets the incoming image template.
        /// </summary>
        public DataTemplate IncomingImageTemplate { get; set; }

        /// <summary>
        /// Gets or sets the outgoing text template.
        /// </summary>
        public DataTemplate OutgoingImageTemplate { get; set; }

        /// <summary>
        /// Gets or sets the incoming audio template.
        /// </summary>
        public DataTemplate IncomingAudioTemplate { get; set; }

        /// <summary>
        /// Gets or sets the outgoing audio template.
        /// </summary>
        public DataTemplate OutgoingAudioTemplate { get; set; }

        /// <summary>
        /// Gets or sets the incoming video template.
        /// </summary>
        public DataTemplate IncomingVideoTemplate { get; set; }

        /// <summary>
        /// Gets or sets the outgoing video template.
        /// </summary>
        public DataTemplate OutgoingVideoTemplate { get; set; }

        /// <summary>
        /// Gets or sets the incoming video template.
        /// </summary>
        public DataTemplate IncomingStickerTemplate { get; set; }

        /// <summary>
        /// Gets or sets the outgoing video template.
        /// </summary>
        public DataTemplate OutgoingStickerTemplate { get; set; }

        /// <summary>
        /// Gets or sets the incoming video template.
        /// </summary>
        public DataTemplate IncomingUnsupportedTemplate { get; set; }

        /// <summary>
        /// Gets or sets the outgoing video template.
        /// </summary>
        public DataTemplate OutgoingUnsupportedTemplate { get; set; }


        #endregion

        #region Methods

        /// <summary>
        /// Returns the incoming or outgoing text template.
        /// </summary>
        /// <param name="item">The item</param>
        /// <param name="container">The bindable object</param>
        /// <returns>Returns the data template</returns>
        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (item != null && ((ChatMessage)item).IsReceived)
            {
                if (((ChatMessage)item).TYPE == ChatMessageType.TEXT)
                    return this.IncomingTextTemplate;
                else if (((ChatMessage)item).TYPE == ChatMessageType.PHOTO)
                    return this.IncomingImageTemplate;
                else if (((ChatMessage)item).TYPE == ChatMessageType.AUDIO)
                    return this.IncomingAudioTemplate;
                else if (((ChatMessage)item).TYPE == ChatMessageType.VIDEO)
                    return this.IncomingVideoTemplate;
                else if (((ChatMessage)item).TYPE == ChatMessageType.STICKER)
                    return this.IncomingStickerTemplate;
                else
                    return this.IncomingUnsupportedTemplate;
            }
            else
            {
                if (((ChatMessage)item).TYPE == ChatMessageType.TEXT)
                    return this.OutgoingTextTemplate;
                else if (((ChatMessage)item).TYPE == ChatMessageType.PHOTO)
                    return this.OutgoingImageTemplate;
                else if (((ChatMessage)item).TYPE == ChatMessageType.AUDIO)
                    return this.OutgoingAudioTemplate;
                else if (((ChatMessage)item).TYPE == ChatMessageType.VIDEO)
                    return this.OutgoingVideoTemplate;
                else if (((ChatMessage)item).TYPE == ChatMessageType.STICKER)
                    return this.OutgoingStickerTemplate;
                else
                    return this.OutgoingUnsupportedTemplate;
            }
         
        }

        #endregion
    }
}
