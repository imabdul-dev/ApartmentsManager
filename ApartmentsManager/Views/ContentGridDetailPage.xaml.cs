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
