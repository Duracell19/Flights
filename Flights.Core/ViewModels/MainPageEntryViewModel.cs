using Acr.UserDialogs;
using Flights.Core.Helpers;
using Flights.Infrastructure.Interfaces;
using Flights.Models;
using Flights.Services.Helpers;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Flights.Core.ViewModels
{
    public class MainPageEntryViewModel : MvxViewModel
    {
        private readonly IJsonConverter _jsonConverter;
        private readonly IFileStore _fileStore;
        private readonly ICitiesService _citiesService;
        private readonly IIataService _iataService;
        private AutoCompleteTextViewHelper _itemsCountriesFrom;
        private AutoCompleteTextViewHelper _itemsCountriesTo;
        private AutoCompleteTextViewHelper _itemsCitiesFrom;
        private AutoCompleteTextViewHelper _itemsCitiesTo;
        private bool _isAirportFromExist;
        private bool _isAirportToExist;
        private bool _isDataAboutFlightExist;
        private bool _isCheckedOneWay;
        private bool _isCheckedReturn;
        private bool _isEnabledDateReturn;
        private string _selectedCountryFrom;
        private string _selectedCountryTo;
        private string _selectedCityFrom;
        private string _selectedCityTo;
        private DateTime _dateOneWay;
        private DateTime _dateReturn;
        private DataOfFlights _dataOfFlights;

        public bool IsAirportFromExist
        {
            get { return _isAirportFromExist; }
            set
            {
                _isAirportFromExist = value;
                RaisePropertyChanged(() => IsAirportFromExist);
            }
        }

        public bool IsAirportToExist
        {
            get { return _isAirportToExist; }
            set
            {
                _isAirportToExist = value;
                RaisePropertyChanged(() => IsAirportToExist);
            }
        }

        public bool IsDataAboutFlightExist
        {
            get { return _isDataAboutFlightExist; }
            set
            {
                _isDataAboutFlightExist = value;
                RaisePropertyChanged(() => IsDataAboutFlightExist);
            }
        }

        public bool IsCheckedOneWay
        {
            get { return _isCheckedOneWay; }
            set
            {
                _isCheckedOneWay = value;
                RaisePropertyChanged(() => IsCheckedOneWay);
            }
        }

        public bool IsCheckedReturn
        {
            get { return _isCheckedReturn; }
            set
            {
                _isCheckedReturn = value;
                RaisePropertyChanged(() => IsCheckedReturn);
            }
        }

        public bool IsEnabledDateReturn
        {
            get { return _isEnabledDateReturn; }
            set
            {
                _isEnabledDateReturn = value;
                RaisePropertyChanged(() => IsEnabledDateReturn);
            }
        }

        public string SelectedCountryFrom
        {
            get { return _selectedCountryFrom; }
            set
            {
                _selectedCountryFrom = value;
                SetCitiesFromAsync();
                RaisePropertyChanged(() => SelectedCountryFrom);
            }
        }

        public string SelectedCountryTo
        {
            get { return _selectedCountryTo; }
            set
            {
                _selectedCountryTo = value;
                SetCitiesToAsync();
                RaisePropertyChanged(() => SelectedCountryTo);
            }
        }

        public string SelectedCityFrom
        {
            get { return _selectedCityFrom; }
            set
            {
                _selectedCityFrom = value;
                SetIatasFromAsync();
                RaisePropertyChanged(() => SelectedCityFrom);
            }
        }

        public string SelectedCityTo
        {
            get { return _selectedCityTo; }
            set
            {
                _selectedCityTo = value;
                SetIatasToAsync();
                RaisePropertyChanged(() => SelectedCityTo);
            }
        }

        public AutoCompleteTextViewHelper ItemsCountriesFrom
        {
            get { return _itemsCountriesFrom; }
            set
            {
                _itemsCountriesFrom = value;
                RaisePropertyChanged(() => ItemsCountriesFrom);
            }
        }

        public AutoCompleteTextViewHelper ItemsCountriesTo
        {
            get { return _itemsCountriesTo; }
            set
            {
                _itemsCountriesTo = value;
                RaisePropertyChanged(() => ItemsCountriesTo);
            }
        }

        public AutoCompleteTextViewHelper ItemsCitiesFrom
        {
            get { return _itemsCitiesFrom; }
            set
            {
                _itemsCitiesFrom = value;
                RaisePropertyChanged(() => ItemsCitiesFrom);
            }
        }

        public AutoCompleteTextViewHelper ItemsCitiesTo
        {
            get { return _itemsCitiesTo; }
            set
            {
                _itemsCitiesTo = value;
                RaisePropertyChanged(() => ItemsCitiesTo);
            }
        }

        public DateTime DateOneWay
        {
            get { return _dateOneWay; }
            set
            {
                _dateOneWay = value;
                RaisePropertyChanged(() => DateOneWay);
            }
        }

        public DateTime DateReturn
        {
            get { return _dateReturn; }
            set
            {
                _dateReturn = value;
                RaisePropertyChanged(() => DateReturn);
            }
        }
        
        public ICommand FindFlightsCommand { get; set; }
        public ICommand SetOneWayCommand { get; set; }
        public ICommand SetReturnCommand { get; set; }

        public MainPageEntryViewModel(IJsonConverter jsonConverter, 
            IFileStore fileStore,
            ICitiesService citiesService,
            IIataService iataService)
        {
            _jsonConverter = jsonConverter;
            _fileStore = fileStore;
            _citiesService = citiesService;
            _iataService = iataService;
            _dataOfFlights = new DataOfFlights();
            _itemsCountriesFrom = new AutoCompleteTextViewHelper();
            _itemsCountriesTo = new AutoCompleteTextViewHelper();
            _itemsCitiesFrom = new AutoCompleteTextViewHelper();
            _itemsCitiesTo = new AutoCompleteTextViewHelper();

            FindFlightsCommand = new MvxCommand(FindFlights);
            SetOneWayCommand = new MvxCommand(SetOneWay);
            SetReturnCommand = new MvxCommand(SetReturn);

            Initialization();
        }

        public void Initialization()
        {
            var countries = new Countries();
            ItemsCountriesFrom.AutoCompleteList = countries.GetCountries();
            ItemsCountriesTo = ItemsCountriesFrom;
            IsAirportFromExist = false;
            IsAirportToExist = false;
            IsDataAboutFlightExist = false;
            IsCheckedOneWay = true;
            IsEnabledDateReturn = false;
            DateOneWay = DateTime.Now;
            DateReturn = DateTime.Now;
        }

        private void FindFlights()
        {
            if (AllFieldsNotEmpty())
            {

            }
            else
            {
                Mvx.Resolve<IUserDialogs>().Alert("Some fields are empty!");
            }
        }

        private void SetOneWay()
        {
            IsCheckedOneWay = true;
            IsCheckedReturn = false;
            IsEnabledDateReturn = false;
            _dataOfFlights.ReturnWay = false;
        }

        private void SetReturn()
        {
            IsCheckedOneWay = false;
            IsCheckedReturn = true;
            IsEnabledDateReturn = true;
            _dataOfFlights.ReturnWay = true;
        }

        private async Task SetCitiesFromAsync()
        {
            _dataOfFlights.CountryFrom = SelectedCountryFrom;
            var citiesFrom = await _citiesService.GetCitiesAsync(SelectedCountryFrom);

            if (citiesFrom != null)
            {
                ItemsCitiesFrom.AutoCompleteList.Clear();
                foreach (var city in citiesFrom)
                {
                    ItemsCitiesFrom.AutoCompleteList.Add(city);
                    IsAirportFromExist = true;
                }
                _dataOfFlights.CitiesFrom = citiesFrom;
            }
            else
            {
                IsAirportFromExist = false;
                _dataOfFlights.CitiesFrom = null;
            }
        }

        private async Task SetCitiesToAsync()
        {
            _dataOfFlights.CountryTo = SelectedCountryTo;
            var citiesTo = await _citiesService.GetCitiesAsync(SelectedCountryTo);

            if (citiesTo != null)
            {
                ItemsCitiesTo.AutoCompleteList.Clear();
                foreach (var city in citiesTo)
                {
                    ItemsCitiesTo.AutoCompleteList.Add(city);
                    IsAirportToExist = true;
                }
                _dataOfFlights.CitiesTo = citiesTo;
            }
            else
            {
                IsAirportToExist = false;
                _dataOfFlights.CitiesTo = null;
            }
        }

        private async Task SetIatasFromAsync()
        {
            var iatasFrom = await _iataService.GetIataAsync(SelectedCityFrom);
            _dataOfFlights.IatasFrom = iatasFrom;
            IsDataAboutFlightExist = (IsDataAboutFlightExistCheck()) ? true : false;
        }

        private async Task SetIatasToAsync()
        {
            var iatasTo = await _iataService.GetIataAsync(SelectedCityTo);
            _dataOfFlights.IatasTo = iatasTo;
            IsDataAboutFlightExist = (IsDataAboutFlightExistCheck()) ? true : false;
        }

        private bool AllFieldsNotEmpty()
        {
            return ItemsCitiesFrom.CurrentTextHint != null && ItemsCitiesTo.CurrentTextHint != null &&
                ItemsCountriesFrom.CurrentTextHint != null && ItemsCountriesTo.CurrentTextHint != null;
        }

        private bool IsDataAboutFlightExistCheck()
        {
            return IsAirportFromExist == true && IsAirportToExist == true &&
                _dataOfFlights.IatasTo != null && _dataOfFlights.IatasFrom != null;
        }
    }
}
