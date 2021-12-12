using WIS.ViewModels.About;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace WIS.Views
{
    /// <summary>
    /// About us with parallax scroll page.
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AboutPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:WIS.Views.AboutUsWithParallaxScrollPage"/> class.
        /// </summary>
        public AboutPage()
        {
            this.InitializeComponent();
            this.BindingContext = AboutUsViewModel.BindingContext;
        }
    }
}