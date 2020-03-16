using System.Windows.Input;
using ApartmentsManager.Helpers;
using ApartmentsManager.Models;
using ApartmentsManager.Services;

namespace ApartmentsManager.ViewModels
{
    public class ContentGridDetailViewModel : Observable
    {
        #region Properties

        private SampleApartment _item;
        public SampleApartment Item
        {
            get { return _item; }
            set { Set(ref _item, value); }
        }

        public string ItemDetail => Item.ToString();

        #endregion

        #region Commands

        private ICommand _goBackCommand;
        public ICommand GoBackCommand => _goBackCommand ?? (_goBackCommand = new RelayCommand(GoBackCommand_));

        #endregion

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
