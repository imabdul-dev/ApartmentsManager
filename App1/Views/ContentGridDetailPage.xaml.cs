using App1.Services;
using App1.ViewModels;
using Microsoft.Toolkit.Uwp.UI.Animations;
using Windows.UI.Xaml.Navigation;
using App1.Models;

namespace App1.Views
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
