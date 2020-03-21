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

            //mainGrid.MinWidth = 1980;
            //mainGrid.MinHeight = 1080;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            await ViewModel.LoadDataAsync();
        }
    }
}
