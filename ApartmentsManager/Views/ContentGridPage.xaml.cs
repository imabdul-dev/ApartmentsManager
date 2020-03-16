using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
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

        //Design limitation
        //Base button command can not be implemented in template row in uwp bacause find ancestor type not supported in UWP.
        private void UIElement_OnTapped(object sender, TappedRoutedEventArgs e)
        {
            var tag = ((FontIcon) sender).Tag.ToString();
            int unitRef = Convert.ToInt32(tag);
            ViewModel.OnItemClick(unitRef);
        }
    }
}
