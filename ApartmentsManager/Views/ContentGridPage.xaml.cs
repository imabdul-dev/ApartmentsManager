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
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            await ViewModel.LoadDataAsync();
        }
    }
}
