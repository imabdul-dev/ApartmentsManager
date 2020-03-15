using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using ApartmentsManager.Helpers;
using ApartmentsManager.Models;
using ApartmentsManager.Services;
using ApartmentsManager.Views;
using Microsoft.Toolkit.Uwp.UI.Animations;

namespace ApartmentsManager.ViewModels
{
    public class ContentGridViewModel : Observable
    {
        #region Properties

        #region Apartments

        public List<SampleApartment> _source;

        private List<SampleApartment> _apartments;
        public List<SampleApartment> Apartments
        {
            get => _apartments;
            set
            {
                Set(ref _apartments, value);
            }
        }

        private bool IsReset
        {
            get
            {
                //|| IsAvailable || IsUnavailable || IsReserved || MinFloors > 0 || MaxFloors > 0 || MinPrice > 0 || MaxPrice > 0
                return !BlockA && !BlockB && !BlockC && !BlockD && !BlockE && !IsAvailable && !IsUnavailable && !IsReserved && !OneBedRoom && !TwoBedRooms && !ThreeBedRooms && !FourBedRooms;
            }
        }

        #endregion

        #region Apartment Blocks

        private bool _blockA;
        public bool BlockA
        {
            get => _blockA;
            set
            {
                _blockA = value;
                OnPropertyChanged(nameof(BlockA));
                FilterByBlock('A', value);
            }
        }

        private bool _blockB;
        public bool BlockB
        {
            get => _blockB;
            set
            {
                _blockB = value;
                OnPropertyChanged(nameof(BlockB));
                FilterByBlock('B', value);
            }
        }

        private bool _blockC;
        public bool BlockC
        {
            get => _blockC;
            set
            {
                _blockC = value;
                OnPropertyChanged(nameof(BlockC));
                FilterByBlock('C', value);
            }
        }

        private bool _blockD;
        public bool BlockD
        {
            get => _blockD;
            set
            {
                _blockD = value;
                OnPropertyChanged(nameof(BlockD));
                FilterByBlock('D', value);
            }
        }

        private bool _blockE;
        public bool BlockE
        {
            get => _blockE;
            set
            {
                _blockE = value;
                OnPropertyChanged(nameof(BlockE));
                FilterByBlock('E', value);
            }
        }

        #endregion

        #region Price Range

        private int _minPrice;
        public int MinPrice
        {
            get => _minPrice;
            set
            {
                Set(ref _minPrice, value);
            }
        }

        private int _maxPrice;
        public int MaxPrice
        {
            get => _maxPrice;
            set
            {
                Set(ref _maxPrice, value);
            }
        }

        #endregion

        #region Floors

        private int _minfloors;
        public int MinFloors
        {
            get => _minfloors;
            set
            {
                Set(ref _minfloors, value);
            }
        }

        private int _maxfloors;
        public int MaxFloors
        {
            get => _maxfloors;
            set
            {
                Set(ref _maxfloors, value);
            }
        }

        #endregion

        #region Status

        private bool _isAvailable;
        public bool IsAvailable
        {
            get => _isAvailable;
            set
            {
                _isAvailable = value;
                OnPropertyChanged(nameof(IsAvailable));
                FilterByAvailibility(Status.Available, value);
            }
        }

        private bool _isUnavailable;
        public bool IsUnavailable
        {
            get => _isUnavailable;
            set
            {
                _isUnavailable = value;
                OnPropertyChanged(nameof(IsUnavailable));
                FilterByAvailibility(Status.Unavailable, value);
            }
        }

        private bool _isReserved;
        public bool IsReserved
        {
            get => _isReserved;
            set
            {
                _isReserved = value;
                OnPropertyChanged(nameof(IsReserved));
                FilterByAvailibility(Status.Reserved, value);
            }
        }

        #endregion

        #region BedRooms

        private bool _oneBedRoom;
        public bool OneBedRoom
        {
            get => _oneBedRoom;
            set
            {
                _oneBedRoom = value;
                OnPropertyChanged(nameof(OneBedRoom));
                FilterByRooms(1, value);
            }
        }

        private bool _twoBedRooms;
        public bool TwoBedRooms
        {
            get => _twoBedRooms;
            set
            {
                _twoBedRooms = value;
                OnPropertyChanged(nameof(TwoBedRooms));
                FilterByRooms(2, value);
            }
        }

        private bool _threeBedRooms;
        public bool ThreeBedRooms
        {
            get => _threeBedRooms;
            set
            {
                _threeBedRooms = value;
                OnPropertyChanged(nameof(ThreeBedRooms));
                FilterByRooms(3, value);
            }
        }

        private bool _fourBedRooms;
        public bool FourBedRooms
        {
            get => _fourBedRooms;
            set
            {
                _fourBedRooms = value;
                OnPropertyChanged(nameof(FourBedRooms));
                FilterByRooms(4, value);
            }
        }

        #endregion

        #endregion

        #region Commands

        private ICommand _itemClickCommand;
        public ICommand ItemClickCommand => _itemClickCommand ?? (_itemClickCommand = new RelayCommand<SampleApartment>(OnItemClick));

        private ICommand _resetFilterApartmentsCommand;
        public ICommand ResetFilterApartmentsCommand => _resetFilterApartmentsCommand ?? (_resetFilterApartmentsCommand = new RelayCommand(ResetFilterApartments));

        #endregion

        #region Constructor

        public ContentGridViewModel()
        {
            _source = new List<SampleApartment>();
            Apartments = new List<SampleApartment>();
        }

        #endregion

        public async Task LoadDataAsync()
        {
            _source.Clear();
            Apartments.Clear();

            var data = await SampleDataService.GetApartmentsDataAsync();
            _source = new List<SampleApartment>(data);
            Apartments = new List<SampleApartment>(_source);
        }

        private void OnItemClick(SampleApartment clickedItem)
        {
            if (clickedItem != null)
            {
                NavigationService.Frame.SetListDataItemForNextConnectedAnimation(clickedItem);
                NavigationService.Navigate<ContentGridDetailPage>(clickedItem);
            }
        }

        private void ResetFilterApartments()
        {

        }

        private void FilterApartments()
        {
            var apartmentsForFilter  = new List<SampleApartment>();
            if (IsReset)
            {
                Apartments.Clear();
                Apartments = _source;
            }
            if ((BlockA || BlockB || BlockC || BlockD || BlockE || IsAvailable || IsUnavailable || IsReserved || MinFloors > 0 || MaxFloors > 0 || MinPrice > 0 || MaxPrice > 0) && !IsReset)
            {
                if (BlockA)
                {
                    var items = _source.Where(x => x.Block == 'A').ToList();
                    apartmentsForFilter.AddRange(items);
                }
                //else
                //{
                //    var removeItems = _source.Where(x => x.Block == 'A');
                //    var apps = new List<SampleApartment>(Apartments);
                //    foreach (var item in removeItems)
                //    {
                //        apps.Remove(item);
                //    }

                //    Apartments = apps;
                //}

                //if (BlockB) Apartments.AddRange(_source.Where(x => x.Block == 'B'));
                //if (BlockC) Apartments.AddRange(_source.Where(x => x.Block == 'C'));
                //if (BlockD) Apartments.AddRange(_source.Where(x => x.Block == 'D'));
                //if (BlockE) Apartments.AddRange(_source.Where(x => x.Block == 'E'));
            }
            else
            {
                Apartments.Clear();
                Apartments = _source;
            }
        }

        private void FilterByBlock(char block, bool isFilter)
        {
            if (IsReset)
            {
                Apartments = new List<SampleApartment>(_source);
            }
            else if (isFilter)
            {
                var apartmentsForFilter = Apartments.Count == _source.Count ? new List<SampleApartment>() : new List<SampleApartment>(Apartments);

                var items = _source.Where(x => x.Block == block).ToList();
                if (items.Count > 0)
                {
                    apartmentsForFilter.AddRange(items);
                    Apartments = new List<SampleApartment>(apartmentsForFilter.OrderBy(x => x.Block));
                }
            }
            else
            {
                var apartmentsForFilter = new List<SampleApartment>();
                var items = Apartments.Where(x => x.Block != block).ToList();
                if (items.Count > 0)
                {
                    apartmentsForFilter.AddRange(items);
                    Apartments = new List<SampleApartment>(apartmentsForFilter.OrderBy(x => x.Block));
                }
            }
        }

        private void FilterByAvailibility(Status availibilityStatus, bool isFilter)
        {
            if (IsReset)
            {
                Apartments = new List<SampleApartment>(_source);
            }
            else if (isFilter)
            {
                var apartmentsForFilter = Apartments.Count == _source.Count ? new List<SampleApartment>() : new List<SampleApartment>(Apartments);

                var items = _source.Where(x => x.Status == availibilityStatus).ToList();
                if (items.Count > 0)
                {
                    apartmentsForFilter.AddRange(items);
                    Apartments = new List<SampleApartment>(apartmentsForFilter.OrderBy(x => x.Block));
                }
            }
            else
            {
                var apartmentsForFilter = new List<SampleApartment>();
                var items = Apartments.Where(x => x.Status != availibilityStatus).ToList();
                if (items.Count > 0)
                {
                    apartmentsForFilter.AddRange(items);
                    Apartments = new List<SampleApartment>(apartmentsForFilter.OrderBy(x => x.Block));
                }
            }
        }

        private void FilterByRooms(int rooms, bool isFilter)
        {
            if (IsReset)
            {
                Apartments = new List<SampleApartment>(_source);
            }
            else if (isFilter)
            {
                var apartmentsForFilter = Apartments.Count == _source.Count ? new List<SampleApartment>() : new List<SampleApartment>(Apartments);

                var items = _source.Where(x => x.Bedrooms == rooms).ToList();
                if (items.Count > 0)
                {
                    apartmentsForFilter.AddRange(items);
                    Apartments = new List<SampleApartment>(apartmentsForFilter.OrderBy(x => x.Block));
                }
            }
            else
            {
                var apartmentsForFilter = new List<SampleApartment>();
                var items = Apartments.Where(x => x.Bedrooms != rooms).ToList();
                if (items.Count > 0)
                {
                    apartmentsForFilter.AddRange(items);
                    Apartments = new List<SampleApartment>(apartmentsForFilter.OrderBy(x => x.Block));
                }
            }
        }
    }
}
