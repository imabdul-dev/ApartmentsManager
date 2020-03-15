using App1.Helpers;
using App1.Models;
using App1.Services;
using System.Windows.Input;

namespace App1.ViewModels
{
    public class ContentGridDetailViewModel : Observable
    {
        private SampleApartment _item;
        private ICommand _goBackCommand;

        public ICommand GoBackCommand => _goBackCommand ?? (_goBackCommand = new RelayCommand(GoBackCommand_));


        public SampleApartment Item
        {
            get { return _item; }
            set { Set(ref _item, value); }
        }

        public void InitializeAsync(SampleApartment apartment)
        {
            Item = apartment;
        }

        private void GoBackCommand_()
        {
            NavigationService.GoBack();
        }
    }
}
