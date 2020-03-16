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
                return !BlockA && !BlockB && !BlockC && !BlockD && !BlockE && !IsAvailable && !IsUnavailable &&
                       !IsReserved && !OneBedRoom && !TwoBedRooms && !ThreeBedRooms && !FourBedRooms
                       && !(MinPrice > 100000 || MaxPrice < 1000000) && !(MinFloors > 0 || MaxPrice < MaxFloors);
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
                FilterApartments();
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
                FilterApartments();
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
                FilterApartments();
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
                FilterApartments();
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
                FilterApartments();
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
                _minPrice = value;
                OnPropertyChanged(nameof(MinPrice));
                FilterApartments();
            }
        }

        private int _maxPrice;
        public int MaxPrice
        {
            get => _maxPrice;
            set
            {
                _maxPrice = value;
                OnPropertyChanged(nameof(MaxPrice));
                FilterApartments();
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
                _minfloors = value;
                OnPropertyChanged(nameof(MinFloors));
                FilterApartments();
            }
        }

        private int _maxfloors;
        public int MaxFloors
        {
            get => _maxfloors;
            set
            {
                _maxfloors = value;
                OnPropertyChanged(nameof(MaxFloors));
                FilterApartments();
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
                FilterApartments();
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
                FilterApartments();
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
                FilterApartments();
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
                FilterApartments();
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
                FilterApartments();
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
                FilterApartments();
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
                FilterApartments();
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
            MinPrice = 100000;
            MaxPrice = 1000000;
            MinFloors = 0;
            MaxFloors = 100;
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
            if (IsReset)
            {
                Apartments = new List<SampleApartment>(_source);
            }
            else
            {
                var apartments1 = new List<SampleApartment>();
                var apartments2 = new List<SampleApartment>();
                var apartments3 = new List<SampleApartment>();
                var isfiltered = false;

                //Filter by blocks
                if (BlockA)
                {
                    var items = _source.Where(x => x.Block == 'A');
                    if (items.Any())
                    {
                        apartments1.AddRange(items);
                        isfiltered = true;
                    }
                }
                if (BlockB)
                {
                    var items = _source.Where(x => x.Block == 'B');
                    if (items.Any())
                    {
                        apartments1.AddRange(items);
                        isfiltered = true;
                    }
                }
                if (BlockC)
                {
                    var items = _source.Where(x => x.Block == 'C');
                    if (items.Any())
                    {
                        apartments1.AddRange(items);
                        isfiltered = true;
                    }
                }
                if (BlockD)
                {
                    var items = _source.Where(x => x.Block == 'D');
                    if (items.Any())
                    {
                        apartments1.AddRange(items);
                        isfiltered = true;
                    }
                }
                if (BlockE)
                {
                    var items = _source.Where(x => x.Block == 'E');
                    if (items.Any())
                    {
                        apartments1.AddRange(items);
                        isfiltered = true;
                    }
                }

                //Filter by availibility
                if (IsAvailable)
                {
                    var items = _source.Where(x => x.Status == Status.Available);
                    if (items.Any())
                    {
                        apartments1.AddRange(items);
                        isfiltered = true;
                    }
                }
                if (IsUnavailable)
                {
                    var items = _source.Where(x => x.Status == Status.Unavailable);
                    if (items.Any())
                    {
                        apartments1.AddRange(items);
                        isfiltered = true;
                    }
                }
                if (IsReserved)
                {
                    var items = _source.Where(x => x.Status == Status.Reserved);
                    if (items.Any())
                    {
                        apartments1.AddRange(items);
                        isfiltered = true;
                    }
                }

                //Filter by rooms
                if (OneBedRoom)
                {
                    var items = _source.Where(x => x.Bedrooms == 1);
                    if (items.Any())
                    {
                        apartments1.AddRange(items);
                        isfiltered = true;
                    }
                }
                if (TwoBedRooms)
                {
                    var items = _source.Where(x => x.Bedrooms == 2);
                    if (items.Any())
                    {
                        apartments1.AddRange(items);
                        isfiltered = true;
                    }
                }
                if (ThreeBedRooms)
                {
                    var items = _source.Where(x => x.Bedrooms == 3);
                    if (items.Any())
                    {
                        apartments1.AddRange(items);
                        isfiltered = true;
                    }
                }
                if (FourBedRooms)
                {
                    var items = _source.Where(x => x.Bedrooms == 4);
                    if (items.Any())
                    {
                        apartments1.AddRange(items);
                        isfiltered = true;
                    }
                }

                if (!isfiltered)
                {
                    apartments1 = new List<SampleApartment>(_source);
                }

                //Filter by price
                if (MinPrice > 100000 || MaxPrice < 1000000)
                {
                    var priceFilter = apartments1.Where(x => x.Price >= MinPrice && x.Price <= MaxPrice);
                    if (priceFilter.Any())
                    {
                        apartments2.AddRange(priceFilter);
                    }
                }
                else
                {
                    apartments2 = !isfiltered ? new List<SampleApartment>(_source) : new List<SampleApartment>(apartments1);
                }

                //Filter by rooms
                if (MinFloors > 0 || MaxFloors < 100)
                {
                    var floorsFilter = apartments2.Where(x => x.Floor >= MinFloors && x.Floor <= MaxFloors);
                    if (floorsFilter.Any())
                    {
                        apartments3.AddRange(floorsFilter);
                    }

                    Apartments = new List<SampleApartment>(apartments3.OrderBy(x => x.Block));
                }
                else
                {
                    Apartments = new List<SampleApartment>(apartments2.OrderBy(x => x.Block));
                }
            }
        }
    }
}
