using Windows.Graphics.Display;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml.Navigation;
using ApartmentsManager.ViewModels;

namespace ApartmentsManager.Views
{
    public sealed partial class ContentGridPage
    {
        public ContentGridViewModel ViewModel { get; } = new ContentGridViewModel();

        public ContentGridPage()
        {
            InitializeComponent();
            var applicationView = ApplicationView.GetForCurrentView();
            var displayInformation = DisplayInformation.GetForCurrentView();
            var bounds = applicationView.VisibleBounds;
            var scale = displayInformation.RawPixelsPerViewPixel;

            mainGrid.Width = bounds.Width * scale;
            mainGrid.Height = bounds.Height * scale;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            await ViewModel.LoadDataAsync();
        }
    }
}
