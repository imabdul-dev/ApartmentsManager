using Windows.Graphics.Display;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml.Navigation;
using ApartmentsManager.Models;
using ApartmentsManager.Services;
using ApartmentsManager.ViewModels;
using Microsoft.Toolkit.Uwp.UI.Animations;

namespace ApartmentsManager.Views
{
    public sealed partial class ContentGridDetailPage
    {
        public ContentGridDetailViewModel ViewModel { get; } = new ContentGridDetailViewModel();

        public ContentGridDetailPage()
        {
            InitializeComponent();
            var applicationView = ApplicationView.GetForCurrentView();
            var displayInformation = DisplayInformation.GetForCurrentView();
            var bounds = applicationView.VisibleBounds;
            var scale = displayInformation.RawPixelsPerViewPixel;

            mainGrid.Width = bounds.Width * scale;
            mainGrid.Height = bounds.Height * scale;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.Parameter is SampleApartment apartment)
            {
                ViewModel.InitializeAsync(apartment);
            }
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);
            if (e.NavigationMode == NavigationMode.Back)
            {
                NavigationService.Frame.SetListDataItemForNextConnectedAnimation(ViewModel.Item);
            }
        }
    }
}
